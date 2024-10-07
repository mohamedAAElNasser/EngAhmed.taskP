
using System.ComponentModel.DataAnnotations;


namespace EngAhmed.TaskP.Domain.Commons
{
    public class EntityBase<T>
    {
        [Key]
        public T? Id { get; set; }
    }
}
