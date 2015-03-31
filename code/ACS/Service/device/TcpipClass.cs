using System; 
using System.Linq; 
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO; 
using System.Net;
using System.Net.Sockets ;
using System.Xml;
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;

namespace TcpipIntface
{ 
     public class TcpipBClass
    {
        #region constdata
        public const byte Loc_Begin = 0;
        public const byte Loc_Temp = 1;
        public const byte Loc_Command = 2;
        public const byte Loc_Address = 3;
        public const byte Loc_DoorAddr = 4;
        public const byte Loc_Len = 5;
        public const byte Loc_Data = 7;

        public const byte TcpErr_OK = 0;
        public const byte TcpErr_NotExists = 1; // 对象不存在
        public const byte  TcpErr_DataErr = 2; // 数据超出边界
        public const byte  TcpErr_OutTime = 3; // 操作超时
        public const byte  TcpErr_UnLink = 4; //
        public const byte TcpErr_ReData = 5; // 返回数据错误
        public const byte TcpErr_Working = 6; //
        public const byte  TcpErr_Unknow = 7; //
        #endregion
        public int OEMCode = 23456;
        public byte TCPLastError = 0;

        #region 内部变量
        private Socket sock;
        private IPEndPoint iep;   
        private System.Timers.Timer timer;  
       // private ManualResetEvent TimeoutObject = new ManualResetEvent(false); 
       // private object lockObj_IsConnectSuccess = new object();

        private string remoteHost = "192.168.0.71";
        private int remotePort = 8000;
        private byte isHeartTime = 0;
        private byte[] BufferRX = new byte[512];
        private byte[] BufferTX = new byte[512]; 
        private byte nBytesWrite = 0; 
        private byte LastCmd;
        private Boolean  Busy;
        private volatile Boolean FisWaiting;        
        private String SockErrorStr = null;
        protected byte Ver, Fun, CardNumInPack, DoorStatus;
        protected Boolean isEndDate, isOrPIN, isShowName, isCardorPIN;
        #endregion

        public Boolean IsconnectSuccess = false; //异步连接情况，由异步连接回调函数置位
                
        #region 委托事件声明 
        public delegate void delSocketDisconnected();
        public event delSocketDisconnected socketDisconnected ;
        public delegate void  RxDataHandler(string buffRX);   //声明委托
        public event RxDataHandler RxDataEvent;        //声明事件
        public delegate void  TOnEventHandler(byte EType,byte Second,byte Minute,byte Hour,byte Day,byte Month,int Year,byte DoorStatus,
            byte Ver,byte FuntionByte,Boolean Online, byte CardsofPackage, UInt64 CardNo,  byte Door,  byte EventType,
            UInt16 CardIndex, byte CardStatus,byte reader,out Boolean OpenDoor, out Boolean Ack);   //声明委托 
        public event TOnEventHandler  OnEventHandler ;        //声明事件
        #endregion

        public TcpipBClass()
        { 
            socketDisconnected = socketDisconnectedHandler;
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(timer_Tick);
            timer.Interval = 1000;
            timer.Enabled = false;
        }

        #region tcp数据组包处理
        private Boolean CheckRxDataCS(byte[] buffRX, int len){
            int i;
            if (len < 4) return false;

            int L = 0;
            if(buffRX[0]!=0x02)
            for (i = 0; i < 4; i++)
            {
                if (buffRX[i] == 0x02) { L = i; break; }
            }
            if (L > 0)
            {
                for (i = 0; i < len; i++)
                {
                    buffRX[i] =  buffRX[i+L];
                }
                len = len - L;
            }
           
            int  Bufferlen  = buffRX[Loc_Len + 1] + Loc_Data + 2 ;
            if (Bufferlen > 512) return false;

            if (buffRX[Bufferlen - 1] != 0x03) return false;  
            
            Boolean result  = false;
            
            byte cs = 0;
            for(i = 0 ;i< Bufferlen - 2 ;i++){
                cs = Convert.ToByte(cs ^ buffRX[i]);
            }

            result  = (cs == buffRX[Bufferlen - 2]);  
            return  result;
        }

        protected void SendDataEnd(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);  
        }

