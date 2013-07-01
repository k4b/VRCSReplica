using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;

namespace Replica
{
    class VRCode
    {
        private VRReplica vrReplica;
 
        public VRCode(VRReplica replica)
        {
            this.vrReplica = replica;
        }

        public void startServer(string address)
        {
            using (Context context = new Context())
            using (Socket server = context.Socket(SocketType.REP))
            {

                //address = "*:5555";
                server.Bind("tcp://" + address);
                Console.WriteLine("Server running");

                int messageID;
                int operationID;
                string path;
                byte[] file;
                int clientID;
                int requestNumber;
                int viewNumber;
              
                while (true)
                {
                    var rcvdMsg = server.Recv();

                    messageID = BitConverter.ToInt32(rcvdMsg, 0);

                    rcvdMsg = server.Recv();
                    operationID = BitConverter.ToInt32(rcvdMsg, 0);

                    path = server.Recv(Encoding.Unicode);

                    rcvdMsg = server.Recv();
                    file = rcvdMsg;

                    rcvdMsg = server.Recv();
                    clientID = BitConverter.ToInt32(rcvdMsg, 0);

                    rcvdMsg = server.Recv();
                    requestNumber = BitConverter.ToInt32(rcvdMsg, 0);

                    rcvdMsg = server.Recv();
                    viewNumber = requestNumber = BitConverter.ToInt32(rcvdMsg, 0);

                    Console.WriteLine("ServRecieved: " + messageID);
                    Console.WriteLine("ServRecieved: " + operationID);
                    Console.WriteLine("ServRecieved: " + path);
                    Console.WriteLine("ServRecieved: " + file);
                    Console.WriteLine("ServRecieved: " + clientID);
                    Console.WriteLine("ServRecieved: " + requestNumber);
                    Console.WriteLine("ServRecieved: " + viewNumber);
                    Console.WriteLine("---");
                    Console.WriteLine("---");
    
                    switch (messageID) //casy sa dla rodzaju wiadomosci jaka przyszla
                    {
                        case 1:
                            Console.WriteLine("case 1");
                            
                            ClientInfo thisClientInfo = null;
                            
                            //sprawdza czy już instnieje taki klient w clientTable
                            for (int i = 0; i < vrReplica.clientTable.Count; i++)
                            {
                                if (vrReplica.clientTable.ElementAt(i).clientID == clientID)
                                {
                                    thisClientInfo = vrReplica.clientTable.ElementAt(i);
                                }
                            }

                            //jeśli nie to go tworzy
                            if (thisClientInfo == null)
                            {
                                thisClientInfo = new ClientInfo(clientID, requestNumber, true);
                                vrReplica.clientTable.Add(thisClientInfo);
                            }

                            //wysyła odpowiedź do klienta
                            server.SendMore(BitConverter.GetBytes(4)); //messageID (4 - reply)
                            server.SendMore(BitConverter.GetBytes(vrReplica.viewNumber)); //viewNumber
                            server.SendMore(BitConverter.GetBytes(thisClientInfo.lastRequestNumber)); //requestNumber
                            server.Send(BitConverter.GetBytes(thisClientInfo.result)); //result

                            break;
                    }
            
                }
            }
        }

        //public void request(int operationID, int clientID, int requestNumber, int viewNumber)
        //{
        //    Console.WriteLine("**Request");
        //}
    }
}