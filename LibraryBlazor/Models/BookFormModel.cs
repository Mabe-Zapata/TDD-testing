using System.ComponentModel.DataAnnotations;

namespace LibraryBlazor.Models;

public class BookFormModel
{
    [Required(ErrorMessage = "El código del libro es obligatorio")]
    [StringLength(20, ErrorMessage = "El código no puede exceder 20 caracteres")]
    [Display(Name = "Código")]
    public string Code { get; set; } = "";

    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
    [Display(Name = "Título")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "El autor es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre del autor no puede exceder 100 caracteres")]
    [Display(Name = "Autor")]
    public string Author { get; set; } = "";

    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    [Display(Name = "Descripción")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "La cantidad de copias es obligatoria")]
    [Range(1, 100, ErrorMessage = "La cantidad de copias debe estar entre 1 y 100")]
    [Display(Name = "Cantidad de Copias")]
    public int Copies { get; set; } = 1;
}
