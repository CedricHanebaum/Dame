using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using graphic;

namespace ISO{

    public partial class Form1 : Form {

        private Bitmap buffer;
        private List<IMouseNoticeable> mouseListeners = new List<IMouseNoticeable>();


        public Form1() {
            InitializeComponent();
            this.buffer = new Bitmap(panel1.Width, panel1.Height);
        }

        private void panelPaint(Object sender, PaintEventArgs e) {
            e.Graphics.DrawImageUnscaled(this.buffer, new Point(0, 0));
        }

        public void repaint(Bitmap buffer) {
            /*if (!BitmapCompare.CompareMemCmp(this.buffer, buffer)) {
                this.buffer = buffer.Clone(new Rectangle(0, 0, buffer.Width, buffer.Height), buffer.PixelFormat);
                panel1.Invalidate();
            }*/

            this.buffer = buffer.Clone(new Rectangle(0, 0, buffer.Width, buffer.Height), buffer.PixelFormat);
            panel1.Invalidate();

            buffer = null;
            System.GC.Collect();

        }

        public void registerMouseListener(IMouseNoticeable m) {
            mouseListeners.Add(m);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            foreach (var m in mouseListeners) {
                m.mouseMoved(e);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e) {
            foreach (var m in mouseListeners) {
                m.mouseClicked(e);
            }
        }

        public int getPanelWidth() {
            return panel1.Width;
        }

        public int getPanelHeight() {
            return panel1.Height;
        }
    }
}
