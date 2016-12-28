using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Hosting
{
    public class ConsoleLogger
    {
        private readonly TextReader _input;
        private readonly TextWriter _output;


        public ConsoleLogger(
            TextReader input,
            TextWriter output)
        {
            _input = input;
            _output = output;
        }

        public void LogInfo(string format,params object[] args)
        {
            _output.Write("{0}:",DateTime.Now);
            _output.WriteLine(format,args);
        }


        public void LogError(string format,params object[] args)
        {
            _output.Write("{0}:", DateTime.Now);
            _output.WriteLine(format, args);
        }


    }
}
