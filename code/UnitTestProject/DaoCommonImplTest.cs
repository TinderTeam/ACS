using ACS.Common.Dao.impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ACS.Common.Dao;
using ACS.Models.Po;

namespace UnitTestProject
{
    
    
    /// <summary>
    ///这是 DaoCommonImplTest 的测试类，旨在
    ///包含所有 DaoCommonImplTest 单元测试
    ///</summary>
    [TestClass()]
    public class DaoCommonImplTest
    {


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
        ///DaoCommonImpl`1 构造函数 的测试
        ///</summary>
        public void DaoCommonImplConstructorTestHelper<E>()
        {
            DaoCommonImpl<E> target = new DaoCommonImpl<E>();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void DaoCommonImplConstructorTest()
        {
            DaoCommonImplConstructorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///create 的测试
        ///</summary>
        public void createTestHelper<E>()
        {
            Dao<E> target = new DaoCommonImpl<E>(); // TODO: 初始化为适当的值
            E obj = default(E); // TODO: 初始化为适当的值
            target.create(obj);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void createTest()
        {
            Dao<User> target = new DaoCommonImpl<User>(); // TODO: 初始化为适当的值
            User user = new User();
            user.Pswd = "1234";
            user.UserName = "admin";
            target.create(user);
        }

        /// <summary>
        ///delete 的测试
        ///</summary>
        public void deleteTestHelper<E>()
        {
            DaoCommonImpl<E> target = new DaoCommonImpl<E>(); // TODO: 初始化为适当的值
            E obj = default(E); // TODO: 初始化为适当的值
            target.delete(obj);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void deleteTest()
        {
            deleteTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///delete 的测试
        ///</summary>
        public void deleteTest1Helper<E>()
        {
            DaoCommonImpl<E> target = new DaoCommonImpl<E>(); // TODO: 初始化为适当的值
            QueryCondition condition = null; // TODO: 初始化为适当的值
            target.delete(condition);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void deleteTest1()
        {
            deleteTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///update 的测试
        ///</summary>
        public void updateTestHelper<E>()
        {
            DaoCommonImpl<E> target = new DaoCommonImpl<E>(); // TODO: 初始化为适当的值
            E obj = default(E); // TODO: 初始化为适当的值
            target.update(obj);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\VSProject\\ACS\\code\\ACS", "/")]
        [UrlToTest("http://localhost:7440/")]
        public void updateTest()
        {
            updateTestHelper<GenericParameterHelper>();
        }
    }
}
