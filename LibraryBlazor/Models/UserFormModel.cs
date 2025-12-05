using System.ComponentModel.DataAnnotations;

namespace LibraryBlazor.Models;

public class UserFormModel
{
    [Required(ErrorMessage = "El código del usuario es obligatorio")]
    [StringLength(20, ErrorMessage = "El código no puede exceder 20 caracteres")]
    [Display(Name = "Código")]
    public string Code { get; set; } = "";

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
    [Display(Name = "Nombre")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(50, ErrorMessage = "El apellido no puede exceder 50 caracteres")]
    [Display(Name = "Apellido")]
    public string LastName { get; set; } = "";
}
