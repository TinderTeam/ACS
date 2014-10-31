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
        void create(Employee employee);
        void delete(List<int> employeeIDList);
        void update(Employee employee);
    }
    public interface LoginService
    {
        UserModel Login(string userName, string password);
        void ModifyPswd(string userName, string oldPswd, string newPswd);
    }
}
