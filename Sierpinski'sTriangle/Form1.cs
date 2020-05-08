using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sierpinski_sTriangle
{
    public partial class Form1 : Form
    {
        private int level;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawGasket();
        }

        // Rysowanie trójkąta
        private void DrawGasket()
        {
            try
            {
                level = int.Parse(comboBox_Level.Text);
            }
            catch (Exception)
            {
                level = 0;
            }

            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics gr = Graphics.FromImage(bitmap);
            
            gr.Clear(Color.Black);
            gr.SmoothingMode = SmoothingMode.AntiAlias; //ustawia jakość renderowania 

            // rysowanie pierwszego trójkąta
            const float margin = 10;
            PointF top_point = new PointF(pictureBox1.Width / 2f, margin);
            PointF left_point = new PointF(margin, pictureBox1.Height - margin);
            PointF right_point = new PointF(pictureBox1.ClientRectangle.Right - margin, pictureBox1.ClientRectangle.Bottom - margin);

            DrawTriangle(gr, level, top_point, left_point, right_point);
            

            // Wyświetlenie wyniku.
            pictureBox1.Image = bitmap;
        }

        // Rysowanie trójkąta pomiędzy wyznaczonymi punktami
        private void DrawTriangle(Graphics gr, int level, PointF top_point, PointF left_point, PointF right_point)
        {
            // Sprawdzenie otrzymania odpowiedniego poziomu
            if (level <= 0)
            {
                // wypełnienie kolorem
                PointF[] points = { top_point, right_point, left_point};
                gr.FillPolygon(Brushes.BlueViolet, points);
            }
            else
            {
                // Znalezienie krawędzi środkowych - wykorzystanie wzoru na środek odcinka.
                PointF left_mid = new PointF((top_point.X + left_point.X) / 2f, (top_point.Y + left_point.Y) / 2f);
                PointF right_mid = new PointF((top_point.X + right_point.X) / 2f, (top_point.Y + right_point.Y) / 2f);
                PointF bottom_mid = new PointF((left_point.X + right_point.X) / 2f, (left_point.Y + right_point.Y) / 2f);

                // Rekurencyjne wysowanie kolejnych mniejszych trójkątów.
                DrawTriangle(gr, level - 1, top_point, left_mid, right_mid);
                DrawTriangle(gr, level - 1, left_mid, left_point, bottom_mid);
                DrawTriangle(gr, level - 1, right_mid, bottom_mid, right_point);
            }
        }

    }
}
