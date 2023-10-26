using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("Customer")]
public partial class Customer
{
    [Key]
    [Column("CusID", TypeName = "decimal(18, 0)")]
    public decimal CusId { get; set; }

    [Column("UID", TypeName = "decimal(18, 0)")]
    public decimal Uid { get; set; }

    [StringLength(50)]
    public string? HomeAddress { get; set; }
}
