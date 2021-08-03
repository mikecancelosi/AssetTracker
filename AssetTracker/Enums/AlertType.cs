using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Enums
{
    /// <summary>
    /// Used to set [Alert].[ar_type] values.
    /// </summary>
    public static class AlertType
    {
        public const string DiscussionReply = "Discussion Reply";
        public const string AssetAssigned = "Assign";
        public const string ReviewAssigned = "AssignForReview";
        public const string AssetUpdate = "AssetUpdate";
        public const string ProjectWideAlert = "ProjectWide";
    }
}
