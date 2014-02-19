using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Draughts;
using System.Windows.Forms;
using Draught;

namespace WindowsFormsApplication1 {
    
    
    class World: Drawable, IMouseNoticeable {

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

            tokens = new Token[size, size];
            board = new Board3D(boardBase, size);

            atMouse = Token.empty;
            atMousePos[0] = -1;
            atMousePos[1] = -1;
        }

        public void setToken(int posX, int posY, Token t) {
            if ((posX < 0 || posX > tokens.Length) || (posY < 0 || posY > tokens.Length)) {
                throw new IndexOutOfRangeException();
            }
            tokens[posX, posY] = t;
            Console.WriteLine(tokens[posX, posY]);
        }

        public void mouseMoved(MouseEventArgs e) {
            
        }

        public void mouseClicked(MouseEventArgs e) {
            Vector3D pos3D = Vector3D.ISOToVector3D(e.Location);


            if (board.isInside(pos3D)) {

                int posX = board.getFieldX(pos3D);
                int posY = board.getFieldY(pos3D);

                Console.WriteLine("X: " + posX + ", Y: " + posY);

                if (atMouse == Token.empty) {

                    atMouse = tokens[posX, posY];
                    atMousePos[0] = posX;
                    atMousePos[1] = posY;

                } else {
                    int[] mousePos = {posX, posY};
                    control.checkTurn(atMousePos, mousePos, map.getToken(mousePos));
                }
            }
        }

        public override void draw(Graphics g) {
            board.draw(g);

            for (int i = 0; i < tokens.GetLength(0); ++i) {
                for (int j = 0; j < tokens.GetLength(1); ++j) {

                    Token3D token1;
                    Token3D token2;

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
                        case Token.WhithDraugth:
                            token1 = new Token3D(tokenPosAbs, Token3D.PlayerColor.White);
                            token2 = new Token3D(tokenPosAbs + v1, Token3D.PlayerColor.White);

                            token1.draw(g);
                            token2.draw(g);

                            break;
                        case Token.empty:
                            break;
                    }

                }
            }
        }

        public enum Token { empty, WhithDraugth, BlackDraugth, White, Black };
    }
}
