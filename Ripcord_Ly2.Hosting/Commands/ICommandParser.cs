using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Hosting.Commands
{
    public interface ICommandParser
    {
        IEnumerable<string> Parse(string commandLine);
    }
}
