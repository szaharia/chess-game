using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChessGame.Business.Contracts.Entities
{
    public enum Winner
    {
        [Display(Name = "1-0")]
        WhiteWins,

        [Display(Name = "0-1")]
        BlackWins,

        [Display(Name = "0.5-0.5")]
        Draw
    }
}
