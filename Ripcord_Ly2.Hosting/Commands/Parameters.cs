using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Hosting.Commands
{
    public class Parameters
    {
        public bool Verbose { get; set; }

        public IList<string> Arguments { get; set; }

        public IDictionary<string, string> Switches { get; set; }
    }
}
