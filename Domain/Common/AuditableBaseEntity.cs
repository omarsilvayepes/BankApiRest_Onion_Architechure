namespace Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public  string CreateBy { get; set; }
        public  DateTime CreateDate { get; set;}
        public  DateTime UpdateDate { get; set;}
        public string LastModifiedBy { get; set; }
    }
}
