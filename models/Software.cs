using System.ComponentModel.DataAnnotations;

namespace apbd_project.models;

public class Software
{
    [Key] public int SoftwareId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public string Category { get; set; }
}