        protected Boolean DoSendData()
        {
            int i,datalen, WriteNum;
            byte OutBufferCS, cmd;  
            try
            {
                datalen = nBytesWrite - Loc_Data;
                BufferTX[Loc_Len] = Convert.ToByte(datalen & 0xFF);
                BufferTX[Loc_Len + 1] = Convert.ToByte((datalen >> 8) & 0xFF);
                BufferTX[Loc_Temp] = Convert.ToByte(OEMCode & 0xff);

                OutBufferCS = 0;
                for (i = 0; i < nBytesWrite; i++)
                    OutBufferCS = Convert.ToByte(OutBufferCS ^ BufferTX[i]);

                BufferTX[nBytesWrite] = OutBufferCS;
                BufferTX[nBytesWrite + 1] = 0x03; 
                WriteNum = nBytesWrite + 2;

                if (IsconnectSuccess)
                {
                    cmd = BufferTX[Loc_Command];
                    LastCmd = BufferTX[Loc_Command]; 
                    sock.BeginSend(BufferTX, 0,WriteNum , SocketFlags.None,new AsyncCallback(SendDataEnd),  sock); 
                    return true;
                }
                TCPLastError = TcpErr_UnLink;
                return false; 
            }
            catch (Exception ex)
            { 
                TCPLastError = TcpErr_Unknow;
                SockErrorStr = ex.ToString(); 
                return false;
            }
        }

        private Boolean  WaitReturn(int delay)
        {
            Boolean te,result;
            int t1=0;            
            while (FisWaiting) 
            {
                Thread.Sleep(2);     
                t1++;
                te = t1 >(200 + delay);
                if(te) {
                    break; 
                }
            }
            result = (!FisWaiting);
            if(result)
            {
                FisWaiting = false; 
            }
            else
                TCPLastError = TcpErr_OutTime;
            return result;
        }

        protected Boolean isWorking()
        {
            if (Busy) TCPLastError = TcpErr_Working;
            return Busy;
        }

        protected Boolean SendAndNOReturn()
        {
            return  DoSendData();
        }

        protected Boolean SendAndReturn(int delay)
        { 
            Boolean result = false;
            Busy = true;
            try{
                FisWaiting = true;
                if(DoSendData())
                result = WaitReturn(delay);
                return result;
            }
            finally{               
                Busy = false; 
            } 
        }

        protected void SetBufCommand(byte command)
        {
            BufferTX[Loc_Begin] = 0x02;
            nBytesWrite = Loc_Data;
            BufferTX[Loc_Command] = command;
            BufferTX[Loc_DoorAddr] = 0;
            BufferTX[Loc_Len] = 0;
            BufferTX[Loc_Len + 1] = 0;
            BufferTX[Loc_Address] = 0xff; 
        }

        protected void SetBufDoorAddr(byte ADoorAddr)
        {
            BufferTX[Loc_DoorAddr] = ADoorAddr;
        }

        private Boolean AskHeart(){
              SetBufCommand(0x56);
              PutBuf(Convert.ToByte(OEMCode >> 8));
              PutBuf(Convert.ToByte(OEMCode & 0xFF)); 
              return  SendAndNOReturn();
        }
        private void AnsEvent(byte Command, byte index, byte Door, Boolean opendoor)
        {
            SetBufCommand(Command);
            SetBufDoorAddr(Door);
            PutBuf(index);
            PutBuf(Convert.ToByte( opendoor));
            SendAndNOReturn();
        }
                 
        protected void PutBuf(byte AData){
                BufferTX[nBytesWrite] = AData;
                nBytesWrite++;
        }

        protected void PutBuf(DateTime AData)
        {
            PutBuf(Convert.ToByte(AData.Hour));
            PutBuf(Convert.ToByte(AData.Minute)); 
        }

        protected void PutBufDate(DateTime AData)
        {
            PutBuf(Convert.ToByte(AData.Year-2000));
            PutBuf(Convert.ToByte(AData.Month)); 
            PutBuf(Convert.ToByte(AData.Day)); 
        }

        protected void PutBufCard(UInt64 AData)
        { 
            PutBuf(Convert.ToByte((AData) & 0xff));
            PutBuf(Convert.ToByte((AData>>8)&0xff));
            PutBuf(Convert.ToByte((AData>>16)&0xff));
            PutBuf(Convert.ToByte((AData>>24)&0xff));
        }

        protected void PutBufPin2(string AData)
        {
            int vPin = Convert.ToInt16(AData);

            PutBuf(Convert.ToByte(vPin >> 8));
            PutBuf(Convert.ToByte(vPin));
        }

