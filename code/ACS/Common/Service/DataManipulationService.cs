using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Service
{
    /// <summary>
    /// 数据操作服务
    /// 为数据提供相应的增删改查功能
    /// </summary>
    public interface DataManipulationService<T>
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="MODEL"></typeparam>
        /// <param name="model"></param>
        void add(T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="MODEL"></typeparam>
        /// <param name="filter"></param>
        void delete(String attr, String value);

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="MODEL"></typeparam>
        /// <param name="filter"></param>
        void modify(T filter);
    }
}