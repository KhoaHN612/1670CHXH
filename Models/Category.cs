using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("Category")]
public partial class Category
{
    [Key]
    [Column("CatID", TypeName = "decimal(18, 0)")]
    public decimal CatId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(18, 0)")]
    public decimal AddBy { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [InverseProperty("Cat")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
