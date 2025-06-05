using Microsoft.AspNetCore.SignalR;
using Backend.Hubs;
using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;

namespace Backend.Services{
    public class NotificationServices{
        private readonly GymDatabase database;
        private readonly IHubContext<NotificationHub> notifyHub;

        public NotificationServices(GymDatabase database, IHubContext<NotificationHub> hubContext)
        {
            this.database = database;
            this.notifyHub = hubContext;
        }

        //Notify all connected clients
        public async Task NotifyAllUsersAsync(string meetingDetails)
        {
            await notifyHub.Clients.All.SendAsync("ReceiveMeetingNotification", meetingDetails);
        }

        // Notify a specific user
        public async Task NotifyUserAsync(string connectionId, string message)
        {
            await notifyHub.Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
        }
    }
}
