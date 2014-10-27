using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Models.Po;
using ACS.Models.Po.Sys;
using ACS.Models.Po.CF;
namespace ACS.Dao
{


    public interface UserDao
    {
        List<User> getAll();
        void create(User user);
        void update(User user);
        void delete(int userID);
    }
    public interface SysMenuDao
    {
        List<Sys_Menu> getListByPrivilegeList(List<Privilege> privilege);
    }

    public interface PrivilegeDao
    {
        List<Privilege> getListByMaster(int userID,String typeName);
    }

    public interface EmployeeDao
    {
        List<Employee> getAll();
        void create(Employee employee);
        void update(Employee employee);
        void delete(int employeeID);
    }
}
