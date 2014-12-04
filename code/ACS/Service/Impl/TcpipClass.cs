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

namespace TcpipIntface
{    
    public  class TcpipClass
    {
        #region constdata
        private const byte Loc_Begin = 0;
        private const byte Loc_Temp = 1;
        private const byte Loc_Command = 2;
        private const byte Loc_Address = 3;
        private const byte Loc_DoorAddr = 4;
        private const byte Loc_Len = 5;
        private const byte Loc_Data = 7;

        public const byte TcpErr_OK = 0;
        public const byte TcpErr_NotExists = 1; // 对象不存在
        public const byte  TcpErr_DataErr = 2; // 数据超出边界
        public const byte  TcpErr_OutTime = 3; // 操作超时
        public const byte  TcpErr_UnLink = 4; //
        public const byte TcpErr_ReData = 5; // 返回数据错误
        public const byte  TcpErr_Unknow = 6; //
        #endregion
       
        Socket sock;
        IPEndPoint iep;
        System.Timers.Timer timer ;
        byte isHeartTime=0;
        byte[] BufferRX = new byte[1000];
        byte[] BufferTX = new byte[512];
        byte nBytesWrite = 0;
        int OEMCode = 23456;
        public byte TCPLastError = 0;
        private byte LastCmd;
        private Boolean FisDoSending,Busy;
        volatile Boolean FisWaiting;
        private byte Ver, Fun, CardNumInPack, DoorStatus;
        private Boolean isEndDate, isOrPIN, isShowName, isCardorPIN;
         
        public delegate void  RxDataHandler(string buffRX);   //声明委托
        public event RxDataHandler RxDataEvent;        //声明事件

        public delegate void  TOnEventHandler(byte EType,byte Second,byte Minute,byte Hour,byte Day,byte Month,int Year,byte DoorStatus,
            byte Ver,byte FuntionByte,Boolean Online, byte CardsofPackage, UInt64 CardNo, out byte Door,  byte EventType,
            UInt16 CardIndex, byte CardStatus,byte reader,out Boolean OpenDoor, out Boolean Ack);   //声明委托
        
        public event TOnEventHandler  OnEventHandler ;        //声明事件
                
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

        private Boolean DoSendData(){
            int i,datalen, RN, WriteNum;
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
                RN = 0;
                WriteNum = nBytesWrite + 2;

                if (sock.Connected)
                {
                    cmd = BufferTX[Loc_Command];
                    LastCmd = BufferTX[Loc_Command];
                    RN = sock.Send(BufferTX,0, WriteNum,System.Net.Sockets.SocketFlags.None);
                }
                else
                {
                    TCPLastError = TcpErr_UnLink;
                }
                return (RN == WriteNum);
            }
            catch (Exception ex)
            {
                TCPLastError = TcpErr_Unknow;
                return false;
            }
        }

