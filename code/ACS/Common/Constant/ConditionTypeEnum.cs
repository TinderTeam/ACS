using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Constant
{
    public enum ConditionTypeEnum
    {
        INCLUDLE,
	    EXCLUDLE, 
	    EQUAL,
	    BIGER, 
	    BIGER_EQ,
	    LOWER, 
	    LOWER_EQ,  
	    BETWEEN,
	    IN,
	    DESC_ORDER,
        ASC_ORDER,
    }
}