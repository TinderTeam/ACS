using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
namespace ACS.Common.Util
{
    public class JsonConvert
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
        // 从一个对象信息生成Json串
        public static string ObjectToJson(object obj)
        {
            return jsonSerialize.Serialize(obj);
        }
        // 从一个Json串生成对象信息
        public static E JsonToObject<E>(string jsonString)
        {
            E obj = default(E);
            if (ValidatorUtil.isEmpty(jsonString))
            {
                return default(E);
            }
            try
            {
                obj = (E)jsonSerialize.Deserialize(jsonString, typeof(E));
            }
            catch (Exception e)
            {
                log.Error("json convert failed", e);
            }
            return obj;
        }
    }
}