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

        public void NotifyNewBeacon(string name, string number)
        {
            Clients.All.newBeaconScanned(name, number);
        }
    }
}