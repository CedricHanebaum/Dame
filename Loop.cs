using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Draught;

namespace WindowsFormsApplication1 {
	
	class Loop {

		private Form1 f;
		private bool running;
		private Bitmap buffer;
		private DrawManager drawManager;
		private List<FilledShape> shapeList = new List<FilledShape>();

		private World world;
		private Draught.Control control;


		public Loop(Form1 f){
			this.f = f;
			this.buffer = new Bitmap(f.getPanelWidth(), f.getPanelWidth());

			drawManager = new DrawManager();
		}

		public void doLoop() {
			this.initialize();
			while (running) {
				this.calc();
				this.repaint();
				Thread.Sleep(30);
			}
			this.close();
		}

		private void close() {
			Console.WriteLine("Close");
		}

		private void repaint() {
			this.buffer = new Bitmap(f.getPanelWidth(), f.getPanelWidth());

			using (Graphics g = Graphics.FromImage(buffer)) {
				drawManager.draw(g);
			}

			f.repaint(buffer);

		}

		private void calc() {
			// Console.WriteLine("Calc");
		}

		private void initialize() {
			//Console.WriteLine("Init");
			running = true;

            Map map = new Map(10);
            world = new World(1, 10, control, map);
            drawManager.addDrawable(world);
            f.registerMouseListener(world);


			world.setToken(3, 5, World.Token.Black);
			world.setToken(8, 6, World.Token.WhithDraugth);
		}
	   

		public DrawManager getDrawManager() {
			return drawManager;
		}
	
	}
}
