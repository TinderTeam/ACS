using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;
using ACS.Common.Dao;
using ACS.Common.Dao.impl;
using ACS.Common.Service.exception;
using ACS.Models.Po.CF;

namespace ACS.Common.Service.impl
{
    public class DataManipulationServiceImpl<T>:DataManipulationService<T>
    {
        DataManipulationService<User> service = new DataManipulationServiceImpl<User>();
        private DataManipulationAdapter addAdapter;

        CommonDao<T> dao = new CommonDaoImpl<T>();

        DataManipulationServiceImpl()
        {
            addAdapter = new EmptyDataManipulationAdapter();
        }
        public void addAddAdaptor(DataManipulationAdapter a)
        {
            addAdapter = a;
        }


        public void add(T po)
        {
            if (addAdapter.adatptor<T>(po))
            {
                try
                {

                    dao.create(po);
                }
                catch (Exception ex)
                {                    
                    throw new Exception(DataManipulationException.AddExceptionMsg);
                }
            }
            else
            {
                throw new Exception(DataManipulationException.VerificationExceptionMsg);
            }
        }

        public void delete(String attr,String value)
        {         
            try
            {
                dao.delete(new QueryCondition(
                    ConditionTypeEnum.EQUAL,
                    attr,
                    value
                    ));
            }
            catch (Exception ex)
            {
                throw new Exception(DataManipulationException.deleteExceptionMsg);
            } 
         
        }

        public void modify(T obj)
        {
            try
            {
                dao.update(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(DataManipulationException.updateExceptionMsg);
            } 
        }
    }
}