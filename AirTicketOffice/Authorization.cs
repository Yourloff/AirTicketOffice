using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using AirTicketOffice.Properties;

namespace AirTicketOffice
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private static string myConnectionString = "Database = AirTO; Data Source = localhost; User Id=root; charset= utf8; Password =";
        private MySqlConnection connect = new MySqlConnection(myConnectionString);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Random Rand = new Random();
        private string text1;
        // Создаем изображение для текста.
        private Bitmap MakeCaptchaImge(string txt,
            int min_size, int max_size, int wid, int hgt)
        {
            // Создаем растровое изображение и связанный с ним объект Graphics.
            Bitmap bm = new Bitmap(wid, hgt);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.Clear(Color.White);

                // Посмотрите, сколько места доступно для каждого персонажа.
                int ch_wid = (int)(wid / txt.Length);

                // Рисуем каждый символ.
                for (int i = 0; i < txt.Length; i++)
                {
                    float font_size = Rand.Next(min_size, max_size);
                    using (Font the_font = new Font("Times New Roman",
                        font_size, FontStyle.Bold))
                    {
                        DrawCharacter(txt.Substring(i, 1), gr,
                            the_font, i * ch_wid, ch_wid, wid, hgt);
                    }
                }
            }
            return bm;
        }
        private int PreviousAngle = 0;
        private void DrawCharacter(string txt, Graphics gr,
            Font the_font, int X, int ch_wid, int wid, int hgt)
        {
            // Центрируем текст.
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Center;
                RectangleF rectf = new RectangleF(X, 0, ch_wid, hgt);

                // Преобразование текста в путь.
                using (GraphicsPath graphics_path = new GraphicsPath())
                {

                    graphics_path.AddString(txt,
                        the_font.FontFamily, (int)the_font.Style,
                        the_font.Size, rectf, string_format);

                    // Произвольные случайные параметры деформации.
                    float x1 = (float)(X + Rand.Next(ch_wid) / 2);
                    float y1 = (float)(Rand.Next(hgt) / 2);
                    float x2 = (float)(X + ch_wid / 2 +
                        Rand.Next(ch_wid) / 2);
                    float y2 = (float)(hgt / 2 + Rand.Next(hgt) / 2);
                    PointF[] pts = {
                                    new PointF(
                                        (float)(X + Rand.Next(ch_wid) / 4),
                                        (float)(Rand.Next(hgt) / 4)),
                                    new PointF(
                                        (float)(X + ch_wid - Rand.Next(ch_wid) / 4),
                                        (float)(Rand.Next(hgt) / 4)),
                                    new PointF(
                                        (float)(X + Rand.Next(ch_wid) / 4),
                                        (float)(hgt - Rand.Next(hgt) / 4)),
                                    new PointF(
                                        (float)(X + ch_wid - Rand.Next(ch_wid) / 4),
                                        (float)(hgt - Rand.Next(hgt) / 4))
                    };
                    Matrix mat = new Matrix();
                    graphics_path.Warp(pts, rectf, mat,
                        WarpMode.Perspective, 0);
                    // Поворачиваем бит случайным образом.
                    float dx = (float)(X + ch_wid / 2);
                    float dy = (float)(hgt / 2);
                    gr.TranslateTransform(-dx, -dy, MatrixOrder.Append);
                    int angle = PreviousAngle;
                    do
                    {
                        angle = Rand.Next(-30, 30);
                    } while (Math.Abs(angle - PreviousAngle) < 20);
                    PreviousAngle = angle;
                    gr.RotateTransform(angle, MatrixOrder.Append);
                    gr.TranslateTransform(dx, dy, MatrixOrder.Append);

                    gr.FillPath(Brushes.Blue, graphics_path);
                    gr.ResetTransform();
                }
            }
        }
        private void PicCaptcha_Click(object sender, EventArgs e)
        {
            text1 = String.Empty;
            text1 = "";
            string ALF = "0123456789abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < 6; ++i) text1 += ALF[Rand.Next(ALF.Length)];
            picCaptcha.Image = this.MakeCaptchaImge(text1, picCaptcha.Height / 2, picCaptcha.Height - 5, picCaptcha.Width,
                picCaptcha.Height);
        }

        private void Buttoncapt_Click(object sender, EventArgs e)
        {
            if (textBoxCap.Text == this.text1)
            {
                MessageBox.Show("Верно!", "Капча", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonToComeIn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ошибка!", "Капча", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonToComeIn.Enabled = false;
            }
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            radioButtonUser.Checked = true;
            buttonToComeIn.Enabled = false;
            text1 = String.Empty;
            text1 = "";
            string ALF = "0123456789abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < 1; ++i) text1 += ALF[Rand.Next(ALF.Length)];
            picCaptcha.Image = this.MakeCaptchaImge(text1, picCaptcha.Height / 2, picCaptcha.Height - 5, picCaptcha.Width,
                picCaptcha.Height);
        }
        static int counter = 0;
        private void ButtonToComeIn_Click(object sender, EventArgs e)
        {
            counter++;

            Class.HashMD5 HMD5 = new Class.HashMD5();
            string LoginMD5 = HMD5.GetHashString(textBox1.Text);
            string PasswordMD5 = HMD5.GetHashString(textBox2.Text);

            if (radioButtonUser.Checked == true) //юзер
            {
                connect.Open();

                string checkUser = "SELECT count(*) FROM User WHERE Login='" + LoginMD5 + "' AND Password='" + PasswordMD5 + "'"; //юзер
                MySqlCommand cmdcheckUser = new MySqlCommand(checkUser, connect);     //юзер
                int countUser = Convert.ToInt32(cmdcheckUser.ExecuteScalar());

                //string checkUser = "SELECT count(*) FROM User WHERE Login='" + LoginMD5 + "' AND Password='" + PasswordMD5 + "'"; //юзер
                //MySqlCommand cmdcheckUser = new MySqlCommand(checkUser, connect);     //юзер
                //int countUser = Convert.ToInt32(cmdcheckUser.ExecuteScalar());

                if (text1 == textBoxCap.Text)
                {
                    if (textBox1.Text != "" && textBox2.Text != "")
                    {
                        if (countUser == 1)
                        {
                            Settings.Default["Login"] = textBox1.Text;
                            Settings.Default["Password"] = textBox2.Text;
                            Settings.Default.Save();
                            Hello hello = new Hello();
                            this.Hide();
                            hello.ShowDialog();
                        }
                        else if (counter >= 3) // лимит превышен
                        {
                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            buttonToComeIn.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Неверно введён логин/пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не все поля введены", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Введите капчу");
                }
                connect.Close();
            }

            if (radioButtonAdmin.Checked == true) //админ
            {
                connect.Open();
                string checkAdmin = "SELECT count(*) FROM Admin WHERE Login='" + textBox1.Text + "'";   //админ
                MySqlCommand cmdcheckAdmin = new MySqlCommand(checkAdmin, connect);     //админ
                int countAdmin = Convert.ToInt32(cmdcheckAdmin.ExecuteScalar());

                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    if (countAdmin == 1)
                    {
                        Settings.Default["Login"] = textBox1.Text;
                        Settings.Default["Password"] = textBox2.Text;
                        Settings.Default.Save();
                        Hello hello = new Hello();
                        this.Hide();
                        hello.ShowDialog();
                    }
                    else if (counter >= 3) // лимит превышен
                    {
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                        buttonToComeIn.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Неверно введён логин/пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Не все поля введены", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                connect.Close();
            }
        }

        private void ButtonReg_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            this.Hide();
            reg.ShowDialog();
            counter = 0;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Label2_MouseEnter(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(255, 77, 77);
        }

        private void Label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(31, 38, 48);
        }
    }
}