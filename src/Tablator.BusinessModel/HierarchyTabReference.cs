namespace Tablator.BusinessModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public class HierarchyTabReferenceModel
    {
        public string Name { get; }
        public string UrlTitle { get; }

        public Dictionary<Guid, int> Parents { get; internal set; }

        public HierarchyTabReferenceModel(string name, string urlTitle)
        {
            Name = name;
            UrlTitle = urlTitle;
        }

        public void AddAscendance(Guid id, int posi)
        {
            if (id == Guid.Empty)
                return;

            if (posi <= 0)
                posi = 999;

            if (Parents == null)
                Parents = new Dictionary<Guid, int>();

            Parents.Add(id, posi);
        }
    }

    public class HierarchyTabReferenceCollectionModel : List<HierarchyTabReferenceModel>
    {
        public HierarchyTabReferenceCollectionModel()
        { }

        public HierarchyTabReferenceCollectionModel(DomainModel.CatalogHierarchyTabReferenceCollection data)
        {
            if (data == null)
                return;

            if (data.Refs == null)
                return;

            if (data.Refs.Count() == 0)
                return;

            HierarchyTabReferenceModel _tabRef = null;
            foreach (DomainModel.CatalogHierarchyTabReference tabRef in data.Refs)
            {
                _tabRef = new HierarchyTabReferenceModel(tabRef.Name, tabRef.UrlPath);
                foreach (DomainModel.CatalogHierarchyTabParentReference parentRef in tabRef.Parents)
                    _tabRef.AddAscendance(parentRef.Id, parentRef.Position);

                Add(_tabRef);
                _tabRef = null;
            }
        }
    }
}