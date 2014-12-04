﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;
using ACS.Common.Dao;
using ACS.Common.Dao.datasource;
using ACS.Dao;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service.Constant;
using ACS.Common.Util;
using ACS.Common;
using ACS.Models.Po.CF;

namespace ACS.Service.Impl
{
    public class AccessDetailServiceImpl : CommonServiceImpl<AccessDetail>, AccessDetailService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<AccessDetail> accessDetailDao = DaoContext.getInstance().getAccessDetailDao();
        ViewDao<AccessDetailView> accessDetailViewDao = DaoContext.getInstance().getAccessDetailViewDao();
        CommonDao<Privilege> privilegeDao = DaoContext.getInstance().getPrivilegeDao();
        ViewDao<DoorTimeView> doorTimeViewDao = DaoContext.getInstance().getDoorTimeViewDao();

        public override String GetPrimaryName()
        {
            return AccessDetail.ACCESS_DETAIL_ID;
        }

        //门禁权限管理
        //获取门禁权限详情List
        public List<AccessDetailView> getAccessDetailViewList(string userID, string parentID)
        {
            //获取门禁权限详情列表
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, parentID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.USER_ID, userID));
            List<AccessDetailView> accessDetailViewList = accessDetailViewDao.getAll(conditionList);
            return accessDetailViewList;
        }
        
        //门禁权限管理
        //新增门禁权限
        public override void Create(int creatUserID, AccessDetail accessDetail)
        {
            //新增门禁权限树列表
            AccessDetail newAccessDetail = new AccessDetail();
            newAccessDetail.AccessID = 0;
            newAccessDetail.AccessName = accessDetail.AccessName;
            newAccessDetail.CreateUserID = creatUserID;
            newAccessDetail.CreateDate = DateTime.Now;
            newAccessDetail.Type = AccessDetail.ACCESS_TYPE;
            accessDetailDao.create(newAccessDetail);
            newAccessDetail.ValueID = newAccessDetail.AccessDetailID;
            accessDetailDao.update(newAccessDetail);

        }
        //门禁权限管理
        //编辑门禁权限名称
        public override void Modify(AccessDetail accessDetail)
        {
            //如果删除的节点是根节点，还需要删除所有包含此节点的权限
            //删除所有以该根节点为子节点的记录
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.VALUE_ID_NAME, accessDetail.ValueID.ToString()));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
            List<AccessDetail> accessDetailList = accessDetailDao.getAll(conditionList);
            foreach (AccessDetail accessItem in accessDetailList)
            {
                accessItem.AccessName = accessDetail.AccessName;
            }
            accessDetailDao.update(accessDetailList);
        }
        //门禁权限管理
        //增加门禁权限中包含的权限
        public void addAccessInAccess(string accessID, List<AccessDetailModel> treeItemList)
        {
            //获取编辑的目标权限
            AccessDetail fatherAccessDetail = getAccessDetailByAccessID(accessID, AccessDetail.ROOT_ID);
            //删除所有以该节点为根节点的Access记录
            List<QueryCondition> parentConditionList = new List<QueryCondition>();
            parentConditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, accessID));
            parentConditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
            accessDetailDao.delete(parentConditionList);
            //取出所有根节点和根节点为accessID的权限
            if (!ValidatorUtil.isEmpty<AccessDetailModel>(treeItemList))
            {
                List<AccessDetail> accessDetailList = new List<AccessDetail>();
                foreach (AccessDetailModel treeGridItem in treeItemList)
                {
                    if (((treeGridItem.AccessID == 0) && (treeGridItem.ValueID.ToString() != accessID)) || (treeGridItem.AccessID.ToString() == accessID))
                    {
                        AccessDetail accessDetail = new AccessDetail();
                        AccessDetail childrenAccessDetail = getAccessDetailByAccessID(treeGridItem.ValueID.ToString(), AccessDetail.ROOT_ID);
                        accessDetail.AccessID = fatherAccessDetail.ValueID;
                        accessDetail.AccessName = childrenAccessDetail.AccessName;
                        accessDetail.Type = AccessDetail.ACCESS_TYPE;
                        accessDetail.ValueID = childrenAccessDetail.ValueID;
                        accessDetail.CreateUserID = childrenAccessDetail.CreateUserID;
                        accessDetail.CreateDate = childrenAccessDetail.CreateDate;
                        accessDetailList.Add(accessDetail);
                    }
                }
                accessDetailDao.create(accessDetailList);
            }
        }
        //门禁权限管理
        //获取用户DoorTimeList
        public List<AccessDetailView> getDoorTimeAccessByAccessID(string selectedAccessID)
        {
            List<AccessDetailView> selectedDoorTimeList = new List<AccessDetailView>();

            //获取选取的权限本身包含的DoorTime列表
            List<QueryCondition> accessConditionList = new List<QueryCondition>();
            accessConditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, selectedAccessID));
            accessConditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.DOORTIME_TYPE));
            selectedDoorTimeList = accessDetailViewDao.getAll(accessConditionList);

            return selectedDoorTimeList;
        }
        //门禁权限管理
        //获取用户DoorTimeList
        public List<DoorTimeView> getDoorTimeViewListByUserID(string userID)
        {
            
            List<DoorTimeView> allDoorTimeViewList = doorTimeViewDao.getAll();
            List<DoorTimeView> doorTimeViewList = new List<DoorTimeView>();
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS, ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN));
            List<Privilege> userPrivilegeList = privilegeDao.getAll(conditionList);
            //显示用户可以增加的DoorTimeTree
            if (!ValidatorUtil.isEmpty<DoorTimeView>(allDoorTimeViewList))
            {
                //根据用户权限，获取DoorTimeList
                foreach (Privilege privilege in userPrivilegeList)
                {
                    foreach (DoorTimeView doorTimeView in allDoorTimeViewList)
                    {
                        if (privilege.PrivilegeAccessValue.Equals(doorTimeView.ControlID.ToString()))
                        {
                            doorTimeViewList.Add(doorTimeView);
                        }
                    }
                }                
            }
            
            return doorTimeViewList;
        }
        //门禁权限管理
        //增加门禁权限中包含的DoorTime
        public void addDeviceInAccess(int userID, string accessID, List<AccessDetailModel> treeItemList)
        {
            
            //删除所有以该节点为根节点的DoorTime记录
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, accessID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.DOORTIME_TYPE));
            accessDetailDao.delete(conditionList);

            //添加提交上来的DoorTime到权限列表中
            if (!ValidatorUtil.isEmpty<AccessDetailModel>(treeItemList))
            {
                foreach (AccessDetailModel treeGridItem in treeItemList)
                {
                    if (treeGridItem.Type == AccessDetail.DOORTIME_TYPE)
                    {
                        AccessDetail accessDetail = new AccessDetail();
                        accessDetail.AccessID = Convert.ToInt32(accessID);
                        accessDetail.AccessName = treeGridItem.NodeName;
                        accessDetail.Type = AccessDetail.DOORTIME_TYPE;
                        accessDetail.ValueID = treeGridItem.ValueID;
                        accessDetail.CreateDate = DateTime.Now;
                        accessDetail.CreateUserID = userID;
                        accessDetailDao.create(accessDetail);
                    }

                }
            }
        }
        //门禁权限管理
        //删除门禁权限
        public override void Delete(List<String> idList)
        {
            foreach (String id in idList)
            {
                //如果删除的节点是根节点，还需要删除所有包含此节点的权限
                //删除所有以该根节点为子节点的记录
                List<QueryCondition> conditionList = new List<QueryCondition>();
                conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.VALUE_ID_NAME, id));
                conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
                accessDetailDao.delete(conditionList);
                //删除所有以该根节点为根节点的记录
                List<QueryCondition> parentConditionList = new List<QueryCondition>();
                parentConditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, id));
                parentConditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
                accessDetailDao.delete(parentConditionList);
            }
             
        }
        
        
        //门禁权限管理
        //根据AccessID和其父本ID获取AccessDetail记录，parentID是没有拼接的ID
        public AccessDetail getAccessDetailByAccessID(string accessID, string parentID)
        {
            AccessDetail accessDetail = new AccessDetail();

            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.VALUE_ID_NAME, accessID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, parentID));
            accessDetail = accessDetailDao.getUniRecord(conditionList);

            return accessDetail;
        }
        //门禁权限管理
        //根据AccessID获取其子权限列表
        public List<AccessDetail> getAccessDetailListByAccessID(string accessID)
        {
            List<AccessDetail> accessDetailList = new List<AccessDetail>();

            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, accessID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
            accessDetailList = accessDetailDao.getAll(conditionList);

            return accessDetailList;
        }

    }
}