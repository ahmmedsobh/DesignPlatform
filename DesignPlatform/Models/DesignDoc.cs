using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class DesignDoc 
    {
        public int Id { get; set; }
        public string DocPath { get; set; }
        public int ProjectId { get; set; }
        public int Type { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
    }
}
