using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Service
{
    public class ServiceConstant
    {
        public static String SYS_VISIABLE = "visible";
        public static String SYS_TYPE_MENU= "MENU";
        public static String SYS_MASTER_TYPE_USER="USER";
        public static String SYS_MASTER_TYPE_ROLE = "ROLE";
        public static String SYS_ACCESS_TYPE_APP = "APP";
        public static String SYS_ACCESS_TYPE_DEVICE_DOMAIN = "DEVICE_DOMAIN";
        public static String SYS_OPRATION_VALUE_VISIBLE = "OP";
        public static String ALARM_IMG_PATH = "../Content/img/alarm.png";

        public const String SUCCESS = "成功";
        public const String FAIL = "失败";

        public const String MODIFY_LOG = "编辑";
        public const String CREATE_LOG = "新增";
        public const String DELETE_LOG = "删除";
        public const String ASSIGN_ACCESS_LOG = "配置权限";
        public const String CANCEL_EMPLOYEE_LOG = "注销员工";
        public const String LEAVE_EMPLOYEE_LOG = "离职员工";
        public const String SAVE_CARD_LOG = "员工发卡";
        public const String MODIFY_PRIVILEGE = "更改权限";

        public const String LOG_OBJ_ACCESS = "门禁权限";
        public const String LOG_OBJ_ALARMRECORD = "报警记录";
        public const String LOG_OBJ_DEPT = "部门";
        public const String LOG_OBJ_EMPLOYEE = "员工";
        public const String LOG_OBJ_EVENTYPE = "事件类型";
        public const String LOG_OBJ_HOLIDAY = "假日";
        public const String LOG_OBJ_JOB = "职位";
        public const String LOG_OBJ_SYSTEMUSER = "用户";
        public const String LOG_OBJ_CONTROL = "控制器";
        public const String LOG_OBJ_DOOR = "门";
        public const String LOG_OBJ_DOORTIME = "时间段";



    }
}