        protected void PutBufPin4(string AData)
        {
            int i, len;
            byte[] p  = new byte[8];
            byte[] v  = new byte[4];

            byte[] ap = UTF8Encoding.UTF8.GetBytes(AData); 

            try
            {
                len = ap.Length; 
                for(i=0;i<8;i++) p[i] = 0xFF;

                if (len > 8) len = 8;
                for(i=0;i<len;i++) 
                    p[i] = Convert.ToByte(ap[i]- 0x30) ;

                 for(i=0;i<4;i++)  
                    v[i] = Convert.ToByte(   (  (p[i * 2] << 4) & 0xF0) + (p[i * 2 + 1] & 0x0F));

                 PutBuf(Convert.ToByte(v[0]));
                 PutBuf(Convert.ToByte(v[1]));
                 PutBuf(Convert.ToByte(v[2]));
                 PutBuf(Convert.ToByte(v[3]));

            }
            catch (SocketException e)
            { 
                SockErrorStr = e.ToString();
            } 
        }

        protected void PutBufCardName(string AData)
        {
            int i, len; 
            byte[] aname = UTF8Encoding.Default.GetBytes(AData); 
            byte[] p = new byte[8];  
            try
            {
                len = aname.Length;
                if (len > 8) len = 8;

                for (i = 0; i < 8; i++) p[i] = 0;

                for (i = 0; i < len; i++)
                    p[i] = Convert.ToByte(aname[i]); 

                for (i = 0; i < 8; i++) 
                  PutBuf(Convert.ToByte(p[i]));   // 178
            }
            catch (SocketException e)
            {
                SockErrorStr = e.ToString();
            }
        } 

        private  byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private Boolean DoFormatStatusvent()
        {
            byte vSecond, vMinute, vHour, vDay, vMonth, vOE;
            int vOEM, vYear;
            Boolean Ack, vopenDoor; 

            vSecond = (BufferRX[Loc_Data + 6]);
            vMinute = BufferRX[Loc_Data + 5];
            vHour = BufferRX[Loc_Data + 4];
            vDay = BufferRX[Loc_Data + 3];
            vMonth = BufferRX[Loc_Data + 2];
            vYear = BufferRX[Loc_Data + 1];
            vOE = BufferRX[Loc_Data + 19];
            vOEM = (vOE * 256) + (BufferRX[Loc_Data + 20]);

            if (OEMCode != 23456)
            {
                if (vOEM != OEMCode)
                {
                    //TCPTime.Enabled = false;
                    sock.Close();
                    return false;
                }
            }
            else
                if (vOE == 0x23) return false;

            DoorStatus = BufferRX[Loc_Data + 7];


            Ver = BufferRX[Loc_Data + 18];
            Fun = BufferRX[Loc_Data + 10];
            isEndDate = (Ver >= 81) & ((Fun & 0x01) == 0x01);
            isOrPIN = (Ver >= 81) & ((Fun & 0x04) == 0x04);
            isShowName = (Ver >= 81) & ((Fun & 0x08) == 0x08);
            isCardorPIN = (Ver >= 81) & ((Fun & 0x10) == 0x10);

            if (Ver >= 90)
            {
                CardNumInPack = (BufferRX[Loc_Data + 8]);
            }
            else
            {
                if (isEndDate)
                {
                    if (isShowName)
                        CardNumInPack = 20;
                    else
                        CardNumInPack = 30;
                }
                else
                {
                    if (isShowName)
                        CardNumInPack = 30;
                    else
                        CardNumInPack = 45;
                }
            }

            OnEventHandler(0, vSecond, vMinute, vHour, vDay, vMonth, vYear, DoorStatus, Ver, Fun, true, CardNumInPack, 0,   0, 0, 0, 0, 0, out vopenDoor, out Ack);

            return true;
        }

