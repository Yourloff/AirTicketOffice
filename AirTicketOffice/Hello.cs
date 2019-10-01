using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AirTicketOffice.Properties;
using MySql.Data.MySqlClient;

namespace AirTicketOffice
{
    public partial class Hello : Form
    {
        Point moveStart;
        public Hello()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(0, 192, 192);
            this.Load += Hello_Load;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;

            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Tick += new EventHandler(Timer1_Tick);
        }

        private static string myConnectionString = "Database = AirTO; Data Source = localhost; User Id=root; charset= utf8; Password =";
        private MySqlConnection connect = new MySqlConnection(myConnectionString);

        private void Hello_Load(object sender, EventArgs e)
        {
            Class.HashMD5 HMD5 = new Class.HashMD5();
            string LoginMD5 = HMD5.GetHashString(Settings.Default["Login"].ToString());

            string checkUser = "SELECT count(*) FROM user WHERE Login = '" + LoginMD5 + "';";
            string checkAdmin = "SELECT count(*) FROM admin WHERE Login = '" + Settings.Default["Login"].ToString() + "';";

            connect.Open();

            MySqlCommand cmdCheckUser = new MySqlCommand(checkUser, connect);
            MySqlCommand cmdCheckAdmin = new MySqlCommand(checkAdmin, connect);

            cmdCheckUser.Prepare();
            cmdCheckAdmin.Prepare();

            int countUser = Convert.ToInt32(cmdCheckUser.ExecuteScalar());
            int countAdmin = Convert.ToInt32(cmdCheckAdmin.ExecuteScalar());

            if (countUser == 1)
            {
                string FIOuser = "SELECT Surname, Name, Middlename FROM user WHERE Login = '" + LoginMD5 + "';";
                MySqlCommand cmdFIOuser = new MySqlCommand(FIOuser, connect);   //юзер
                cmdFIOuser.Parameters.AddWithValue("@Login", labelname.Text);
                MySqlDataReader reader = cmdFIOuser.ExecuteReader();
                while (reader.Read())
                {
                    labelname.Text = reader["Surname"].ToString() + " " + reader["Name"].ToString() + " " + reader["Middlename"].ToString();
                    Settings.Default["Surname"] = reader["Surname"].ToString();
                    Settings.Default["Name"] = reader["Name"].ToString();
                    Settings.Default["Middlename"] = reader["Middlename"].ToString();
                    Settings.Default.Save();
                }
                    reader.Close();
            }

            if (countAdmin == 1)
            {
                string FIOadmin = "SELECT Surname, Name, Middlename FROM admin WHERE Login = '" + Settings.Default["Login"].ToString() + "';";
                MySqlCommand cmdFIOadmin = new MySqlCommand(FIOadmin, connect);
                cmdFIOadmin.Parameters.AddWithValue("@Login", labelname.Text);
                MySqlDataReader reader = cmdFIOadmin.ExecuteReader();
                while (reader.Read())
                {
                    labelname.Text = reader["Surname"].ToString() + " " + reader["Name"].ToString() + " " + reader["Middlename"].ToString();
                    Settings.Default["Surname"] = reader["Surname"].ToString();
                    Settings.Default["Name"] = reader["Name"].ToString();
                    Settings.Default["Middlename"] = reader["Middlename"].ToString();
                    Settings.Default.Save();
                }
                reader.Close();
            }
            connect.Close();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moveStart = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Point deltaPos = new Point(e.X - moveStart.X, e.Y - moveStart.Y);
                this.Location = new Point(this.Location.X + deltaPos.X,
                  this.Location.Y + deltaPos.Y);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("HH:mm:ss \nd MMMM yyyy");

            DateTime Now = DateTime.Now;

            int Month = Now.Month;
            int Year = Now.Year;
            int Day = Now.Day;

            //03:01 - 11:00 утро, 11:01 - 16:00 день, 16:01 - 23:00 вечер, 23:01 - 03:00 ночь.
            DateTime Morning = new DateTime(Year, Month, Day, hour:03, minute:01, second:00);
            DateTime Morning1 = new DateTime(Year, Month, Day, hour: 11, minute: 00, second: 00);

            DateTime Dinner = new DateTime(Year, Month, Day, hour: 11, minute: 01, second: 00);
            DateTime Dinner1 = new DateTime(Year, Month, Day, hour: 16, minute: 00, second: 00);

            DateTime Evening = new DateTime(Year, Month, Day, hour: 16, minute: 01, second: 00);
            DateTime Evening1 = new DateTime(Year, Month, Day, hour: 23, minute: 00, second: 00);

            DateTime Night = new DateTime(Year, Month, Day, hour: 23, minute: 01, second: 00);
            DateTime Night1 = new DateTime(Year, Month, Day, hour: 03, minute: 00, second: 00);

            if (Now > Morning && Now < Morning1)
            {
                labelDay.Text = "Доброе утро!";
            }

            if (Now > Dinner && Now < Dinner1)
            {
                labelDay.Text = "Добрый день!";
            }

            if (Now > Evening && Now < Evening1)
            {
                labelDay.Text = "Добрый вечер!";

            }

            if (Now > Night && Now < Night1)
            {
                labelDay.Text = "Доброй ночи!";
            }
        }
    }
}