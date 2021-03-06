﻿using ACS.Common;
using ACS.Common.Dao;
using ACS.Common.Model;
using ACS.Common.Util;
using ACS.Service;
using ACS.Service.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACS.Controllers
{
    public abstract class MiniUITableController<E> : BaseController
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const String  CREATE = "create";
        public const String  MODIFY = "modify";
        public const String  DELETE = "delete";


        /**
         * 该方法绑定了MiniUI 分页数据提交
         */
        public PageModel getPage()
        {

            int currentPage = Convert.ToInt32(Request.Form["pageIndex"]) + 1;
            int pageSize = Convert.ToInt32(Request.Form["pageSize"]);
            PageModel page = new PageModel();
      
            page.setCurrentPage(currentPage);
            page.setPageSize(pageSize);

            return page;
        }

         
        /**
         * 该方法绑定了MiniUI 表格选择id 数据提交
         */
        public List<String> getIDList(String str)
        {
            List<String> list = new List<String>();
            foreach (String i in str.Split(','))
            {
                list.Add(i);
            }
            return list;

        }

        public virtual List<QueryCondition> GetFilterCondition(String json)
        {
            return null;
        }
        //展示IndexPage页面
        public virtual ActionResult IndexPage()
        {
            return View();
        }
        //展示编辑页面
        public virtual ActionResult ShowPage()
        {
            return View();
        }
        public virtual ActionResult Load(String data)
        {

            TableDataModel<E> table = new TableDataModel<E>();
            table.setPage(getPage());
            table.setDataSource(getService().GetDataSource(GetFilterCondition(data)));
            return ReturnJson(table.getMiniUIJson());
        }

        public ActionResult LoadTable(List<QueryCondition> conditionList)
        {

            TableDataModel<E> table = new TableDataModel<E>();
            table.setPage(getPage());
            table.setDataSource(getService().GetDataSource(conditionList));
            return ReturnJson(table.getMiniUIJson());
        }

		protected ActionResult LoadTable<T>(List<QueryCondition> conditionList)
        {

            TableDataModel<T> table = new TableDataModel<T>();
            table.setPage(getPage());
            table.setDataSource(getService().GetDataSource<T>(conditionList));
            return ReturnJson(table.getMiniUIJson());
        }

        public virtual ActionResult LoadTree()
        {
            List<E> tree = this.getService().GetDataSource().getAllPageData();
            return ReturnJson(tree);
        }
 
        public virtual ActionResult Show(String data)
        {
            try
            {
                if (ValidatorUtil.isEmpty(data))
                {
                    Rsp.Obj = System.Activator.CreateInstance<E>();
                    log.Info("the id is empty,return a defualt object");
                }
                else 
                {
                    E e = getService().Get(data);
                    Rsp.Obj = e;
                }
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

        protected virtual ActionResult Show<T>(String key,String value)
        {
            try
            {
                if (ValidatorUtil.isEmpty(key)||ValidatorUtil.isEmpty(value))
                {
                    Rsp.Obj = System.Activator.CreateInstance<T>();
                    log.Info("the key or value is empty,return a defualt object");
                }
                else
                {
                    T e = getService().Get<T>(key, value);
                    if (null == e)
                    {
                        log.Error("can not find the object by key is " + key + "value is " + value);
                    }
                    Rsp.Obj = e;
                }

                
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }
        public virtual ActionResult Create(String data)
        {
            log.Debug("Creating OBJ ... " + data);
        
            try
            {   
                E obj = JsonConvert.JsonToObject<E>(data);
                getService().Create(this.getSessionUser().UserID,obj);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

        public virtual ActionResult Modify(String data)
        {
            log.Debug("Modify OBJ ... " + data); 
            try
            {
                E obj = JsonConvert.JsonToObject<E>(data);
                getService().Modify(this.getSessionUser().UserID, obj);
            }
            catch (FuegoException e)
            {
                log.Error("Modify failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("Modify failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

        public virtual ActionResult Delete(String data)
        {
            log.Debug("Deleting ObjList, ObjIDList is " + data);
            try
            {
                List<String> idList = JsonConvert.JsonToObject<List<String>>(data);
                getService().Delete(this.getSessionUser().UserID,idList);
            }
            catch (FuegoException e)
            {
                log.Error("delete failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("delete failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }
           // Response.Write(JsonConvert.ObjectToJson(Rsp));

            return ReturnJson(Rsp);
        }

        public abstract CommonService<E> getService();
    }
}
