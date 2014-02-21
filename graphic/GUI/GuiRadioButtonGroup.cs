using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace graphic.GUI {


	class GuiRadioButtonGroup{

		private List<GuiRadioButton> radioButtons = new List<GuiRadioButton>();
		private int activeId;

		public GuiRadioButtonGroup() {
			activeId = -1;
		}

		public void addRadioButton(GuiRadioButton radioButton) {
			radioButtons.Add(radioButton);
			radioButton.setGroup(this);
		}

		public void deactivateButtons() {
			foreach (var r in radioButtons) {
				r.deactivate();
			}

			activeId = -1;
		}

		public void activateButton(int id) {
			if (!this.containsButtonId(id)) throw new ArgumentException();

			this.deactivateButtons();

			foreach (var r in radioButtons) {
				if (r.getID() == id) {
					r.activate();
				}
			}

			this.activeId = id;
		}

		public int getActiveButtonId() {
			return activeId;
		}

		public bool containsButtonId(int id) {
			bool ret = false;

			foreach (var r in radioButtons) {
				if (r.getID() == id) ret = true;
			}

			return ret;
		}

	}
}
