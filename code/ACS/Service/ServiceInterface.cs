using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Models.Model;
using ACS.Models.Po;
namespace ACS.Service
{
    public interface PlatFormService
    {
        List<ColumnModel> getTableColumn(String tableName);
    }
    public interface UserService
    {
        List<UserModel> getAllUsers();
    }

}
