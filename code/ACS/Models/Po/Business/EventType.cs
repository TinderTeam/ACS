using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class EventType
    {
        public const String ID = "EventTypeID";
        public virtual int EventTypeID { get; set; }                        //事件ID
        public virtual String EventTypeName { get; set; }                   //事件名称
        public virtual int Level { get; set; }                          //级别
        public virtual bool Visible { get; set; }                       //是否显示
        public virtual bool Affirm { get; set; }                        //是否需要确认
        public virtual bool Alarm { get; set; }                         //是否报警
        public virtual String ForegroundColor { get; set; }                //前景色
        public virtual String BackgroundColor { get; set; }                //背景色
        public virtual int Event0 { get; set; }                         //
        public virtual int Event1 { get; set; }                         //
        public virtual int Event2 { get; set; }                         //
        public virtual int Event3 { get; set; }                         //
        public virtual int Event4 { get; set; }                         //
        public virtual int Event5 { get; set; }                         //
    }
}