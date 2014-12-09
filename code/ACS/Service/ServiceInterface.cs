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
        void Delete(List<String> idList);
        E Get(String id);
        List<E> Get(List<String> idList);
        T Get<T>(String key, String value);
    }

    public interface EmployeeService : CommonService<Employee>
    {
 
        void cancel(List<String> employeeIDList);
        void leave(List<String> employeeIDList);
        void saveEmployeeCard(List<Employee> employeeModelList);
    }
    public interface DeviceOperatorService
    {
        bool openDoor(String doorID);
        bool closeDoor(String doorID);
    }

    public interface AccessDetailService : CommonService<AccessDetail>
    {
        List<AccessDetailView> getAccessDetailViewList(string userID,string parentID);
        void addAccessInAccess(string accessID, List<AccessDetailModel> treeItem);
        AccessDetail getAccessDetailByAccessID(string accessID, string parentID);
        List<AccessDetailView> getDoorTimeAccessByAccessID(string selectedAccessID);
        List<DoorTimeView> getDoorTimeViewListByUserID(string userID);
        void addDeviceInAccess(int userID, string accessID, List<AccessDetailModel> treeItemList);
        List<AccessDetail> getAccessDetailListByAccessID(string accessID);
    }
    public interface DeptService  : CommonService<Dept>
    {
 
    }
    public interface JobService : CommonService<Job>
    {

    }
    public interface DeviceService : CommonService<Control>
    {
        List<TreeModel> getDeviceTreeByID(String userID);
        void ModifyDoorTime(int userID, DoorTime doorTime);
        void ModifyDoor(int userID, Door door);
        DoorModel getDoorByID(string DoorID);

        void StartMonitorAll();
        void StartMonitor(List<String> idList);
        void OpenDoor(String doorID);
        void CloseDoor(String doorID);
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
        
    }
    public interface AlarmRecordService : CommonService<AlarmRecord>
    {

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
    public interface LoginService
    {
        UserModel Login(string userName, string password);
        void ModifyPswd(string userName, string oldPswd, string newPswd);
    }
}
