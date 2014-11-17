using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po;
using ACS.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using ACS.Common.Constant;
using ACS.Models.Po.CF;
using ACS.Service.Constant;
using ACS.Common.Util;
using ACS.Models.Po.Sys;
using ACS.Models.Po.Business;
namespace ACS.Service.Impl
{
    public class DeptServiceImpl : DeptService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<Dept> deptDao = DaoContext.getInstance().getDeptDao();

        //部门管理
        //获取部门树
        public TreeModel getDeptTree()
        {
            //获取所有部门树列表
            List<Dept> deptList = deptDao.getAll();
            TreeModel deptTree = ModelConventService.toDeptTreeModel(deptList);
            return deptTree;
        }
        //部门管理
        //根据部门ID获取部门
        public Dept getDeptByID(string deptID)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Dept.ID, deptID);
            Dept dept = deptDao.getUniRecord(condition);
            return dept;
        }
        //部门管理
        //新增部门
        public void addDept(DeptModel deptModel)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Dept.Name, deptModel.DeptName);
            if (null != deptDao.getUniRecord(condition))
            {
                log.Error("create failed, the deptName has exist. user name is " + deptModel.DeptName);
                throw new SystemException(ExceptionMsg.DEPTNAME_EXIST);
            }
            Dept newDept = new Dept();
            Dept dept = ModelConventService.toDept(newDept,deptModel);
            deptDao.create(dept);
        }
        //部门管理
        //编辑部门
        public void editDept(DeptModel deptModel)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Dept.Name, deptModel.DeptName);
            if (null != deptDao.getUniRecord(condition))
            {
                log.Error("create failed, the deptName has exist. user name is " + deptModel.DeptName);
                throw new SystemException(ExceptionMsg.DEPTNAME_EXIST);
            }
            QueryCondition IDcondition = new QueryCondition(ConditionTypeEnum.EQUAL, Dept.ID, deptModel.DeptID.ToString());
            Dept orignalDept = deptDao.getUniRecord(IDcondition);
            Dept dept = ModelConventService.toDept(orignalDept,deptModel);
            deptDao.update(dept);
        }
        //部门管理
        //删除部门
        public void deleteDept(string deptID)
        {
            QueryCondition fatherIDCondition = new QueryCondition(ConditionTypeEnum.EQUAL, Dept.FatherID, deptID);
            QueryCondition IDcondition = new QueryCondition(ConditionTypeEnum.EQUAL, Dept.ID, deptID);
            Dept dept = deptDao.getUniRecord(IDcondition);
            List<Dept> childrenDeptList = deptDao.getAll(fatherIDCondition);
            if (!ValidatorUtil.isEmpty<Dept>(childrenDeptList))
            {
                log.Error("Delete failed, the Dept to be deleted has children Dept. Dept name is " + dept.DeptName);
                throw new SystemException(ExceptionMsg.DEPT_HAS_CHILDREN);
            }

            deptDao.delete(IDcondition);
        }
  
    }
}