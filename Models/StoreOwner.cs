using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("StoreOwner")]
public partial class StoreOwner
{
    [Key]
    [Column("OwnID", TypeName = "decimal(18, 0)")]
    public decimal OwnId { get; set; }

    [Column("UID", TypeName = "decimal(18, 0)")]
    public decimal Uid { get; set; }
}