        private void DoFormCardevent()
        {
            byte ReturnIndex;
            byte vSecond, vMinute, vHour, vDay, vReader, vMonth, vYear, vDoor, EventType;
            byte[] vCard = new byte[4];
            UInt64 Card;
            Boolean Ask, vopenDoor;
            string vpin;

            vCard[0] = BufferRX[Loc_Data + 3];
            vCard[1] = BufferRX[Loc_Data + 2];
            vCard[2] = BufferRX[Loc_Data + 1];
            vCard[3] = BufferRX[Loc_Data + 0];
            vSecond = (BufferRX[Loc_Data + 4]);
            vMinute = (BufferRX[Loc_Data + 5]);
            vHour = (BufferRX[Loc_Data + 6]);
            vDay = (BufferRX[Loc_Data + 7]);
            vMonth = (BufferRX[Loc_Data + 8]);
            vYear = (BufferRX[Loc_Data + 9]);

            Card = Convert.ToUInt64(vCard[3] + vCard[2] * 0x100 + +vCard[1] * 0x10000 + vCard[0] * 0x1000000);
            EventType = Convert.ToByte(BufferRX[Loc_Data + 10] & 0x7F);
            vReader = Convert.ToByte((BufferRX[Loc_Data + 10] & 0x80) >> 7);
            vDoor = BufferRX[Loc_Data + 11];
            ReturnIndex = BufferRX[Loc_Data + 13];

            vpin = BufferRX[Loc_Data + 14].ToString() + BufferRX[Loc_Data + 15].ToString() + BufferRX[Loc_Data + 16].ToString() + BufferRX[Loc_Data + 17].ToString();

            Ask = false;
            vopenDoor = false;
            OnEventHandler(1, vSecond, vMinute, vHour, vDay, vMonth, vYear + 2000, DoorStatus, Ver, Fun, true, CardNumInPack, Card,   vDoor, EventType, 0, 0, vReader, out vopenDoor, out Ask);

            if (Ask)
                AnsEvent(0x53, ReturnIndex, vDoor, vopenDoor);

        }
        private void DoForMatAlarmevent()
        {
            byte ReturnIndex;
            byte vSecond, vMinute, vHour, vDay, vReader, vMonth, vYear, vDoor, EventType;
            Boolean Ask, vopenDoor;

            vSecond = (BufferRX[Loc_Data + 0]);
            vMinute = (BufferRX[Loc_Data + 1]);
            vHour = (BufferRX[Loc_Data + 2]);
            vDay = (BufferRX[Loc_Data + 3]);
            vMonth = (BufferRX[Loc_Data + 4]);
            vYear = (BufferRX[Loc_Data + 5]);

            EventType = Convert.ToByte(BufferRX[Loc_Data + 6] & 0x7F);
            vReader = Convert.ToByte((BufferRX[Loc_Data + 6] & 0x80) >> 7);
            vDoor = BufferRX[Loc_Data + 7];
            ReturnIndex = BufferRX[Loc_Data + 9];

            Ask = false;
            vopenDoor = false;
            OnEventHandler(2, vSecond, vMinute, vHour, vDay, vMonth, vYear + 2000, DoorStatus, Ver, Fun, true, CardNumInPack, 0,   vDoor, EventType, 0, 0, vReader, out vopenDoor, out Ask);

            if (Ask)
                AnsEvent(0x54, ReturnIndex, vDoor, vopenDoor);
        }

        #endregion

        #region 通信连接控制
        /// 设置心跳 
        private  void SetXinTiao()
        { 
        /*    byte[] inValue = new byte[] { 1, 0, 0, 0, 0x88, 0x13, 0, 0, 0xd0, 0x07, 0, 0 };// 首次探测时间5 秒, 间隔侦测时间2 秒

            uint dummy = 0;
            byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
            BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);//是否启用Keep-Alive
            BitConverter.GetBytes((uint)2000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));//多长时间开始第一次探测
            BitConverter.GetBytes((uint)2000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);//探测时间间隔

            sock.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);*/
        }

        /// <summary>
        /// 当socket.connected为false时，进一步确定下当前连接状态
        /// </summary>
        /// <returns></returns>
        private bool IsSocketConnected()
        {
            #region remarks
            /********************************************************************************************
             * 当Socket.Conneted为false时， 如果您需要确定连接的当前状态，请进行非阻塞、零字节的 Send 调用。
             * 如果该调用成功返回或引发 WAEWOULDBLOCK 错误代码 (10035)，则该套接字仍然处于连接状态； 
             * 否则，该套接字不再处于连接状态。
             * Depending on http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.connected.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2
            ********************************************************************************************/
            #endregion

            #region 过程
            // This is how you can determine whether a socket is still connected.
            bool connectState = true;
            bool blockingState = sock.Blocking;
            try
            {
                byte[] tmp = new byte[1];

                sock.Blocking = false;
                sock.Send(tmp, 1, 0);
                //Console.WriteLine("Connected!");
                connectState = true; //若Send错误会跳去执行catch体，而不会执行其try体里其之后的代码
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                {
                    //Console.WriteLine("Still Connected, but the Send would block");
                    connectState = true;
                } 
                else
                {
                    SockErrorStr = e.ToString();
                    //Console.WriteLine("Disconnected: error code {0}!", e.NativeErrorCode);
                    connectState = false;
                    IsconnectSuccess = false;
                    Console.WriteLine("IsSocketConnected ");
                }
            }
            finally
            {
                sock.Blocking = blockingState;
            }

            //Console.WriteLine("Connected: {0}", client.Connected);
            return connectState;
            #endregion
        }
         
