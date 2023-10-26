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
    [Column("CartID", TypeName = "decimal(18, 0)")]
    public decimal CartId { get; set; }

    [Column("CusID", TypeName = "decimal(18, 0)")]
    public decimal CusId { get; set; }

    [Column("BookID", TypeName = "decimal(18, 0)")]
    public decimal BookId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Quantitity { get; set; }
}
