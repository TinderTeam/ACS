using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Models.Model;
using ACS.Models.Po;
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
    }

}
