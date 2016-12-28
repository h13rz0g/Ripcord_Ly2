using System.Collections.Generic;
using System.Security;

namespace Ripcord_Ly2.Hosting.Commands
{
    public class ParametersParser : IParametersParser
    {
        [SecurityCritical]
        public Parameters Parser(CommandParameters parameters)
        {
            var result = new Parameters
            {
                Arguments = new List<string>(),
                Switches = new Dictionary<string, string>()
            };

            foreach (var arg in parameters.Arguments)
            {
               
            }

            foreach (var switches in parameters.Switches)
            {
                switch (switches.Key.ToLowerInvariant())
                {
                   
                    default:
                        break;
                }
            }

            return result;

        }
    }
}
