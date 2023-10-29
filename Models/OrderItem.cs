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
    [Column("OrItemID")]
    public int OrItemId { get; set; }

    [Column("OrID")]
    public int OrId { get; set; }

    [Column("BookID")]
    public int BookId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("OrderItems")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("OrId")]
    [InverseProperty("OrderItems")]
    public virtual Order Or { get; set; } = null!;
}
