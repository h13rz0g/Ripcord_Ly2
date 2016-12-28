namespace Ripcord_Ly2.Hosting.HostContext
{
    public interface ICommandHostContextProvider
    {
        CommandHostContext CreateContext();
        void Shutdown(CommandHostContext context);
    }
}
