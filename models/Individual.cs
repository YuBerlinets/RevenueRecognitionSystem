using System.ComponentModel.DataAnnotations;

namespace apbd_project.models;

public class Individual : Client
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string PESEL { get; set; }
}