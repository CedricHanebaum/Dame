using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic.GUI {
	
	
	class InGameLabel: Gui {

		private GuiLabel label;

		public InGameLabel(GuiManager guiManager, int priority): base(guiManager, priority) {

			this.setBounds(new Rectangle(200, 600, 600, 100));
			label = new GuiLabel(0, this);
			label.setBounds(new Rectangle(220, 620, 560, 60));
            this.addComponent(label);
		}

		public void setText(string text) {
			this.label.setText(text);
		}

		public override void actionPerformed(int id) {}
	
	}
}
