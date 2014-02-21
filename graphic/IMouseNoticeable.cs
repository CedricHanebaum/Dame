﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace graphic {
    
    public interface IMouseNoticeable {

        void mouseMoved(MouseEventArgs e);

        void mouseClicked(MouseEventArgs e);
            
    }
}
