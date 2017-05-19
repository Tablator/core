namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessModel;
    using Infrastructure.Enumerations;

    public interface ITablatureService
    {
        TablatureModel Get(Guid id);
    }
}