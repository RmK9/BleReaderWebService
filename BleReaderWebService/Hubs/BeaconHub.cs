using Microsoft.AspNet.SignalR;

namespace BleReaderWebService.Hubs
{
    public class BeaconHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}