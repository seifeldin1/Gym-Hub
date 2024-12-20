using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs{
    public class NotificationHub : Hub{
        // This method can be called by the server to send notifications to connected clients
        public async Task SendNotification(string meetingDetails)
        {
            await Clients.All.SendAsync("ReceiveMeetingNotification", meetingDetails);
        }
    }
}
