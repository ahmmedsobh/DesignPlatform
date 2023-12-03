using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPlatform.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Displayed { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public ApplicationUser Sender { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public ApplicationUser Receiver { get; set; }



    }
}
