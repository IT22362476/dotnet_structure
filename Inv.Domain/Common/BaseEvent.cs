using MediatR;


namespace Inv.Domain.Common
{

public abstract class BaseEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }
}
