using System.Collections.Generic;

namespace Kwetter.BuildingBlocks.KwetterDomain
{
    public abstract class BaseEntity
    {
        private int? _requestedHashCode;

        public virtual int Id { get; protected set; }
        private List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(DomainEvent eventItem)
        {
            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(DomainEvent eventItem) => _domainEvents?.Remove(eventItem);
        public void ClearDomainEvents() => _domainEvents?.Clear();
        public bool IsTransient() => Id == default;

        public override bool Equals(object obj)
        {
            if (!(obj is BaseEntity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (BaseEntity) obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                _requestedHashCode ??= Id.GetHashCode() ^ 31;
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(BaseEntity left, BaseEntity right) => left?.Equals(right) ?? Equals(right, null);
        public static bool operator !=(BaseEntity left, BaseEntity right) => !(left == right);
    }
}