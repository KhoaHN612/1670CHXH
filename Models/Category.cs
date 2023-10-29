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
    [Column("CatID")]
    public int CatId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public string AddBy { get; set; }

    public byte Status { get; set; }

    [InverseProperty("Cat")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
