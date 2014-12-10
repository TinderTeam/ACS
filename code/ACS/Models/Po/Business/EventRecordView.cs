using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class EventRecordView
    {
        public const String ID = "EventID";
        public const String DEPTNAME = "DeptName";
        public const String JOBNAME = "JobName";
        public const String DOORNAME = "DoorName";
        public const String EVNETTIME = "EventTime";
        public const String CARDNO = "CardNo";
        public const String EMPLOYEENAME = "EmployeeName";
        public const String EMPLOYEECODE = "EmployeeCode";
        public const String EVENTTYPE = "EventType";

        private int eventID;                        //事件ID
        private DateTime eventTime;                 //事件时间
        private String cardNo;                      //员工ID
        private int controlID;                      //控制器ID
        private int doorID;                         //门ID
        private int eventTypeID;                      //事件类型
        private int modify;                         //是否修改过

        private String doorName;

        private String controlName;

        private String employeeCode;//工号
        private String employeeName;//员工名称
        private String englishName;//英文名称
        private bool empEnable;//是否注销
        private bool sex;//
        private String jobName;//
        private String deptName;//
        private bool leave;//离职
        private DateTime leaveDate;//
        private bool beKQ;//是否参与考勤
        private DateTime regDate;//注册日期
        private DateTime birthday;//
        private String personCode;//
        private String home;//
        private String phone;//
        private String email;//
        private String car;//
        private String note1;//
        private String note2;//
        private String note3;//
        private String note4;//

        public virtual int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }
        public virtual String CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        public virtual DateTime EventTime
        {
            get { return eventTime; }
            set { eventTime = value; }
        }
        public virtual int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }
        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }
        public virtual int EventTypeID
        {
            get { return eventTypeID; }
            set { eventTypeID = value; }
        }
        public virtual int Modify
        {
            get { return modify; }
            set { modify = value; }
        }


        public virtual String DoorName
        {
            get { return doorName; }
            set { doorName = value; }
        }

        public virtual String ControlName
        {
            get { return controlName; }
            set { controlName = value; }
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

        public virtual String JobName
        {
            get { return jobName; }
            set { jobName = value; }
        }

        public virtual String DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        public virtual DateTime RegDate
        {
            get { return regDate; }
            set { regDate = value; }
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

        public virtual String Note3
        {
            get { return note3; }
            set { note3 = value; }
        }

        public virtual String Note4
        {
            get { return note4; }
            set { note4 = value; }
        }

    }
}