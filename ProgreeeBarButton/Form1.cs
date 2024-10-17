using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgreeeBarButton
{
    public partial class Form1 : Form
    {
        private Bitmap _bm;
        private int _process = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            using (Graphics graphics = Graphics.FromImage(_bm))
            {
                graphics.Clear(button1.BackColor);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            const int max_progress = 100;
            _process++;
            button1.BeginInvoke(new Action(() =>
            {
                if (_process > 0)
                {
                    button1.Text = "Cargando " + _process + "% completado";
                }
            }));
            if (_process >= max_progress)
            {
                _process = -1;
                button1.Text = "Carga finalizada";
                using (Graphics graphics = Graphics.FromImage(_bm))
                {
                    graphics.Clear(button1.BackColor);
                }
                timer1.Enabled = false;
                return;
            }

            using (SolidBrush solidBrush = new SolidBrush(Color.Red))
            {
                using (Graphics graphics = Graphics.FromImage(_bm))
                {
                    float wid = _bm.Width * _process / (max_progress - 5);
                    float hgt = _bm.Height;
                    RectangleF rect = new RectangleF(0, 0, wid, hgt);
                    graphics.FillRectangle(solidBrush, rect);
                }
            }
            button1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _bm = new Bitmap(button1.ClientSize.Width, button1.ClientSize.Height);
            button1.BackgroundImage = _bm;
        }
    }
}
