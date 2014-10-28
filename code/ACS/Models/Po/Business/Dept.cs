using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Dept
    {
        private int deptID;//部门ID
        private String deptName;//部门名称
        private String fatherDeptID;//父部门ID
        private String deptCode;//部门编号
        private String hasLeaf;//拥有子节点
        private String note;//备注
    }
}