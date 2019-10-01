using AirTicketOffice.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AirTicketOffice
{
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
            ProfileF();
        }

        private bool _moving;
        private Point _startLocation;

        private static string myConnectionString = "Database = AirTO; Data Source = localhost; User Id=root; charset= utf8; Password =";
        private MySqlConnection connect = new MySqlConnection(myConnectionString);

        private void ProfileF()
        {
            textBoxSurname.Hide();
            textBoxName.Hide();
            textBoxMiddlename.Hide();
            textBoxPhone.Hide();
            textBoxEmail.Hide();
            buttonSave.Hide();
            panel5.Hide();

            string myConnectionString = "Database = AirTO; Data Source = localhost; User Id=root; charset= utf8; Password =";
            MySqlConnection connect = new MySqlConnection(myConnectionString);
            connect.Open();

            Class.HashMD5 HMD5 = new Class.HashMD5();
            string LoginMD5 = HMD5.GetHashString(Settings.Default["Login"].ToString());

            string checkUser = "SELECT count(*) FROM User WHERE Login = '" + LoginMD5 + "';";
            MySqlCommand cmdcheckUser = new MySqlCommand(checkUser, connect);
            int countUser = Convert.ToInt32(cmdcheckUser.ExecuteScalar());

            if (countUser == 1)
            {
                string query = "SELECT * FROM User ORDER BY IDUser";
                string countryNumUser = "SELECT IDCountry FROM User WHERE Login ='" + LoginMD5 + "';";
                MySqlCommand cmdcountryNum = new MySqlCommand(countryNumUser, connect);
                object countryObjUser = cmdcountryNum.ExecuteScalar();
                int country = Convert.ToInt32(countryObjUser);

                string countryNumCountrys = "SELECT Namecountry FROM Countrys WHERE IDCountry = " + country + ";";
                MySqlCommand cmdcountryNumCountrys = new MySqlCommand(countryNumCountrys, connect);
                object countryObj = cmdcountryNumCountrys.ExecuteScalar();
                string countryString = Convert.ToString(countryObj);

                string MWNumUser = "SELECT IDGender FROM User WHERE Login ='" + LoginMD5 + "';";
                MySqlCommand cmdMWNumUser = new MySqlCommand(MWNumUser, connect);
                object MWObjUser = cmdMWNumUser.ExecuteScalar();
                int MWInt = Convert.ToInt32(MWObjUser);

                string MWNumGender = "SELECT Gendername FROM Gender WHERE IDGender = " + MWInt + ";";
                MySqlCommand cmdMWNumGender = new MySqlCommand(MWNumGender, connect);
                object GenderObj = cmdMWNumGender.ExecuteScalar();
                string GenderString = Convert.ToString(GenderObj);

                MySqlCommand command = new MySqlCommand(query, connect);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[12]);

                    data[data.Count - 1][1] = reader[3].ToString();      //фамилия
                    data[data.Count - 1][2] = reader[4].ToString();      //имя
                    data[data.Count - 1][3] = reader[5].ToString();      //отчество
                    data[data.Count - 1][4] = reader[6].ToString();      //телефон
                    data[data.Count - 1][5] = reader[7].ToString();      //почта
                    data[data.Count - 1][7] = reader[9].ToString();      //дата рождения
                    data[data.Count - 1][10] = reader[12].ToString();      //страна
                }
                reader.Close();
                connect.Close();
                // где 1 и 1 - это номер столбца и строки
                foreach (string[] s in data)
                {
                    dataGridView1.Rows.Add(s);
                }
                dataGridView1[0, 0].Value = Settings.Default["Login"].ToString();
                dataGridView1[6, 0].Value = GenderString;
                dataGridView1[8, 0].Value = countryString;
                labelPassword.Text = "Пароль: " + Settings.Default["Password"].ToString();
            }
            else
            {
                string checkAdmin = "SELECT count(*) FROM Admin WHERE Login = '" + Settings.Default["Login"].ToString() + "';"; //юзер
                MySqlCommand cmdcheckAdmin = new MySqlCommand(checkAdmin, connect);
                int countAdmin = Convert.ToInt32(cmdcheckAdmin.ExecuteScalar());

                if (countAdmin == 1)
                {
                    string query = "SELECT * FROM Admin ORDER BY IDAdmin";
                    string countryNumAdmin = "SELECT IDCountry FROM Admin WHERE Login ='" + Settings.Default["Login"].ToString() + "';";
                    MySqlCommand cmdcountryNum = new MySqlCommand(countryNumAdmin, connect);
                    object countryObjAdmin = cmdcountryNum.ExecuteScalar();
                    int country = Convert.ToInt32(countryObjAdmin);

                    string countryNumCountrys = "SELECT Namecountry FROM Countrys WHERE IDCountry = " + country + ";";
                    MySqlCommand cmdcountryNumCountrys = new MySqlCommand(countryNumCountrys, connect);
                    object countryObj = cmdcountryNumCountrys.ExecuteScalar();
                    string countryString = Convert.ToString(countryObj);

                    string MWNumAdmin = "SELECT IDGender FROM Admin WHERE Login ='" + Settings.Default["Login"].ToString() + "';";
                    MySqlCommand cmdMWNumAdmin = new MySqlCommand(MWNumAdmin, connect);
                    object MWObjAdmin = cmdMWNumAdmin.ExecuteScalar();
                    int MWInt = Convert.ToInt32(MWObjAdmin);

                    string MWNumGender = "SELECT Gendername FROM Gender WHERE IDGender = " + MWInt + ";";
                    MySqlCommand cmdMWNumGender = new MySqlCommand(MWNumGender, connect);
                    object GenderObj = cmdMWNumGender.ExecuteScalar();
                    string GenderString = Convert.ToString(GenderObj);

                    MySqlCommand command = new MySqlCommand(query, connect);
                    MySqlDataReader reader = command.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    while (reader.Read())
                    {
                        data.Add(new string[9]);

                        data[data.Count - 1][0] = reader[1].ToString();      //логин
                        data[data.Count - 1][1] = reader[3].ToString();      //фамилия
                        data[data.Count - 1][2] = reader[4].ToString();      //имя
                        data[data.Count - 1][3] = reader[5].ToString();      //отчество
                        data[data.Count - 1][4] = reader[6].ToString();      //телефон
                        data[data.Count - 1][5] = reader[7].ToString();      //почта
                        data[data.Count - 1][7] = reader[9].ToString();      //дата рождения 
                    }
                    reader.Close();
                    connect.Close();
                    // где 1 и 1 - это номер столбца и строки
                    foreach (string[] s in data)
                    {
                        dataGridView1.Rows.Add(s);
                    }
                    dataGridView1[6, 0].Value = GenderString;
                    dataGridView1[8, 0].Value = countryString;
                    labelPassword.Text = "Пароль: " + Settings.Default["Password"].ToString();
                }
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

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            textBoxSurname.Show();
            textBoxName.Show();
            textBoxMiddlename.Show();
            textBoxPhone.Show();
            textBoxEmail.Show();
            buttonSave.Show();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            connect.Open();
            string checkAdmin = "SELECT count(*) FROM Admin WHERE Login = '" + Settings.Default["Login"].ToString() + "';";
            MySqlCommand cmdcheckAdmin = new MySqlCommand(checkAdmin, connect);
            int countAdmin = Convert.ToInt32(cmdcheckAdmin.ExecuteScalar());

            if (countAdmin == 1)
            {
                if (textBoxSurname.Text != "")
                {
                    dataGridView1[1, 0].Value = textBoxSurname.Text;
                    string updateSurname = "UPDATE Admin SET Surname = '" + textBoxSurname.Text + "' WHERE Login = '" + Settings.Default["Login"].ToString() + "'";
                    MySqlCommand cmdupdateSurname = new MySqlCommand(updateSurname, connect);
                    cmdupdateSurname.ExecuteNonQuery();
                }
                if (textBoxName.Text != "")
                {
                    dataGridView1[2, 0].Value = textBoxName.Text;
                    string updateName = "UPDATE Admin SET Name = '" + textBoxName.Text + "' WHERE Login = '" + Settings.Default["Login"].ToString() + "'";
                    MySqlCommand cmdupdateName = new MySqlCommand(updateName, connect);
                    cmdupdateName.ExecuteNonQuery();
                }
                if (textBoxMiddlename.Text != "")
                {
                    dataGridView1[3, 0].Value = textBoxMiddlename.Text;
                    string updateMiddleame = "UPDATE Admin SET Middlename = '" + textBoxMiddlename.Text + "' WHERE Login = '" + Settings.Default["Login"].ToString() + "'";
                    MySqlCommand cmdupdateMiddleame = new MySqlCommand(updateMiddleame, connect);
                    cmdupdateMiddleame.ExecuteNonQuery();
                }
                if (textBoxPhone.Text != "")
                {
                    dataGridView1[4, 0].Value = textBoxPhone.Text;
                    string updatePhone = "UPDATE Admin SET Phone = '" + textBoxPhone.Text + "' WHERE Login = '" + Settings.Default["Login"].ToString() + "'";
                    MySqlCommand cmdupdatePhone = new MySqlCommand(updatePhone, connect);
                    cmdupdatePhone.ExecuteNonQuery();
                }
                if (textBoxEmail.Text != "")
                {
                    dataGridView1[5, 0].Value = textBoxEmail.Text;
                    string updateEmail = "UPDATE Admin SET Email = '" + textBoxEmail.Text + "' WHERE Login = '" + Settings.Default["Login"].ToString() + "'";
                    MySqlCommand cmdupdateEmail = new MySqlCommand(updateEmail, connect);
                    cmdupdateEmail.ExecuteNonQuery();
                }
            }
            else
            {
                Class.HashMD5 HMD5 = new Class.HashMD5();
                string LoginMD5 = HMD5.GetHashString(Settings.Default["Login"].ToString());

                string checkUser = "SELECT count(*) FROM User WHERE Login = '" + LoginMD5 + "';"; //юзер
                MySqlCommand cmdcheckUser = new MySqlCommand(checkUser, connect);     //юзер
                int countUser = Convert.ToInt32(cmdcheckUser.ExecuteScalar());

                if (countUser == 1)
                {
                    if (textBoxSurname.Text != "")
                    {
                        dataGridView1[1, 0].Value = textBoxSurname.Text;
                        string updateSurname = "UPDATE User SET Surname = '" + textBoxSurname.Text + "' WHERE Login = '" + LoginMD5 + "'";
                        MySqlCommand cmdupdateSurname = new MySqlCommand(updateSurname, connect);
                        cmdupdateSurname.ExecuteNonQuery();
                    }
                    if (textBoxName.Text != "")
                    {
                        dataGridView1[2, 0].Value = textBoxName.Text;
                        string updateName = "UPDATE User SET Name = '" + textBoxName.Text + "' WHERE Login = '" + LoginMD5 + "'";
                        MySqlCommand cmdupdateName = new MySqlCommand(updateName, connect);
                        cmdupdateName.ExecuteNonQuery();
                    }
                    if (textBoxMiddlename.Text != "")
                    {
                        dataGridView1[3, 0].Value = textBoxMiddlename.Text;
                        string updateMiddleame = "UPDATE User SET Middlename = '" + textBoxMiddlename.Text + "' WHERE Login = '" + LoginMD5 + "'";
                        MySqlCommand cmdupdateMiddleame = new MySqlCommand(updateMiddleame, connect);
                        cmdupdateMiddleame.ExecuteNonQuery();
                    }
                    if (textBoxPhone.Text != "")
                    {
                        dataGridView1[4, 0].Value = textBoxPhone.Text;
                        string updatePhone = "UPDATE User SET Phone = '" + textBoxPhone.Text + "' WHERE Login = '" + LoginMD5 + "'";
                        MySqlCommand cmdupdatePhone = new MySqlCommand(updatePhone, connect);
                        cmdupdatePhone.ExecuteNonQuery();
                    }
                    if (textBoxEmail.Text != "")
                    {
                        dataGridView1[5, 0].Value = textBoxEmail.Text;
                        string updateEmail = "UPDATE User SET Email = '" + textBoxEmail.Text + "' WHERE Login = '" + LoginMD5 + "'";
                        MySqlCommand cmdupdateEmail = new MySqlCommand(updateEmail, connect);
                        cmdupdateEmail.ExecuteNonQuery();
                    }
                }
            }
            connect.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            panel5.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                connect.Open();
                string checkAdmin = "SELECT count(*) FROM Admin WHERE Login = '" + Settings.Default["Login"].ToString() + "';"; //юзер
                MySqlCommand cmdcheckAdmin = new MySqlCommand(checkAdmin, connect);
                int countAdmin = Convert.ToInt32(cmdcheckAdmin.ExecuteScalar());

                if (countAdmin == 1)
                {
                    if (textBox1.Text != Settings.Default["Password"].ToString())
                    {
                        string updatePassword = "UPDATE Admin SET Password = '" + textBox1.Text + "' WHERE Login = '" + Settings.Default["Login"].ToString() + "'";
                        MySqlCommand cmdupdatePassword = new MySqlCommand(updatePassword, connect);
                        cmdupdatePassword.ExecuteNonQuery();
                        labelPassword.Text = textBox1.Text;
                        connect.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароли совпадают, введите новый пароль");
                        connect.Close();
                    }
                }
                else
                {
                    Class.HashMD5 HMD5 = new Class.HashMD5();
                    string LoginMD5 = HMD5.GetHashString(Settings.Default["Login"].ToString());
                    string PasswordMD5 = HMD5.GetHashString(textBox1.Text);

                    if (textBox1.Text != PasswordMD5)
                    {
                        string updatePassword = "UPDATE User SET Password = '" + PasswordMD5 + "' WHERE Login = '" + LoginMD5 + "'";
                        MySqlCommand cmdupdatePassword = new MySqlCommand(updatePassword, connect);
                        cmdupdatePassword.ExecuteNonQuery();
                        labelPassword.Text = textBox1.Text;
                        connect.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароли совпадают, введите новый пароль");
                        connect.Close();
                    }
                }
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _moving = true; _startLocation = e.Location;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _moving = false;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_moving) { pictureBox1.Left += e.Location.X - _startLocation.X; pictureBox1.Top += e.Location.Y - _startLocation.Y; }
        }

        private void ДобавитьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image;

            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}