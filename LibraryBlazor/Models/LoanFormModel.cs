using System.ComponentModel.DataAnnotations;

namespace LibraryBlazor.Models;

public class LoanFormModel
{
    [Required(ErrorMessage = "El ID del préstamo es obligatorio")]
    [StringLength(30, ErrorMessage = "El ID no puede exceder 30 caracteres")]
    [Display(Name = "ID del Préstamo")]
    public string LoanId { get; set; } = "";

    [Required(ErrorMessage = "Debe seleccionar un libro")]
    [Display(Name = "Libro")]
    public string BookCode { get; set; } = "";

    [Required(ErrorMessage = "Debe seleccionar un usuario")]
    [Display(Name = "Usuario")]
    public string UserCode { get; set; } = "";

    // Propiedades calculadas para el resumen (no se envían)
    public string? BookTitle { get; set; }
    public string? BookAuthor { get; set; }
    public string? UserFullName { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.Now;
}
