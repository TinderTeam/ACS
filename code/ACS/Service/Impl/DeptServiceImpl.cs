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
using ACS.Common;
namespace ACS.Service.Impl
{
    public class DeptServiceImpl :CommonServiceImpl<Dept>,DeptService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<Dept> deptDao = DaoContext.getInstance().getDeptDao();

        public override void Validator(Dept obj)
        {
            base.Validator(obj);
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Dept.Name, obj.DeptName);
            if (null != deptDao.getUniRecord(condition))
            {
                log.Error("create failed, the department has exist. department name is " + obj.DeptName);
                throw new FuegoException(ExceptionMsg.DEPTNAME_EXIST);
            }
          
        }
  
        
        //部门管理
        //删除部门
        public override void Delete(List<string> idList)
        {
            foreach(string id in idList)
            {
                QueryCondition fatherIDCondition = new QueryCondition(ConditionTypeEnum.EQUAL, Dept.FatherID, id);
                List<Dept> childrenDeptList = deptDao.getAll(fatherIDCondition);
                if (!ValidatorUtil.isEmpty<Dept>(childrenDeptList))
                {
                    log.Error("Delete failed, the Dept to be deleted has children Dept");
                    throw new FuegoException(ExceptionMsg.DEPT_HAS_CHILDREN);
                }
            }

 	        base.Delete(idList);
        }
       

        public override string GetPrimaryName()
        {
            return Dept.ID;
        }
    }
}