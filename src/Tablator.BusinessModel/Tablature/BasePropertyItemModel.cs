using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.BusinessModel.Tablature
{
    public abstract class BasePropertyItemModel
    {
        public int Code { get; set; }

        public string Value { get; set; }

        public BasePropertyItemModel(int code, string val)
        {
            Code = code;
            Value = val;
        }
    }
}