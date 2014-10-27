using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using ACS.Models.Po.Sys;
using ACS.Models.Po.CF;
using ACS.Service;
using ACS.Models.Model;
using ACS.Dao;
using ACS.Test;
namespace ACS.Models.Model
{
    public class PrivilegeSet : Dictionary<int, Privilege>
    {
        public void AddList(List<Privilege> list)
        {
            foreach (Privilege p in list)
            {
                if (!this.ContainsKey(p.PrivilegeID))
                {
                    this.Add(p.PrivilegeID,p);
                }
            }
        }


    }
}