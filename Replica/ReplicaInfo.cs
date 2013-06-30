using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Replica
{
    class ReplicaInfo
    {
        public int ID { get; set; }
        public string address { get; set; }

        public ReplicaInfo(int id, string address)
        {
            this.ID = id;
            this.address = address;
        }
    }
}
