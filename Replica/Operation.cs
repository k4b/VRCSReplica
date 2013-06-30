using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Replica
{
    class Operation
    {
        public int operationID { get; protected set; }
        public String path { get; protected set; }
        public byte[] file { get; protected set; }

        public Operation(byte[] file, string path)
        {
            operationID = 1;
            this.file = file;
            this.path = path;
        }

        public Operation(string path)
        {
            operationID = 2;
            this.path = path;
        }
    }
}
