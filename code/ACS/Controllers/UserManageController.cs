using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Common.Model;
using ACS.Service;
using ACS.Models.Po.CF;
namespace ACM.Controllers
{
    public class UserManageController : Controller
    {
        UserService userService = ServiceContext.getInstance().getUserService();
        public ActionResult UserManage(TableForm tableForm, User filter)
        {
            TableDataModel<User> userModelTable = new TableDataModel<User>();
            userModelTable.setPage(tableForm.getPage());
            userModelTable.setDataSource(userService.getUserList(filter));
            @ViewBag.UserList = userModelTable.getCurrentPageData();
            return View();
        }

        /// <summary>
        /// 新增用户
        /// 可以改成用Ajax调用的响应
        /// </summary>
        /// <returns></returns>
        public ActionResult createUser(UserModel usermodle)
        {
            User user = ModelConventService.toUser(usermodle);
            if (ModelVerificationService.UserVerification(user))
            {
                //校验成功
                userService.create(user);
            }else{
                //校验失败
                //TODO: 
            }
            return View();
        }

        /// <summary>
        /// 修改用户
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult modifyUser(UserModel usermodle)
        {
            User user = ModelConventService.toUser(usermodle);
            if (ModelVerificationService.UserVerification(user))
            {
                //校验成功
                userService.update(user);
            }
            else
            {
                //校验失败
                //TODO: 
            }
            return View();
        }

        /// <summary>
        /// 删除用户
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult deleteUser(int userID)
        {

            if (ModelVerificationService.UserIDExist(userID))
            {
                //校验成功
                userService.delete(userID);
            }
            else
            {
                //校验失败
                //TODO: 
            }
            return View();
        }
    }
}
