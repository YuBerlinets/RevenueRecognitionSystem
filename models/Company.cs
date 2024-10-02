using System.ComponentModel.DataAnnotations;

namespace apbd_project.models;

public class Company : Client
{
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string KRS { get; set; }
}