﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Dao;
using ACS.Common.Dao.impl;
using ACS.Common.Dao;
using ACS.Models.Po.Sys;
using ACS.Models.Po;
using ACS.Models.Po.CF;
using ACS.Models.Po.Business;

namespace ACS.Dao
{
    public class DaoContext
    {
        private static DaoContext daoContext;
        private CommonDao<Privilege> privilegeDao;
        private CommonDao<Sys_Menu> sysMenuDao;
        private CommonDao<SystemUser> userDao;
        private CommonDao<Employee> employeeDao;
        private CommonDao<UserRole> userRoleDao;
        private CommonDao<EventRecord> eventRecordDao;
        private ViewDao<EventRecordView> eventRecordViewDao;
        private CommonDao<AlarmRecord> alarmRecordDao;
        private CommonDao<Holiday> holidayDao;
        private CommonDao<Control> controlDao;
        private CommonDao<Door> doorDao;
        private CommonDao<Dept> deptDao;
		private CommonDao<DoorTime> doorTimeDao;
        private CommonDao<AccessDetail> accessDetailDao;
        private ViewDao<AccessDetailView> accessDetailViewDao;
        private ViewDao<DoorTimeView> doorTimeViewDao;
        private CommonDao<Job> jobDao;

        public static DaoContext getInstance()
        {
            if (daoContext == null)
            {
                daoContext = new DaoContext();
            }
            return daoContext;
        }

        public CommonDao<E> getDao<E>()
        {
            CommonDao<E> dao = new CommonDaoImpl<E>();
            return dao;
        }

        public ViewDao<E> getViewDao<E>()
        {
            ViewDao<E> dao = new ViewDaoCommonImpl<E>();
            return dao;
        }

        public CommonDao<EventRecord> getEventRecordDao()
        {
            if (eventRecordDao == null)
            {
                eventRecordDao = new CommonDaoImpl<EventRecord>();
            }
            return eventRecordDao;
        }

        public CommonDao<DoorTime> getDoorTimeDao()
        {
            if (doorTimeDao == null)
            {
                doorTimeDao = new CommonDaoImpl<DoorTime>();
            }
            return doorTimeDao;
        }


        public CommonDao<Control> getControlDao()
        {
            if (controlDao == null)
            {
                controlDao = new CommonDaoImpl<Control>();
            }
            return controlDao;
        }

        public CommonDao<Sys_Menu> getSysMenuDao()
        {
            if (sysMenuDao == null)
            {
                sysMenuDao = new CommonDaoImpl<Sys_Menu>();
            }
            return sysMenuDao;
        }


        public CommonDao<UserRole> getUserRoleDao()
        {
            if (userRoleDao == null)
            {
                userRoleDao = new CommonDaoImpl<UserRole>();
            }
            return userRoleDao;
        }

        public CommonDao<Privilege> getPrivilegeDao()
         {
             if (privilegeDao == null)
             {
                 privilegeDao = new CommonDaoImpl<Privilege>();
             }
             return privilegeDao;
         }
         public CommonDao<SystemUser> getUserDao()
         {
             if (userDao == null)
             {
                 userDao = new CommonDaoImpl<SystemUser>();
             }
             return userDao;
         }

        public CommonDao<Employee> getEmployeeDao()
         {
             if (employeeDao == null)
             {
                 employeeDao = new CommonDaoImpl<Employee>();
             }
             return employeeDao;
         }

        public ViewDao<EventRecordView> getEventRecordViewDao()
        {
            if (eventRecordViewDao == null)
            {
                eventRecordViewDao = new ViewDaoCommonImpl<EventRecordView>();
            }
            return eventRecordViewDao;
        }
        public CommonDao<AlarmRecord> getAlarmRecordDao()
        {
            if (alarmRecordDao == null)
            {
                alarmRecordDao = new CommonDaoImpl<AlarmRecord>();
            }
            return alarmRecordDao;
        }
        public CommonDao<Holiday> getHolidayDao()
        {
            if (holidayDao == null)
            {
                holidayDao = new CommonDaoImpl<Holiday>();
            }
            return holidayDao;
        }
        public CommonDao<Door> getDoorDao()
        {
            if (doorDao == null)
            {
                doorDao = new CommonDaoImpl<Door>();
            }
            return doorDao;
        }
        public CommonDao<Dept> getDeptDao()
        {
            if (deptDao == null)
            {
                deptDao = new CommonDaoImpl<Dept>();
            }
            return deptDao;
        }
        public CommonDao<AccessDetail> getAccessDetailDao()
        {
            if (accessDetailDao == null)
            {
                accessDetailDao = new CommonDaoImpl<AccessDetail>();
            }
            return accessDetailDao;
        }
        public ViewDao<AccessDetailView> getAccessDetailViewDao()
        {
            if (accessDetailViewDao == null)
            {
                accessDetailViewDao = new CommonDaoImpl<AccessDetailView>();
            }
            return accessDetailViewDao;
        }
        public ViewDao<DoorTimeView> getDoorTimeViewDao()
        {
            if (doorTimeViewDao == null)
            {
                doorTimeViewDao = new CommonDaoImpl<DoorTimeView>();
            }
            return doorTimeViewDao;
        }
        public CommonDao<Job> getJobDao()
        {
            if (jobDao == null)
            {
                jobDao = new CommonDaoImpl<Job>();
            }
            return jobDao;
        }
    }
}