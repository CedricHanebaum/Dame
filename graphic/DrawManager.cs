using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic {
    
    
    class DrawManager {

        private List<Drawable> drawList = new List<Drawable>();

        public void draw(Graphics g) {
            for (int i = 0; i < drawList.Count; ++i )
            {
                drawList[i].draw(g);
            }
        }

        public void addDrawable(Drawable d) {
            drawList.Add(d);
            drawList.Sort();
        }
    
    }
}
