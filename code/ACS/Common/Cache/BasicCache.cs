using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Cache
{
    public abstract class BasicCache<T>
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<String, T> cache = new Dictionary<string, T>();
        #region 说明
        /*
         * 抽象方法：getKey(T t)的作用是向缓存提供一个确定key值的接口，这个方法在子类中实现。
         * 抽象方法：List<T> initCache()的作用是向缓存提供一个初始化数据接口。
        */
        #endregion

        #region 抽象方法定义
        public abstract String getKey(T t);
            public abstract List<T> initCache();
        #endregion

        #region 构造方法
            /// <summary>
            /// 从抽象初始化方法获得缓存初值
            /// </summary>

            public BasicCache()
            {
                load();
            }

            private void load()
            {
                foreach (T t in initCache())
                {
                    cache.Add(getKey(t), t);
                }
            }
        #endregion
       

        /// <summary>
        /// 重新加载所有缓存
        /// </summary>
        public void reload()
        {
            load();
        }

        /// <summary>
        /// 获取一个缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T getValue(String key)
        {
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            return default(T);
        }

        /// <summary>
        /// 更新一个缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void updateValue(String key, T value)
        {
            if (cache.ContainsKey(key))
            {
                cache[key] = value;
            }
            else
            {
                cache.Add(key, value);
            }
        }

        /// <summary>
        /// 获取全部缓存
        /// </summary>
        /// <returns></returns>
        public List<T> getAll()
        {
            return new List<T>(cache.Values);
        }
    }
}