using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISO {


    class BufferedPanel: Panel {

        public BufferedPanel() {
            this.DoubleBuffered = true;
        }
    
    
    }
}
