using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1 {

	static class Program {



		static void Main(string[] args) {


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Form1 f = new Form1();
			Loop l = new Loop(f);
			Thread t1 = new Thread(l.doLoop);
			t1.Start();

			Application.Run(f);

		}
	}
}
