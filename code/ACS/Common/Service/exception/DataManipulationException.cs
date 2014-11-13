using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Service.exception
{
    public class DataManipulationException:SystemException
    {
        public const String VerificationExceptionMsg="Verification Error";
        public const String AddExceptionMsg = "Add Error";
        public const String deleteExceptionMsg = "Delete Error";
        public const String updateExceptionMsg = "Update Error";
    }
}