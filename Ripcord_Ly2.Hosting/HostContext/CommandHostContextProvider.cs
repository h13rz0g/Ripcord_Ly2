using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ripcord_Ly2.Hosting.HostContext
{
    public class CommandHostContextProvider : ICommandHostContextProvider
    {
        private readonly string[] _args;
        private readonly IServiceProvider _serviceProvider;
        private readonly ConsoleLogger _logger;

        public CommandHostContextProvider(
            IServiceProvider serviceProvider,
            ConsoleLogger logger,
            string[] args)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _args = args;
        }

        public CommandHostContext CreateContext()
        {
            var context = new CommandHostContext
            {
                RetryResult = Command.CommandReturnCodes.Retry
            };
            _logger.LogInfo("----:Initialize session");
            Initialize(context);
            return context;
        }

        public void Shutdown(CommandHostContext context)
        {
            _logger.LogInfo("###########Shuting down session.###########");
        }


        private void Initialize(CommandHostContext context)
        {
            //context.DisplayUsageHelp=context.Arguments.Switches.ContainsKey("?");
            //if (context.DisplayUsageHelp)
            //{
            //    return;
            //}


            context.StartSessionResult=Command.CommandReturnCodes.Ok;
        }
    }
}
