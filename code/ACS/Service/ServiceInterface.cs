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
namespace ACS.Service
{


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
        List<DoorTimeView> getDoorTimeViewList(string userID);
    }
    public interface DeptService
    {
        TreeModel getDeptTree();
        Dept getDeptByID(string deptID);
        void addDept(DeptModel deptModel);
        void editDept(DeptModel deptModel);
        void deleteDept(string deptID);
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
    public interface UserService
    {
        AbstractDataSource<User> getUserList(User filter);
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

    public interface EmployeeService
    {
        AbstractDataSource<Employee> getEmployeeList(Employee filter);
        void create(EmployeeModel employeeModel);
        void delete(List<int> employeeIDList);
        void cancel(List<int> employeeIDList);
        void leave(List<int> employeeIDList);
        EmployeeModel getEmployeeByID(string employeeID);
        void update(EmployeeModel employeeModel);
        string getEmployeeList(List<string> idList);
        void saveEmployeeCard(List<EmployeeModel> employeeModelList);
    }
    public interface EventRecordViewService
    {
        AbstractDataSource<EventRecordView> getEventRecordViewList(EventRecordView filter);

    }
    public interface AlarmRecordService
    {
        AbstractDataSource<AlarmRecord> getAlarmRecordList(AlarmRecord filter);

    }
    public interface HolidayService
    {
        AbstractDataSource<Holiday> getHolidayList(Holiday filter);
        void create(HolidayModel holidayModel);
        HolidayModel getHolidayModelByID(string holidayID);
        void update(HolidayModel holidayModel);
        void delete(List<int> holidayIDList);
    }
    public interface MonitorService
    {
        AbstractDataSource<Control> getControlList(Control filter);
    }
    public interface PrivilegeService
    {
        
    }
    public interface LoginService
    {
        UserModel Login(string userName, string password);
        void ModifyPswd(string userName, string oldPswd, string newPswd);
    }
}
