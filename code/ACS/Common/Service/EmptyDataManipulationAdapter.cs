using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Service
{
    public class EmptyDataManipulationAdapter:DataManipulationAdapter
    {


        #region DataManipulationAdapter 成员

        bool DataManipulationAdapter.adatptor<T>(T po)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}