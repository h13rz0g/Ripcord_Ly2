using Ripcord_Ly2.Command;
using Ripcord_Ly2.Hosting.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Hosting.HostContext
{
    public class CommandHostContext
    {

        public CommandReturnCodes StartSessionResult { get; set; }
        public CommandReturnCodes RetryResult { get; set; }

        public Parameters Arguments { get; set; }
        public bool DisplayUsageHelp { get; set; }
      

    }

}
