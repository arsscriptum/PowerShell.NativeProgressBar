
using System;
using System.Management.Automation;

namespace NativeProgressBar.Utility
{
    /// <summary>
    /// A base cmdlet object that provides common functionality.
    /// </summary>
    public class BaseNativeProgressBarCmdlet : Cmdlet
    {

        /// <summary>
        /// The name of this cmdlet activity for progress tracking.
        /// </summary>
        internal string ActivityName { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseNativeProgressBarCmdlet()
        {
        }

   

    }
}
