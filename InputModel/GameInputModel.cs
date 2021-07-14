using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogue.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "This field must have between 3 and 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "This field must have between 3 and 100 characters")]
        public string Producer { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "The price must be between 1 and 1000 euros")]
        public double Price { get; set; }
    }
}
