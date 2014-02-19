using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace WindowsFormsApplication1 {
    
    class Loop {

        private Form1 f;
        private bool running;
        private Bitmap buffer;
        private DrawManager drawManager;

        private List<Shape> shapeList = new List<Shape>();


        public Loop(Form1 f){
            this.f = f;
            this.buffer = new Bitmap(1024, 768);

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

            using (Graphics g = Graphics.FromImage(buffer)) {
                drawManager.draw(g);
            }

            f.repaint(buffer);

        }

        private void calc() {
            //Console.WriteLine("Calc");
        }

        private void initialize() {
            //Console.WriteLine("Init");
            running = true;

            World w = new World(1, 10);
            drawManager.addDrawable(w);

            w.setToken(3, 5, World.Token.Black);


            /*
            Vector3D diamondBase = new Vector3D(176, -80, 0);
            Shape diamond = new Diamond3D(diamondBase);

            shapeList.Add(diamond);



            Vector3D boardBase = new Vector3D(340, -176, 0);
            Shape board = new Board3D(boardBase);

            shapeList.Add(board);



            int startX = 364, startY = -152;

            for (int i = 0; i < 10; i++) {
                Vector3D basePoint = new Vector3D(startX + (48*i), startY + (48*i), 0);
                Shape tokenTest = new Token3D(basePoint, Token3D.PlayerColor.White);

                Console.WriteLine("X: " + (startX + (48 * i)));
                Console.WriteLine("Y: " + (startY + (48 * i)));
                Console.WriteLine("----------------");

                shapeList.Add(tokenTest);
            }
            */
            
        }
       

        public DrawManager getDrawManager() {
            return drawManager;
        }
    
    }
}
