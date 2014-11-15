using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class DeptModel
    {
        private int deptID;//部门ID
        private String deptName;//部门名称
        private String fatherDeptID;//父部门ID
        private String fatherDeptName;//父部门名称
        private String deptCode;//部门编号
        private String note;//备注

        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        
        public String DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        
        public String FatherDeptID
        {
            get { return fatherDeptID; }
            set { fatherDeptID = value; }
        }
        
        public String FatherDeptName
        {
            get { return fatherDeptName; }
            set { fatherDeptName = value; }
        }
        
        public String DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        
        public String Note
        {
            get { return note; }
            set { note = value; }
        }

    }
}