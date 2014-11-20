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
        private String doorName;
        private String doorTimeName;
        private String startTime;
        private String endTime;

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
        
        public String DoorName
        {
            get { return doorName; }
            set { doorName = value; }
        }
        
        public String DoorTimeName
        {
            get { return doorTimeName; }
            set { doorTimeName = value; }
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
        
        public String ToJsonStr()
        {
            String str = "{id:\"" + id + "\", text:\"" + text + "\",type:\"" + type + "\"";
            if((pid!=null)&&(!pid.Equals(""))){
         
                str = str+", pid:\""+pid+"\"";
            }
            if ((type!= null) && (type.Equals("DoorTimeID")))
            {

                str = str + ", controlName:\"" + controlName + "\",doorName:\"" + doorName + "\",doorTimeName:\"" + doorTimeName
                      + "\",startTime:\"" + startTime + "\",endTime:\"" + endTime+"\"";
            }
            return str + "}";
            
        }
    }
}