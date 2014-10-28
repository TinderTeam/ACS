using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Constant
{
    public class CommonMsg
    {
        public  const String USER_NOT_EXISTED = "不存在该用户，请重新输入!";
	    public  const String LOGIN_FAILED = "用户名或密码错误";
	    public  const String USER_EXISTED = "用户名已经存在";
	    public  const String OLD_PASSWORD_WRONG = "原始密码错误";
	    public  const String DATE_FOMATE ="日期格式错误";
	    public  const String DATA_CONVERT_ERROR ="数据格式转换错误";
	
	
	    public  const String OPERATE_SUCCESS ="操作成功";
        public  const String OPERATE_FAILED = "操作失败";
    }
}