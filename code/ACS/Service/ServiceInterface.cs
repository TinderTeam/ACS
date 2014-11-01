using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Models.Model;
using ACS.Models.Po.CF;
using ACS.Models.Po.Business;
using ACS.Common.Dao.datasource;
namespace ACS.Service
{
    public interface PlatFormService
    {
        TreeModel getMenuTreeByUserID(int userid);
        AbstractDataSource<Privilege> getPrivilegeList(Privilege filter);
    }
    public interface UserService
    {
        AbstractDataSource<User> getUserList(User filter);
        void create(UserModel userModel);
        void delete(List<int> userIDList);
        UserModel getUserByID(string userID);
        void update(UserModel userModel);
    }

    public interface EmployeeService
    {
        AbstractDataSource<Employee> getEmployeeList(Employee filter);
        void create(EmployeeModel employeeModel);
        void delete(List<int> employeeIDList);
        EmployeeModel getEmployeeByID(string employeeID);
        void update(EmployeeModel employeeModel);
    }
    public interface EventRecordViewService
    {
        AbstractDataSource<EventRecordView> getEventRecordViewList(EventRecordView filter);

    }
    public interface AlarmRecordService
    {
        AbstractDataSource<AlarmRecord> getAlarmRecordList(AlarmRecord filter);

    }
    public interface LoginService
    {
        UserModel Login(string userName, string password);
        void ModifyPswd(string userName, string oldPswd, string newPswd);
    }
}
