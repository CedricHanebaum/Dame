using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISO;

namespace graphic.GUI {
	
	
	class GuiManager {

		private Loop loop;
		private DrawManager drawManager;

        Gui optionsGui;

		private Gui activeGui;

		public GuiManager(Loop loop, DrawManager drawManager) {
			this.loop = loop;
			this.drawManager = drawManager;
			activeGui = null;

            optionsGui = new Options(this);
            drawManager.addDrawable(optionsGui);
            loop.getForm().registerMouseListener(optionsGui);
		}

        public void showOptionsGui()
        {
            this.closeActiveGui();
            activeGui = optionsGui;
            optionsGui.setVisible(true);
        }

		public void closeActiveGui() {
			if (activeGui != null) activeGui.setVisible(false);
			activeGui = null;
		}

        public void startGame(int size, Draught.Control.Players p1, Draught.Control.Players p2)
        {
            loop.startGame(size, p1, p2);
        }


	}
}
