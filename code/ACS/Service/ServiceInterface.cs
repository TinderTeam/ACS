using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Models.Model;
using ACS.Models.Po.CF;
using ACS.Models.Po.Business;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using TcpipIntface;
using ACS.Models.Po.Sys;
using ACS.Service.Constant;
using ACS.Models.Po.Log;
using ACS.Common;
namespace ACS.Service
{

    public interface CommonService<E>
    {
        void Validator(E obj);
        AbstractDataSource<E> GetDataSource();
        AbstractDataSource<T> GetDataSource<T>(List<QueryCondition> conditionList);
        AbstractDataSource<E> GetDataSource(List<QueryCondition> conditionList);
        void Create(E obj); 
        void Create(int userID,E obj);
        void Modify(E obj);
        void Modify(int userID, E obj);
        void Delete(String id);
        void Delete(int userID, List<String> idList);
        E Get(String id);
        List<E> Get(List<String> idList);
        T Get<T>(String key, String value);

    }

    public interface EmployeeService : CommonService<Employee>
    {
        void cancel(int userID, List<String> employeeIDList);
        void leave(int userID, List<String> employeeIDList);
        void saveEmployeeCard(int userID, List<Employee> employeeModelList);
        void modifyAccess(int userID, String employeeID, String AccessID);
        void UpdateLastEvent(string cardID, int eventID);
        int GetEmployeeIDByCardID(int cardID);
        void DownCardList(List<string> list, String uuID);

        void DeleteIndex(List<string> idList);

        int getReceiveIndex();

        void CreateIndex(Employee obj);

        void UpdateIndex(int receiveIndex, Employee obj);
        int getIndexByEmployeeID(String EmployeeID);

        void DownAllCardList(List<string> list, string uuID);
    }
    public interface DeviceOperatorService
    {
        bool openDoor(String doorID);
        bool closeDoor(String doorID);
    }

    public interface AccessDetailService : CommonService<AccessDetail>
    {
        List<AccessDetailView> getAccessDetailViewList(string userID,string parentID);
        void addAccessInAccess(int userID, string accessID, List<AccessDetailModel> treeItemList);
        AccessDetail getAccessDetailByAccessID(string accessID, string parentID);
        List<AccessDetailView> getDoorTimeAccessByAccessID(string selectedAccessID);
        List<DoorTimeView> getDoorTimeViewListByUserID(string userID);
        List<DoorTimeView> getDoorTimeViewListByAccessID(string AccessID,string ControlID);
        void addDeviceInAccess(int userID, string accessID, List<AccessDetailModel> treeItemList);
        List<AccessDetail> getAccessDetailListByAccessID(string accessID);

        Dictionary<Control, List<DoorTimeView>> getControlListByAccessID(String accessID);

        List<DoorTimeView> getDoorTimeViewListByControlID(string id);
    }
    public interface DeptService  : CommonService<Dept>
    {
 
    }
    public interface JobService : CommonService<Job>
    {

    }
    public interface LogService : CommonService<SystemLog>
    {
        void CreateLog(int userID, String operType, List<LogOperable> operObjList, String operResult);
        void CreateLog(int userID, String operType,LogOperable operObj,String operResult);
        void CreateLog(int userID, String logEvent,String  msg,String result);
    }
    public interface DeviceService : CommonService<Control>
    {
        List<TreeModel> getDeviceTreeByID(String userID);
        void ModifyDoorTime(int userID, DoorTime doorTime);
        void ModifyDoor(int userID, Door door);
        void StartMonitorAll();
        void StartMonitor(List<String> idList);
        void OperateDevice(OperateDeviceCmdEnum cmdCode,String doorID);

        void OnlineStatus(Control control,bool status);
        void DeviceDownload(string controlID, string uuID);

        //更新设备列表批量操作

        void UpdateDeviceInfo(int p, List<String> controlIDList);
    }

    public interface PlatFormService
    {
        AbstractDataSource<Privilege> getPrivilegeList(Privilege filter);
    }
    public interface UserService : CommonService<SystemUser>
    {
        List<TreeModel> getMenuPrivilegeTree(string userID);
        void saveMenuPrivilege(string userID, List<string> menuIDList);
        List<TreeModel> getDevicePrivilegeTree(string userID);
        void saveDevicePrivilege(string userID, List<string> deviceIDList);
    }


    public interface EventRecordService : CommonService<EventRecord>
    {
        List<EventRecordView> GetCurEvent(String indexID, String doorID);
        List<EventTypeModel> GetEventTypeList();
    }
    public interface EventTypeService : CommonService<EventType>
    {

    }
    public interface AlarmRecordService : CommonService<AlarmRecord>
    {
        List<AlarmRecordView> GetCurAlarm(String indexID, String doorID);
        List<EventTypeModel> GetEventTypeList();
    }
    public interface HolidayService : CommonService<Holiday>
    {
       
    }
    public interface MonitorService
    {
        AbstractDataSource<Control> getControlList(Control filter);
    }
    public interface PrivilegeService
    {
        List<String> getPrivilegeValueList(string userID, string PrivilegeType);
        void CreateDomainPrivilege(string userID, string controlID);
    }
    public interface LoginService : CommonService<SystemUser>
    {
        SystemUser Login(SystemUser loginUser);
        void ModifyPswd(string userName, string oldPswd, string newPswd);
        List<Sys_Menu> getSysMenuListByID(int userID);
    }
}
