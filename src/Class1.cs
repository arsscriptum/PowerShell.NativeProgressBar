﻿using System;
using System.Text;
using System.Management.Automation;
using System.Diagnostics;

namespace AsciiProgressBar
{
    static class Globals
    {

        static DateTime _startTime;
        public static DateTime GStartTime
        {
            set { _startTime = value; }
            get { return _startTime; }
        }
        static int _currentSpinnerIndex;
        public static int GCurrentSpinnerIndex
        {
            set { _currentSpinnerIndex = value; }
            get { return _currentSpinnerIndex; }
        }
        static double _max;
        public static double GMax
        {
            set { _max = value; }
            get { return _max; }
        }

        static double _half;
        public static double GHalf
        {
            set { _half = value; }
            get { return _half; }
        }

   
        static double _pos;
        public static double GPos
        {
            set { _pos = value; }
            get { return _pos; }
        }

        static Encoding _encoding;
        public static Encoding GEncoding
        {
            set { _encoding = value; }
            get { return _encoding; }
        }

        // global int using get/set
        static double _gsize;
        public static double GSize
        {
            set { _gsize = value; }
            get { return _gsize; }
        }
        public static Stopwatch GProgressSw
        {
            set { _stopwatch = value; }
            get { return _stopwatch; }
        }
        static Stopwatch _stopwatch = new Stopwatch();
    }
    internal class MyCommonParmeters
    {

        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = false)]
        public double Size
        {
            get { return Globals.GSize; }
            set { Globals.GSize = value; }
        }
  

    }
    // Declare the class as a cmdlet and specify the
    // appropriate verb and noun for the cmdlet name.
    [Cmdlet(VerbsLifecycle.Register, "AsciiProgressBar")]
    public class RegisterAsciiProgressBar : PSCmdlet, IDynamicParameters
    {
        private MyCommonParmeters MyCommonParameters = new MyCommonParmeters();

        object IDynamicParameters.GetDynamicParameters()
        {
            return this.MyCommonParameters;
        }


        // Override the ProcessRecord method to process
        // the supplied user name and write out a
        // greeting to the user by calling the WriteObject
        // method.
        protected override void ProcessRecord()
        {
            Console.WriteLine("Current Encoding is {0}, set to {1}", Console.OutputEncoding, Encoding.Unicode);
            Globals.GEncoding = Console.OutputEncoding;
            Console.OutputEncoding = Encoding.Unicode;

            Console.WriteLine("Set Values!");
            Globals.GProgressSw.Reset();
            Globals.GProgressSw.Start();

            Globals.GStartTime = DateTime.Now;
            Globals.GMax = MyCommonParameters.Size;
            Globals.GSize = MyCommonParameters.Size;
            Globals.GHalf = MyCommonParameters.Size  /2 ;
            Globals.GPos = 0;
            Globals.GCurrentSpinnerIndex = 0;
        }
    }


    [Cmdlet(VerbsLifecycle.Unregister, "AsciiProgressBar")]
    public class UnregisterAsciiProgressBar : Cmdlet
    {


        protected override void ProcessRecord()
        {

            Console.WriteLine("Current Encoding is {0}, set back to {1}", Console.OutputEncoding, Globals.GEncoding);
            Console.OutputEncoding = Globals.GEncoding;

            Console.WriteLine("StopTimer");
            Globals.GProgressSw.Reset();
            Globals.GProgressSw.Stop();
        }
    }


    // Declare the class as a cmdlet and specify the
    // appropriate verb and noun for the cmdlet name.
    [Cmdlet(VerbsCommunications.Write, "AsciiProgressBar")]
    public class WriteAsciiProgressBar :   PSCmdlet, IDynamicParameters
    {
        private MyCommonParmeters MyCommonParameters = new MyCommonParmeters();

        object IDynamicParameters.GetDynamicParameters()
        {
            return this.MyCommonParameters;
        }
            
        [Parameter(Mandatory = true)]
        public int Percentage
        {
            get { return percentage; }
            set { percentage = value; }
        }
        private int percentage;

        [Parameter(Mandatory = true)]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        private string message;

        [Parameter(Mandatory = false)]
        public int UpdateDelay
        {
            get { return updatedelay; }
            set { updatedelay = value; }
        }
        private int updatedelay;

        [Parameter(Mandatory = false)]
        public int ProcessDelay
        {
            get { return processdelay; }
            set { processdelay = value; }
        }
        private int processdelay;

        [Parameter(Mandatory = false)]
        public ConsoleColor ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; }
        }
        private ConsoleColor foregroundColor;


        [Parameter(Mandatory = false)]
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

            WriteExt(strprogress, -1, -1, Console.ForegroundColor, Console.BackgroundColor, true, true);
           


        }
    }

}