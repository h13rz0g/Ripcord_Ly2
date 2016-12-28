using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Hosting.Commands
{
    public class CommandParser : ICommandParser
    {
        [SecurityCritical]
        public IEnumerable<string> Parse(string commandLine)
        {
            return SplitArgs(commandLine);
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/17w5ykft.aspx
        /// </summary>
       
        private IEnumerable<string> SplitArgs(string commandLine)
        {
            var state = new State(commandLine);
            while (!state.EOF)
            {
                switch (state.Current)
                {
                    case '"':
                        ProcessQuote(state);
                        break;

                    case '\\':
                        ProcessBackslash(state);
                        break;

                    case ' ':
                    case '\t':
                        if (state.StringBuilder.Length > 0)
                            state.AddArgument();
                        state.MoveNext();
                        break;

                    default:
                        state.AppendCurrent();
                        state.MoveNext();
                        break;
                }
            }
            if (state.StringBuilder.Length>0)
            {
                state.AddArgument();
            }
            return state.Arguments;
        }

        private void ProcessBackslash(State state)
        {
            state.MoveNext();
            if (state.EOF)
            {
                state.Append('\\');
                return;
            }

            if (state.Current == '"')
            {
                state.Append('"');
                state.MoveNext();
            }
            else
            {
                state.Append('\\');
                state.AppendCurrent();
                state.MoveNext();
            }
        }

        private void ProcessQuote(State state)
        {
            state.MoveNext();
            while (!state.EOF)
            {
                if (state.Current == '"')
                {
                    state.MoveNext();
                    break;
                }
                state.AppendCurrent();
                state.MoveNext();
            }
            state.AddArgument();
        }
    }
}
