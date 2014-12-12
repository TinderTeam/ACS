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
    public class AccessManageController : MiniUITableController<AccessDetail>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AccessDetailService accessService = ServiceContext.getInstance().getAccessDetailService();

        private int currentAccessID = 1;        //前台显示树时，作为唯一标识ID

        public override CommonService<AccessDetail> getService()
        {
            return accessService;
        }

        // 加载所有门禁权限List
        public ActionResult LoadAllAccessDetail()
        {

            List<AccessDetailModel> accessDetailModelList = LoadAccessDetailListByID("0", "0", "0");
            return ReturnJson(accessDetailModelList);
        }

        /// <summary>
        /// 根据accessID获取AccessList
        /// </summary>
        // parentID前台显示树时的父本ID，第一级树parentID为0；第一级树ID由getCurTreeID自动生成，第二级树的parentID为第一级树的ID
        // accessID用户查询数据库中子节点使用
        //selectedID，在添加权限时，被选中的ID在新弹出的窗口中不显示
        public List<AccessDetailModel> LoadAccessDetailListByID(string parentID, string accessID, string selectedID)
        {
            log.Info("LoadAccessDetailListByID" + "the parent id is " + parentID + ",selected id is :" + selectedID);
            UserModel loginUser = (UserModel)Session["SystemUser"];
            List<AccessDetailModel> rspTreeList = new List<AccessDetailModel>();

            List<AccessDetailView> accessDetailViewList = accessService.getAccessDetailViewList(loginUser.UserID.ToString(), accessID);

            if (ValidatorUtil.isEmpty<AccessDetailView>(accessDetailViewList))
            {
                log.Warn("can not find child access by access id. the access id is " + accessID);
                return rspTreeList;
            }

            Dictionary<String, AccessDetailModel> controlMap = new Dictionary<String, AccessDetailModel>();
            Dictionary<String, AccessDetailModel> doorMap = new Dictionary<String, AccessDetailModel>();

            foreach (AccessDetailView accessDetailView in accessDetailViewList)
            {

                if (accessDetailView.Type == AccessDetail.DOORTIME_TYPE)
                {

                    AccessDetailModel control = null;

                    if (!controlMap.ContainsKey(accessDetailView.ControlID.ToString()))
                    {
                        control = new AccessDetailModel(parentID, getCurTreeID());
                        control.ValueID = accessDetailView.ControlID;
                        control.NodeName = accessDetailView.ControlName;
                        control.Type = AccessDetail.CONTROL_TYPE;
                        control.AccessID = accessDetailView.AccessID;
                        controlMap.Add(accessDetailView.ControlID.ToString(), control);
                    }
                    else
                    {
                        control = controlMap[accessDetailView.ControlID.ToString()];
                    }


                    AccessDetailModel door = null;

                    if (!doorMap.ContainsKey(accessDetailView.DoorID.ToString()))
                    { 
                        door = new AccessDetailModel(control.Id, getCurTreeID());
                        door.ValueID = accessDetailView.DoorID;
                        door.NodeName = accessDetailView.DoorName;
                        door.Type = AccessDetail.DOOR_TYPE;
                        door.AccessID = accessDetailView.AccessID;
                        doorMap.Add(accessDetailView.DoorID.ToString(), door);
                    }
                    else
                    {
                        door = doorMap[accessDetailView.DoorID.ToString()];
                    }

                    AccessDetailModel doorTime = new AccessDetailModel(door.Id, getCurTreeID());
                    doorTime.ValueID = accessDetailView.ValueID;
                    doorTime.NodeName = accessDetailView.DoorTimeName;
                    doorTime.AccessID = accessDetailView.AccessID;
                    doorTime.Type = AccessDetail.DOORTIME_TYPE;
                    doorTime.StartTime = accessDetailView.StartTime;
                    doorTime.EndTime = accessDetailView.EndTime;
                    doorTime.Monday = accessDetailView.Monday;
                    doorTime.Tuesday = accessDetailView.Tuesday;
                    doorTime.Wednesday = accessDetailView.Wednesday;
                    doorTime.Thursday = accessDetailView.Thursday;
                    doorTime.Friday = accessDetailView.Friday;
                    doorTime.Saturday = accessDetailView.Saturday;
                    doorTime.Sunday = accessDetailView.Sunday;
                    doorTime.Holiday = accessDetailView.Holiday;
                    rspTreeList.Add(doorTime);

                }
                else if (accessDetailView.Type == AccessDetail.ACCESS_TYPE)
                {
                    AccessDetailModel item = new AccessDetailModel(parentID, getCurTreeID());
                    item.AccessDetailID = accessDetailView.AccessDetailID;
                    item.ValueID = accessDetailView.ValueID;
                    item.NodeName = accessDetailView.AccessName;
                    item.AccessID = accessDetailView.AccessID;
                    item.Type = AccessDetail.ACCESS_TYPE;
                    if ((accessDetailView.AccessID == 0) && (selectedID == item.ValueID.ToString()))
                    {
                        //被选中的Access在增加权限的List中不显示
                    }
                    else 
                    {
                        rspTreeList.Add(item);
                        rspTreeList.AddRange(LoadAccessDetailListByID(item.Id, accessDetailView.ValueID.ToString(), "0"));
                    }
                }

            }
            rspTreeList.AddRange(controlMap.Values.ToList<AccessDetailModel>());
            rspTreeList.AddRange(doorMap.Values.ToList<AccessDetailModel>());

            return rspTreeList;
        }
        //用于生成树的ID
        private string getCurTreeID()
        {
            return (currentAccessID++).ToString();
        }
        //打开编辑门禁权限内容窗口
        public ActionResult AccessEdit()
        {
            return View();
        }
        // 加载增加门禁权限列表
        public ActionResult LoadAddAccessDetail(string selectedID)
        {
            //被选中的Access不再增加权限列表中显示
            List<AccessDetailModel> treeGridItemList = LoadAccessDetailListByID("0", "0", selectedID);
            //原权限中包含的权限打勾
            List<AccessDetail> accessDetilList = accessService.getAccessDetailListByAccessID(selectedID);
            foreach (AccessDetail accessDetail in accessDetilList)
            {
                int valueID = accessDetail.ValueID;
                foreach (AccessDetailModel treeGridItem in treeGridItemList)
                {
                    if ((valueID == treeGridItem.ValueID) && (treeGridItem.AccessID == 0))
                    {
                        treeGridItem.CheckNode = true;
                    }
                }
            }
            return ReturnJson(treeGridItemList);
        }
        // 编辑门禁权限中的子权限，数据提交
        public ActionResult AddAccessOfAccess(string accessID, string data)
        {
            //accessID是正在编辑的AccessID,data是编辑后提交上来的ID列表
            try
            {
                List<AccessDetailModel> treeItemList = JsonConvert.JsonToObject<List<AccessDetailModel>>(data);
                accessService.addAccessInAccess(accessID, treeItemList);
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
        /// <summary>
        /// 根据DeviceID获取设备树列表
        public ActionResult LoadDeviceTreeList(string selectedID)
        {
            log.Info("LoadDeviceTreeList" +  ",selected id is :" + selectedID);
            UserModel loginUser = (UserModel)Session["SystemUser"];
            List<DoorTimeView> doorTimeViewList = accessService.getDoorTimeViewListByUserID(loginUser.UserID.ToString());
            List<AccessDetailView> selectedDoorTimeList = accessService.getDoorTimeAccessByAccessID(selectedID);
            List<AccessDetailModel> rspTreeList = new List<AccessDetailModel>();

            if (ValidatorUtil.isEmpty<DoorTimeView>(doorTimeViewList))
            {
                log.Warn("can not find  doortime of the user. the user id is " + loginUser.UserID.ToString());
                return null;
            }

            Dictionary<String, AccessDetailModel> controlMap = new Dictionary<String, AccessDetailModel>();
            Dictionary<String, AccessDetailModel> doorMap = new Dictionary<String, AccessDetailModel>();
            foreach (DoorTimeView doorTimeView in doorTimeViewList)
            {
                //加入Control
                AccessDetailModel control = null;
                if (!controlMap.ContainsKey(doorTimeView.ControlID.ToString()))
                {
                    control = new AccessDetailModel(AccessDetail.ROOT_ID, getCurTreeID());
                    control.NodeName = doorTimeView.ControlName;
                    control.Type = AccessDetail.CONTROL_TYPE;
                    controlMap.Add(doorTimeView.ControlID.ToString(), control);
                }
                else
                {
                    control = controlMap[doorTimeView.ControlID.ToString()];
                }

                //加入Door
                AccessDetailModel door = null;
                if (!doorMap.ContainsKey(doorTimeView.DoorID.ToString()))
                {
                    door = new AccessDetailModel(control.Id, getCurTreeID());
                    door.NodeName = doorTimeView.DoorName;
                    door.Type = AccessDetail.DOOR_TYPE;
                    doorMap.Add(doorTimeView.DoorID.ToString(), door);
                }
                else
                {
                    door = doorMap[doorTimeView.DoorID.ToString()];
                }

                
                //加入Doortime
                AccessDetailModel doorTime = new AccessDetailModel(door.Id, getCurTreeID());
                doorTime.NodeName = doorTimeView.DoorTimeName;
                doorTime.Type = AccessDetail.DOORTIME_TYPE;
                doorTime.StartTime = doorTimeView.StartTime;
                doorTime.EndTime = doorTimeView.EndTime;
                doorTime.ValueID = doorTimeView.DoorTimeID;
                doorTime.Monday = doorTimeView.Monday;
                doorTime.Tuesday = doorTimeView.Tuesday;
                doorTime.Wednesday = doorTimeView.Wednesday;
                doorTime.Thursday = doorTimeView.Thursday;
                doorTime.Friday = doorTimeView.Friday;
                doorTime.Saturday = doorTimeView.Saturday;
                doorTime.Sunday = doorTimeView.Sunday;
                doorTime.Holiday = doorTimeView.Holiday;
                foreach (AccessDetailView doorTimeAccess in selectedDoorTimeList)
                {
                    if (doorTimeView.DoorTimeID == doorTimeAccess.ValueID)
                    {
                        doorTime.CheckNode = true;
                    }
                }
                rspTreeList.Add(doorTime);

            }
            rspTreeList.AddRange(doorMap.Values.ToList<AccessDetailModel>());
            rspTreeList.AddRange(controlMap.Values.ToList<AccessDetailModel>());
            List<AccessDetailModel> doorTimeTreeList = rspTreeList;
            return ReturnJson(doorTimeTreeList);
        }  
        // 编辑门禁权限中的子权限，数据提交
        public ActionResult AddDeviceOfAccess(string accessID, string data)
        {
            //accessID是正在编辑的AccessID,data是编辑后提交上来的ID列表
            UserModel loginUser = (UserModel)Session["SystemUser"];
            try
            {
                List<AccessDetailModel> treeItemList = JsonConvert.JsonToObject<List<AccessDetailModel>>(data);
                accessService.addDeviceInAccess(loginUser.UserID, accessID, treeItemList);
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
