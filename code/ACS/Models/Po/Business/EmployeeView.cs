using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class EmployeeView
    {
        public const String ID = "EmployeeID";
        public const String Card = "CardNo";
        public const String EMPLOYEENAME = "EmployeeName";
        public const String DEPTID = "DeptID";
        public const String JOBID = "JobID";
        public const String EMPLOYEECODE = "EmployeeCode";
        //从Employee表中获得
        private int employeeID;                         //员工ID
        private String employeeName;                    //员工名称
        private String employeeCode;                    //工号
        private String cardNo;                          //卡号
        private bool empEnable;                       //是否注销
        private bool sex;                             //性别
        private String personCode;                      //
        private String phone;                           //
        private int jobID;                              //
        private int deptID;                             //
        private String photo1;                           //
        private String photo2;
        private DateTime endDate = DateTime.Now;        //有效期
        private bool leave;                           //离职
        private int lastEventID;                        //最后一次刷卡
        public virtual int AccessID { get; set; }               //权限ID

        //从Dept表中获得
        private String deptName;

        //从Job表中活动
        private String jobName;

        //从AccessDetail中获得
        public virtual String AccessName { get; set; }          //权限名称

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

        //public virtual String EnglishName
        //{
        //    get { return englishName; }
        //    set { englishName = value; }
        //}

        public virtual String CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        //public virtual String Pin
        //{
        //    get { return pin; }
        //    set { pin = value; }
        //}

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

        //public virtual DateTime Birthday
        //{
        //    get { return birthday; }
        //    set { birthday = value; }
        //}

        public virtual String PersonCode
        {
            get { return personCode; }
            set { personCode = value; }
        }

        //public virtual String Home
        //{
        //    get { return home; }
        //    set { home = value; }
        //}

        public virtual String Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        //public virtual String Email
        //{
        //    get { return email; }
        //    set { email = value; }
        //}

        //public virtual String Car
        //{
        //    get { return car; }
        //    set { car = value; }
        //}

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
        //public virtual DateTime RegDate
        //{
        //    get { return regDate; }
        //    set { regDate = value; }
        //}

        public virtual DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        //public virtual String Deleted
        //{
        //    get { return deleted; }
        //    set { deleted = value; }
        //}

        public virtual bool Leave
        {
            get { return leave; }
            set { leave = value; }
        }

        //public virtual DateTime LeaveDate
        //{
        //    get { return leaveDate; }
        //    set { leaveDate = value; }
        //}
        //public virtual String BeKQ
        //{
        //    get { return beKQ; }
        //    set { beKQ = value; }
        //}
        //public virtual String Pswd
        //{
        //    get { return pswd; }
        //    set { pswd = value; }
        //}
        //public virtual int MapID
        //{
        //    get { return mapID; }
        //    set { mapID = value; }
        //}
        //public virtual int XPoint
        //{
        //    get { return xPoint; }
        //    set { xPoint = value; }
        //}
        //public virtual int YPoint
        //{
        //    get { return yPoint; }
        //    set { yPoint = value; }
        //}

        //public virtual String MapVisible
        //{
        //    get { return mapVisible; }
        //    set { mapVisible = value; }
        //}

        //public virtual int OwnerDoor
        //{
        //    get { return ownerDoor; }
        //    set { ownerDoor = value; }
        //}

        public virtual int LastEventID
        {
            get { return lastEventID; }
            set { lastEventID = value; }
        }

        //public virtual int Event2EmoID
        //{
        //    get { return event2EmoID; }
        //    set { event2EmoID = value; }
        //}

        //public virtual int Status
        //{
        //    get { return status; }
        //    set { status = value; }
        //}

        //public virtual DateTime TimeStamp
        //{
        //    get { return timeStamp; }
        //    set { timeStamp = value; }
        //}

        //public virtual String ShowCardNo
        //{
        //    get { return showCardNo; }
        //    set { showCardNo = value; }
        //}

        //public virtual String Note1
        //{
        //    get { return note1; }
        //    set { note1 = value; }
        //}

        //public virtual String Note2
        //{
        //    get { return note2; }
        //    set { note2 = value; }
        //}

        //public virtual  String Note3
        //{
        //    get { return note3; }
        //    set { note3 = value; }
        //}

        //public virtual String Note4
        //{
        //    get { return note4; }
        //    set { note4 = value; }
        //}

        //public virtual String Note5
        //{
        //    get { return note5; }
        //    set { note5 = value; }
        //}

        //public virtual DateTime TimeStampx
        //{
        //    get { return timeStampx; }
        //    set { timeStampx = value; }
        //}

        //public virtual String IsBlackCard
        //{
        //    get { return isBlackCard; }
        //    set { isBlackCard = value; }
        //}

        //public virtual String AscString
        //{
        //    get { return ascString; }
        //    set { ascString = value; }
        //}

        public virtual String DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        public virtual String JobName
        {
            get { return jobName; }
            set { jobName = value; }
        }

    }
}