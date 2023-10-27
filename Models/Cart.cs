using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("Cart")]
public partial class Cart
{
    [Key]
    [Column("CartID")]
    public int CartId { get; set; }

    [Column("UID")]
    public string Uid { get; set; } = null!;

    [Column("BookID")]
    public int BookId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Carts")]
    public virtual Book Book { get; set; } = null!;
}
