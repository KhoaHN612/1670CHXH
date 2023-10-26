using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("OrderItem")]
public partial class OrderItem
{
    [Key]
    [Column("OrItemID", TypeName = "decimal(18, 0)")]
    public decimal OrItemId { get; set; }

    [Column("OrderID", TypeName = "decimal(18, 0)")]
    public decimal OrderId { get; set; }

    [Column("BookID", TypeName = "decimal(18, 0)")]
    public decimal BookId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Quantity { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("OrderItems")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;
}
