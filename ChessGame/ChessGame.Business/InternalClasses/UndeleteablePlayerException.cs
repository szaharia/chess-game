using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Business.InternalClasses
{
    public class UndeleteablePlayerException: Exception
    {
        public UndeleteablePlayerException(string exception):base(exception)
        {

        }
    }
}
