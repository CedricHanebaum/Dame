using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Draught;
using System.Diagnostics;
using tmp;

namespace ISO {
	
	class Loop {

		private Form1 f;

		private bool running;
		private long delta;
		private long lastFrame;
		private List<ITickable> updateList = new List<ITickable>();

		private Bitmap buffer;
		private DrawManager drawManager;

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
				this.calcDelta();
				Thread.Sleep(30);
			}
			this.close();
		}

		private void calcDelta() {
			long thisFrame = Loop.getCurrentTime();
			delta = thisFrame - lastFrame;
			lastFrame = thisFrame;
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
			foreach (var t in updateList) {
				t.update(delta);
			}
		}

		private void initialize() {
			this.lastFrame = Loop.getCurrentTime();
			running = true;


			Map map = new Map(8);

			control = new Draught.Control(map, Draught.Control.Players.HumanWhite, Draught.Control.Players.AIBlack, this);

			world = new World(1, 8, control, map);
			drawManager.addDrawable(world);
			f.registerMouseListener(world);
		}
	   

		public DrawManager getDrawManager() {
			return drawManager;
		}

		public void addToUpdateList(ITickable t) {
			updateList.Add(t);
		}

		public void exit() {
			this.running = false;
		}

		private static long getCurrentTime() {
			return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
		}
	
	}
}
