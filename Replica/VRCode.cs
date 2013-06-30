using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;

namespace Replica
{
    class VRCode
    {

        public void startServer(string address)
        {
            using (Context context = new Context())
            using (Socket server = context.Socket(SocketType.REP))
            {
                address = "*:5555";
                server.Bind("tcp://" + address);
                Console.WriteLine("Server running");

                string reply = "World";
                while (true)
                {
                    string message = server.Recv(Encoding.Unicode);
                    Console.WriteLine(message);
                    server.Send(reply, Encoding.Unicode);
                }
            }
        }
    }
}
