using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("User")]
public partial class User
{
    [Key]
    [Column("UID", TypeName = "decimal(18, 0)")]
    public decimal Uid { get; set; }

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    public string FullName { get; set; } = null!;

    [StringLength(50)]
    public string Role { get; set; } = null!;

    [StringLength(50)]
    public string Status { get; set; } = null!;
}
