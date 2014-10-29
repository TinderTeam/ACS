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
        MenuTreeModel getMenuTreeByUserID(int userid);
      
    }
    public interface UserService
    {
        AbstractDataSource<User> getUserList(User filter);
        void create(User user);
        void delete(List<int> userIDList);
        void update(User user);
    }

    public interface EmployeeService
    {
        AbstractDataSource<Employee> getEmployeeList(Employee filter);
        void create(Employee employee);
        void delete(int employeeId);
        void update(Employee employee);
    }
    public interface LoginService
    {
        UserModel Login(string userName, string password);
        void ModifyPswd(string userName, string oldPswd, string newPswd);
    }
}
