using System;
using System.Collections.Generic;
using System.Text;
using Tablator.Infrastructure.Enumerations;

namespace Tablator.BusinessModel.Tablature
{
    public sealed class LanguageResourceContentItemModel
    {
        public int TypeCode { get; }

        public LanguageContentItemEnum Type => (LanguageContentItemEnum)TypeCode;

        public int FieldCode { get; }

        public LanguageContentItemPropertyEnum Field => (LanguageContentItemPropertyEnum)FieldCode;

        public Guid Id { get; }

        public string Content { get; }

        public LanguageResourceContentItemModel(int type, int field, Guid id, string content)
        {
            TypeCode = type;
            FieldCode = field;
            Id = id;
            Content = content;
        }
    }
}