using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_project.models;

public class Payment
{
    public int Id { get; set; }
    public int ContractId { get; set; }
    [Precision(10, 2)] public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    [ForeignKey("ContractId")] public virtual Contract Contract { get; set; }
}