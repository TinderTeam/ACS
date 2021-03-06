﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service.Impl;


using ACS.Models.Po.Business;
namespace ACS.Service
{
    public class ServiceContext
    {
        private static ServiceContext serviceContext;

        private PlatFormService platFormService;
        private UserService userService;
        private EmployeeService employeeService;
        private LoginService loginService;
        private EventRecordService eventRecordViewService;
        private AlarmRecordService alarmRecordService;
        private HolidayService holidayService;
        private MonitorService monitorService;
        private PrivilegeService privilegeService;
        private DeviceService deviceService;
        private EventTypeService eventTypeService;
        private AccessDetailService accessDetailService;
        private DeptService deptService;
        private JobService jobService;
        private LogService logService;

        public static ServiceContext getInstance()
        {
            if (serviceContext == null)
            {
                serviceContext = new ServiceContext();
            }
            return serviceContext;
        }

        public LogService getLogService()
        {

            if (logService == null)
            {
                logService = new LogServiceImpl();
            }
            return logService;

        }

        public AccessDetailService getAccessDetailService()
        {

            if (accessDetailService == null)
            {
                accessDetailService = new AccessDetailServiceImpl();
            }
            return accessDetailService;
        }

         public DeviceService getDeviceService()
        {

            if (deviceService == null)
            {
                deviceService = new DeviceServiceImpl();
            }
            return deviceService;
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
        public EventRecordService getEventRecordService()
        {

            if (eventRecordViewService == null)
            {
                eventRecordViewService = new EventRecordServiceImpl();
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
        public PrivilegeService getPrivilegeService()
        {

            if (privilegeService == null)
            {
                privilegeService = new PrivilegeServiceImpl();
            }
            return privilegeService;
        }
        public DeptService getDeptService()
        {

            if (deptService == null)
            {
                deptService = new DeptServiceImpl();
            }
            return deptService;
        }
        public JobService getJobService()
        {

            if (jobService == null)
            {
                jobService = new JobServiceImpl();
            }
            return jobService;
        }
        public EventTypeService getEventTypeService()
        {

            if (eventTypeService == null)
            {
                eventTypeService = new EventTypeServiceImpl();
            }
            return eventTypeService;
        }
    }
}