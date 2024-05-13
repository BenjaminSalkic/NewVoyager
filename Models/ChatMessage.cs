namespace NewVoyager.Models;

 public class ChatMessage
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

    // Foreign key properties
    public int PlanId { get; set; }
    public string SenderId { get; set; }

    // Navigation properties
    public Plans Plan { get; set; }
    public ApplicationUser Sender { get; set; }
}
