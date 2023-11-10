using System.ComponentModel.DataAnnotations;

namespace TestApp.Data.Models;

public class Tag
{
    public Guid TagId { get; set; }
  
    [Required]
    public string Value { get; set; } = default!;
  
    [Required]
    public string Domain { get; set; } = default!;
    
}