using Ripcord_Ly2.Command;
using Ripcord_Ly2.Hosting.Commands;
using Ripcord_Ly2.Hosting.HostContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Hosting
{
    public class RipcordHost
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConsoleLogger _logger;
        private readonly ICommandHostContextProvider _commandHostContextProvider;

        private readonly TextReader _input;
        private readonly TextWriter _output;

        public RipcordHost(
            IServiceProvider serviceProvider, 
            TextReader input, 
            TextWriter output,
            string[] args)
        {
            _serviceProvider = serviceProvider;
            _input = input;
            _output = output;
            _logger = new ConsoleLogger(input, output);

            _commandHostContextProvider = new CommandHostContextProvider(
                serviceProvider,
                _logger,
                args);
        }


        public async Task<CommandReturnCodes> RunAsync()
        {
            try
            {
                return await DoRunAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Error:");
                for (; e !=null; e=e.InnerException)
                {
                    _logger.LogError("Message: {0}", e.Message);
                    _logger.LogError("Source: {0}", e.Source);
                    _logger.LogError("StackTrace: {0}", e.StackTrace);
                }
                return CommandReturnCodes.Fail;
            }
            
        }


        public async Task<CommandReturnCodes> DoRunAsync()
        {
            var content = CommandHostContext();
            if (content.DisplayUsageHelp)
            {
                await DisplayUsageHelpAsync();
                return CommandReturnCodes.Ok;
            }
            if (content.StartSessionResult==CommandReturnCodes.Fail)
            {
                _commandHostContextProvider.Shutdown(content);
                return content.StartSessionResult;
            }
            CommandReturnCodes result = CommandReturnCodes.Fail;

            result = await ExecuteInteractiveAsync(content);

            _commandHostContextProvider.Shutdown(content);
            return result;

        }

     
     

        private async Task<CommandReturnCodes> ExecuteInteractiveAsync(
            CommandHostContext context)
        {
            await _output.WriteLineAsync("Type \"?\" for Help, \"exit\" to Exit,\"cls\" Clear Console.");
            while (true)
            {
                var command = await ReadCommandAsync(context);
                switch (command?.ToLowerInvariant())
                {
                    case "e":
                    case "exit":
                        return 0;
                    case "?":
                    case "help":
                        await DisplayInteractiveHelpAsync();
                        break;
                    case "cls":
                        System.Console.Clear();
                        break;
                    default:
                        context = await RunCommandAsync(context, command);
                        break;
                }
            }
           
        }

        private async Task<CommandHostContext> RunCommandAsync(
            CommandHostContext context, 
            string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return context;
            }
            //CommandReturnCodes result = await RunCommandInSessionAsync(context, command);
            //if (result== context.RetryResult)
            //{
            //    _commandHostContextProvider.Shutdown(context);
            //    context = CommandHostContext();
            //    result = await RunCommandInSessionAsync(context, command);
            //    if (result!=CommandReturnCodes.Ok)
            //    {
            //        await _output.WriteLineAsync($"Returns non-zero result:{result}");
            //    }
            //}

            await _output.WriteLineAsync($"RunCommandAsync:");
            return context;
        }

        //private async Task<CommandReturnCodes> RunCommandInSessionAsync(
        //    CommandHostContext context,
        //    string command)
        //{
        //    var args = new ParametersParser().Parser(new CommandParametersParser().Parse(new CommandParser().Parse(command)));

        //    return await context.CommandHost
        //}

        private async Task<string> ReadCommandAsync(
            CommandHostContext content)
        {
            await _output.WriteLineAsync();
            await _output.WriteAsync("Ripcord_Ly2 >");
            return await _input.ReadLineAsync();
        }

        private CommandHostContext CommandHostContext()
        {
            return _commandHostContextProvider.CreateContext();
        }

        private async Task DisplayUsageHelpAsync()
        {
            await _output.WriteLineAsync("   =================");
            await _output.WriteLineAsync();
            await _output.WriteLineAsync("   help|h|?");
            await _output.WriteLineAsync("       Displays this message");
            await _output.WriteLineAsync();
            await _output.WriteLineAsync("   exit|quit|e|q");
            await _output.WriteLineAsync("       Terminates the interactive session");
            await _output.WriteLineAsync();
            await _output.WriteLineAsync("   cls");
            await _output.WriteLineAsync("       Clears the console screen");

            await _output.WriteLineAsync();
        }


        private async Task DisplayInteractiveHelpAsync()
        {
           
            await _output.WriteLineAsync("   =================");
            await _output.WriteLineAsync();
            await _output.WriteLineAsync("   help|h|?");
            await _output.WriteLineAsync("       Displays this message");
            await _output.WriteLineAsync();
            await _output.WriteLineAsync("   exit|quit|e|q");
            await _output.WriteLineAsync("       Terminates the interactive session");
            await _output.WriteLineAsync();
            await _output.WriteLineAsync("   cls");
            await _output.WriteLineAsync("       Clears the console screen");

            await _output.WriteLineAsync();
        }

    }
}
