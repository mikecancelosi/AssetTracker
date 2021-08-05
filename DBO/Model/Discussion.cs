using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    /// <summary>
    /// Discussions are displayed under an asset detail. There will be 
    /// one parent discussion and many child discussions. If a child 
    /// discussion is 'replied' to, the new reply will still go under
    /// the parent.
    /// 
    /// Discussion replies will alert other people who have written in 
    /// the parent discussion
    /// 
    /// Discussion starters will alert whoever is assigned to the asset.
    /// </summary>
    public partial class Discussion : DatabaseBackedObject
    {
        public override int ID
        {
            get => di_id;
            set => di_id = value;
        }

        public override string Name
        {
            get => di_contents;
            set => di_contents = value;
        }
    }
}
