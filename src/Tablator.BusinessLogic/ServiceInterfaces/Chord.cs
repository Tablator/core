﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.BusinessLogic.Services
{
    public interface IChordService
    {
        T Get<T>(string name);
    }
}