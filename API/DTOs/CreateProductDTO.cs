using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Range must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]

        public string Brand { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Quantiity must be at least 1")]
        public int QuantityInStock { get; set; }
    }
}