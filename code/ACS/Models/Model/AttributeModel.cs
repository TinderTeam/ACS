using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class AttributeModel
    {
        public AttributeModel()
        {
             
        }
        public AttributeModel(String key,String value)
        {
            this.AttrKey = key;
            this.AttrValue = value;
        }
        public String AttrKey { get; set; }              
        public String AttrValue { get; set; }            
    }
}