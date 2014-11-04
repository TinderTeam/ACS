using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service.Impl;
namespace ACS.Service
{
    public class ServiceContext
    {
        static ServiceContext serviceContext;

        private PlatFormService platFormService;
        private UserService userService;
        private EmployeeService employeeService;
        private LoginService loginService;
        private EventRecordViewService eventRecordViewService;
        private AlarmRecordService alarmRecordService;
        private HolidayService holidayService;
        private MonitorService monitorService;

        public static ServiceContext getInstance()
        {
            if (serviceContext == null)
            {
                serviceContext = new ServiceContext();
            }
            return serviceContext;
        }

        public PlatFormService getPlatFormService()
        {

            if (platFormService == null)
            {
                platFormService = new PlatFormServiceImpl();
            }
            return platFormService;
        }

        public UserService getUserService()
        {

            if (userService == null)
            {
                userService = new UserServiceImpl();
            }
            return userService;
        }
        public EmployeeService getEmployeeService()
        {

            if (employeeService == null)
            {
                employeeService = new EmployeeServiceImpl();
            }
            return employeeService;
        }
        public LoginService getLoginService()
        {

            if (loginService == null)
            {
                loginService = new LoginServiceImpl();
            }
            return loginService;
        }
        public EventRecordViewService getEventRecordViewService()
        {

            if (eventRecordViewService == null)
            {
                eventRecordViewService = new EventRecordViewServiceImpl();
            }
            return eventRecordViewService;
        }
        public AlarmRecordService getAlarmRecordService()
        {

            if (alarmRecordService == null)
            {
                alarmRecordService = new AlarmRecordServiceImpl();
            }
            return alarmRecordService;
        }
        public HolidayService getHolidayService()
        {

            if (holidayService == null)
            {
                holidayService = new HolidayServiceImpl();
            }
            return holidayService;
        }
        public MonitorService getMonitorService()
        {

            if (monitorService == null)
            {
                monitorService = new MonitorServiceImpl();
            }
            return monitorService;
        }

    }
}