using ACS.Common.Dao.impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using ACS.Common.Dao;
using ACS.Models.Po;

namespace UnitTestProject
{
    
    
    /// <summary>
    ///这是 ViewDaoCommonImplTest 的测试类，旨在
    ///包含所有 ViewDaoCommonImplTest 单元测试
    ///</summary>
    [TestClass()]
    public class ViewDaoCommonImplTest
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private TestContext testContextInstance;

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
        ///ViewDaoCommonImpl`1 构造函数 的测试
        ///</summary>
        public void ViewDaoCommonImplConstructorTestHelper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void ViewDaoCommonImplConstructorTest()
        {
            ViewDaoCommonImplConstructorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///getAll 的测试
        ///</summary>
        public void getAllTestHelper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>(); // TODO: 初始化为适当的值
            List<E> expected = null; // TODO: 初始化为适当的值
            List<E> actual;
            actual = target.getAll();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void getAllTest()
        {
            ViewDao<User> target = new ViewDaoCommonImpl<User>(); // TODO: 初始化为适当的值
            List<User> actual;
            actual = target.getAll();
            log.Error(actual);

        }

        /// <summary>
        ///getAll 的测试
        ///</summary>
        public void getAllTest1Helper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>(); // TODO: 初始化为适当的值
            List<QueryCondition> conditionList = null; // TODO: 初始化为适当的值
            List<E> expected = null; // TODO: 初始化为适当的值
            List<E> actual;
            actual = target.getAll(conditionList);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void getAllTest1()
        {
            getAllTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///getAll 的测试
        ///</summary>
        public void getAllTest2Helper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>(); // TODO: 初始化为适当的值
            List<QueryCondition> conditionList = null; // TODO: 初始化为适当的值
            int startNum = 0; // TODO: 初始化为适当的值
            int pageSize = 0; // TODO: 初始化为适当的值
            List<E> expected = null; // TODO: 初始化为适当的值
            List<E> actual;
            actual = target.getAll(conditionList, startNum, pageSize);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void getAllTest2()
        {
            getAllTest2Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///getAll 的测试
        ///</summary>
        public void getAllTest3Helper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>(); // TODO: 初始化为适当的值
            QueryCondition condition = null; // TODO: 初始化为适当的值
            List<E> expected = null; // TODO: 初始化为适当的值
            List<E> actual;
            actual = target.getAll(condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void getAllTest3()
        {
            getAllTest3Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///getCount 的测试
        ///</summary>
        public void getCountTestHelper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>(); // TODO: 初始化为适当的值
            List<QueryCondition> conditionList = null; // TODO: 初始化为适当的值
            long expected = 0; // TODO: 初始化为适当的值
            long actual;
            actual = target.getCount(conditionList);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void getCountTest()
        {
            getCountTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///getFeaturedClass 的测试
        ///</summary>
        public void getFeaturedClassTestHelper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>(); // TODO: 初始化为适当的值
            Type expected = null; // TODO: 初始化为适当的值
            Type actual;
            actual = target.getFeaturedClass();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void getFeaturedClassTest()
        {
            getFeaturedClassTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///getUniRecord 的测试
        ///</summary>
        public void getUniRecordTestHelper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>(); // TODO: 初始化为适当的值
            QueryCondition condition = null; // TODO: 初始化为适当的值
            E expected = default(E); // TODO: 初始化为适当的值
            E actual;
            actual = target.getUniRecord(condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void getUniRecordTest()
        {
            getUniRecordTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///getUniRecord 的测试
        ///</summary>
        public void getUniRecordTest1Helper<E>()
        {
            ViewDaoCommonImpl<E> target = new ViewDaoCommonImpl<E>(); // TODO: 初始化为适当的值
            List<QueryCondition> conditionList = null; // TODO: 初始化为适当的值
            E expected = default(E); // TODO: 初始化为适当的值
            E actual;
            actual = target.getUniRecord(conditionList);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void getUniRecordTest1()
        {
            getUniRecordTest1Helper<GenericParameterHelper>();
        }
    }
}
