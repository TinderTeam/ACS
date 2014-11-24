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
        private AccessService accessService = ServiceContext.getInstance().getAccessService();

        private int currentAccessID = 1;        //前台显示树时，作为唯一标识ID
        //
        // 加载门禁权限窗口

        public ActionResult AccessManage()
        {
            return View();
        }
        // 加载所有门禁权限List
        public string LoadAllAccessDetail()
        {

            List<TreeGirdItem> tree = LoadAccessDetailListByID("0", "0", "0");

            TreeGridModel treeModel = new TreeGridModel();
            treeModel.TreeGridItemList = tree;
            return treeModel.ToJsonStr();
        }

        /// <summary>
        /// 根据accessID获取AccessList
        /// </summary>
        // parentID前台显示树时的父本ID，第一级树parentID为0；第一级树ID由getCurTreeID自动生成，第二级树的parentID为第一级树的ID
        // accessID用户查询数据库中子节点使用
        //selectedID，在添加权限时，被选中的ID在新弹出的窗口中不显示
        public List<TreeGirdItem> LoadAccessDetailListByID(string parentID, string accessID, string selectedID)
        {
            log.Info("LoadAccessDetailListByID" + "the parent id is " + parentID + ",selected id is :" + selectedID);
            UserModel loginUser = (UserModel)Session["SystemUser"];
            List<TreeGirdItem> rspTreeList = new List<TreeGirdItem>();

            List<AccessDetailView> accessDetailViewList = accessService.getAccessDetailViewList(loginUser.UserID.ToString(), accessID);

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

                    TreeGirdItem control = null;

                    string controlID = AccessDetail.CONTROL_TYPE + AccessDetail.SPLIT + accessDetailView.ControlID.ToString();

                    if (!accessMap.ContainsKey(controlID))
                    {
                        control = new TreeGirdItem(parentID, getCurTreeID());
                        control.ValueID = controlID;
                        control.Text = accessDetailView.ControlName;
                        control.AccessID = AccessDetail.ACCESS_TYPE + AccessDetail.SPLIT + accessDetailView.AccessID.ToString();
                        accessMap.Add(control.ValueID, control);
                    }
                    else
                    {
                        control = accessMap[controlID];
                    }


                    TreeGirdItem door = null;
                    string doorID = AccessDetail.DOOR_TYPE + AccessDetail.SPLIT + accessDetailView.DoorID.ToString();

                    if (!accessMap.ContainsKey(doorID))
                    { 
                        door = new TreeGirdItem(control.Id, getCurTreeID());
                        door.ValueID = doorID;
                       
                        door.Text = accessDetailView.DoorName;
                        door.AccessID = control.ValueID;
                        accessMap.Add(door.ValueID, door);
                    }
                    else
                    {
                        door = accessMap[doorID];
                    }

                    TreeGirdItem doorTime = new TreeGirdItem(door.Id, getCurTreeID());
                    doorTime.ValueID = AccessDetail.DOORTIME_TYPE + AccessDetail.SPLIT + accessDetailView.ValueID.ToString();
                    doorTime.Text = accessDetailView.DoorTimeName;
                    doorTime.AccessID = door.ValueID;
                    doorTime.Type = AccessDetail.DOORTIME_TYPE;
                    doorTime.StartTime = accessDetailView.StartTime;
                    doorTime.EndTime = accessDetailView.EndTime;
                    accessMap.Add(doorTime.ValueID, doorTime);

                }
                else if (accessDetailView.Type == AccessDetail.ACCESS_TYPE)
                {
                    TreeGirdItem item = new TreeGirdItem(parentID, getCurTreeID());
                    item.ValueID = AccessDetail.ACCESS_TYPE + AccessDetail.SPLIT + accessDetailView.ValueID.ToString();
                    item.Text = accessDetailView.AccessName;
                    item.AccessID = AccessDetail.ACCESS_TYPE + AccessDetail.SPLIT + accessDetailView.AccessID.ToString();
                    item.Type = AccessDetail.ACCESS_TYPE;
                    if ((accessDetailView.AccessID == 0) && (selectedID == item.ValueID))
                    {
                        //被选中的Access在增加权限的List中不显示
                    }
                    else
                    {
                        accessMap.Add(item.ValueID, item);
                        rspTreeList.AddRange(LoadAccessDetailListByID(item.Id, accessDetailView.ValueID.ToString(), "0"));
                    }


                }

            }
            rspTreeList.AddRange(accessMap.Values.ToList<TreeGirdItem>());

            return rspTreeList;
        }
        //用于生成树的ID
        private string getCurTreeID()
        {
            return (currentAccessID++).ToString();
        }
        //编辑门禁权限名称，打开编辑窗口
        public ActionResult AccessCreate()
        {
            return View();
        }
        //加载编辑权限名称信息
        public string ShowEditAccessName(string accessID)
        {
            //获取编辑的目标权限
            AccessDetail fatherAccessDetail = accessService.getAccessDetailByAccessID(accessID, AccessDetail.ROOT_ID);
            string accessDetailJson = JsonConvert.ObjectToJson(fatherAccessDetail);
            Response.Write(accessDetailJson);
            return null;

        }
        //提交新增的门禁权限
        public string CreateAccess(string data, string accessName)
        {

            log.Debug("Create Access...");
            UserModel loginUser = (UserModel)Session["SystemUser"];
            try
            {
                //校验成功
                AccessDetail newAccessDetail = accessService.createAccessDetail(loginUser.UserID, accessName);

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


            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;
        }
        //提交编辑门禁权限名称
        public string EditAccessName(string data)
        {

            log.Debug("Edit access name...");
            AccessDetail accessDetail = JsonConvert.JsonToObject<AccessDetail>(data);
            accessService.editAccessName(accessDetail);
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;

        }
        //打开编辑门禁权限内容窗口
        public ActionResult AccessEdit()
        {
            return View();
        }
        // 加载增加门禁权限列表
        public string LoadAddAccessDetail(string selectedID)
        {
            //被选中的Access不再增加权限列表中显示
            List<TreeGirdItem> tree = LoadAccessDetailListByID("0", "0", selectedID);
            TreeGridModel treeModel = new TreeGridModel();
            treeModel.TreeGridItemList = tree;
            return treeModel.ToJsonStr();
        }
        // 编辑门禁权限中的子权限，数据提交
        public string AddAccessOfAccess(string accessID, string data)
        {
            //accessID是正在编辑的AccessID,data是编辑后提交上来的ID列表

            TreeGirdItem treeItem = JsonConvert.JsonToObject<TreeGirdItem>(data);

            accessService.addAccessInAccess(accessID, treeItem);
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;
        }
        /// <summary>
        /// 根据DeviceID获取设备树列表
        public string LoadDeviceTreeList(string selectedID)
        {
            log.Info("LoadDeviceTreeList" +  ",selected id is :" + selectedID);
            UserModel loginUser = (UserModel)Session["SystemUser"];
            List<DoorTimeView> doorTimeViewList = accessService.getDoorTimeViewList(loginUser.UserID.ToString(),selectedID);

            List<TreeGirdItem> rspTreeList = new List<TreeGirdItem>();
            if (ValidatorUtil.isEmpty<DoorTimeView>(doorTimeViewList))
            {
                log.Warn("can not find  doortime of the user. the user id is " + loginUser.UserID.ToString());
                return null;
            }
            TreeGridModel accessDetailViewTreeModel = new TreeGridModel();
            Dictionary<String, TreeGirdItem> doorTimeMap = new Dictionary<String, TreeGirdItem>();

            foreach (DoorTimeView doorTimeView in doorTimeViewList)
            {
                //加入Control
                string controlParentID = AccessDetail.CONTROL_TYPE + AccessDetail.SPLIT + AccessDetail.ROOT_ID;
                string controlID = AccessDetail.CONTROL_TYPE + AccessDetail.SPLIT + doorTimeView.ControlID.ToString();
                TreeGirdItem control = new TreeGirdItem(controlParentID, controlID);
                control.Text = doorTimeView.ControlName;
                control.Type = AccessDetail.CONTROL_TYPE;
                if (!doorTimeMap.ContainsKey(control.Id))
                {
                    doorTimeMap.Add(control.Id, control);
                }

                //加入Door
                string doorParentID = AccessDetail.CONTROL_TYPE + AccessDetail.SPLIT + doorTimeView.ControlID.ToString();
                string doorID = AccessDetail.DOOR_TYPE + AccessDetail.SPLIT + doorTimeView.DoorID.ToString();
                TreeGirdItem door = new TreeGirdItem(doorParentID, doorID);
                door.Text = doorTimeView.DoorName;
                door.Type = AccessDetail.DOOR_TYPE;
                if (!doorTimeMap.ContainsKey(door.Id))
                {
                    doorTimeMap.Add(door.Id, door);
                }

                //加入Doortime
                string doorTimeParentID = AccessDetail.DOOR_TYPE + AccessDetail.SPLIT + doorTimeView.DoorID.ToString();
                string doorTimeID = AccessDetail.DOORTIME_TYPE + AccessDetail.SPLIT + doorTimeView.DoorTimeID.ToString();
                TreeGirdItem doorTime = new TreeGirdItem(doorTimeParentID, doorTimeID);
                doorTime.Text = doorTimeView.DoorTimeName;
                doorTime.Type = AccessDetail.DOORTIME_TYPE;
                doorTimeMap.Add(doorTime.Id, doorTime);

            }
            rspTreeList.AddRange(doorTimeMap.Values.ToList<TreeGirdItem>());
            List<TreeGirdItem> doorTimeTreeList = rspTreeList;
            TreeGridModel treeModel = new TreeGridModel();
            treeModel.TreeGridItemList = doorTimeTreeList;
            string text = treeModel.ToJsonStr();
            return text;
        }  
        // 编辑门禁权限中的子权限，数据提交
        public string AddDeviceOfAccess(string accessID, string data)
        {
            //accessID是正在编辑的AccessID,data是编辑后提交上来的ID列表
            UserModel loginUser = (UserModel)Session["SystemUser"];
            List<TreeGirdItem> treeItemList = JsonConvert.JsonToObject<List<TreeGirdItem>>(data);
            accessService.addDeviceInAccess(loginUser.UserID, accessID, treeItemList);
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;
        }
        //删除门禁权限
        public string DeleteAccess(string data)
        {
            string text = null;
            log.Debug("Delete Access...");
            TreeGirdItem treeItem = JsonConvert.JsonToObject<TreeGirdItem>(data);
            try
            {
                //校验成功
                accessService.deleteAccess(treeItem);
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
        
        
        public override void basicDelete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
