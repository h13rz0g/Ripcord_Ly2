using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Hosting.Commands
{
    public class CommandParametersParser : ICommandParametersParser
    {
        [SecurityCritical]
        public CommandParameters Parse(IEnumerable<string> args)
        {
            var arguments = new List<string>();
            var switches=new Dictionary<string, string>();


            return new CommandParameters
            {
                Arguments = arguments,
                Switches = switches
            };

        }
    }
}
