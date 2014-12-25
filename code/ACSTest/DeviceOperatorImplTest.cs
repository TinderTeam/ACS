using ACS.Service.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ACS.Models.Po.Business;
using ACS.Service.device;
using System.Threading;
using ACS.Dao;
using ACS.Common.Dao;
using ACS.Common.Constant;

namespace ACSTest
{
    
    
    /// <summary>
    ///这是 DeviceOperatorImplTest 的测试类，旨在
    ///包含所有 DeviceOperatorImplTest 单元测试
    ///</summary>
    [TestClass()]
    public class DeviceOperatorImplTest
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TestContext testContextInstance;
        DeviceOperator target;

        public void before()
        {
            log.Info("初始化测试信息");
            Control control = new Control(); // TODO: 初始化为适当的值
            control.ControlID = 24;
            control.Ip = "192.168.0.149";
            target = ACS.Service.device.DeviceOperatorFactory.getInstance().getDeviceOperator(control);
        }
        
        
        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
             

                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///DelHoliday 的测试
        ///</summary>
        // TODO: 确保 UrlToTest 特性指定一个指向 ASP.NET 页的 URL(例如，
        // http://.../Default.aspx)。这对于在 Web 服务器上执行单元测试是必需的，
        //无论要测试页、Web 服务还是 WCF 服务都是如此。
        [TestMethod()]
        public void DelHolidayTest()
        {
            before();
            log.Info("测试开始");
            Thread.Sleep(5 * 1000);
            log.Info("运行清除假日");
            target.DelHoliday();
            log.Info("测试完毕");
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void DelTimeZone()
        {
            before();
            log.Info("测试开始");
            Thread.Sleep(5 * 1000);
            log.Info("运行清除时间");
            for (int i = 0; i < 16; i++)
            {
                target.DelTimeZone(i);
            }
              
            log.Info("测试完毕");
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SetDoorTime()
        {
            before();
            log.Info("测试开始");
            Thread.Sleep(5 * 1000);
            log.Info("运行设置时间");
            DoorTimeView doortime = new DoorTimeView();
            doortime.DoorNum = 0;
            doortime.DoorTimeNum = 0;
            doortime.EndTime = "2014-01-01 12:00:00";
            doortime.StartTime = "2014-01-01 8:00:00";
            doortime.LimitDate = DateTime.Now;
            doortime.Holiday = true;
            doortime.Identify = 1;
            doortime.Friday = true;
            doortime.Monday = true;
            doortime.Thursday = true;
            doortime.Tuesday = true;
            doortime.Wednesday = true;
            doortime.Saturday= true;



            target.SetDoorTime(doortime);

            log.Info("测试完毕");
            Assert.IsTrue(true);
        }
    }
}
