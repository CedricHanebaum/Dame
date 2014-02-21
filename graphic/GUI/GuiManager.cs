using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISO;

namespace graphic.GUI {
	
	
	class GuiManager {

		private Loop loop;
		private DrawManager drawManager;

		// Gui declaration
		// ...

		private Gui activeGui;

		public GuiManager(Loop loop, DrawManager drawManager) {
			this.loop = loop;
			this.drawManager = drawManager;
			activeGui = null;

			// add Guis to drawList
			// ...

		}

		public void closeActiveGui() {
			if (activeGui != null) activeGui.setVisible(false);
			activeGui = null;
		}


	}
}
