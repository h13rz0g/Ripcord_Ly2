using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Hosting.Commands
{
    public class State
    {
        private readonly string _commandLine;
        private readonly StringBuilder _stringBuilder;
        private readonly List<string> _arguments;
        private int _index;


        public State(string commandLine)
        {
            _commandLine = commandLine;
            _stringBuilder = new StringBuilder();
            _arguments = new List<string>();
        }

        public StringBuilder StringBuilder { get { return _stringBuilder; } }

        public bool EOF { get { return _index >= _commandLine.Length; } }

        public char Current { get { return _commandLine[_index]; } }

        public IEnumerable<string> Arguments { get { return _arguments; } }

        public void AddArgument()
        {
            _arguments.Add(StringBuilder.ToString());
            StringBuilder.Clear();
        }

        public void AppendCurrent()
        {
            StringBuilder.Append(Current);
        }

        public void Append(char ch)
        {
            StringBuilder.Append(ch);
        }

        public void MoveNext()
        {
            if (!EOF)
                _index++;
        }



    }
}
