using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Models.Model;
using ACS.Models.Po.CF;
using ACS.Models.Po.Business;
using ACS.Common.Dao.datasource;
using AccessTcp;
using ACS.Common.Dao;
namespace ACS.Service
{

    public interface CommonService<E>
    {
        void Validator(E obj);
        AbstractDataSource<E> GetDataSource();

        AbstractDataSource<T> GetDataSource<T>(List<QueryCondition> conditionList);

        AbstractDataSource<E> GetDataSource(List<QueryCondition> conditionList);
        void Create(E obj);
        void Modify(E obj);
        void Delete(String id);
        void Delete(List<String> idList);
        E Get(String id);
        List<E> Get(List<String> idList);
    }

    public interface EmployeeService : CommonService<Employee>
    {
 
        void cancel(List<String> employeeIDList);
        void leave(List<String> employeeIDList);
        void saveEmployeeCard(List<Employee> employeeModelList);
    }
    public interface DeviceOperatorService
    {
        TCPAcs getControllerTCP(string ip);
    }

    public interface AccessService
    {
        AccessDetail createAccessDetail(int creatUserID, string accessName);
        void deleteAccess(TreeGirdItem treeItem);
        List<AccessDetailView> getAccessDetailViewList(string userID,string parentID);
        void addAccessInAccess(string accessID, TreeGirdItem treeItem);
        AccessDetail getAccessDetailByAccessID(string accessID, string parentID);
        void editAccessName(AccessDetail accessDetail);
        List<DoorTimeView> getDoorTimeViewList(string userID, string selectedAccessID);
        void addDeviceInAccess(int userID, string accessID, List<TreeGirdItem> treeItemList);
    }
    public interface DeptService  : CommonService<Dept>
    {
 
    }
    public interface DeviceService
    {
        AbstractDataSource<Control> getDeviceList(Control filter);
        AbstractDataSource<Door> getDoorList(Door filter);

        DeviceModel getDeviceByID(string DeviceID);

        DoorModel getDoorByID(string DoorID);

        List<DoorTime> getDoorTimeListByDoorID(string DoorID);

        void updateDoorTimeList(List<DoorTime> list);

        void updateControl(Control control);

        void deleteControlById(string id);
        int getTimeNumberByDeviceType(string type);
        int getDoorNumberByDeviceType(string type);
        List<String> getDeviceTypeList();

        Control addControl(Control c);
    }

    public interface PlatFormService
    {
        TreeModel getMenuTreeByUserID(int userid);
        AbstractDataSource<Privilege> getPrivilegeList(Privilege filter);
        TreeModel getUserTree();
    }
    public interface UserService : CommonService<User>
    {
        AbstractDataSource<User> getUserList(string userName);
        void create(UserModel userModel);
        void delete(List<int> userIDList);
        UserModel getUserByID(string userID);
        void update(UserModel userModel);
        TreeModel getPrivilegeMenuTree(string userID);
        void updateMenuPrivilege(string userID,List<string> menuIDList);
        TreeModel getDevicePrivilegeTree(string userID);
        TreeModel getUserDevicePrivilegeTree(string userID);
        void updateDevicePrivilege(string userID, List<string> deviceIDList);

    }


    public interface EventRecordViewService : CommonService<EventRecordView>
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

        void addDomainPrivilege(string userID, string controlID);
    }
    public interface LoginService
    {
        UserModel Login(string userName, string password);
        void ModifyPswd(string userName, string oldPswd, string newPswd);
    }
}
