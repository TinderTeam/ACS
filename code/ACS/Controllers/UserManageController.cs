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
using ACS.Controllers;
using ACS.Common.Dao;
using ACS.Common;
using ACS.Service.Constant;
using ACS.Common.Constant;
namespace ACM.Controllers
{
    public class UserManageController : MiniUITableController<SystemUser>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        PlatFormService platFormService = ServiceContext.getInstance().getPlatFormService();
        UserService userService = ServiceContext.getInstance().getUserService();

        public override CommonService<SystemUser> getService()
        {
            return userService;
        }
        //用于实现条件查询功能
        public override List<QueryCondition> GetFilterCondition(String json)
        {

            List<QueryCondition> filterCondition = new List<QueryCondition>();
            SystemUser userFilter = JsonConvert.JsonToObject<SystemUser>(json);
            if (null != userFilter)
            {
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, SystemUser.NAME, userFilter.UserName));
            }

            return filterCondition;
        }
        //显示菜单、设备权限编辑页面
        public ActionResult ShowPrivilege()
        {
            return View();
        }
        //加载用户对应的菜单树列表
        public ActionResult LoadMenuPrivilegeTree(string userID)
        {
            List<TreeModel> MenuTreeList = userService.getMenuPrivilegeTree(userID);
            return ReturnJson(MenuTreeList);
        }
        //提交修改后的用户菜单权限列表
        public ActionResult SaveMenuPrivilege(string userID, string data)
        {
            try
            {
                List<string> menuIDList = JsonConvert.JsonToObject<List<string>>(data);
                userService.saveMenuPrivilege(userID, menuIDList);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }
        //显示设备权限编辑树
        public ActionResult LoadDevicePrivilegeTree(String userID)
        {
            List<TreeModel> MenuTreeList = userService.getDevicePrivilegeTree(userID);
            return ReturnJson(MenuTreeList);
        }

        //提交修改后的用户设备权限列表
        public ActionResult SaveDevicePrivilege(string userID, string data)
        {
            try
            {
                List<string> deviceIDList = JsonConvert.JsonToObject<List<string>>(data);
                userService.saveDevicePrivilege(userID, deviceIDList);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }
       
    }


}
