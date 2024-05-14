using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apirest.Model
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El precio de la categoría es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un número válido.")]
        public decimal Price { get; set; } = decimal.Zero;

        [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser un número válido.")]
        public decimal Amount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El saldo debe ser un número válido.")]
        public decimal Balance { get; set; }

    }
}
