using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Models.Po;
namespace ACS.Dao
{


    public interface UserDao
    {
        List<User> getAll();
    }
}
