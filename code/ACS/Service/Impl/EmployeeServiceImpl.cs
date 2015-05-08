using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using ACS.Common.Constant;
using ACS.Service.Constant;
using ACS.Common.Util;
using ACS.Common;
using System.IO;
using ACS.Common.Model;
using ACS.Service.device;
namespace ACS.Service.Impl
{
    public class EmployeeServiceImpl : CommonServiceImpl<Employee>, EmployeeService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<Employee> employeeDao = DaoContext.getInstance().getEmployeeDao();

        //获取对象信息
        public override PersistenceObjInfo GetObjectInfo()
        {
            PersistenceObjInfo perObjInfo = new PersistenceObjInfo();
            perObjInfo.PrimaryName = Employee.ID;
            return perObjInfo;
        }

        public override void Validator(Employee obj)
        {
            if(null == obj)
            {
                log.Info("the obj is empty");
                throw new FuegoException(ExceptionMsg.EMPLOYEE_NOT_EXIST);
            }

            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeCode", obj.EmployeeCode);
            Employee old = employeeDao.getUniRecord(condition);

           
            //对时间进行处理
             DateTime now=DateTime.Now;
             obj.EndDate = now.AddYears(10);

            if ((null != old) && (old.EmployeeID != obj.EmployeeID))
            {
                log.Error("create failed, the employeeCode has exist. employeeCode is " + obj.EmployeeCode);
                throw new FuegoException(ExceptionMsg.EMPLOYEE_CODE_EXIST);
            }
        }
 
