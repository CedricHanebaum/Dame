using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ISO {
    
    
    class DrawManager {

        private List<Drawable> drawList = new List<Drawable>();

        public void draw(Graphics g) {
            foreach (var d in drawList) {
                d.draw(g);
            }
        }

        public void addDrawable(Drawable d) {
            drawList.Add(d);
            drawList.Sort();
        }
    
    }
}
