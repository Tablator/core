namespace Tablator.BusinessModel
{
    using DomainModel;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CatalogModel
    {
        private HierarchyCollectionModel Hierarchy { get; set; }

        public CatalogModel()
        {
            Hierarchy = new HierarchyCollectionModel();
        }

        public static explicit operator CatalogModel(CatalogHierarchyCollectionLevel hierarchyLevels)
        {
            CatalogModel cat = new CatalogModel();

            if (hierarchyLevels == null || hierarchyLevels.HierarchyLevels == null || hierarchyLevels.HierarchyLevels.Count() == 0)
                return new CatalogModel();

            hierarchyLevels.HierarchyLevels.Where(x => !x.ParentId.HasValue).ToList().ForEach(delegate (CatalogHierarchyLevel h)
            {
                cat.Hierarchy.Add(new HierarchyModel(h.Id, h.Name, h.Description, h.Picture, h.Position));
            });

            foreach (HierarchyModel h in cat.Hierarchy)
                cat.LoadDescendanceRecursive(h, ref hierarchyLevels);

            return cat;
        }

        private void LoadDescendanceRecursive(HierarchyModel h, ref CatalogHierarchyCollectionLevel hierarchyLevels)
        {
            Guid? _id = h.Id == Guid.Empty ? null : (Guid?)h.Id;
            foreach (CatalogHierarchyLevel _h in hierarchyLevels.HierarchyLevels.Where(x => x.ParentId == _id))
                h.Descendance.Add(new HierarchyModel(_h.Id, _h.Name, _h.Description, _h.Picture, _h.Position));

            _id = null;

            if (h.Descendance == null || h.Descendance.Count == 0)
                return;

            foreach (HierarchyModel __h in h.Descendance)
                LoadDescendanceRecursive(__h, ref hierarchyLevels);
        }

        public HierarchyCollectionModel GetArborescence() => Hierarchy;
    }
}