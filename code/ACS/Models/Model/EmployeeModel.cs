using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class EmployeeModel
    {
        private int employeeID;//员工ID
        private String employeeName;//员工名称
        private String employeeCode;//员工工号
        private String englishName;//英文名称
        private String sex;//性别
        private DateTime birthday;//生日
        private String personCode;//身份证号
        private String home;//家庭地址
        private String phone;//电话
        private String email;//邮箱
        private String car;//车牌号
        private int jobID;//职位
        private int deptID;//部门
        private String photo;//照片
        private DateTime regDate;//注册日期
        private DateTime endDate;//有效期
        private DateTime leaveDate = DateTime.Now;//离职时间
        private DateTime timeStamp = DateTime.Now;//修改时间
        private DateTime timeStampx = DateTime.Now;//修改时间
        private String note1;//
        private String note2;//
        private String note3;//
        private String note4;//

        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        
        public String EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }
        
        public String EmployeeCode
        {
            get { return employeeCode; }
            set { employeeCode = value; }
        }
        
        public String EnglishName
        {
            get { return englishName; }
            set { englishName = value; }
        }
        
        public String Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        
        public String PersonCode
        {
            get { return personCode; }
            set { personCode = value; }
        }
        
        public String Home
        {
            get { return home; }
            set { home = value; }
        }
        
        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        
        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        
        public String Car
        {
            get { return car; }
            set { car = value; }
        }
        
        public int JobID
        {
            get { return jobID; }
            set { jobID = value; }
        }
        
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        
        public String Photo
        {
            get { return photo; }
            set { photo = value; }
        }
        
        public DateTime RegDate
        {
            get { return regDate; }
            set { regDate = value; }
        }
        
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        
        public DateTime LeaveDate
        {
            get { return leaveDate; }
            set { leaveDate = value; }
        }
        
        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public DateTime TimeStampx
        {
            get { return timeStampx; }
            set { timeStampx = value; }
        }

        public String Note1
        {
            get { return note1; }
            set { note1 = value; }
        }
        
        public String Note2
        {
            get { return note2; }
            set { note2 = value; }
        }
        
        public String Note3
        {
            get { return note3; }
            set { note3 = value; }
        }
        
        public String Note4
        {
            get { return note4; }
            set { note4 = value; }
        }
    }
}