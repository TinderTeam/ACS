using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class MenuTreeModel
    {
        private List<MenuTreeItem> menuTreeItemList;

        public List<MenuTreeItem> MenuTreeItemList
        {
            get { return menuTreeItemList; }
            set { menuTreeItemList = value; }
        }

        public String ToJsonStr()
        {
            String str = "[";
            foreach (MenuTreeItem i in MenuTreeItemList)
            {
                if (str.Equals("["))
                {
                    str = str + i.ToJsonStr();
                }
                else
                {
                    str = str +","+ i.ToJsonStr();
                }           
            }
            str=str+"]";
            return str;
        }
    }

    public class MenuTreeItem
    {
        private String id;

        public String Id
        {
            get { return id; }
            set { id = value; }
        }
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }
        private String pid;

        public String Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        public String ToJsonStr()
        {
            String str;
            if(pid==null||pid.Equals("")){
                str = "{id:\"" + id + "\", text: \"" + text + "\"}";
            }else{
                str = "{id:\"" + id + "\", text: \"" + text + "\", pid: \""+pid+"\" }";
            }
            return str;
           
            
        }
    }
}