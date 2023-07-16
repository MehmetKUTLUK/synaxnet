using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace synaxnet.Data
{
    public class Product
    {
        public Guid Id { get; set; }

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string Name { get; set; }
        [Display(Name = "Görsel")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string Photo { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }

        [Display(Name = "Masal")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string Text { get; set; }
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; } = DateTime.UtcNow;
      


    }
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasIndex(x => x.Name)
                .IsUnique(false);
            builder
                .Property(x => x.Text)
                .IsRequired(true);
           


        }
    }
}
