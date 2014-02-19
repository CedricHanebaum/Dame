using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Draughts;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    
    
    class World: Drawable, IMouseNoticeable {

        private Token[,] tokens;
        private Board3D board;

        private Vector3D boardBase = new Vector3D(340, -176, 0);
        private Vector3D tokenBase = new Vector3D(364, -152, 0);

        private Token atMouse;

        public World(int priority, int size): base(priority) {
            tokens = new Token[size, size];
            board = new Board3D(boardBase, size);

            atMouse = Token.empty;
        }

        public void setToken(int posX, int posY, Token t) {
            if ((posX < 0 || posX > tokens.Length) || (posY < 0 || posY > tokens.Length)) {
                throw new IndexOutOfRangeException();
            }
            tokens[posX, posY] = t;
        }

        public void mouseMoved(MouseEventArgs e) {
            
        }

        public void mouseClicked(MouseEventArgs e) {
            Console.WriteLine("Click!");
            Vector3D pos3D = Vector3D.ISOToVector3D(e.Location);

            Console.WriteLine("3D: " + pos3D);

            if (board.isInside(pos3D)) {

                int posX = board.getFieldX(pos3D);
                int posY = board.getFieldY(pos3D);

                Console.WriteLine("X: " + posX + ", Y: " + posY);

                if (atMouse == Token.empty) {

                    atMouse = tokens[posX, posY];

                } else {
                    throw new NotImplementedException();

                    // Marco mach hinne, ich brauch die Control!
                    // An Control uebergeben: Ursprungsposition, neue Position.

                }
            }
        }

        public override void draw(Graphics g) {
            board.draw(g);

            for (int i = 0; i < tokens.GetLength(0); ++i) {
                for (int j = 0; j < tokens.GetLength(1); ++j) {

                    Token3D token = null;
                    Vector3D tokenPosRel = new Vector3D(48*i, 48*j, 0);
                    Vector3D tokenPosAbs = tokenBase + tokenPosRel;

                    switch (tokens[i, j]) {
                        case Token.Black:
                            token = new Token3D(tokenPosAbs, Token3D.PlayerColor.Black);
                            break;
                        case Token.White:
                            token = new Token3D(tokenPosAbs, Token3D.PlayerColor.White);
                            break;
                        case Token.BlackDraugth:
                            throw new NotImplementedException();
                        case Token.WhithDraugth:
                            throw new NotImplementedException();
                        case Token.empty:
                            break;
                    }

                    if (token != null) token.draw(g);
                }
            }
        }

        public enum Token { empty, WhithDraugth, BlackDraugth, White, Black };
    }
}
