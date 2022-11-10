
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
    [Cmdlet(VerbsCommunications.Write, "AsciiProgressBar")]
    public class WriteAsciiProgressBar :   BaseNativeProgressBarCmdlet, IDynamicParameters
    {
        private CommonParmeters CommonParameters = new CommonParmeters();
        object IDynamicParameters.GetDynamicParameters()
        {
            return this.CommonParameters;
        }
            
        [Parameter(Position = 0, Mandatory = true)]
        public int Percentage
        {
            get { return percentage; }
            set { percentage = value; }
        }
        private int percentage;

        [Parameter(Position = 1, Mandatory = true)]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        private string message;

        [Parameter(Position = 2, Mandatory = false)]
        public int UpdateDelay
        {
            get { return updatedelay; }
            set { updatedelay = value; }
        }
        private int updatedelay = 100;

        [Parameter(Position = 3, Mandatory = false)]
        public int ProcessDelay
        {
            get { return processdelay; }
            set { processdelay = value; }
        }
        private int processdelay = 5;

        [Parameter(Position = 4, Mandatory = false)]
        public ConsoleColor ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; }
        }
        private ConsoleColor foregroundColor;


        [Parameter(Position = 5, Mandatory = false)]
        public ConsoleColor BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        private ConsoleColor backgroundColor;

        protected static int origRow;
        protected static int origCol;

        protected static void WriteExt(string s, int x = -1, int y=-1, ConsoleColor foregroudColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black, bool clearline = false, bool noNewLine = true)
        {
            try
            {
                ConsoleColor bg_color = Console.BackgroundColor;
                ConsoleColor fg_color = Console.ForegroundColor;
                int cursor_top = Console.CursorTop;
                int cursor_left = Console.CursorLeft;

                int new_cursor_x = cursor_left;
                if (x > 0) {
                    new_cursor_x = x;
                }

                int new_cursor_y = cursor_top;
                if (y > 0){
                    new_cursor_y = y;
                }

                if (clearline) {
                    int len = Console.WindowWidth - 1;

                    var empty = new string(' ', len);

                    Console.SetCursorPosition(0, new_cursor_y);
                    Console.Write(empty);
                }

                Console.BackgroundColor = backgroundColor;
                Console.ForegroundColor = foregroudColor;

                Console.SetCursorPosition(new_cursor_x, new_cursor_y);
                Console.Write(s);
                Console.WriteLine();
                if (noNewLine) {
                    Console.SetCursorPosition(cursor_left, cursor_top); 
                }

                Console.BackgroundColor = bg_color;
                Console.ForegroundColor = fg_color;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

    
        protected override void ProcessRecord()
        {
            bool wasCursorVisible = Console.CursorVisible;
            
            if (Globals.GShowCursor == false)
            {
                Console.CursorVisible = false;
            }

            TimeSpan timeSpan = Globals.GProgressSw.Elapsed;
            Double elapsedMillisecs = timeSpan.TotalMilliseconds;
            if (elapsedMillisecs < updatedelay)
            {
                return;
            }
            Console.OutputEncoding = Encoding.Unicode;
          
            char[] spinners = new char[4];
            spinners[0] = '-';
            spinners[1] = '\\';
            spinners[2] = '|';
            spinners[3] = '/';

            Globals.GCurrentSpinnerIndex++;

            if (Globals.GCurrentSpinnerIndex >= 4)
            {
                Globals.GCurrentSpinnerIndex = 0;
            }

            char currentSpinner = spinners[Globals.GCurrentSpinnerIndex];

            Double elapsedSeconds = timeSpan.TotalSeconds;
            Globals.GProgressSw.Restart();

            Double tmpVal = (Globals.GMax / 100) * percentage; 
            Globals.GPos = Convert.ToInt32(Math.Round(tmpVal));
           
            string p = "";
            for (int i = 0; i < Globals.GPos; i++)
            {
                p += '.';
            }
            p += currentSpinner;
            for (int i = Convert.ToInt32(Globals.GPos); i < Globals.GMax; i++)
            {
                p += ' ';
            }

            string strprogress = String.Format("[{0}] {1}", p, message);

            WriteExt(strprogress, -1, -1, foregroundColor, backgroundColor, true, true);

            Console.CursorVisible = wasCursorVisible;

        }
    }

}
