namespace Tablator.BusinessModel.Tablature
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Globalization;
    using Tablator.Infrastructure.Enumerations;

    public sealed class LanguageResourceModel
    {
        public string LangCode { get; set; }

        public string Comment { get; set; }

        public List<LanguageResourceContentItemModel> Content { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public LanguageResourceModel(DomainModel.LanguageResource src)
        {
            LangCode = src.Lang;
            Comment = src.Comment;
            Tags = src.Tags;

            Content = new List<LanguageResourceContentItemModel>();
            if (src.Content != null && src.Content.Count() > 0)
                foreach (DomainModel.LanguageResourceContentItem lrc in src.Content)
                    Content.Add(new LanguageResourceContentItemModel(lrc.Type, lrc.Field, lrc.Id, lrc.Content));
        }
    }

    public sealed class LanguageResourceCollectionModel
    {
        public List<LanguageResourceModel> Resources { get; private set; }

        public LanguageResourceCollectionModel()
        { }

        public LanguageResourceCollectionModel(IEnumerable<DomainModel.LanguageResource> src)
        {
            Resources = new List<LanguageResourceModel>();

            foreach (DomainModel.LanguageResource res in src)
                Resources.Add(new LanguageResourceModel(res));
        }

        public string GetPartName(Guid id, CultureInfo ci) => Resources?.Where(x => x.LangCode == ci.TwoLetterISOLanguageName).FirstOrDefault()?.Content?.Where(x => x.FieldCode == (int)LanguageContentItemPropertyEnum.Nom && x.TypeCode == (int)LanguageContentItemEnum.Partie && x.Id == id).Select(x => x.Content).FirstOrDefault();
    }
}