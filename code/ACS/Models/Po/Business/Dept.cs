using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Dept
    {
        public const String ID = "DeptID";
        public const String Name = "DeptName";
        public const String FatherID = "FatherDeptID";
        private int deptID;//部门ID
        private String deptName;//部门名称
        private int fatherDeptID;//父部门ID
        private String deptCode;//部门编号
        private String note;//备注

        public virtual int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }

        public virtual String DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        public virtual int FatherDeptID
        {
            get { return fatherDeptID; }
            set { fatherDeptID = value; }
        }

        public virtual String DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }

        public virtual String Note
        {
            get { return note; }
            set { note = value; }
        }
    }
}