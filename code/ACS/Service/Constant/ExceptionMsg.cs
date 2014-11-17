using ACS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Service.Constant
{
    public class ExceptionMsg : FuegoCommonMsg
    {

        public const String USER_NOT_EXISTED = "USER_NOT_EXISTED";                 //用户不存在
        public const String USERNAME_PASSWORD_WRONG = "USERNAME_PASSWORD_WRONG";   //用户密码错误
        public const String LOGIN_FAILED = "LOGIN_FAILED";                          //登录失败
        public const String USER_EXISTED = "USER_EXISTED";                          //用户已存在
        public const String OLD_PASSWORD_WRONG = "OLD_PASSWORD_WRONG";              //原密码错误
        public const String DATE_FOMATE = "DATE_FOMATE";                            //数据格式
        public const String DATA_CONVERT_ERROR = "DATA_CONVERT_ERROR";              //数据转换错误
        public const String USERNAME_EXIST = "USERNAME_EXIST";                      //用户名已存在
        public const String EMPLOYEE_CODE_EXIST = "EMPLOYEE_CODE_EXIST";            //工号已存在
        public const String EMPLOYEE_NOT_EXIST = "EMPLOYEE_NOT_EXIST";              //工号不存在
        public const String HOLIDAY_NOT_EXIST = "HOLIDAY_NOT_EXIST";                //假日不存在
        public const String EMPLOYEE_CARDNO_DUPLICATE = "EMPLOYEE_CARDNO_DUPLICATE";    //员工卡号重复
        public const String DEPTNAME_EXIST = "DEPTNAME_EXIST";                      //用户名已存在
        public const String DEPT_HAS_CHILDREN = "DEPT_HAS_CHILDREN";                //部门存在子部门
        public const String ACCESS_NOT_EXIST = "ACCESS_NOT_EXIST";                  //门禁权限不存在

        public const String OPERATE_SUCCESS = "OPERATE_SUCCESS";                    //操作成功
        public const String OPERATE_FAILED = "OPERATE_FAILED";                      //操作失败


    }
}