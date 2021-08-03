using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    /// <summary>
    /// Violations are local internal error messages that result
    /// from improper database CRUD operations
    /// </summary>
    public class Violation
    {
        /// <summary>
        /// Message to show
        /// </summary>
        public string ErrorMessage { get; private set; }
        /// <summary>
        /// What property is throwing the issue
        /// </summary>
        public string PropertyName { get; private set; }

        public Violation(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public Violation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
}
