using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TcpipIntface;

namespace ACS.Service
{
    public class DeviceControlThread
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string ip;
        private int port;   
        private TcpipClass connector;

        public DeviceControlThread(TcpipClass connector,String ip,int port)
        {
            log.Debug("DeviceControlThread Init: ip=" + ip+";port="+port);
            this.connector = connector;
            this.Ip = ip;
            this.Port = port;
        }

        public void connect()
        {
            
           
        }



        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
    }
}