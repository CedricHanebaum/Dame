using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1 {
    
    
    class Token3D : FilledShape{

        private Color firstColor;
        private Color secondColor;
        public const int RADIUS = 16;
        public const int RESOLUTION = 16;
        public const int HEIGHT = 8;

        public Token3D(Vector3D basePoint, PlayerColor color):base(basePoint) {

            switch (color) {
                case PlayerColor.Black:
                    firstColor = Color.Blue;
                    secondColor = Color.DarkBlue;
                    break;
                case PlayerColor.White:
                    firstColor = Color.Red;
                    secondColor = Color.DarkRed;
                    break;
            }

            List<Vector3D> up = new List<Vector3D>();
            List<Vector3D> down = new List<Vector3D>();

            for (int i = 0; i <= 16; ++i) {
                int gegenkathete = (int) (Math.Sin((Math.PI / (RESOLUTION / 2)) * i) * RADIUS);
                int ankathete = (int)(Math.Cos((Math.PI / (RESOLUTION / 2)) * i) * RADIUS);

                up.Add(new Vector3D(ankathete, gegenkathete, HEIGHT));
                down.Add(new Vector3D(ankathete, gegenkathete, 0));
            }


            // Circle down
            for (int i = 0; i < down.Count - 1; ++i) {
                this.addTriangle(new Triangle3D(new Vector3D(0, 0, 10), down[i], down[i + 1], firstColor));
            }
            this.addTriangle(new Triangle3D(new Vector3D(0, 0, 10), down[down.Count - 1], down[0], firstColor));

            
            // Side Triangle up, up, down 
            for (int i = 0; i < up.Count - 1; ++i) {
                this.addTriangle(new Triangle3D(up[i], up[i + 1], down[i], secondColor));
            }
            this.addTriangle(new Triangle3D(up[up.Count - 1], up[1], down[down.Count - 1], secondColor));
            
            
            // Side Triangle down, down, up
            for (int i = 0; i < down.Count - 1; ++i) {
                this.addTriangle(new Triangle3D(down[i], down[i + 1], up[i + 1], secondColor));
            }
            this.addTriangle(new Triangle3D(down[down.Count - 1], down[1], up[1], secondColor));


            // Circle Up
            for (int i = 0; i < up.Count - 1; ++i) {
                this.addTriangle(new Triangle3D(new Vector3D(0, 0, 10), up[i], up[i + 1], firstColor));
            }
            this.addTriangle(new Triangle3D(new Vector3D(0, 0, 10), up[up.Count - 1], up[0], firstColor));

        }

        public enum PlayerColor {Black, White};
    
    }
}
