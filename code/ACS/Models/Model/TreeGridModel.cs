using ACS.Models.Po.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class TreeGridModel
    {
        private List<TreeGirdItem> treeGridItemList;

        public List<TreeGirdItem> TreeGridItemList
        {
            get { return treeGridItemList; }
            set { treeGridItemList = value; }
        }

        public String ToJsonStr()
        {
            String str = "[";
            foreach (TreeGirdItem i in TreeGridItemList)
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

    public class TreeGirdItem
    {
        private String id;
        private String text;
        private String pid;
        private String type;
        private String controlName;
        private String startTime;
        private String endTime;
        private bool checkNode;
        private String accessID;
        public TreeGirdItem()
        {
        }
        public String AccessID
        {
            get { return accessID; }
            set { accessID = value; }
        }
        private String valueID;

        public String ValueID
        {
            get { return valueID; }
            set { valueID = value; }
        }
        private List<TreeGirdItem> children;

        public TreeGirdItem(string pid,string id)
        {
            this.pid = pid;
            this.id = id;
        }
        public List<TreeGirdItem> Children
        {
            get { return children; }
            set { children = value; }
        }

        public String Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public String Text
        {
            get { return text; }
            set { text = value; }
        }
        
        public String Pid
        {
            get { return pid; }
            set { pid = value; }
        }
        
        public String Type
        {
            get { return type; }
            set { type = value; }
        }
        
        public String ControlName
        {
            get { return controlName; }
            set { controlName = value; }
        }
        
        public String StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        
        public String EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public bool CheckNode
        {
            get { return checkNode; }
            set { checkNode = value; }
        }

        public String ToJsonStr()
        {
            String str = "{Id:\"" + id + "\", Text:\"" + text + "\",ValueID:\"" + ValueID + "\",AccessID:\"" + AccessID + "\",Type:\"" + type + "\"";
            if((pid!=null)&&(!pid.Equals(""))){
         
                str = str+", Pid:\""+pid+"\"";
            }
            if ((type != null) && (type.Equals(AccessDetail.DOORTIME_TYPE)))
            {

                str = str + ", ControlName:\"" + controlName
                      + "\",StartTime:\"" + startTime + "\",EndTime:\"" + endTime+"\"";
            }
            return str + "}";
            
        }
    }
}