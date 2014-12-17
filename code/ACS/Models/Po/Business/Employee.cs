using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Employee
    {
        public const String ID = "EmployeeID";
        public const String Card = "CardNo";
        private int employeeID;//员工ID
        private String employeeName;//
        private String employeeCode;//
        private String englishName;//
        private String cardNo;//卡号
        private String pin;//密码开门
        private bool empEnable;//是否注销
        private bool sex;//
        private DateTime birthday = DateTime.Now;//
        private String personCode;//
        private String home;//
        private String phone;//
        private String email;//
        private String car;//
        private int jobID;//
        private int deptID;//
        private String photo1;//
        private String photo2;//
        private DateTime regDate = DateTime.Now;//注册日期
        private DateTime endDate = DateTime.Now;//有效期
        private bool deleted;//
        private bool leave;//离职
        private DateTime leaveDate = DateTime.Now;//
        private bool beKQ;//是否参与考勤
        private String pswd;//web密码
        private int mapID;//
        private int xPoint;//
        private int yPoint;//
        private bool mapVisible;//
        private int ownerDoor;//所属办公室门ID
        private int lastEventID;//最后一次刷卡
        private int event2EmoID;//最后一次刷卡
        private int status;//实时状态
        private DateTime timeStamp = DateTime.Now;//修改时间
        private bool showCardNo;//
        private String note1;//
        private String note2;//
        private String note3;//
        private String note4;//
        private String note5;//
        private DateTime timeStampx = DateTime.Now;//
        private bool isBlackCard;//
        private String ascString;//
        private int accessID;

        public virtual int AccessID
        {
            get { return accessID; }
            set { accessID = value; }
        }


        public virtual int EmployeeID
        {
          get { return employeeID; }
          set { employeeID = value; }
        }
        public virtual String EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }
        public virtual String EmployeeCode
        {
            get { return employeeCode; }
            set { employeeCode = value; }
        }

        public virtual String EnglishName
        {
            get { return englishName; }
            set { englishName = value; }
        }

        public virtual String CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        public virtual String Pin
        {
            get { return pin; }
            set { pin = value; }
        }

        public virtual bool EmpEnable
        {
            get { return empEnable; }
            set { empEnable = value; }
        }

        public virtual bool Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public virtual DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public virtual String PersonCode
        {
            get { return personCode; }
            set { personCode = value; }
        }

        public virtual String Home
        {
            get { return home; }
            set { home = value; }
        }

        public virtual String Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public virtual String Email
        {
            get { return email; }
            set { email = value; }
        }

        public virtual String Car
        {
            get { return car; }
            set { car = value; }
        }

        public virtual int JobID
        {
            get { return jobID; }
            set { jobID = value; }
        }

        public virtual int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }

        public virtual String Photo1
        {
            get { return photo1; }
            set { photo1 = value; }
        }

        public virtual String Photo2
        {
            get { return photo2; }
            set { photo2 = value; }
        }

        public virtual DateTime RegDate
        {
            get { return regDate; }
            set { regDate = value; }
        }

        public virtual DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public virtual bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public virtual bool Leave
        {
            get { return leave; }
            set { leave = value; }
        }

        public virtual DateTime LeaveDate
        {
            get { return leaveDate; }
            set { leaveDate = value; }
        }
        public virtual bool BeKQ
        {
            get { return beKQ; }
            set { beKQ = value; }
        }
        public virtual String Pswd
        {
            get { return pswd; }
            set { pswd = value; }
        }
        public virtual int MapID
        {
            get { return mapID; }
            set { mapID = value; }
        }
        public virtual int XPoint
        {
            get { return xPoint; }
            set { xPoint = value; }
        }
        public virtual int YPoint
        {
            get { return yPoint; }
            set { yPoint = value; }
        }

        public virtual bool MapVisible
        {
            get { return mapVisible; }
            set { mapVisible = value; }
        }

        public virtual int OwnerDoor
        {
            get { return ownerDoor; }
            set { ownerDoor = value; }
        }

        public virtual int LastEventID
        {
            get { return lastEventID; }
            set { lastEventID = value; }
        }

        public virtual int Event2EmoID
        {
            get { return event2EmoID; }
            set { event2EmoID = value; }
        }

        public virtual int Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public virtual bool ShowCardNo
        {
            get { return showCardNo; }
            set { showCardNo = value; }
        }

        public virtual String Note1
        {
            get { return note1; }
            set { note1 = value; }
        }

        public virtual String Note2
        {
            get { return note2; }
            set { note2 = value; }
        }

        public virtual  String Note3
        {
            get { return note3; }
            set { note3 = value; }
        }

        public virtual String Note4
        {
            get { return note4; }
            set { note4 = value; }
        }

        public virtual String Note5
        {
            get { return note5; }
            set { note5 = value; }
        }

        public virtual DateTime TimeStampx
        {
            get { return timeStampx; }
            set { timeStampx = value; }
        }

        public virtual bool IsBlackCard
        {
            get { return isBlackCard; }
            set { isBlackCard = value; }
        }

        public virtual String AscString
        {
            get { return ascString; }
            set { ascString = value; }
        }

    }
}