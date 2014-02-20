using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Draught;
using System.Windows.Forms;
using Draught;
using tmp.graphic;
using Draughts;

namespace WindowsFormsApplication1 {
	
	
	class World: Drawable, IMouseNoticeable, IMap 
	{

		private Draught.Control control;
		private Map map;

		private Vector3D boardBase = new Vector3D(340, -176, 0);
		private Vector3D tokenBase = new Vector3D(364, -152, 0);

		private Token[,] tokens;
		private Board3D board;

		private Token atMouse;
		private int[] atMousePos = new int[2];

		public World(int priority, int size, Draught.Control control, Map map): base(priority) {
			this.control = control;
			this.map = map;
			map.addListener(this);

			tokens = new Token[size, size];
			board = new Board3D(boardBase, size);

			atMouse = Token.empty;
			atMousePos[0] = -1;
			atMousePos[1] = -1;

			this.refresh();
		}

		public void setToken(int posX, int posY, Token t) {
			if ((posX < 0 || posX > tokens.Length) || (posY < 0 || posY > tokens.Length)) {
				throw new IndexOutOfRangeException();
			}
			tokens[posX, posY] = t;
			Console.WriteLine(tokens[posX, posY]);
		}

		public void mouseMoved(MouseEventArgs e) {
			Vector3D pos3D = Vector3D.ISOToVector3D(e.Location);

			this.removeAllGhosts();

			if (board.isInside(pos3D) && atMouse != Token.empty) {

				int posX = board.getFieldX(pos3D);
				int posY = board.getFieldY(pos3D);

				if (tokens[posX, posY] == Token.empty) {

					switch (atMouse) {
						case Token.Black:
							tokens[posX, posY] = Token.BlackGhost;
							break;
						case Token.White:
							tokens[posX, posY] = Token.WhiteGhost;
							break;
						case Token.BlackDraugth:
							tokens[posX, posY] = Token.BlackDraugthGhost;
							break;
						case Token.WhiteDraugth:
							tokens[posX, posY] = Token.WhiteDraugthGhost;
							break;
					}
				}
			}

		}

		private void removeAllGhosts() {

			for (int i = 0; i < tokens.GetLength(0); ++i) {
				for (int j = 0; j < tokens.GetLength(1); ++j) {
					Token t = tokens[i, j];

					if (t == Token.BlackDraugthGhost || t == Token.WhiteDraugthGhost || t == Token.BlackGhost || t == Token.WhiteGhost) {
						tokens[i, j] = Token.empty;
					}

				}
			}


		}

		public void mouseClicked(MouseEventArgs e) {
			Vector3D pos3D = Vector3D.ISOToVector3D(e.Location);

			Console.WriteLine(">> click");

			if (board.isInside(pos3D)) {

				int posX = board.getFieldX(pos3D);
				int posY = board.getFieldY(pos3D);

				if (atMouse == Token.empty) {
					if (tokens[posX, posY] != Token.BlackDraugthGhost || tokens[posX, posY] != Token.WhiteDraugthGhost || tokens[posX, posY] != Token.BlackGhost || tokens[posX, posY] != Token.WhiteGhost) {
						atMouse = tokens[posX, posY];
						atMousePos[0] = posX;
						atMousePos[1] = posY;
					}

				} else {
					int[] mousePos = {posX, posY};
					control.checkTurn(atMousePos, mousePos);
					atMouse = Token.empty;
				}
			}

			Console.WriteLine("<<");
		}

		public void refresh() {

			Console.WriteLine(">>> refresh");

			for (int i = 0; i < tokens.GetLength(0); ++i) {
				for (int j = 0; j < tokens.GetLength(1); ++j) {
					int[] pos = { i, j };

					Draught.Token t = map.getToken(pos);

					if (t != null) {
						if (t.Tok == "stone") {
							if (t.Color == Draught.Token.PlayerColor.Black) {
								tokens[i, j] = Token.Black;
							} else {
								tokens[i, j] = Token.White;
							}
						} else {
							if (t.Color == Draught.Token.PlayerColor.Black) {
								tokens[i, j] = Token.BlackDraugth;
							} else {
								tokens[i, j] = Token.WhiteDraugth;
							}
						}
					} else {
						tokens[i, j] = Token.empty;
					}

				}
			}

			Console.WriteLine("<<<");
		}

		public override void draw(Graphics g) {
			board.draw(g);

			for (int i = 0; i < tokens.GetLength(0); ++i) {
				for (int j = 0; j < tokens.GetLength(1); ++j) {

					Token3D token1;
					Token3D token2;

					GhostToken3D gToken1;
					GhostToken3D gToken2;

					Vector3D v1 = new Vector3D(0, 0, 10);

					Vector3D tokenPosRel = new Vector3D(48*i, 48*j, 0);
					Vector3D tokenPosAbs = tokenBase + tokenPosRel;

					switch (tokens[i, j]) {
						case Token.Black:
							token1 = new Token3D(tokenPosAbs, Token3D.PlayerColor.Black);
							token1.draw(g);
							break;
						case Token.White:
							token1 = new Token3D(tokenPosAbs, Token3D.PlayerColor.White);
							token1.draw(g);
							break;
						case Token.BlackDraugth:
							token1 = new Token3D(tokenPosAbs, Token3D.PlayerColor.Black);
							token2 = new Token3D(tokenPosAbs + v1, Token3D.PlayerColor.Black);

							token1.draw(g);
							token2.draw(g);

							break;
						case Token.WhiteDraugth:
							token1 = new Token3D(tokenPosAbs, Token3D.PlayerColor.White);
							token2 = new Token3D(tokenPosAbs + v1, Token3D.PlayerColor.White);

							token1.draw(g);
							token2.draw(g);

							break;
						case Token.BlackGhost:
							gToken1 = new GhostToken3D(tokenPosAbs, GhostToken3D.PlayerColor.Black);
							gToken1.draw(g);
							break;
						case Token.WhiteGhost:
							gToken1 = new GhostToken3D(tokenPosAbs, GhostToken3D.PlayerColor.White);
							gToken1.draw(g);
							break;
						case Token.BlackDraugthGhost:
							gToken1 = new GhostToken3D(tokenPosAbs, GhostToken3D.PlayerColor.Black);
							gToken2 = new GhostToken3D(tokenPosAbs + v1, GhostToken3D.PlayerColor.Black);
							
							gToken1.draw(g);
							gToken2.draw(g);
							break;
						case Token.WhiteDraugthGhost:
							gToken1 = new GhostToken3D(tokenPosAbs, GhostToken3D.PlayerColor.White);
							gToken2 = new GhostToken3D(tokenPosAbs + v1, GhostToken3D.PlayerColor.White);

							gToken1.draw(g);
							gToken2.draw(g);
							break;
						case Token.empty:
							break;
					}

				}
			}
		}

		public enum Token { empty, WhiteDraugth, BlackDraugth, White, Black, WhiteDraugthGhost, BlackDraugthGhost, WhiteGhost, BlackGhost};
	}
}
