using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
namespace ACS.Util.Json
{
    public class JsonService
    {
        public static String ToJson(Object obj)
        {
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            String json = jsonSerialize.Serialize(obj);
            return json;
        }
       
    }
}