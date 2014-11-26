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
using ACS.Common;
namespace ACS.Service.Impl
{
    public class HolidayServiceImpl : HolidayService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<Holiday> holidayDao = DaoContext.getInstance().getHolidayDao();

        public AbstractDataSource<Holiday> getHolidayList(Holiday filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<Holiday> dataSource = new DatabaseSourceImpl<Holiday>(conditionList);
            return dataSource;
        }
        //创建新员工
        public void create(HolidayModel holidayModel)
        {
            Holiday holiday = new Holiday();
            holiday = ModelConventService.toHoliday(holiday, holidayModel);
            holidayDao.create(holiday);
        }
        /// <summary>
        ///批量删除员工
        /// </summary>
        /// <returns></returns>
        public void delete(List<int> holidayIDList)
        {
            foreach (int i in holidayIDList)
            {
                holidayDao.delete(
                    new QueryCondition(
                       ConditionTypeEnum.EQUAL,
                       Holiday.ID,
                       i.ToString()
                    )
                );
            }
        }
        public HolidayModel getHolidayModelByID(string holidayID)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "HolidayID", holidayID);
            Holiday holiday = holidayDao.getUniRecord(condition);
            if (null == holiday)
            {
                log.Error("get holiday failed, the holiday is not exist. holidayID is " + holidayID);
                throw new FuegoException(ExceptionMsg.HOLIDAY_NOT_EXIST);
            }
            HolidayModel holidayModel = ModelConventService.toHolidayModel(holiday);
            return holidayModel;
        }
        public void update(HolidayModel holidayModel)
        {
            //判断用户是否存在
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "HolidayID", holidayModel.HolidayID.ToString());
            Holiday orignalHoliday = holidayDao.getUniRecord(condition);
            if (null == orignalHoliday)
            {
                log.Error("modify holiday failed, the holiday is not exist. HolidayID is " + holidayModel.HolidayID);
                throw new FuegoException(ExceptionMsg.HOLIDAY_NOT_EXIST);
            }
            Holiday holiday = ModelConventService.toHoliday(orignalHoliday, holidayModel);
            holidayDao.update(holiday);
        }
    }
}