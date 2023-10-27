using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Models;

[Table("Book")]
public partial class Book
{
    [Key]
    [Column("BookID")]
    public int BookId { get; set; }

    [StringLength(50)]
    public string Title { get; set; } = null!;

    [StringLength(50)]
    public string? Image { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    [Column("CatID")]
    public int CatId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Price { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("CatId")]
    [InverseProperty("Books")]
    public virtual Category? Cat { get; set; } = null!;
    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
     
}