        private Boolean  WaitReturn(int delay)
        {
            Boolean te,result;
            int t1;  
            FisDoSending = true;
            t1 = 0 ; 
           // FisWaiting = true;          
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
                FisDoSending = false;
            }
            else
                TCPLastError = TcpErr_OutTime;
            return false;
        }         

        private Boolean SendAndNOReturn()
        {
            return  DoSendData();
        }
         
        private Boolean SendAndReturn(int delay){ 
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
        
        private void SetBufCommand(byte command)
        {
            BufferTX[Loc_Begin] = 0x02;
            nBytesWrite = Loc_Data;
            BufferTX[Loc_Command] = command;
            BufferTX[Loc_DoorAddr] = 0;
            BufferTX[Loc_Len] = 0;
            BufferTX[Loc_Len + 1] = 0;
            BufferTX[Loc_Address] = 0xff; 
        }

        private void SetBufDoorAddr(byte ADoorAddr)
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

        private void PutBuf(byte AData){
                BufferTX[nBytesWrite] = AData;
                nBytesWrite++;
        }

        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int REnd = sock.EndReceive(ar);
                if (REnd > 0)
                { 
                    byte[] returnBytes =  new byte[REnd]; 
                    Array.Copy(BufferRX , returnBytes, REnd);

                    string str = BitConverter.ToString(returnBytes ).Replace("-", " ");

                    this.HandleMessage(returnBytes, str);
                }
                sock.BeginReceive(BufferRX, 0, BufferRX.Length, 0, new AsyncCallback(ReceiveCallBack), null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void timer_Tick(object sender, ElapsedEventArgs e)
        { 
            try
            {
                timer.Enabled = false;
                if (!sock.Connected)  
                { 
                    sock.Connect(iep);
                    isHeartTime = 0;
                }
            
                if(sock.Connected)
                {
                    isHeartTime++;
                    if(isHeartTime > 7)
                    {
                         sock.Disconnect(true);
                       // sock.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    }
                }
            }
            finally
            {
               timer.Enabled = true; 
            }
        }
 
        public bool OpenIP(string ip, int port)
        {  
            try
            {
                if (sock == null)
                {
                    sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  
                    timer = new System.Timers.Timer(); 
                    timer.Elapsed += new ElapsedEventHandler(timer_Tick);  
                    timer.Interval = 1000;
                    timer.Enabled = false; 
                }

                IPAddress serverIp = IPAddress.Parse(ip);
                int serverPort = Convert.ToInt32(port);
                iep = new IPEndPoint(serverIp, serverPort);

                if (sock.Connected) return true;  
                sock.Connect(iep);
                if (sock.Connected)
                {  
                    timer.Enabled = true;
                    sock.BeginReceive(BufferRX, 0, BufferRX.Length, 0, new AsyncCallback(ReceiveCallBack), null);
                    
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to connect with TCP to {0}: {1}", "", ex.Message));
                return false;
            }
        }

        private Boolean DoFormatStatusvent()
        { 
            byte vSecond, vMinute, vHour, vDay, vMonth,  vOE; 
            int vOEM,vYear;
            Boolean Ack ,vopenDoor ;
            byte vDoor ;
 
            vSecond  = (BufferRX[Loc_Data + 6]);
            vMinute  = BufferRX[Loc_Data + 5];
            vHour  = BufferRX[Loc_Data + 4];
            vDay = BufferRX[Loc_Data + 3];
            vMonth = BufferRX[Loc_Data + 2];
            vYear = BufferRX[Loc_Data + 1];
            vOE =  BufferRX[Loc_Data + 19] ;
            vOEM = (vOE * 256) + (BufferRX[Loc_Data + 20]);

            if (OEMCode != 23456)
            {
                if( vOEM != OEMCode)
                { 
                    //TCPTime.Enabled = false;
                    this.sock.Close();
                    return false;
                }
            } else 
                if (vOE == 0x23) return false; 

            DoorStatus = BufferRX[Loc_Data + 7];

           
            Ver = BufferRX[Loc_Data + 18];
            Fun = BufferRX[Loc_Data + 10];
            isEndDate = (Ver >= 81) & ((Fun & 0x01) == 0x01);
            isOrPIN = (Ver >= 81) & ((Fun & 0x04) == 0x04);
            isShowName = (Ver >= 81) & ((Fun & 0x08) == 0x08);
            isCardorPIN = (Ver >= 81) & ((Fun & 0x10) == 0x10);

            if(Ver >= 90)
            {
                CardNumInPack = (BufferRX[Loc_Data + 8]);
            }
            else
            {
                if(isEndDate)
                {
                    if (isShowName )
                        CardNumInPack = 20;
                    else
                        CardNumInPack = 30;
                } else
                {
                    if( isShowName )
                        CardNumInPack = 30;
                    else
                        CardNumInPack = 45;
                }
            }

            OnEventHandler(0, vSecond, vMinute, vHour, vDay, vMonth, vYear , DoorStatus, Ver, Fun, true, CardNumInPack, 0,out vDoor, 0, 0, 0,  0,out vopenDoor,out Ack);

            return true;
        }

        private void DoFormCardevent() { 
            byte  ReturnIndex;
            byte vSecond, vMinute, vHour, vDay, vReader, vMonth, vYear,  vDoor, EventType;
            byte[]  vCard  = new byte[4];
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
            EventType = Convert.ToByte( BufferRX[Loc_Data + 10] & 0x7F);
            vReader = Convert.ToByte((BufferRX[Loc_Data + 10] & 0x80) >> 7);
            vDoor = BufferRX[Loc_Data + 11];
            ReturnIndex = BufferRX[Loc_Data + 13];

            vpin = BufferRX[Loc_Data + 14].ToString() + BufferRX[Loc_Data + 15].ToString() + BufferRX[Loc_Data + 16].ToString() + BufferRX[Loc_Data + 17].ToString();
             
            Ask = false;
            vopenDoor = false;
            OnEventHandler(1,vSecond, vMinute, vHour, vDay, vMonth, vYear + 2000,DoorStatus, Ver,Fun,true,CardNumInPack,Card,out vDoor,EventType,0,0,vReader,out vopenDoor,out Ask );

            if (Ask)
            AnsEvent(0x53, ReturnIndex, vDoor, vopenDoor);
            
        }
        private void DoForMatAlarmevent()
        {
            byte  ReturnIndex;
            byte vSecond, vMinute, vHour, vDay, vReader, vMonth, vYear,  vDoor, EventType; 
            Boolean Ask, vopenDoor; 

            vSecond = (BufferRX[Loc_Data + 0]);
            vMinute = (BufferRX[Loc_Data + 1]);
            vHour = (BufferRX[Loc_Data + 2]);
            vDay = (BufferRX[Loc_Data + 3]);
            vMonth = (BufferRX[Loc_Data + 4]);
            vYear = (BufferRX[Loc_Data + 5]); 
              
            EventType = Convert.ToByte( BufferRX[Loc_Data + 6] & 0x7F);
            vReader = Convert.ToByte((BufferRX[Loc_Data + 6] & 0x80) >> 7);
            vDoor = BufferRX[Loc_Data + 7]; 
            ReturnIndex = BufferRX[Loc_Data + 9]; 

            Ask = false;
            vopenDoor = false;
            OnEventHandler(2,vSecond, vMinute, vHour, vDay, vMonth, vYear + 2000, DoorStatus, Ver,Fun,true,CardNumInPack,0,out vDoor,EventType,0,0,vReader,out vopenDoor,out Ask );

            if (Ask)
               AnsEvent(0x54, ReturnIndex, vDoor, vopenDoor); 
        }

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
            RxDataEvent(str);
        }

        public Boolean Opendoor(byte Door)
        {
            SetBufCommand(0x2C);
            SetBufDoorAddr(Convert.ToByte( Door + 1));
            return  SendAndReturn(10); 
        }

        public Boolean Closedoor(byte Door)
        {
            SetBufCommand(0x2e);
            SetBufDoorAddr(Convert.ToByte(Door + 1));
            return SendAndReturn(10);
        }

        public Boolean Reset()
        {
            SetBufCommand(0x04);
            SetBufDoorAddr( 0 );
            return  SendAndReturn(20); 
        }

   }    

}
