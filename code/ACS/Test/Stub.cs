using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
namespace ACS.Test
{
    public class Stub
    {
        public static List<DbTable> getDbTableList()
        {
            List<DbTable> list = new List<DbTable>();
            for (int i = 0; i < 5; i++)
            {
                DbTable t = new DbTable();
                t.ColumnName="name"+i;
                t.Type="varchar";
                list.Add(t);
            }
            return list;
        }

        public static List<User> getUserList()
        {
            List<User> list = new List<User>();
            for (int i = 0; i < 5; i++)
            {
                User t = new User();
                t.UserID = i;
                t.UserName = "User"+i;
                t.Pswd = "1234";
                list.Add(t);
            }
            return list;
        }
    }
}