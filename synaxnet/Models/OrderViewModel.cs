
using System.ComponentModel.DataAnnotations;

namespace synaxnet.Models;

public class OrderViewModel
{
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public string? Name { get; set; }


    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name = "Telefon Numarası")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    
    public string? PhoneNumber { get; set; }

    [Display(Name = "il")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public string? City { get; set; }

    [Display(Name = "il")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public string? Address { get; set; }

  

    public string? ReturnUrl { get; set; }
}
