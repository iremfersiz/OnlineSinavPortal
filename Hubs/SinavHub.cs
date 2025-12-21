using Microsoft.AspNetCore.SignalR;

namespace OnlineSinavPortal.Hubs
{
    public class SinavHub : Hub
    {
        public async Task SendSinavBildirimi(string sinavBaslik, string mesaj)
        {
            await Clients.All.SendAsync("YeniSinavBildirimi", sinavBaslik, mesaj);
        }

        public async Task SendSinavIstatistik(int sinavId, int katilimciSayisi, int ortalamaPuan)
        {
            await Clients.Group("Admins").SendAsync("SinavIstatistik", sinavId, katilimciSayisi, ortalamaPuan);
        }

        public async Task JoinAdminGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }

        public async Task LeaveAdminGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
        }
    }
}