        /// 另一种判断connected的方法，但未检测对端网线断开或ungraceful的情况 
        public bool IsSocketConnected(Socket s)
        {
            #region remarks
            /* As zendar wrote, it is nice to use the Socket.Poll and Socket.Available, but you need to take into consideration 
             * that the socket might not have been initialized in the first place. 
             * This is the last (I believe) piece of information and it is supplied by the Socket.Connected property. 
             * The revised version of the method would looks something like this: 
             * from：http://stackoverflow.com/questions/2661764/how-to-check-if-a-socket-is-connected-disconnected-in-c */
            #endregion

            #region 过程
            try
            {
                if (s == null)
                    return false;
                return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
            }
            catch (SocketException e)
            {
                IsconnectSuccess = false;
                return false;
            }
            /* The long, but simpler-to-understand version:

                    bool part1 = s.Poll(1000, SelectMode.SelectRead);
                    bool part2 = (s.Available == 0);
                    if ((part1 && part2 ) || !s.Connected)
                        return false;
                    else
                        return true;

            */
            #endregion
        }

        /// <summary>
        /// 检测socket的状态
        /// </summary>
        /// <returns></returns>
        /* public bool checkSocketState()
        {
            try
            {  
                if (!IsconnectSuccess)
                {
                    return false;
                }
                else//已创建套接字，但未connected
                {
                    #region 异步连接代码

                 //   TimeoutObject.Reset(); //复位timeout事件
                    try
                    {  
                        IPAddress serverIp = IPAddress.Parse(remoteHost);
                        int serverPort = Convert.ToInt32(remotePort);
                        iep = new IPEndPoint(serverIp, serverPort);
                      //  sock.BeginConnect(iep , connectedCallback, sock); 
                        sock.BeginConnect(iep, new AsyncCallback( connectedCallback), sock );

                        SetXinTiao();//设置心跳参数
                    }
                    catch (Exception err)
                    {
                        SockErrorStr = err.ToString();
                        return false;
                    }

                    if (TimeoutObject.WaitOne(2000, false))//直到timeout，或者TimeoutObject.set()
                    {
                        if (IsconnectSuccess)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        SockErrorStr = "Time Out";
                        return false;
                    }

                    #endregion
                }

            }
            catch (SocketException se)
            {
                SockErrorStr = se.ToString();
                return false;
            }
        }
         */
        /// 异步连接回调函数
        private void connectedCallback(IAsyncResult iar)
        {   
                Socket client = (Socket)iar.AsyncState;
                try
                {
                    timer.Enabled = true; 
                    if (sock.Connected)
                    { 
                        sock.EndConnect(iar); 
                        IsconnectSuccess = true; 
                        sock.BeginReceive(BufferRX, 0, BufferRX.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), sock);
                    }
                    else
                    {
                        IsconnectSuccess = false;
                        //sock.BeginConnect(iep, new AsyncCallback(connectedCallback), sock);
                    } 
                }
                catch (Exception e)
                { 
                    SockErrorStr = e.ToString();
                    IsconnectSuccess = false;
                    Console.WriteLine("connectedCallback " );
                }  
        }
          
        /// 创建套接字+异步连接函数
        private bool socket_create_connect()
        {
            socketDisconnected = socketDisconnectedHandler;
            IPAddress serverIp = IPAddress.Parse(remoteHost);
            int serverPort = Convert.ToInt32(remotePort);
            iep = new IPEndPoint(serverIp, serverPort); 

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.SendTimeout = 1000;

            SetXinTiao();//设置心跳参数

            #region 异步连接代码 

            //TimeoutObject.Reset(); //复位timeout事件
            try
            { 
                sock.BeginConnect(iep, new AsyncCallback(connectedCallback ), sock );
            }
            catch (Exception err)
            {
                SockErrorStr = err.ToString();
                Console.WriteLine( "socket_create_connect"  + err.Message );
                return false;
            }
            return true; 
            #endregion
        }

        private void EndDisconnect(IAsyncResult result)
        {
            try
            {
                sock.EndDisconnect(result);
            }
            catch
            {

            }
        }


        public bool CloseTcpip()
        {
            timer.Enabled = false;
            socketDisconnected = null; // socketDisconnectedHandler;

            lock (this)
            {
                if (sock != null)
                    if (IsconnectSuccess)
                    {
                        try
                        {
                            //关闭socket
                            //sock.Shutdown(SocketShutdown.Both);
                           // sock.BeginDisconnect(false, EndDisconnect, false); //.Disconnect(true);
                            timer.Enabled = false;
                            sock.Disconnect(false);
                            IsconnectSuccess = false;
                            //sock.Close(); 
                        }
                        catch (Exception ex)
                        {
                            SockErrorStr = ex.ToString();
                        }
                    }
            }
            return true; 
        }

        private void timer_Tick(object sender, ElapsedEventArgs e)
        {
            try
            { 
                isHeartTime++;
                if (isHeartTime >5)
                { 
                    isHeartTime = 0; 
                    if (sock == null)
                    {
                        timer.Enabled = false;
                        socket_create_connect();
                    }
                    else // if
                    {
                        if ((!sock.Connected) || (!IsSocketConnected(sock)) ||(!IsSocketConnected()))
                        {
                            timer.Enabled = false;
                            IsconnectSuccess = false;
                            Reconnect();
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                SockErrorStr = ex.ToString();
                IsconnectSuccess = false;
                Console.WriteLine(string.Format("timer_Tick {0}", ex.Message)); 
                timer.Enabled = true;
            } 
        }        

        private void ReceiveCallBack(IAsyncResult ar)
        {
            if (sock == null)
            {
                socket_create_connect();
            }
            else if (!sock.Connected)
            {
                if (!IsSocketConnected())
                    Reconnect();
            } 

            try
            {     
                Socket peerSock = (Socket)ar.AsyncState;
                int BytesRead = peerSock.EndReceive(ar);
                if (BytesRead > 0)
                {
                    byte[] returnBytes = new byte[BytesRead];
                    if (BytesRead > 512) BytesRead = 512;
                    Array.ConstrainedCopy(BufferRX, 0, returnBytes, 0, BytesRead);
                    string str = BitConverter.ToString(returnBytes).Replace("-", " ");
                    this.HandleMessage(returnBytes, str);
                    RxDataEvent(str);
                }
                else//对端gracefully关闭一个连接
                {
                    if (sock.Connected)//上次socket的状态
                    {
                        if (socketDisconnected != null)
                        {
                            //1-重连
                            socketDisconnected();
                            //2-退出，不再执行BeginReceive
                            return;
                        }
                    }
                }
                //此处buffer似乎要清空--待实现 zq
                sock.BeginReceive(BufferRX, 0, BufferRX.Length, 0, new AsyncCallback(ReceiveCallBack), sock);
            }
            catch (Exception ex)
            {
                SockErrorStr = ex.ToString();
                if (socketDisconnected != null)
                {
                    socketDisconnected(); //Keepalive检测网线断开引发的异常在这里捕获
                    return;
                }
            } 
        } 

        private bool Reconnect()
        {
            try
            {
                //关闭socket
                sock.Shutdown(SocketShutdown.Both);
                sock.Disconnect(true);
                IsconnectSuccess = false;
                sock.Close();
            }
            catch (Exception ex)
            {
                SockErrorStr = ex.ToString();
            } 
            //创建socket
            return socket_create_connect(); 
        }

        public void socketDisconnectedHandler()
        {
            Reconnect();
        }

        #endregion
        public bool OpenIP(string ip, int port)
        { 
                remoteHost = ip;
                remotePort = port; 
               // timer.Enabled = true;
                return socket_create_connect();  
        }

        #region 门禁控制器通信指令 

        private void HandleMessage(byte[] buffRX,string str)
        {            
            if (CheckRxDataCS(buffRX, buffRX.Length))
            {
                isHeartTime = 0;
                switch (buffRX[Loc_Command])
                {
                    case 0x56: if(DoFormatStatusvent()) AskHeart(); break;
                    case 0x52: break;   // card status
                    case 0x53: DoFormCardevent(); break;   // card event
                    case 0x54: DoForMatAlarmevent();  break;   // alarm
                    default: 
                             if (buffRX[Loc_Command] == LastCmd)
                                 FisWaiting = false;
                             break;
                 }                 
            } 
        }
          
        #endregion
    }

    public class TcpipClass:TcpipBClass
    {
        #region 参数类指令  
        public Boolean SetTime(DateTime datetime)
        {    
            if (isWorking()) return false;
            DateTime dt = datetime; 
            SetBufCommand(0x07);  
            PutBuf(Convert.ToByte(dt.Second));
            PutBuf(Convert.ToByte(dt.Minute));
            PutBuf(Convert.ToByte(dt.Hour));
            PutBuf(Convert.ToByte(dt.DayOfWeek + 1));
            PutBuf(Convert.ToByte(dt.Day));
            PutBuf(Convert.ToByte(dt.Month));
            PutBuf(Convert.ToByte(dt.Year-2000));

            return SendAndReturn(0);
        }

        public Boolean SetDoor(byte Door, UInt16 OpenTime, UInt16 OpenOutTime, Boolean TooLongAlarm, UInt16 AlarmMast, UInt16 AlarmTime,
            Boolean DoublePath, byte CardsOpen, byte CardsOpenInOut)
        {
            if (isWorking()) return false;
            SetBufCommand(0x61);
            SetBufDoorAddr(Convert.ToByte(Door + 1));

            PutBuf(Convert.ToByte(OpenTime));
            PutBuf(Convert.ToByte(OpenOutTime));
            PutBuf(Convert.ToByte(DoublePath));
            PutBuf(Convert.ToByte(TooLongAlarm));
            PutBuf(Convert.ToByte(OpenTime>>8));
            PutBuf(Convert.ToByte(AlarmMast));
            PutBuf(Convert.ToByte(AlarmTime));
            PutBuf(Convert.ToByte(AlarmTime>>8));
            PutBuf(Convert.ToByte(CardsOpen));
            PutBuf(Convert.ToByte(CardsOpenInOut)); 
            return SendAndReturn(0);
        }
      
        public Boolean SetControl(UInt16 FireTime, UInt16 AlarmTime, string DuressPIN, byte LockEach)
        {
            byte[] pin = new byte[4];
            if (isWorking()) return false;
            SetBufCommand(0x63); 
            PutBuf(Convert.ToByte(LockEach));
            PutBuf(Convert.ToByte(FireTime));
            PutBuf(Convert.ToByte(FireTime>>8));
            PutBuf(Convert.ToByte(AlarmTime));
            PutBuf(Convert.ToByte(AlarmTime>>8));
            PutBuf(Convert.ToByte(pin[0]));
            PutBuf(Convert.ToByte(pin[1]));
            PutBuf(Convert.ToByte(pin[2]));
            PutBuf(Convert.ToByte(pin[3]));  
            return SendAndReturn(0);
        }

        public Boolean DelTimeZone(byte Door)
        {
            if (isWorking()) return false;
            SetBufCommand(0x0f);
            SetBufDoorAddr(Convert.ToByte(Door + 1));
            return SendAndReturn(20);
        }
       
        public Boolean  AddTimeZone(UInt16 Door, byte Index,DateTime frmtime, DateTime totime, byte Week,Boolean PassBack, byte Indetify, DateTime Enddatetime,byte Group )
        {
            if (isWorking()) return false;
            byte vIndetify =0 ; 
            SetBufCommand(0x0d);
            SetBufDoorAddr(Convert.ToByte(Door + 1));             
            PutBuf(Convert.ToByte(Index));
            PutBuf(frmtime);
            PutBuf(totime);
            PutBuf(Convert.ToByte(Week));
            if(PassBack)
              vIndetify |= 0x80;
            PutBuf(Convert.ToByte(vIndetify));
            PutBufDate(Enddatetime);
            PutBuf(Convert.ToByte(Group));
            return  SendAndReturn(0); 
        }

        public Boolean DelHoliday()
        {
            if (isWorking()) return false;
            SetBufCommand(0x0c); 
            return SendAndReturn(0);
        }

        public Boolean  AddHoliday(byte Index,DateTime frmdate, DateTime todate)
        {
            if (isWorking()) return false;
            SetBufCommand(0x09);
           
            PutBuf(Convert.ToByte(Index));
            PutBufDate(frmdate);
            PutBufDate(todate);
            return  SendAndReturn(0); 
        } 
         
        #endregion

        #region 辅助类指令

        public Boolean Reset()
        {
            if (isWorking()) return false;
            SetBufCommand(0x04); 
            return SendAndReturn(1000);
        }

        public Boolean  Restart()
        {
            if (isWorking()) return false;
            SetBufCommand(0x05); 
            return   SendAndNOReturn(); 
        } 
        #endregion

        #region 下载卡
        public Boolean  AddCard(UInt16 Index, UInt64 CardNo, string pin,string name, byte TZ1, byte TZ2, byte TZ3, byte TZ4,  byte Status,DateTime enddatetime)
        {
             if (isWorking()) return false;
             SetBufCommand(0x62);             
             PutBuf(Convert.ToByte(Index));
             PutBuf(Convert.ToByte(Index >>8));
             PutBufCard(CardNo);

            if(isOrPIN)
            {  
                PutBufPin2(pin);
            }
            else if(isCardorPIN)
            {
                PutBufPin4(pin);
            }else
                PutBufPin2(pin); 

            PutBuf(Convert.ToByte(TZ1));
            PutBuf(Convert.ToByte(TZ2));
            PutBuf(Convert.ToByte(TZ3));
            PutBuf(Convert.ToByte(TZ4));

            if(isEndDate)
            {
                PutBufDate(enddatetime); 
                PutBuf(Convert.ToByte(enddatetime.Hour));
                PutBuf(Convert.ToByte(enddatetime.Minute));
            }
            else
            {
               PutBuf(Convert.ToByte(0));
               PutBuf(Convert.ToByte(0));
               PutBuf(Convert.ToByte(0));
               PutBuf(Convert.ToByte(0));
               PutBuf(Convert.ToByte(Status));
            }

            if (isShowName)
                PutBufCardName(name); 

             return  SendAndReturn(0);
        }

        public Boolean DelCard(UInt16 Index)
        {
            if (isWorking()) return false;
            SetBufCommand(0x16);
            PutBuf(Convert.ToByte(Index));
            PutBuf(Convert.ToByte(Index >> 8));
            return SendAndReturn(0);
        }

        public Boolean  AddCards(UInt16 PackIndex, byte  CardofPack , Boolean LastRecord,  UInt16  CardIndex, UInt64 CardNo, UInt16 pin,
            byte TZ1, byte TZ2, byte TZ3, byte TZ4, byte Status,string Name,UInt16 EndYear,byte EndMonth, byte EndDay, byte EndHour, byte EndMinute, byte EndSecond)
        {
            if (isWorking()) return false;

            //return  SendAndReturn(0);    
            return false;
        } 

        public Boolean  ClearAllCards(){
            if (isWorking()) return false;
            SetBufCommand(0x17);  
            return  SendAndReturn(3000); 
        }
        #endregion

        #region 控制类指令
        public Boolean Opendoor(byte Door)
        {
            if (isWorking()) return false;
            SetBufCommand(0x2C);
            SetBufDoorAddr(Convert.ToByte( Door + 1));
            return  SendAndReturn(10); 
        }

        public Boolean Closedoor(byte Door)
        {
            if (isWorking()) return false;
            SetBufCommand(0x2e);
            SetBufDoorAddr(Convert.ToByte(Door + 1));
            return SendAndReturn(10);
        }

        public Boolean LockDoor(byte Door, Boolean Lock)
        {
            if (isWorking()) return false;
            SetBufCommand(0x2f);
            SetBufDoorAddr(Convert.ToByte(Door + 1));
            PutBuf(Convert.ToByte(Lock));
            PutBuf(Convert.ToByte(Lock));
            return SendAndReturn(0);
        }

        public Boolean OpenDoorLong(byte Door)
        {
            if (isWorking()) return false;
            SetBufCommand(0x2d);
            SetBufDoorAddr(Convert.ToByte(Door + 1));
            return SendAndReturn(0);
        }

        public Boolean SetAlarm(Boolean AClose, Boolean ALong)
        {
            if (isWorking()) return false;
            SetBufCommand(0x18);
            PutBuf(Convert.ToByte(AClose));
            PutBuf(Convert.ToByte(ALong));
            return SendAndReturn(0);
        }

        public Boolean SetFire(Boolean AClose, Boolean ALong)
        {
            if (isWorking()) return false;
            SetBufCommand(0x19);
            PutBuf(Convert.ToByte(AClose));
            PutBuf(Convert.ToByte(ALong)); 
            return SendAndReturn(0);
        }
        #endregion
    }


}
