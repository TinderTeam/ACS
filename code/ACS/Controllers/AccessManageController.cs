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
using ACM.Controllers;
using ACS.Service.Constant;
using ACS.Common;

namespace ACS.Controllers
{
    public class AccessManageController : BaseController
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
        /// 新增门禁权限
        /// </summary>
        /// <param name="AccessID"></param>
        /// <returns></returns>
        public string AddAccess(string data)
        {
            
 
            log.Debug("Edite Access...");
            UserModel loginUser = (UserModel)Session["SystemUser"];
            try
            {
                //校验成功
                Access newAccess = accessService.addAccess(loginUser.UserID, data);
                Rsp.Obj = newAccess;

            }
            catch (FuegoException e)
            {
                Rsp.ErrorCode = e.GetErrorCode();
                log.Error("add accesss failed", e);
            }
            catch (SystemException ex)
            {
                Rsp.ErrorCode = ExceptionMsg.FAIL;
                log.Error("add accesss failed", ex);
            }


            Response.Write(getRspJson());
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
        // 加载门禁权限List
        public string LoadAccessDetail(string accessID)
        {

           List<TreeGirdItem> tree =  LoadAccessDetailListByID(accessID);
         
            TreeGridModel treeModel =   new TreeGridModel();
            treeModel.TreeGridItemList = tree;

            return treeModel.ToJsonStr();
        }
      
        /// <summary>
        /// 根据accessID获取AccessList
        /// </summary>
        /// <returns></returns>
        public List<TreeGirdItem> LoadAccessDetailListByID(string accessID)
        {
            List<TreeGirdItem> rspTreeList = new List<TreeGirdItem>();
            
            List<AccessDetailView> accessDetailViewList = accessService.getAccessDetailViewList(accessID);

            if (ValidatorUtil.isEmpty<AccessDetailView>(accessDetailViewList))
            {
                log.Warn("can not find child access by access id. the access id is " + accessID);
                return rspTreeList;
            }

            TreeGridModel accessDetailViewTreeModel = new TreeGridModel();
            Dictionary<String, TreeGirdItem> accessMap = new Dictionary<String, TreeGirdItem>();
 
            foreach (AccessDetailView accessDetailView in accessDetailViewList)
            {
                

                if (accessDetailView.Type == AccessDetail.DOORTIME_TYPE)
                {
                   
                    TreeGirdItem control = new TreeGirdItem();
                    control.Id = AccessDetail.CONTROL_TYPE + AccessDetail.SPLIT + accessDetailView.ControlID.ToString();
                    control.Text = accessDetailView.ControlName;
                    control.Pid = AccessDetail.ACCESS_TYPE + AccessDetail.SPLIT + accessDetailView.AccessID.ToString();
                    if (!accessMap.ContainsKey(control.Id))
                    { 
                        accessMap.Add(control.Id, control);
                    }
                   

                    TreeGirdItem door = new TreeGirdItem();
                    door.Id = AccessDetail.DOOR_TYPE + AccessDetail.SPLIT + accessDetailView.DoorID.ToString();
                    door.Text = accessDetailView.DoorName;
                    door.Pid = control.Id;
                    if (!accessMap.ContainsKey(door.Id))
                    {
                        accessMap.Add(door.Id, door);
                    }

                    TreeGirdItem doorTime = new TreeGirdItem();
                    doorTime.Id = AccessDetail.DOORTIME_TYPE + AccessDetail.SPLIT + accessDetailView.ValueID.ToString();
                    doorTime.Text = accessDetailView.DoorTimeName;
                    doorTime.Pid = door.Id;
                    doorTime.StartTime = accessDetailView.StartTime;
                    doorTime.EndTime = accessDetailView.EndTime;
                    accessMap.Add(doorTime.Id, doorTime);

                }
                else if (accessDetailView.Type == AccessDetail.ACCESS_TYPE)
                {
                    TreeGirdItem item = new TreeGirdItem();
                    item.Id = AccessDetail.ACCESS_TYPE + AccessDetail.SPLIT + accessDetailView.ValueID.ToString();
                    item.Text = accessDetailView.AccessName;
                    item.Pid = AccessDetail.ACCESS_TYPE + AccessDetail.SPLIT + accessDetailView.AccessID.ToString();
                    accessMap.Add(item.Id, item);
                    rspTreeList = LoadAccessDetailListByID(accessDetailView.ValueID.ToString());

                }
                
            }
            rspTreeList.AddRange(accessMap.Values.ToList<TreeGirdItem>());

            return rspTreeList;
        }
        public ActionResult AccessEdit()
        {
            return View();
        }

        public override void basicDelete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
