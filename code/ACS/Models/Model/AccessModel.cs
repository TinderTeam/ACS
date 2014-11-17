using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class AccessModel
    {
        private String id;      //门禁权限ID
        private String text;    //门禁权限名称
        private String url;      //门禁权限ID
        private String expanded;    //门禁权限名称
        private String _id;      //门禁权限ID
        private String _uid;    //门禁权限名称
        private String _pid;      //门禁权限ID
        private String _level;    //门禁权限名称
        private String _state;  //规则名称

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
        
        public String Url
        {
            get { return url; }
            set { url = value; }
        }
        
        public String Expanded
        {
            get { return expanded; }
            set { expanded = value; }
        }
        
        public String _Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public String _Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        
        public String _Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        
        public String _Level
        {
            get { return _level; }
            set { _level = value; }
        }
        
        public String _State
        {
            get { return _state; }
            set { _state = value; }
        }

    }
}