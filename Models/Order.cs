using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    [Column("OrID", TypeName = "decimal(18, 0)")]
    public decimal OrId { get; set; }

    [Column("CusID", TypeName = "decimal(18, 0)")]
    public decimal CusId { get; set; }

    [Column(TypeName = "date")]
    public DateTime OrDate { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
