using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_project.models;

public class Contract
{
    [Key] public int ContractId { get; set; }
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Precision(10, 2)]
    public decimal Price { get; set; }
    public bool IsSigned { get; set; }
    [ForeignKey("ClientId")]
    public virtual Client Client { get; set; }
    [ForeignKey("SoftwareId")]
    public virtual Software Software { get; set; }
    public virtual IEnumerable<Payment> Payments { get; set; }
}