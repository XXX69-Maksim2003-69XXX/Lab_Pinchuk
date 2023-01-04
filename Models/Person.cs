using System.ComponentModel.DataAnnotations;

namespace PinchuckLab.Models;

public class Person
{
    [Key]
    public int Id { get; set; }
    [Required][MaxLength(50)]public string FirstName { get; set; } = string.Empty;
    [Required][MaxLength(50)] public string LastName { get; set; } = string.Empty;
}