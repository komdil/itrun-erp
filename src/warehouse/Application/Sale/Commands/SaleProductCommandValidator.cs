using System.ComponentModel.DataAnnotations;

namespace Application.Sale.Commands
{
    public class SaleProductCommandValidator
    {
        [Required]
        public string ProductName { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

    }
}
