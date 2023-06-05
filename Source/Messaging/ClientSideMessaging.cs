using Microsoft.AspNetCore.SignalR;

namespace AppSettings.Messaging;

public class ClientSideMessaging : Hub
{
  public override async Task OnConnectedAsync()
  {
    await base.OnConnectedAsync();
  }

  public async Task SubscribeServerMessage(string id)
  {
    await Groups.AddToGroupAsync(Context.ConnectionId, id);
  }

  public async Task UnsubscribeServerMessage(string id)
  {
    await Groups.RemoveFromGroupAsync(Context.ConnectionId, id);
  }
}