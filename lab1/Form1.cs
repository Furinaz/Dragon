using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        int n1 = 0;
        Pen h = new Pen(Color.Red, 1);
        const int bm_width = 772, bm_height = 529;
        static Bitmap bm = new Bitmap(bm_width, bm_height);
        Graphics gg = Graphics.FromImage(bm);
        public Form1()
        {
            InitializeComponent();
        }
  
        private void buttonToClose(object sender, EventArgs e)
        {
            this.Close();
        }
        private void paaint(int x1, int y1, int x2, int y2, int k)
        {

            int tx, ty;
            if (k == 0)
            {
                gg.DrawLine(h, x1, y1, x2, y2);
                return;
            }

            tx = (x1 + x2) / 2 + (y2 - y1) / 2;
            ty = (y1 + y2) / 2 - (x2 - x1) / 2;
            paaint(x2, y2, tx, ty, k - 1);
            paaint(x1, y1, tx, ty, k - 1);
            pictureBox1.Image = bm;
        }
        private void Draw()
        {
            gg.Clear(Color.White);
            n1 = Convert.ToInt32(textBox1.Text);
            int x1 = int.Parse(textBox2.Text);
            int x2 = int.Parse(textBox3.Text);
            int y1 = int.Parse(textBox4.Text);
            int y2 = int.Parse(textBox5.Text);

            if (n1 >= 16)
            {
                MessageBox.Show("Введіть менше значення");
            }
            else
            {
                paaint(x1, y1, x2, y2, n1);
            }
        }
        private void buttonToClear(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            var BgColor = new SolidBrush(Color.White);
            g.FillRectangle(BgColor, 0, 0, 660, 600);
        }

        private void buttonToDraw(object sender, EventArgs e)
        {
            Draw();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void Save()
        {
            if (pictureBox1.Image != null)
            {
                //створення діалогового вікна "Зберегти як...", для збереження
                SaveFileDialog SaveDialog = new SaveFileDialog();
                SaveDialog.Title = "Зберегти як...";
                //відображати попередження, якщо користувач вказує ім'я вже існуючого файлу
                SaveDialog.OverwritePrompt = true;
                //відображати попередження, якщо користувач вказує неіснуючий шлях
                SaveDialog.CheckPathExists = true;
                //список форматів файлів, які відображаються в полі "Тип файлу"
                SaveDialog.Filter = "Image Files(.BMP)|.BMP|Image Files(.JPG)|.JPG|Image Files(.GIF)|.GIF|Image Files(.PNG)|.PNG|All files (.)|.";
                //показувати чи відображається кнопка "Довідка" у діалоговому вікні
                SaveDialog.ShowHelp = true;
                if (SaveDialog.ShowDialog() == DialogResult.OK) //якщо користувач нажав "Ок"
                {
                    try
                    {
                        bm.Save(SaveDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Неможливо зберегти зображення", "Помилка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void buttonToSave(object sender, EventArgs e)
        {
            Save();
        }
    }
}
