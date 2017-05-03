using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.DataAccess.Repositories
{
    public interface IChordRepository
    {
        T Get<T>(string name);
    }
}