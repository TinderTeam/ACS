using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class TreeModel
    {
        private List<TreeItem> menuTreeItemList;

        public List<TreeItem> MenuTreeItemList
        {
            get { return menuTreeItemList; }
            set { menuTreeItemList = value; }
        }

        public String ToJsonStr()
        {
            String str = "[";
            foreach (TreeItem i in MenuTreeItemList)
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

    public class TreeItem
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
        private bool checkNode;

        public bool CheckNode
        {
            get { return checkNode; }
            set { checkNode = value; }
        }

        private String url;

        public String Url
        {
            get { return url; }
            set { url = value; }
        }

        public String ToJsonStr()
        {
            String str = "{id:\"" + id + "\", text:\"" + text + "\",url:\""+url+"\"";
            if((pid!=null)&&(!pid.Equals(""))){
         
                str = str+", pid:\""+pid+"\"";
            }
            if ((checkNode!= false) && (!checkNode.Equals("")))
            {

                str = str + ", checked:\"" + checkNode + "\"";
            }
            return str + "}";
            
        }
    }
}