﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger
{
    public interface ICustomErrorLogger
    {

        Task ErrorLogger(ErrorLogViewModel errorLogViewModel);
    }
}
