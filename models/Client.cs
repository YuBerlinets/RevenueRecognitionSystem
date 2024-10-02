using System.ComponentModel.DataAnnotations;

namespace apbd_project.models;

public abstract class Client
{
    [Key] public int ClientId { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDeleted { get; set; } = false;
    
}