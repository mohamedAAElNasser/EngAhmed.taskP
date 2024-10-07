
namespace EngAhmed.TaskP.Domain.Commons
{
    public class EntityAudit<T> : EntityBase<T>
    {
     public DateTime CreatedDate {  get; set; }   
    }
}
