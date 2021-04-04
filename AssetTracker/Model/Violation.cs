using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public class Violation
    {
        public string ErrorMessage { get; private set; }
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
