﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Business.InternalClasses
{
    public class GameValidationException: Exception
    {
        public GameValidationException(string message): base(message)
        {

        }
    }
}
