using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChessGame.Data.Contracts.Entities
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(0,3000)]
        public int Rating { get; set; }

        //public List<Game> Games { get; set; }
    }
}
