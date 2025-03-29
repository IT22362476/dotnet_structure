using Inv.Application.Request;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IMessagingHubService
    {
        Task<bool> SendMessageAsync(CreateUserMessageRequest messageRequest);
        Task<List<CreateUserMessageRequest>> GetMessagesAsync(int userId);
    }
}
