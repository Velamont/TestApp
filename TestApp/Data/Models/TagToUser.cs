using System.ComponentModel.DataAnnotations;

namespace TestApp.Data.Models;

public class TagToUser 
{
    public Guid EntityId { get; set;}
    public Guid UserId { get; set; }
    public Guid TagId { get; set; }
  
  
    public User? User { get; set; }
    public List<Tag>? Tags { get; set; }
}