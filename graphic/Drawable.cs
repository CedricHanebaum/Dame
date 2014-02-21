using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ISO {
    
    
    abstract class Drawable: IComparable {

        private int priority;

        protected Drawable(int priority) {
            this.priority = priority;
        }

        public abstract void draw(Graphics g);

        public int getPriority() {
            return priority;
        }

        public int CompareTo(object d) {
            return this.priority - ((Drawable)d).priority;
        }
    }
}
