
using System;
using System.Management.Automation;

namespace NativeProgressBar.Utility
{
    /// <summary>
    /// A base cmdlet object that provides common functionality.
    /// </summary>
    internal class CommonParmeters
    {
        [Parameter(Mandatory = false)]
        public double Size
        {
            get { return Globals.GSize; }
            set { Globals.GSize = value; }
        }
        [Parameter(Mandatory = false)]
        public bool ShowCursor
        {
            get { return Globals.GShowCursor; }
            set { Globals.GShowCursor = value; }
        }
    }
}
