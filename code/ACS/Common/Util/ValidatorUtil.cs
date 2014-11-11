﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Util
{
    public class ValidatorUtil
    {

        public static bool isEmpty<E>(List<E> objList)
        {
            if (null == objList || objList.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}