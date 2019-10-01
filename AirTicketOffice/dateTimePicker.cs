using System;
using System.Drawing;
using System.Windows.Forms;
using AirTicketOffice.Properties;
using MySql.Data.MySqlClient;

namespace AirTicketOffice
{
    public partial class dateTimePicker : Form
    {
        Point moveStart;
        public dateTimePicker()
        {
            InitializeComponent();
            timer1.Tick += Timer1_Tick;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
        }
        private static string myConnectionString = "Database = AirTO; Data Source = localhost; User Id=root; charset= utf8; Password =";
        private MySqlConnection connect = new MySqlConnection(myConnectionString);

        private void Timer1_Tick(object sender, EventArgs e)
        {
            connect.Open();

            string dayBase = "SELECT Day FROM ditetimepicker;";
            string monthBase = "SELECT Month FROM ditetimepicker;";
            string NameDate = "SELECT NameDate FROM ditetimepicker;";
            MySqlCommand cmddayBase = new MySqlCommand(dayBase, connect);
            MySqlCommand cmdmonthBase = new MySqlCommand(monthBase, connect);
            MySqlCommand cmdNameDate = new MySqlCommand(NameDate, connect);
            object dayBaseObj = cmddayBase.ExecuteScalar();
            int dayBaseInt = Convert.ToInt32(dayBaseObj);
            object monthBaseObj = cmdmonthBase.ExecuteScalar();
            int monthBaseInt = Convert.ToInt32(monthBaseObj);
            object NameDateObj = cmdNameDate.ExecuteScalar();
            string NameDateString = Convert.ToString(NameDateObj);

            connect.Close();
            DateTime Now = DateTime.Now;
            int Year = Now.Year;
            DateTime dt = new DateTime(Year, monthBaseInt, dayBaseInt);

            TimeSpan TimeRemaining = dt - DateTime.Now;
            label3.Text = TimeRemaining.Days + " дней " + TimeRemaining.Hours + "ч : " + TimeRemaining.Minutes + "м : " + TimeRemaining.Seconds + "c";
            label4.Text = NameDateString;
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // если нажата левая кнопка мыши
            if (e.Button == MouseButtons.Left)
            {
                moveStart = new Point(e.X, e.Y);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // если нажата левая кнопка мыши
            if ((e.Button & MouseButtons.Left) != 0)
            {
                // получаем новую точку положения формы
                Point deltaPos = new Point(e.X - moveStart.X, e.Y - moveStart.Y);
                // устанавливаем положение формы
                this.Location = new Point(this.Location.X + deltaPos.X,
                  this.Location.Y + deltaPos.Y);
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Label2_MouseEnter(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(255, 77, 77);
        }

        private void Label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(31, 38, 48);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            connect.Open();

            if (textBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text != "")
            {
                string updateDTP = "UPDATE ditetimepicker SET NameDate = '" + textBox3.Text + "', Day =" + comboBox1.SelectedItem + ", Month = " + comboBox2.SelectedItem + ";";
                MySqlCommand cmdupdateDTP = new MySqlCommand(updateDTP, connect);
                cmdupdateDTP.ExecuteNonQuery();
                label4.Text = textBox3.Text;
                Settings.Default.Save();
            }

            else if (textBox3.Text != "" && comboBox1.Text == "" && comboBox2.Text == "")
            {
                string updateDTP = "UPDATE ditetimepicker SET NameDate = '" + textBox3.Text + "';";
                MySqlCommand cmdupdateDTP = new MySqlCommand(updateDTP, connect);
                cmdupdateDTP.ExecuteNonQuery();
                label4.Text = textBox3.Text;
                Settings.Default.Save();
            }

            if (textBox3.Text == "" && comboBox1.Text != "" && comboBox2.Text != "")
            {
                string updateDTP = "UPDATE ditetimepicker SET Day = " + comboBox1.SelectedItem + ", Month = " + comboBox2.SelectedItem + ";";
                MySqlCommand cmdupdateDTP = new MySqlCommand(updateDTP, connect);
                cmdupdateDTP.ExecuteNonQuery();
                Settings.Default.Save();
            }

            if (textBox3.Text == "" && comboBox1.Text == "" && comboBox2.Text != "")
            {
                string updateDTP = "UPDATE ditetimepicker SET Month = " + comboBox2.SelectedItem + ";";
                MySqlCommand cmdupdateDTP = new MySqlCommand(updateDTP, connect);
                cmdupdateDTP.ExecuteNonQuery();
                Settings.Default.Save();
            }

            if (textBox3.Text == "" && comboBox1.Text != "" && comboBox2.Text == "")
            {
                string updateDTP = "UPDATE ditetimepicker SET Day = " + comboBox1.SelectedItem + ";";
                MySqlCommand cmdupdateDTP = new MySqlCommand(updateDTP, connect);
                cmdupdateDTP.ExecuteNonQuery();
                Settings.Default.Save();
            }

            if (textBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text == "")
            {
                string updateDTP = "UPDATE ditetimepicker SET NameDate = '" + textBox3.Text + "', Day =" + comboBox1.SelectedItem + ";";
                MySqlCommand cmdupdateDTP = new MySqlCommand(updateDTP, connect);
                cmdupdateDTP.ExecuteNonQuery();
                label4.Text = textBox3.Text;
                Settings.Default.Save();
            }

            if (textBox3.Text != "" && comboBox1.Text == "" && comboBox2.Text != "")
            {
                string updateDTP = "UPDATE ditetimepicker SET NameDate = '" + textBox3.Text + "', Month =" + comboBox2.SelectedItem + ";";
                MySqlCommand cmdupdateDTP = new MySqlCommand(updateDTP, connect);
                cmdupdateDTP.ExecuteNonQuery();
                label4.Text = textBox3.Text;
                Settings.Default.Save();
            }

            if (textBox3.Text == "" && comboBox1.Text == "" && comboBox2.Text == "")
            {
                MessageBox.Show("Поля пустые");
            }

            connect.Close();

        }
    }
}
