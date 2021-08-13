﻿namespace DomainModel
{
    /// <summary>
    /// A phase indicates the state of the asset. Many phases will
    /// belong to one category. Phases are custom per category.
    /// Phases should be created in such a way that as phases get
    /// finished, it means the asset is closer to getting completed.
    /// </summary>
    public partial class Phase : DatabaseBackedObject
    {
        public override int ID
        {
            get => ph_id;
            set => ph_id = value;
        }

        public override string Name
        {
            get => ph_name;
            set => ph_name = value;
        }
    }
}
