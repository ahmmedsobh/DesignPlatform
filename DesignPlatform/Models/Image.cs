using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int ProjectId { get; set; }
        public int Type { get; set; } // image or inspiration

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
    }
}
