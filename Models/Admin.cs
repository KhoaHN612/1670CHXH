using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("Admin")]
public partial class Admin
{
    [Key]
    [Column("AdID", TypeName = "decimal(18, 0)")]
    public decimal AdId { get; set; }

    [Column("UID", TypeName = "decimal(18, 0)")]
    public decimal Uid { get; set; }
}
