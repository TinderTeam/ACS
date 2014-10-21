using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service.Impl;
namespace ACS.Service
{
    public class ServiceContext
    {
        static ServiceContext serviceContext;
        PlatFormService platFormService;

        public static ServiceContext getInstance()
        {
            if (serviceContext == null)
            {
                serviceContext = new ServiceContext();
            }
            return serviceContext;
        }

        public PlatFormService getPlatFormService()
        {

            if (platFormService == null)
            {
                platFormService = new PlatFormServiceImpl();
            }
            return platFormService;
        }
    }
}