using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Common.Util;
using ACS.Controllers.Constant;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service;
using ACS.Test;

namespace ACS.Controllers
{
    public class DeptManageController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DeptService deptService = ServiceContext.getInstance().getDeptService();

        // GET: /DeptManage/
        //加载部门管理页面
        public ActionResult DeptManage()
        {
            return View();
        }
        //加载部门树
        public String LoadTree()
        {
            TreeModel tree = deptService.getDeptTree();
            return tree.ToJsonStr();

        }

        //打开编辑部门窗口
        public ActionResult DeptEdit()
        {
            return View();
        }
        //加载要新增部门信息
        public string ShowAddDept(string fatherDeptID)
        {
            Dept dept = new Dept();
            Dept fatherDept = deptService.getDeptByID(fatherDeptID);
            DeptModel deptModel = ModelConventService.toDeptModel(dept, fatherDept);
            string deptJson = JsonConvert.ObjectToJson(deptModel);
            Response.Write(deptJson);
            return null;

        }
        //加载编辑部门信息
        public string ShowEditDept(string deptId)
        {
            Dept dept = deptService.getDeptByID(deptId);
            Dept fatherDept = deptService.getDeptByID(dept.FatherDeptID);
            DeptModel deptModel = ModelConventService.toDeptModel(dept, fatherDept);
            string deptJson = JsonConvert.ObjectToJson(deptModel);
            Response.Write(deptJson);
            return null;

        }

        //加入部门
        public string AddDept(string data)
        {
            string text = null;
            log.Debug("Create Dept...");
            DeptModel deptModel = JsonConvert.JsonToObject<DeptModel>(data);

            try
            {
                //校验成功
                deptService.addDept(deptModel);
            }
            catch (SystemException ex)
            {
                text = ex.Message;
                Response.Write(text);
                return null;
            }
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;

        }
        //编辑部门
        public string EditDept(string data)
        {
            string text = null;
            log.Debug("Edit Dept...");
            DeptModel deptModel = JsonConvert.JsonToObject<DeptModel>(data);

            try
            {
                //校验成功
                deptService.editDept(deptModel);
            }
            catch (SystemException ex)
            {
                text = ex.Message;
                Response.Write(text);
                return null;
            }
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;

        }
        //删除部门
        public string DeleteDept(string data)
        {
            string text = null;
            log.Debug("Delete Dept...");

            try
            {
                //校验成功
                deptService.deleteDept(data);
            }
            catch (SystemException ex)
            {
                text = ex.Message;
                Response.Write(text);
                return null;
            }
            Response.Write(AjaxConstant.AJAX_SUCCESS);
            return null;

        }
    }
}
