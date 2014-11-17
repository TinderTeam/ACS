using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Test;
using ACS.Models.Po.Business;
using ACS.Service;
using ACS.Common.Util;
using ACS.Controllers.Constant;

namespace ACS.Controllers
{
    public class AccessManageController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        AccessService accessService = ServiceContext.getInstance().getAccessService();
        //
        // GET: /AccessManage/

        public ActionResult AccessManage()
        {
            return View();
        }

        /// <summary>
        /// 加载门禁权限树
        /// </summary>
        /// <returns></returns>
        public String LoadAccessList()
        {
            UserModel loginUser = (UserModel)Session["SystemUser"];
            List<Access> accessList = accessService.getAccessList(loginUser.UserID.ToString());
            TreeModel accessTree = ModelConventService.toAccessTreeModel(accessList);
            return accessTree.ToJsonStr();
        }
        
        /// <summary>
        /// Load access detail
        /// </summary>
        /// <param name="AccessID"></param>
        /// <returns></returns>
        public ActionResult AccessDetail(String AccessID)
        {
            return View();
        }
        /// <summary>
        /// 新增门禁权限
        /// </summary>
        /// <param name="AccessID"></param>
        /// <returns></returns>
        public string AddAccess(string data)
        {
            string text = null;
            log.Debug("Edite Access...");
            UserModel loginUser = (UserModel)Session["SystemUser"];
            try
            {
                //校验成功
                accessService.addAccess(loginUser.UserID, data);

            }
            catch (SystemException ex)
            {
                text = ex.Message;
                Response.Write(text);
                return null;
            }
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;
        }
        /// <summary>
        /// 修改门禁权限
        /// </summary>
        /// <param name="AccessID"></param>
        /// <returns></returns>
        public string EditAccessName(string data)
        {
            string text = null;
            log.Debug("Edite Access...");

            AccessModel accessModel = JsonConvert.JsonToObject<AccessModel>(data);
            Access access = ModelConventService.toAccess(accessModel);
            try
            {
                //校验成功

                accessService.updateAccess(access);
                
            }
            catch (SystemException ex)
            {
                text = ex.Message;
                Response.Write(text);
                return null;
            }
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;
        }
        /// <summary>
        /// 删除门禁权限
        /// </summary>
        /// <param name="AccessID"></param>
        /// <returns></returns>
        public string DeleteAccess(string data)
        {
            string text = null;
            log.Debug("Delete Access...");

            try
            {
                //校验成功
                accessService.deleteAccess(data);
            }
            catch (SystemException ex)
            {
                text = ex.Message;
                Response.Write(text);
                return null;
            }
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;

        }
    }
}
