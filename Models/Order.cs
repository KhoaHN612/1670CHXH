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
    [Column("OrID")]
    public int OrId { get; set; }

    [Column("CusID")]
    public string CusId { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime OrTime { get; set; }

    [InverseProperty("Or")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
