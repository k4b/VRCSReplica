using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Replica
{
    class ClientInfo
    {
        public int clientID { get; set; }
        public int lastRequestNumber { get; set; }
        public bool result { get; set; }

        public ClientInfo(int clientID, int lastRequestNumber)
        {
            this.clientID = clientID;
            this.lastRequestNumber = lastRequestNumber;
        }

        public ClientInfo(int clientID, int lastRequestNumber, bool result)
        {
            this.clientID = clientID;
            this.lastRequestNumber = lastRequestNumber;
            this.result = result;
        }
    }
}
