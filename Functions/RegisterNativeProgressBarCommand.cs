
/*
#̷𝓍   𝓐𝓡𝓢 𝓢𝓒𝓡𝓘𝓟𝓣𝓤𝓜
#̷𝓍   🇵​​​​​🇴​​​​​🇼​​​​​🇪​​​​​🇷​​​​​🇸​​​​​🇭​​​​​🇪​​​​​🇱​​​​​🇱​​​​​ 🇸​​​​​🇨​​​​​🇷​​​​​🇮​​​​​🇵​​​​​🇹​​​​​ 🇧​​​​​🇾​​​​​ 🇬​​​​​🇺​​​​​🇮​​​​​🇱​​​​​🇱​​​​​🇦​​​​​🇺​​​​​🇲​​​​​🇪​​​​​🇵​​​​​🇱​​​​​🇦​​​​​🇳​​​​​🇹​​​​​🇪​​​​​.🇶​​​​​🇨​​​​​@🇬​​​​​🇲​​​​​🇦​​​​​🇮​​​​​🇱​​​​​.🇨​​​​​🇴​​​​​🇲​​​​​
*/


using System;
using System.Text;
using System.Management.Automation;
using System.Diagnostics;
using NativeProgressBar.Utility;

namespace NativeProgressBar.Functions
{
   
    [Cmdlet(VerbsLifecycle.Register, "AsciiProgressBar")]
    public class RegisterAsciiProgressBar : BaseNativeProgressBarCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public double Size
        {
            get { return _size; }
            set { _size = value; }
        }
        private double _size;

        [Parameter( Mandatory = false )]
        public bool ShowCursor
        {
            get { return _showCursor; }
            set { _showCursor = value; }
        }
        private bool _showCursor = false;

        protected override void ProcessRecord()
        {
            Globals.GEncoding = Console.OutputEncoding;
            Console.OutputEncoding = Encoding.Unicode;

            Globals.GProgressSw.Reset();
            Globals.GProgressSw.Start();

            Globals.GStartTime = DateTime.Now;
            Globals.GMax = Size;
            Globals.GSize = Size;
            Globals.GHalf = Size  /2 ;
            Globals.GPos = 0;
            Globals.GCurrentSpinnerIndex = 0;

            Globals.GShowCursor = ShowCursor;
        }
    }


}
