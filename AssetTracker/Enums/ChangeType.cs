﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Enums
{
    /// <summary>
    /// Used to set [Change].[ch_description]
    /// </summary>
    public static class ChangeType
    {
        public static string CreatedAsset => "Asset Created";
        public static string ChangedPhase => "Phase Changed";
        public static string NameChange => "Name Changed";
        public static string UserAssigned => "User Assign";
    }
}
