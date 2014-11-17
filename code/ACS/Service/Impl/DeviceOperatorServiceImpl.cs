using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccessTcp;

namespace ACS.Service.Impl
{
    public class DeviceOperatorServiceImpl : DeviceOperatorService
    {
        Dictionary<string, TCPAcs> tcpMap = new Dictionary<string, TCPAcs>();
        /// <summary>
        /// 事物处理方法
        /// </summary>
        /// <param name="EType"></param>
        /// <param name="second"></param>
        /// <param name="minute"></param>
        /// <param name="hour"></param>
        /// <param name="day"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <param name="DoorStatus"></param>
        /// <param name="Ver"></param>
        /// <param name="FuntionByte"></param>
        /// <param name="Online"></param>
        /// <param name="CardsofPackage"></param>
        /// <param name="CardNo"></param>
        /// <param name="Door"></param>
        /// <param name="EventType"></param>
        /// <param name="CardIndex"></param>
        /// <param name="CardStatus"></param>
        /// <param name="Reader"></param>
        /// <param name="Ack"></param>
        public void OnEvent(byte EType, byte second, byte minute, byte hour, byte day, byte Month, int Year,
              byte DoorStatus, byte Ver, byte FuntionByte,
            bool Online, byte CardsofPackage, long CardNo, byte Door, byte EventType, int CardIndex,
              byte CardStatus, byte Reader, out bool Ack)
        {
            Ack = true;
            switch (EType)
            {
                case 0: break;
                case 1: break;
                case 2: break;
                case 3: break;
            }

        }

        
        public TCPAcs getControllerTCP(string ip)
        {

            TCPAcs acsTcp;
            acsTcp = tcpMap[ip];
            if (acsTcp == null)
            {
                return acsTcp;
            }
            else
            {
                acsTcp = new TCPAcs();
                acsTcp.OnEvent += new ITCPAcsEvents_OnEventEventHandler(OnEvent);
            }
            return acsTcp;
        }      
    }
}