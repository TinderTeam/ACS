using System;
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
    public class AccessServiceImpl:AccessService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        CommonDao<Access> accessDao = DaoContext.getInstance().getAccessDao();
        CommonDao<AccessDetail> accessDetailDao = DaoContext.getInstance().getAccessDetailDao();
        ViewDao<AccessDetailView> accessDetailViewDao = DaoContext.getInstance().getAccessDetailViewDao();
        CommonDao<Privilege> privilegeDao = DaoContext.getInstance().getPrivilegeDao();
        ViewDao<DoorTimeView> doorTimeViewDao = DaoContext.getInstance().getDoorTimeViewDao();

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
        public AccessDetail createAccessDetail(int creatUserID, string accessName)
        {
            //新增门禁权限树列表
            AccessDetail newAccessDetail = new AccessDetail();
            newAccessDetail.AccessID = 0;
            newAccessDetail.AccessName = accessName;
            newAccessDetail.CreateUserID = creatUserID;
            newAccessDetail.CreateDate = DateTime.Now;
            newAccessDetail.Type = AccessDetail.ACCESS_TYPE;
            accessDetailDao.create(newAccessDetail);
            newAccessDetail.ValueID = newAccessDetail.AccessDetailID;
            accessDetailDao.update(newAccessDetail);
            return newAccessDetail;

        }
        //门禁权限管理
        //编辑门禁权限名称
        public void editAccessName(AccessDetail accessDetail)
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
        public void addAccessInAccess(string accessID, TreeGirdItem treeItem)
        {
            //获取编辑的目标权限
            AccessDetail fatherAccessDetail = getAccessDetailByAccessID(accessID, AccessDetail.ROOT_ID);
            //取出所有根节点ID
            AccessDetail accessDetail = new AccessDetail();
            //foreach (TreeGirdItem treeGridItem in treeItemList)
            //{
            if (treeItem.AccessID == AccessDetail.ROOT_ACCESS_ID)
            {
                AccessDetail childrenAccessDetail = getAccessDetailByAccessID(treeItem.ValueID, AccessDetail.ROOT_ID);
                accessDetail.AccessID = fatherAccessDetail.ValueID;
                accessDetail.AccessName = childrenAccessDetail.AccessName;
                accessDetail.Type = AccessDetail.ACCESS_TYPE;
                accessDetail.ValueID = childrenAccessDetail.ValueID;
                accessDetail.CreateUserID = childrenAccessDetail.CreateUserID;
                accessDetail.CreateDate = childrenAccessDetail.CreateDate;
                accessDetailDao.create(accessDetail);
            }
            //}

        }
        //门禁权限管理
        //获取用户DoorTimeList
        public List<DoorTimeView> getDoorTimeViewList(string userID)
        {
            List<DoorTimeView> allDoorTimeViewList = doorTimeViewDao.getAll();
            List<DoorTimeView> doorTimeViewList = new List<DoorTimeView>();
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS, ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN));
            List<Privilege> userPrivilegeList = privilegeDao.getAll(conditionList);
            //对用户存在的权限菜单打勾
            if (!ValidatorUtil.isEmpty<DoorTimeView>(allDoorTimeViewList))
            {
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
        //删除门禁权限
        public void deleteAccess(TreeGirdItem treeItem)
        {

            string[] accessInfo = treeItem.ValueID.Split(AccessDetail.SPLIT);
            string[] parentInfo = treeItem.AccessID.Split(AccessDetail.SPLIT);
            if ((accessInfo.Length == 2)&&(parentInfo.Length == 2))
            {
                if (parentInfo[1] != AccessDetail.ROOT_ID)
                {
                    //如果要删除的节点不是根节点，仅删除其本身即可
                    List<QueryCondition> conditionList = new List<QueryCondition>();
                    conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.VALUE_ID_NAME, accessInfo[1]));
                    conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
                    conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, parentInfo[1]));

                    if (null == accessDetailDao.getUniRecord(conditionList))
                    {
                        log.Error("Delete failed, the Access to be deleted is not exit, AccessID is" + treeItem.AccessID);
                        throw new FuegoException(ExceptionMsg.ACCESS_NOT_EXIST);
                    }
                    accessDetailDao.delete(conditionList);
                }
                else
                {
                    //如果删除的节点是根节点，还需要删除所有包含此节点的权限
                    //删除所有以该根节点为子节点的记录
                    List<QueryCondition> conditionList = new List<QueryCondition>();
                    conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.VALUE_ID_NAME, accessInfo[1]));
                    conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
                    accessDetailDao.delete(conditionList);
                    //删除所有以该根节点为根节点的记录
                    List<QueryCondition> parentConditionList = new List<QueryCondition>();
                    parentConditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, accessInfo[1]));
                    parentConditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
                    accessDetailDao.delete(parentConditionList);
                }
            }  
        }
        
        
        //门禁权限管理
        //根据拼接好的AccessID和其父本ID获取AccessDetail记录，parentID是没有拼接的ID
        public AccessDetail getAccessDetailByAccessID(string accessID, string parentID)
        {
            AccessDetail accessDetail = new AccessDetail();
            //拆分AccessID
            string[] accessInfo = accessID.Split(AccessDetail.SPLIT);
            if (accessInfo.Length == 2)
            {
                List<QueryCondition> conditionList = new List<QueryCondition>();
                conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.VALUE_ID_NAME, accessInfo[1]));
                conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.TYPE_NAME, AccessDetail.ACCESS_TYPE));
                conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, parentID));
                accessDetail = accessDetailDao.getUniRecord(conditionList);
            }
            return accessDetail;
        }
        

    }
}