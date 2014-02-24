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
		Gui inGameLabel;

		private Gui activeGui;

		public GuiManager(Loop loop, DrawManager drawManager) {
			this.loop = loop;
			this.drawManager = drawManager;
			activeGui = null;

			optionsGui = new Options(this);
			drawManager.addDrawable(optionsGui);
			loop.getForm().registerMouseListener(optionsGui);

			inGameLabel = new InGameLabel(this, 2);
			drawManager.addDrawable(inGameLabel);
		}

		public void showOptionsGui() {
			this.closeActiveGui();
			activeGui = optionsGui;
			optionsGui.setVisible(true);
		}

		public void showInGameLable() {
			this.closeActiveGui();
			activeGui = inGameLabel;
			inGameLabel.setVisible(true);
		}

		public void closeActiveGui() {
			if (activeGui != null) activeGui.setVisible(false);
			activeGui = null;
		}

		public void setInGameLableText(string text) {
			((InGameLabel)this.inGameLabel).setText(text);
		}

		public void startGame(int size, Draught.Control.Players p1, Draught.Control.Players p2) {
			loop.startGame(size, p1, p2);
			this.showInGameLable();
		}

		public void closeGame() {
			loop.exit();
		}


	}
}
