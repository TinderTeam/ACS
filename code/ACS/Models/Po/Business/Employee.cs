using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po
{
    public class Employee
    {
        private int employeeID;//员工ID

        public int EmployeeID
        {
          get { return employeeID; }
          set { employeeID = value; }
        }

        public const String ID = "EmployeeID";

        private String employeeName;//
        private String employeeCode;//
        private String englishName;//
        private String cardNo;//
        private String pin;//密码开门
        private String empEnable;//是否注销
        private String sex;//
        private String birthday;//
        private String personCode;//
        private String home;//
        private String phone;//
        private String email;//
        private String car;//
        private int jobID;//
        private int deptID;//
        private String photo;//
        private DateTime regDate;//注册日期
        private DateTime endDate;//有效期
        private String deleted;//
        private String leave;//离职
        private DateTime leaveDate;//
        private String beKQ;//是否参与考勤
        private String pswd;//web密码
        private int mapID;//
        private int xPoint;//
        private int yPoint;//
        private String mapVisible;//
        private int ownerDoor;//所属办公室门ID
        private int lastEventID;//最后一次刷卡
        private int event2EmoID;//最后一次刷卡
        private int status;//实时状态
        private DateTime timeStamp;//修改时间
        private String showCardNo;//
        private String note1;//
        private String note2;//
        private String note3;//
        private String note4;//
        private String note5;//
        private DateTime timeStampx;//
        private String isBlackCard;//
        private String ascString;//



    }
}