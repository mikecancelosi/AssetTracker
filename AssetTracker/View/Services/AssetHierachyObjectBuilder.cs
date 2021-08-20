using DomainModel;
using System.Collections.Generic;

namespace Quipu.View.Services
{
    public  class AssetHierachyObjectBuilder
    {
        public struct AssetHierarchyObject
        {
            public Asset asset { get; set; }
            public int level { get; set; }
            public System.Windows.Thickness marginFactor => new System.Windows.Thickness(level * 10, 0, 0, 10);
            public bool currentObject { get; set; }
        }

        /// <summary>
        /// Create a hierarchy based on the given refAsset
        /// </summary>
        /// <param name="refAsset">The asset to build the hierarchy of</param>
        /// <returns>The complete hierarchy of the given asset</returns>
        public static List<AssetHierarchyObject> CreateHierarchy(Asset refAsset)
        {
            var Hierarchy = new List<AssetHierarchyObject>();

            Asset parentAsset = refAsset;
            while (parentAsset.Parent != null)
            {
                parentAsset = parentAsset.Parent;
            }

            if (parentAsset.Children.Count > 0)
            {
                CreateHierarchyFromParent(refAsset,parentAsset, 0).ForEach(x => Hierarchy.Add(x));
            }

            return Hierarchy;

        }       

        /// <summary>
        /// Creates the heirarchy of objects using recursion
        /// </summary>
        /// <param name="currentAsset">The asset that should be labeled as the currentObject in the heirarchyobject</param>
        /// <param name="parentAsset">The asset used to create the hierarchy</param>
        /// <param name="level">What generation are we in?</param>
        /// <returns>The complete hierarchy of the given parentAsset</returns>
        private static List<AssetHierarchyObject> CreateHierarchyFromParent(Asset currentAsset, Asset parentAsset, int level)
        {
            List<AssetHierarchyObject> objectList = new List<AssetHierarchyObject>();
            objectList.Add(new AssetHierarchyObject()
            {
                asset = parentAsset,
                level = level,
                currentObject = parentAsset.ID == currentAsset.ID
            });

            foreach (Asset child in parentAsset.Children)
            {
                objectList.AddRange(CreateHierarchyFromParent(currentAsset,child, level + 1));
            }

            return objectList;
        }

    }
}
