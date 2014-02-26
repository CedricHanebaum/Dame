using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace ISO 
{

	static class Program 
	{

        private static Form1 f;
        private static Thread t;

		static void Main(string[] args) 
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			f = new Form1();
			Loop l = new Loop(f);
			t = new Thread(l.doLoop);
			t.Start();

			Application.Run(f);
		}

        public static void exit()
        {
            t.Join();
            f.Close();
        }
	}
}