        public override void Modify(int userID,Employee newEmployee)
        {
            Validator(newEmployee);

            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeID", newEmployee.EmployeeID.ToString());
            Employee oldEmployee = employeeDao.getUniRecord(condition);

            oldEmployee.EmployeeName = newEmployee.EmployeeName;
            oldEmployee.EnglishName = newEmployee.EnglishName;
            oldEmployee.Sex = newEmployee.Sex;
            oldEmployee.Home = newEmployee.Home;
            oldEmployee.Birthday = newEmployee.Birthday;
            oldEmployee.Phone = newEmployee.Phone;
            oldEmployee.PersonCode = newEmployee.PersonCode;
            oldEmployee.Home = newEmployee.Home;
            oldEmployee.Car = newEmployee.Car;

            oldEmployee.DeptID = newEmployee.DeptID;
            oldEmployee.Email = newEmployee.Email;

            oldEmployee.EmployeeCode = newEmployee.EmployeeCode;
 
            oldEmployee.JobID = newEmployee.JobID;
 
            oldEmployee.RegDate = newEmployee.RegDate;
            oldEmployee.EndDate = newEmployee.EndDate;
            oldEmployee.Note1 = newEmployee.Note1;
            oldEmployee.Note2 = newEmployee.Note2;
            oldEmployee.Note3 = newEmployee.Note3;
            oldEmployee.Note4 = newEmployee.Note4;
            if (oldEmployee.Photo1 != newEmployee.Photo1)
            {
                //判断文件是不是存在
                if (File.Exists(ServiceConfigConstants.getAppPath() + oldEmployee.Photo1))
                {
                    //如果存在则删除
                    File.Delete(ServiceConfigConstants.getAppPath() + oldEmployee.Photo1);
                }
                oldEmployee.Photo1 = newEmployee.Photo1;
            }
            if (oldEmployee.Photo2 != newEmployee.Photo2)
            {
                //判断文件是不是存在
                if (File.Exists(ServiceConfigConstants.getAppPath() + oldEmployee.Photo2))
                {
                    //如果存在则删除
                    File.Delete(ServiceConfigConstants.getAppPath() + oldEmployee.Photo2);
                }
                oldEmployee.Photo2 = newEmployee.Photo2;
            }
            employeeDao.update(oldEmployee);
			//加入日志
            CreateOperateLog(userID, ServiceConstant.MODIFY_LOG, oldEmployee);
             
        }
 
 
        /// <summary>
        ///批量注销员工
        /// </summary>
        /// <returns></returns>
        public void cancel(int userID, List<String> employeeIDList)
        {
            if (!ValidatorUtil.isEmpty(employeeIDList))
            {
                List<Employee> employeeList = new List<Employee>();
                employeeList = Get(employeeIDList);
                Modify(employeeIDList, "EmpEnable", true);
                //加入日志
                CreateOperateLog(userID, ServiceConstant.CANCEL_EMPLOYEE_LOG, employeeList);
            }
            else 
            {
                log.Info("Canceling employee, but employeeIDList is null");
            }
 
        }
        /// <summary>
        ///批量离职
        /// </summary>
        /// <returns></returns>
        public void leave(int userID,List<String> employeeIDList)
        {
            
            if (!ValidatorUtil.isEmpty(employeeIDList))
            {
                List<Employee> employeeList = new List<Employee>();
                employeeList = Get(employeeIDList);
                Modify(employeeIDList, "Leave", true);
                //加入日志
                CreateOperateLog(userID, ServiceConstant.LEAVE_EMPLOYEE_LOG, employeeList);
            }
            else
            {
                log.Info("Canceling employee, but employeeIDList is null");
            }
        }
        //更改员工权限
        public void modifyAccess(int userID,String employeeID, String accessID)
        {
            if (!ValidatorUtil.isEmpty(accessID))
            {
                AccessDetail acessDetail = new AccessDetail();
                List<QueryCondition> conditionList = new List<QueryCondition>();
                conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetail.VALUE_ID, accessID));
                conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetail.TYPE, AccessDetail.ACCESS_TYPE));
                conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetailView.ID, AccessDetail.ROOT_ID));
                acessDetail = GetDao<AccessDetail>().getUniRecord(conditionList);
                if (null != acessDetail)
                {
                    QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Employee.ID, employeeID);
                    Employee orignalEmployee = GetDao<Employee>().getUniRecord(condition);
                    orignalEmployee.AccessID = Convert.ToInt32(accessID);
                    GetDao<Employee>().update(orignalEmployee);
                    //加入日志
                    CreateOperateLog(userID, ServiceConstant.ASSIGN_ACCESS_LOG, orignalEmployee);
                }
                else
                {
                    log.Error("Add access to employee error, The access is not exist, accessID is " + accessID);
                    throw new FuegoException(ExceptionMsg.ACCESS_NOT_EXIST);
                }
                
            }
            else 
            {
                log.Error("Add access to employee error, The accessID is null.");
                throw new FuegoException(ExceptionMsg.ACCESS_NOT_EXIST);
            }
            
        }

        /// <summary>
        ///员工批量发卡
        /// </summary>
        /// <returns></returns>
        public void saveEmployeeCard(int userID, List<Employee> employeeModelList)
        {
            //判断卡号是否重复
            for(int i=0;i<employeeModelList.Count;i++)
            {
                //判断提交的卡号是否已存在
                QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Employee.Card,employeeModelList[i].CardNo);
                Employee employeeTest = employeeDao.getUniRecord(condition);
                if (null != employeeTest)
                {
                    if(employeeModelList[i].EmployeeID != employeeTest.EmployeeID)
                    {
                        log.Error("employee card NO duplicate, " + employeeModelList[i].EmployeeName + " and " + employeeTest.EmployeeName + "are the same");
                        throw new FuegoException(ExceptionMsg.EMPLOYEE_CARDNO_DUPLICATE);
                    }   
                }
                //判断提交的卡号是否存在重复
                for(int j=i+1;j<employeeModelList.Count;j++)
                {
                    if(employeeModelList[i].CardNo==employeeModelList[j].CardNo)
                    {
                        log.Error("employee card NO duplicate, "+employeeModelList[i].EmployeeName+" and " +employeeModelList[j].EmployeeName+"are the same");
                        throw new FuegoException(ExceptionMsg.EMPLOYEE_CARDNO_DUPLICATE);
                    }
                }
            }

            //更新发卡信息
            for (int i = 0; i < employeeModelList.Count; i++)
            {
                QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Employee.ID, employeeModelList[i].EmployeeID.ToString());
                Employee orignalEmployee = employeeDao.getUniRecord(condition);
                orignalEmployee.CardNo = employeeModelList[i].CardNo;
                employeeDao.update(orignalEmployee);
                //加入日志
                CreateOperateLog(userID, ServiceConstant.SAVE_CARD_LOG, orignalEmployee);
            }
        }

        #region EmployeeService 成员

        /// <summary>
        /// 通过卡ID获取雇员ID信息
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns>没有则返回-1</returns>
        public int GetEmployeeIDByCardID(String cardID)
        {
             QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                Employee.Card,
                cardID.ToString());
            Employee employee=GetDao().getUniRecord(condition);
            if (employee != null)
            {
                return employee.EmployeeID;
            }else{
                return -1;
            }
        }



        /// <summary>
        /// 根据卡号更新员工时间刷新
        /// </summary>
        /// <param name="cardID"></param>
        public void UpdateLastEvent(string cardID,int eventID)
        {
            QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                Employee.Card,
                cardID);
            Employee employee=GetDao().getUniRecord(condition);
            if (employee != null)
            {
                employee.LastEventID=eventID;
                GetDao().update(employee);
            }
                /* 修改 2015/1/28 如果没有关联人员则只记录，不抛出错误
            else
            {
                throw new FuegoException(ExceptionMsg.EMPLOYEE_CODE_NOT_EXIST);
            }
                 */
        }

        #endregion

        #region EmployeeService 成员

        /// <summary>
        /// 根据员工ID列表进行下发员工卡片信息到设备的操作
        /// </summary>
        /// <param name="list"></param>
        public void DownCardList(List<string> list,String uuID)
        {
            log.Info("Download card list by Employees : uuid=" + uuID + ".Employee list is" + JsonConvert.ObjectToJson(list));

            int i = 0;
            foreach (String id in list)
            {
                int p = (100 * (i + 1) / list.Count);
                ProcessManageCache.Update(uuID, p);

                Employee employee=Get(id);
                //获取这个员工涉及到的控制器-门时间列表
                Dictionary<Control, List<DoorTimeView>> controlDateTimeMap = 
                    ServiceContext.getInstance().getAccessDetailService().getControlListByAccessID(employee.AccessID.ToString());
                ///进度条处理
                ///
                log.Info("Download card list by control datetime map : uuid=" + uuID + ".map is" + JsonConvert.ObjectToJson(controlDateTimeMap.Keys) + JsonConvert.ObjectToJson(controlDateTimeMap.Values));
                foreach (Control c in controlDateTimeMap.Keys)
                {
                    if (controlDateTimeMap.ContainsKey(c) && controlDateTimeMap[c] != null)
                    {
                      
                        DeviceOperatorFactory.getInstance().getDeviceOperator(c).cardInfoDownLoad(
                            employee, 
                            controlDateTimeMap[c],
                            getIndexByEmployeeID(employee.EmployeeID.ToString()));
                    }
                }               
            }
        }

        public void DownAllCardList(List<String> list, String uuID)
        {
            log.Info("Download card list by Control : uuid=" + uuID + ".Control list is" + JsonConvert.ObjectToJson(list));

            int i = 0;
            foreach (String id in list)
            {
                int p = (100 * (i + 1) / list.Count);
                ProcessManageCache.Update(uuID, p);
                //获取这个控制器的时间列表
                List<DoorTimeView> dateViewList=ServiceContext.getInstance().getAccessDetailService().getDoorTimeViewListByControlID(id);
                Control control=Get<Control>(Control.CONTROL_ID, id);
                if (control==null)
                {
                    throw new FuegoException(ExceptionMsg.CONTROL_NOT_EXIST); 
                }
                List< Employee> employeeList=GetDao<Employee>().getAll();
                foreach (Employee e in employeeList)
                {
                     DeviceOperatorFactory.getInstance().getDeviceOperator(control).cardInfoDownLoad(
                            e,
                            dateViewList,
                            getIndexByEmployeeID(e.EmployeeID.ToString()));
                }                 
            }
        }


        #endregion

        #region 员工发卡序号相关服务

        /// <summary>
        /// 将idList中的状态转为回收
        /// </summary>
        /// <param name="idList"></param>
        public void DeleteIndex(List<string> idList)
        {
            foreach (String employeeId in idList)
            {
                QueryCondition condition = new QueryCondition(
                    ConditionTypeEnum.EQUAL, 
                    Employee.ID,
                    employeeId
                    );
                EmployeeIndex ei=GetDao<EmployeeIndex>().getUniRecord(condition);
                //将序号状态转换为已回收
                if (ei == null)
                {
                    throw new FuegoException(ExceptionMsg.EMPLOYEE_INDEX_NOT_EXIST); 
                }
                ei.IndexStatus =EmployeeIndex.RECEIVE;
                GetDao<EmployeeIndex>().update(ei);
            }
        }

        /// <summary>
        /// 获得一个已回收的序号
        /// </summary>
        /// <returns></returns>
        public int getReceiveIndex()
        {
            QueryCondition condition = new QueryCondition(
                   ConditionTypeEnum.EQUAL,
                   EmployeeIndex.INDEX_STATUS,
                   EmployeeIndex.RECEIVE
                   );
            List<EmployeeIndex> ei = GetDao<EmployeeIndex>().getAll(condition);
            if (ei.Count == 0)
            {
                return 0;
            }
            return ei[0].EmployeeIndexID;
        }

        public int getIndexByEmployeeID(String EmployeeID)
        {
            QueryCondition condition = new QueryCondition(
                   ConditionTypeEnum.EQUAL,
                   EmployeeIndex.EMPLOYEE_ID,
                   EmployeeID
                   );
           EmployeeIndex ei = GetDao<EmployeeIndex>().getUniRecord(condition);
            if (ei == null)
            {
                throw new FuegoException(ExceptionMsg.EMPLOYEE_INDEX_NOT_EXIST); 
            }
            return ei.EmployeeIndexID;
        }
        /// <summary>
        /// 创建一个新的序号
        /// </summary>
        /// <param name="obj"></param>
        public void CreateIndex(Employee obj)
        {
            EmployeeIndex ei = new EmployeeIndex();
            ei.EmployeeID = obj.EmployeeID;
            GetDao<EmployeeIndex>().create(ei);    
        }

        public void UpdateIndex(int receiveIndex, Employee obj)
        {
            EmployeeIndex ei = Get<EmployeeIndex>(EmployeeIndex.EMPLOYEE_INDEX_ID, receiveIndex.ToString());
            ei.EmployeeID = obj.EmployeeID;
            ei.IndexStatus = EmployeeIndex.OK;
            GetDao<EmployeeIndex>().update(ei); ;
        }
        #endregion
    }
}