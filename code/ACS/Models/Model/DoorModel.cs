using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po.Business;

namespace ACS.Models.Model
{
    public class DoorModel
    {
        private Door door;

        private List<DoorTime> doorTimeList;

        public List<DoorTime> DoorTimeList
        {
            get { return doorTimeList; }
            set { doorTimeList = value; }
        }

        public Door Door
        {
            get { return door; }
            set { door = value; }
        }

    }
}