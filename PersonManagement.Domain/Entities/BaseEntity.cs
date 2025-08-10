namespace PersonManagement.Domain
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; protected set; } 
        public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; protected set; }
        public bool IsDeleted { get; protected set; } = false;

    }
}
