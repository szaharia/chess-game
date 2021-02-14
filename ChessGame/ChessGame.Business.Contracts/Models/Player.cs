using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessGame.Business.Contracts.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Range(0,3000, ErrorMessage = "Rating needs to be an integer in range 0 - 3000")]
        public int Rating { get; set; }

        public List<Game> Games { get; set; }
    }
}
