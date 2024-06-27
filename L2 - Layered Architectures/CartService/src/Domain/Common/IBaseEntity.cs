
namespace CartService.Domain.Common;

public interface IBaseEntity
{
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }
    int Id { get; set; }

    void AddDomainEvent(BaseEvent domainEvent);
    void ClearDomainEvents();
    void RemoveDomainEvent(BaseEvent domainEvent);
}