using ChessGame.Business.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChessGame.Business.Contracts.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Display(Name = "White Player Id")]
        public int WhitePlayerId { get; set; }

        public Player WhitePlayer { get; set; }

        [Display(Name = "Black Player Id")]
        public int BlackPlayerId { get; set; }

        public Player BlackPlayer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression("[A-E]{1}[0-9]{2}", ErrorMessage = "Opening Classification needs to contain 1 upper case letter (A-E), followed by 2 digits")]
        [StringLength(3)]
        [Display(Name = "Opening Classification")]
        public string OpeningClassification { get; set; }

        public Winner Result { get; set; }
    }
}
