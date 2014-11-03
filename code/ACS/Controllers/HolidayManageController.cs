using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Common.Model;
using ACS.Service;
using ACS.Models.Po.Business;
using System.Web.Script.Serialization;
using ACS.Common.Util;
namespace ACM.Controllers
{
    public class HolidayManageController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        HolidayService holidayService = ServiceContext.getInstance().getHolidayService();
        public ActionResult HolidayManage()
        {
            return View();
        }
         
         public ActionResult Load(TableForm tableForm, Holiday filter)
         {
             log.Debug("Load Holiday Data...");
             //数据库操作：使用查询条件、分页、排序等参数进行查询
             TableDataModel<Holiday> holidayModelTable = new TableDataModel<Holiday>();
             holidayModelTable.setPage(tableForm.getPage());
             holidayModelTable.setDataSource(holidayService.getHolidayList(filter));
          
             log.Debug("pageIndex = " + tableForm.PageIndex + ";pageSize=" + tableForm.PageSize);

             Response.Write(holidayModelTable.getMiniUIJson());
             return null;
         }
        public ActionResult HolidayEdit(String id)
        {
            ViewBag.Type = "EDIT";
            HolidayModel holidayModel = holidayService.getHolidayModelByID(id);
            ViewBag.holiday = holidayModel;
            return View();
        }

        public ActionResult HolidayCreate()
        {
            ViewBag.Type = "CREATE";
            return View("HolidayEdit");
        }
        /// <summary>
        /// 新增员工
        /// 可以改成用Ajax调用的响应
        /// </summary>
        /// <returns></returns>
        public string create(string data)
        {
            string text = null;
            log.Debug("Create Holiday...");
            HolidayModel holidayModel = JsonConvert.JsonToObject<HolidayModel>(data);

            if (ModelVerificationService.HolidayVerification(holidayModel))
            {
                
                try
                {
                    //校验成功
                    holidayService.create(holidayModel);
                }
                catch (SystemException ex)
                {
                    text = ex.Message;
                    Response.Write(text);
                    return null;
                }
                text = "Success";
                Response.Write(text);
                return null;
            }else{
                //校验失败
                //TODO: 
            }
            return null;
        }

        /// <summary>
        /// 修改用户
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string data)
        {
            string text = null;
            log.Debug("Modify Holiday...");
            HolidayModel holidayModel = JsonConvert.JsonToObject<HolidayModel>(data);

            if (ModelVerificationService.HolidayVerification(holidayModel))
            {
                try
                {
                    //校验成功
                    holidayService.update(holidayModel);
                }
                catch (SystemException ex)
                {
                    text = ex.Message;
                    Response.Write(text);
                    return null;
                }
                text = "Success";
                Response.Write(text);
                return null;

            }
            else
            {
                //校验失败
                //TODO: 
            }
            return null;
        }

        /// <summary>
        /// 删除员工
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult Remove(String idstr)
        {
            List<int> idList = ModelConventService.toIDList(idstr);
            log.Debug("Delete Holiday (id=" + idList + ") ...");
            if (ModelVerificationService.HolidayIDExist(idList))
            {
                //校验成功
                holidayService.delete(idList);
            }
            else
            {
                //校验失败
                //TODO: 
            }
            Response.Write("ok");
            return null;
        }
    }
}
