using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessGame.Data.Contracts.Entities
{
    public class Game
    {
        public int Id { get; set; }
        
        public int? WhitePlayerId { get; set; }

        public Player WhitePlayer { get; set; }

        public int? BlackPlayerId { get; set; }

        public Player BlackPlayer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression("[A-E]{1}[0-9]{2}", ErrorMessage ="Opening Classification needs to contain 1 letter (A-E), followed by 2 digits")]
        [StringLength(3)]
        public string OpeningClassification { get; set; }

        [Column(TypeName = "nvarchar(7)")]
        public Winner Result { get; set; }
    }
}
