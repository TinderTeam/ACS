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

namespace ACS.Service.Impl
{
    public class DeviceServiceImpl:DeviceService

    {

        CommonDao<Control> controlDao = DaoContext.getInstance().getControlDao();
        CommonDao<Door> doorDao = DaoContext.getInstance().getDoorDao();
        CommonDao<DoorTime> doorTimeDao = DaoContext.getInstance().getDoorTimeDao();
        #region DeviceService 成员

        public Common.Dao.datasource.AbstractDataSource<Models.Po.Business.Control> getDeviceList(Models.Po.Business.Control filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<Control> dataSource = new DatabaseSourceImpl<Control>(conditionList);
            return dataSource;
        }

        #endregion

        #region DeviceService 成员


        public AbstractDataSource<Door> getDoorList(Door filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<Door> dataSource = new DatabaseSourceImpl<Door>(conditionList);
            return dataSource;
        }

        #endregion

        #region DeviceService 成员

        /// <summary>
        /// 通过ID获取控制器Model
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public Models.Model.DeviceModel getDeviceByID(string DeviceID)
        {
            DeviceModel model = new DeviceModel();

            //获取Control
            QueryCondition condition = new QueryCondition(
                  ConditionTypeEnum.EQUAL,
                  Control.CONTROL_ID,
                  DeviceID
                );
            Control control = controlDao.getUniRecord(condition);
            model.Control = control;

            //获取DoorList
            QueryCondition doorCondition = new QueryCondition(
                  ConditionTypeEnum.EQUAL,
                  Control.CONTROL_ID,
                  DeviceID
                );
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(doorCondition);
            List<Door> doorList = new DatabaseSourceImpl<Door>(conditionList).getAllPageData();
            model.DoorList = doorList;
            return model; 
        }

        #endregion

        #region DeviceService 成员

        /// <summary>
        /// 根据ID获取门信息
        /// </summary>
        /// <param name="DoorID"></param>
        /// <returns></returns>
        public DoorModel getDoorByID(string DoorID)
        {
            DoorModel model = new DoorModel();

            //获取Control
            QueryCondition condition = new QueryCondition(
                  ConditionTypeEnum.EQUAL,
                  Door.DOOR_ID,
                  DoorID
                );
            Door door = doorDao.getUniRecord(condition);
            model.Door = door;
            //获取DoorTimeList
            QueryCondition doorTimeCondition = new QueryCondition(
                  ConditionTypeEnum.EQUAL,
                 Door.DOOR_ID,
                  DoorID
                );
            List<QueryCondition> DoorConditionList = new List<QueryCondition>();
            DoorConditionList.Add(doorTimeCondition);

            List<DoorTime> doorTimeList = new DatabaseSourceImpl<DoorTime>(DoorConditionList).getAllPageData();
            model.DoorTimeList = doorTimeList;
            return model;
        }

        #endregion

        #region DeviceService 成员


        public List<DoorTime> getDoorTimeListByDoorID(string DoorID)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                  DoorTime.DOOR_ID,
                  DoorID
            );
            conditionList.Add(condition);
            AbstractDataSource<DoorTime> dataSource = new DatabaseSourceImpl<DoorTime>(conditionList);
            return dataSource.getAllPageData() ;
        }

        #endregion

        #region DeviceService 成员


        public void updateDoorTimeList(List<DoorTime> list)
        {
            foreach (DoorTime doorTime in list)
            {
                
                
           }
        }

        #endregion
    }
}