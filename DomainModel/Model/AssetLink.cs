
namespace DomainModel
{
    /// <summary>
    /// We want to save links to the assets in the project. This
    /// helps the user navigate to the object but also allows for 
    /// further implementations into external programs e.g Unity, Unreal
    /// </summary>
   public partial class AssetLink : DatabaseBackedObject
    {
        public override int ID
        {
            get => li_id;
            set => li_id = value;
        }

        public override string Name
        {
            get => li_location;
            set => li_location = value;
        }
    }
}
