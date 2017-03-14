namespace Tablator.BusinessModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HierarchyModel
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string Picture { get; }

        public int Position { get; }

        public Guid? ParentId { get; }

        public HierarchyModel Ascendance { get; }

        public HierarchyCollectionModel Descendance { get; }

        public HierarchyTabReferenceCollectionModel Tablatures { get; internal set; }

        public bool Root => ParentId == null || ParentId.Value == Guid.Empty ? true : false;

        public bool HasChildren => Descendance == null || Descendance.Count == 0 ? true : false;

        public HierarchyModel(Guid id, string name, string desc, string pic, int posi)
        {
            Id = id;
            Name = name;
            Description = desc;
            Picture = pic;
            Position = posi;

            Descendance = new HierarchyCollectionModel();
        }

        public void AddTablatures(IEnumerable<HierarchyTabReferenceModel> tabs)
        {
            if (tabs == null)
                return;

            if (tabs.Count() == 0)
                return;

            Tablatures.AddRange(tabs);
        }

        public void AddTablatures(HierarchyTabReferenceCollectionModel tabs)
        {
            if (tabs == null)
                return;

            if (tabs.Count == 0)
                return;

            Tablatures.AddRange(tabs);
        }
    }

    public class HierarchyCollectionModel : List<HierarchyModel>
    {
        public HierarchyCollectionModel()
        { }

        public HierarchyCollectionModel GetRootLevel()
        {
            HierarchyCollectionModel ret = new HierarchyCollectionModel();
            ret.AddRange(this.Where(x => !x.ParentId.HasValue));
            return ret;
        }
    }
}