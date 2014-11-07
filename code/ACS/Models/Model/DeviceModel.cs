using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po.Business;

namespace ACS.Models.Model
{
    public class DeviceModel
    {
        private Control control;
        private List<Door> doorList;

     
        public List<Door> DoorList
        {
            get { return doorList; }
            set { doorList = value; }
        }

        public Control Control
        {
            get { return control; }
            set { control = value; }
        }
    }
}