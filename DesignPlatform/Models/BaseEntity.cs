using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
