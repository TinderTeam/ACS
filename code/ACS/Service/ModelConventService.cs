using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Model;
using ACS.Models.Po;

namespace ACS.Service
{
    public class ModelConventService
    {
       public  static  List<FormColumn> toFormColumnList(List<DbTable> list){
           return null;
       }
       public  static  List<ColumnModel> toColumnList(List<DbTable> list){
             List < ColumnModel > columnList= new List<ColumnModel>();
             foreach (DbTable item in list){
                 columnList.Add(toColumn(item));             
            }
             return columnList;
       }

       public static ColumnModel toColumn(DbTable tablePo)
       {
           ColumnModel cm = new ColumnModel();
           cm.Name = tablePo.ColumnName;
           cm.Title = tablePo.ColumnName;    //国际化调整此处
           cm.Type = tablePo.Type;
           return cm;
       }  

        public static List<UserModel> toUserModelList(List<User> userList){
            List<UserModel> userModelList = new List<UserModel>();
            foreach (User item in userList)
            {
                userModelList.Add(toUserModel(item));
            }
            return userModelList;
        }

        public static UserModel toUserModel(User user)
        {
            UserModel u = new UserModel();
            u.Username = user.UserName;
            u.Pswd = user.Pswd;
            u.UserID = user.UserID;
            return u;
        }

       

    }
}