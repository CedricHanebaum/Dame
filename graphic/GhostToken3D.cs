using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISO;
using System.Drawing;

namespace ISO {
	
	
	class GhostToken3D: GhostShape{

		public const int RADIUS = 16;
		public const int RESOLUTION = 16;
		public const int HEIGHT = 8;

		private Color color;

		private Color colorWhite = Color.Pink;
		private Color colorBlack = Color.LightBlue;

		public GhostToken3D(Vector3D basePoint, PlayerColor playerColor): base(basePoint) {

			switch (playerColor) {
				case PlayerColor.Black:
					color = colorBlack;
					break;
				case PlayerColor.White:
					color = colorWhite;
					break;
			}


			List<Vector3D> up = new List<Vector3D>();
			List<Vector3D> down = new List<Vector3D>();

			for (int i = 0; i <= 16; ++i) {
				int gegenkathete = (int)(Math.Sin((Math.PI / (RESOLUTION / 2)) * i) * RADIUS);
				int ankathete = (int)(Math.Cos((Math.PI / (RESOLUTION / 2)) * i) * RADIUS);

				up.Add(new Vector3D(ankathete, gegenkathete, HEIGHT));
				down.Add(new Vector3D(ankathete, gegenkathete, 0));
			}

			// Circle down
			for (int i = 0; i < down.Count - 1; ++i) {
				this.addLine(new Line3D(down[i], down[i + 1], color));
			}
			this.addLine(new Line3D(down[down.Count -1], down[0], color));

			// Side Triangle up, up, down 
			for (int i = 0; i < up.Count; ++i) {
				this.addLine(new Line3D(up[i], down[i], color));
			}


			// Circle Up
			for (int i = 0; i < up.Count - 1; ++i) {
				this.addLine(new Line3D(up[i], up[i + 1], color));
			}
			this.addLine(new Line3D(up[up.Count - 1], up[0], color));

		}



		public enum PlayerColor { Black, White };
	
	}
}
