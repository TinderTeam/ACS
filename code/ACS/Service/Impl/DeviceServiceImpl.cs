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
using System.Xml;
using ACS.Models.Po.CF;
namespace ACS.Service.Impl
{
    public class DeviceServiceImpl:DeviceService

    {

        CommonDao<Control> controlDao = DaoContext.getInstance().getControlDao();
        CommonDao<Door> doorDao = DaoContext.getInstance().getDoorDao();
        CommonDao<DoorTime> doorTimeDao = DaoContext.getInstance().getDoorTimeDao();
        ViewDao<AccessDetailView> accessDetailViewDao = DaoContext.getInstance().getAccessDetailViewDao();
        CommonDao<AccessDetail> accessDetailDao = DaoContext.getInstance().getAccessDetailDao();
        CommonDao<Privilege> privilegeDao = DaoContext.getInstance().getPrivilegeDao();



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
                doorTimeDao.update(doorTime);              
            }
        }

        #endregion

        #region DeviceService 成员


        public void updateControl(Control control)
        {
            controlDao.update(control);  
        }
        #endregion

  

        #region DeviceService 成员


        public void deleteControlById(string id)
        {

            //删除该控制器相关门禁权限
            deleteAccessByControlID(id);

            //删除该控制器的域管理权限
            deleteDomainPrivilegeByControlID(id);

            //删除控制器
            QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                    Control.CONTROL_ID,
                    id
            );
            controlDao.delete(condition);
            //删除控制器所属的门的时间段
            deleteDooTimeByControlId(id);
            //删除控制器所属的门
            deleteDoorByControlId(id);
                 
        }

        private void deleteDomainPrivilegeByControlID(string id)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(
                new QueryCondition(
                ConditionTypeEnum.EQUAL,
                    Privilege.ACCESS,
                    ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN
                )   
            );
            conditionList.Add(
                new QueryCondition(
                ConditionTypeEnum.EQUAL,
                    Privilege.ACCESS_VALUE,
                    id
                )
            );
            privilegeDao.delete(conditionList);
        }

        /// <summary>
        /// 根据控制器ID删除Access 中与其相关的信息
        /// </summary>
        /// <param name="id"></param>
        private void deleteAccessByControlID(string id)
        {
            //获取AccessDetail列表控制器
            QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                    Control.CONTROL_ID,
                    id
            );
            List<AccessDetailView> viewList =accessDetailViewDao.getAll(condition);
            List<QueryCondition> accessDetialIDList = ModelConventService.getAccessDetailIDConditionByViewList(viewList);
            //删除AccessDetailList
            if (accessDetialIDList.Count != 0)
            {
                accessDetailDao.delete(accessDetialIDList);
            }
   

        }

     

        #endregion
        /// <summary>
        /// 检查是否具备控制器删除条件
        /// </summary>
        /// <returns></returns>
        private bool ControlDeleteCheck(string id)
        {
            //1.检查是否含有时间段
            List<Door> doorList=getDoorListByControlID(id);
            foreach (Door door in doorList)
            {
                List<QueryCondition> conditionList = new List<QueryCondition>();
                QueryCondition doorTimeCondition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                Door.DOOR_ID,
                door.DoorID.ToString()
                );
                conditionList.Add(doorTimeCondition);
                if (doorTimeDao.getCount(conditionList) > 0)
                {
                    return false;
                }
            }
            //2.检查是否有权限引用
            List<QueryCondition> viewConditionList = new List<QueryCondition>();
            QueryCondition ViewCondition = new QueryCondition(
               ConditionTypeEnum.EQUAL,
               Control.CONTROL_ID,
               id
               );
            viewConditionList.Add(ViewCondition);
            if (accessDetailViewDao.getCount(viewConditionList) > 0)
            {
                return false;
            }
            return true;
        }
   

        public void deleteDoorByControlId(string id)
        {
            QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                  Door.CONTROL_ID,
                  id
            );
            doorDao.delete(condition);
        }


        private void deleteDooTimeByControlId(string id)
        {
            List<Door> list = getDoorListByControlID(id);
            foreach(Door door in list){
                QueryCondition doorTimeCondition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                Door.DOOR_ID,
                door.DoorID.ToString()
                );
                doorTimeDao.delete(doorTimeCondition);
            }
        }

        private List<Door> getDoorListByControlID(string id)
        {
            QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                  Door.CONTROL_ID,
                  id
            );
            List<Door> list = doorDao.getAll(condition);
            return list;
        }

        #region DeviceService 成员

        /// <summary>
        /// 通过读取配置文件获得设备类型对应关系
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int getDoorNumberByDeviceType(string type)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(ServiceConfigConstants.getDeviceConfigXmlPath());
            XmlNode root = xml.SelectSingleNode("/root");
            XmlNodeList xnl = root.ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement device = (XmlElement)xnl.Item(i);
                if (device.GetAttribute("type") == type)
                {
                    return System.Convert.ToInt16(device.GetAttribute("door_num"));
                }
            }
            return 0;
        }
        public int getTimeNumberByDeviceType(string type)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(ServiceConfigConstants.getDeviceConfigXmlPath());
            XmlNode root = xml.SelectSingleNode("/root");
            XmlNodeList xnl = root.ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement device = (XmlElement)xnl.Item(i);
                if (device.GetAttribute("type") == type)
                {
                    return System.Convert.ToInt16(device.GetAttribute("time_num"));
                }
            }
            return 0;
        }

        public List<String> getDeviceTypeList()
        {
            List<String> list = new List<String>();
            XmlDocument xml = new XmlDocument();
            xml.Load(ServiceConfigConstants.getDeviceConfigXmlPath());
            XmlNode root = xml.SelectSingleNode("/root");
            XmlNodeList xnl = root.ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement device = (XmlElement)xnl.Item(i);
                list.Add(device.GetAttribute("type"));
            }
            return list;
        }

        #endregion

        #region DeviceService 成员


        public Control addControl(Control c)
        {
            //创建控制器
            controlDao.create(c);
            //根据控制器创建门
            int doorNum=getDoorNumberByDeviceType(c.Type);
            int doorTimeNum = getTimeNumberByDeviceType(c.Type);
            //根据控制器创建时间
            for (int i = 0; i < doorNum; i++)
            {
                Door door = new Door();
                door.ControlID = c.ControlID;
                door.DoorName = i.ToString();
                doorDao.create(door);

                for (int j = 0; j < doorTimeNum; j++)
                {
                    DoorTime doorTime = new DoorTime();
                    doorTime.DoorID = door.DoorID;
                    doorTimeDao.create(doorTime);
                }
            }
            return c;
        }

        #endregion
    }
}