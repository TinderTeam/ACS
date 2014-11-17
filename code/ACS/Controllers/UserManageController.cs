using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Common.Model;
using ACS.Service;
using ACS.Test;
using ACS.Models.Po;
using System.Collections;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using ACS.Models.Po.CF;
using ACS.Common.Util;
using ACS.Controllers.Constant;
namespace ACM.Controllers
{
    public class UserManageController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        PlatFormService platFormService = ServiceContext.getInstance().getPlatFormService();
        UserService userService = ServiceContext.getInstance().getUserService();

        public ActionResult UserManage()
        {
            return View();
        }

        public ActionResult UserEdit(String id)
        {
            ViewBag.Type = "EDIT";
            UserModel userModel = userService.getUserByID(id);
            ViewBag.user = userModel;
            return View();
        }

        public ActionResult UserCreate()
        {
            ViewBag.Type = "CREATE";
            return View("UserEdit");
        }


        public ActionResult Load(TableForm tableForm, User filter)
        {
            log.Debug("Load Data...");
            //数据库操作：使用查询条件、分页、排序等参数进行查询
            TableDataModel<User> userModelTable = new TableDataModel<User>();
            userModelTable.setPage(tableForm.getPage());
            userModelTable.setDataSource(userService.getUserList(filter));

            log.Debug("pageIndex = " + tableForm.PageIndex + ";pageSize=" + tableForm.PageSize);
            Response.Write(userModelTable.getMiniUIJson());
            return null;
        }
        /// <summary>
        /// 新增用户
        /// 可以改成用Ajax调用的响应
        /// </summary>
        /// <returns></returns>
        public string create(string data)
        {
            string text = null;
            log.Debug("Create User...");
            UserModel userModel = JsonConvert.JsonToObject<UserModel>(data);
            UserModel model = (UserModel) Session["SystemUser"];
            userModel.CreateUserID =model.UserID;

            if (ModelVerificationService.UserVerification(userModel))
            {
                try
                {
                    //校验成功
                    userService.create(userModel);
                }
                catch (SystemException ex)
                {
                    text = ex.Message;
                    Response.Write(text);
                    return null;    
                }
                text = "Success";
                Response.Write(text);
                return null;

            }else{
                //校验失败
                //TODO: 
            }
            return null;
        }

        /// <summary>
        /// 修改用户
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string data)
        {
            string text = null;
            log.Debug("Modify User...");
            UserModel userModel = JsonConvert.JsonToObject<UserModel>(data);
            UserModel model = (UserModel)Session["SystemUser"];
            userModel.ModifyUserID = model.UserID;

            if (ModelVerificationService.UserVerification(userModel))
            {
                try
                {
                    //校验成功
                    userService.update(userModel);
                }
                catch (SystemException ex)
                {
                    text = ex.Message;
                    Response.Write(text);
                    return null;
                }
                text = "Success";
                Response.Write(text);
                return null;
                
            }
            else
            {
                //校验失败
                //TODO: 
            }
            return null;
        }
        //显示菜单权限编辑页面
        public ActionResult MenuPrivilege(String userID)
        {

            ViewBag.userID = userID;
            return View();
        }
        //加载用户对应的菜单树列表
        public String MenuPrivilegeTree(string userID)
        {

            //Test
            TreeModel tree = userService.getPrivilegeMenuTree(userID);
            return tree.ToJsonStr();
        }
        //提交修改后的用户菜单权限列表
        public string MenuEdit(string userID,string data)
        {
            List<string> menuIDList = JsonConvert.JsonToObject<List<string>>(data);
            userService.updateMenuPrivilege(userID,menuIDList);
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;
        }
        //显示设备权限编辑页面
        public ActionResult DevicePrivilege(String userID)
        {

            ViewBag.userID = userID;
            return View();
        }

        //提交修改后的用户设备权限列表
        public string DeviceEdit(string userID, string data)
        {
            List<string> deviceIDList = JsonConvert.JsonToObject<List<string>>(data);
            userService.updateDevicePrivilege(userID, deviceIDList);
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;
        }
        
        /// <summary>
        /// 删除用户
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult Remove(String idstr)
        {
            List<int> idList=ModelConventService.toIDList(idstr);
            log.Debug("Delete User (id=" + idList + ") ...");
            if (ModelVerificationService.UserIDExist(idList))
            {
                //校验成功
                userService.delete(idList);
            }
            else
            {
                //校验失败
                //TODO: 
            }
            Response.Write("ok");
            return null;
        }
    }
}
