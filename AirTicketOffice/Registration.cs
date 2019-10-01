using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using AirTicketOffice.Class;

namespace AirTicketOffice
{
    public partial class Registration : Form
    {
        public Registration()
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

        private void Registration_Load(object sender, EventArgs e)
        {
            label5.Text = "Регистрируясь в приложении с целью " +
                            "бронирования услуг," +
                                " представленных в приложении, я даю своё согласие" +
                                " на обработку моих персональных данных.";

            labelErrorEmail.Hide();
            labelErrorLogin.Hide();
            labelYear.Hide();
            labelPasswordError.Hide();
            labelRepeatPassword.Hide();

            dateTimePickerBirthday.Value = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);
            comboBox1.SelectedIndex = 0;
            string myConnectionString = "Database = AirTO; Data Source = localhost; User Id=root; charset= utf8; Password =";
            MySqlConnection connect = new MySqlConnection(myConnectionString);
            connect.Open();

            string Country = "SELECT NameCountry FROM Countrys";
            MySqlCommand cmdCountry = new MySqlCommand(Country, connect);
            MySqlDataReader reader = cmdCountry.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString("NameCountry"));
            }
            reader.Close();
            connect.Close();
        }

    private void Label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label2_MouseEnter(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(255, 77, 77);
        }

        private void Label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(31, 38, 48);
        }
        
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void LabelCancel_Click(object sender, EventArgs e)
        {
            Authorization auth = new Authorization();
            this.Hide();
            auth.ShowDialog();
            
        }
        private void DateTimePickerBirthday_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            int year = dateNow.Year - dateTimePickerBirthday.Value.Year;
            if (dateNow.Month < dateTimePickerBirthday.Value.Month ||
                (dateNow.Month == dateTimePickerBirthday.Value.Month && dateNow.Day < dateTimePickerBirthday.Value.Day)) year--;

            if (year < 18)
            {
                labelYear.Show();
                labelYear.Text = "Только с 18 лет!";
                MessageBox.Show("Регистрация доступна только с наступления 18 лет", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonRegistration.Enabled = false;
            }
            else
            {
                buttonRegistration.Enabled = true;
                labelYear.Hide();
            }
        }
        
        private void TextBoxPassword_TextChanged(object sender, EventArgs e)
        {
            
            if (textBoxPassword.TextLength < 6)
            {
                labelPasswordError.Show();
                labelPasswordError.Text = "Пароль не менее 6 символов";
                return;
            }
            else
            {
                labelPasswordError.Hide();
            }
            if (textBoxPassword.Text != textBoxRepeatPassword.Text)
            {
                labelRepeatPassword.Show();
                labelRepeatPassword.ForeColor = Color.FromArgb(128, 0, 0);
                labelRepeatPassword.Text = "Пароли не совпадают";
            }
            else
            {
                if (textBoxPassword.Text != "" && textBoxRepeatPassword.Text != "")
                {
                    labelRepeatPassword.ForeColor = Color.FromArgb(12, 165, 47);
                    labelRepeatPassword.Text = "Пароли совпадают";
                }
            }
            string pattern;
            if (textBoxPassword.Text != "")
            {
                pattern = @"([\$\@#%\^!]+?)";
                if(!System.Text.RegularExpressions.Regex.IsMatch(textBoxPassword.Text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    labelPasswordError.Show();
                    labelPasswordError.Text = "Пароль должен содержать символ: ! @ # $ % ^";
                    return;
                }
                else
                {
                    labelPasswordError.Hide();
                }
            }

            pattern = @"([0-9]+?)";
            if(!System.Text.RegularExpressions.Regex.IsMatch(textBoxPassword.Text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                labelPasswordError.Show();
                labelPasswordError.Text = "Не содержит по крайней мере одну цифру";
                return;
            }
            else
            {
                labelPasswordError.Hide();
            }

            pattern = @"([A-Z]+?)";
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBoxPassword.Text, pattern))
            {
                labelPasswordError.Show();
                labelPasswordError.Text = "Не содержит по крайней мере одну заглавную букву";
                return;
            }
            else
            {
                labelPasswordError.Hide();
            }
        }

        private void TextBoxRepeatPassword_TextChanged(object sender, EventArgs e)
        {
            if(textBoxPassword.Text != textBoxRepeatPassword.Text)
            {
                labelRepeatPassword.Show();
                labelRepeatPassword.ForeColor = Color.FromArgb(128, 0, 0);
                labelRepeatPassword.Text = "Пароли не совпадают";
            }
            else
            {
                if(textBoxPassword.Text != "" && textBoxRepeatPassword.Text != "")
                {
                    labelRepeatPassword.ForeColor = Color.FromArgb(12, 165, 47);
                    labelRepeatPassword.Text = "Пароли совпадают";
                }
            }
        }
        private void ButtonRegistration_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Не выбран пол, выберите свой пол", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonRegistration.Enabled = false;
            }

            maskedPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            maskedPassportSeria.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            maskedTextBoxPassportNumber.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (textBoxPassword.Text == textBoxRepeatPassword.Text && textBoxname.Text != "" && textBoxSurname.Text != "" &&
                textBoxMiddlename.Text != "" && textBoxEmail.Text != "" && textBoxLogin.Text != "" && comboBox1.Text != "Не выбрано" &&
                (!String.IsNullOrEmpty(maskedTextBoxPassportNumber.Text) || !String.IsNullOrWhiteSpace(maskedTextBoxPassportNumber.Text) &&
                (!String.IsNullOrEmpty(maskedPassportSeria.Text) || !String.IsNullOrWhiteSpace(maskedPassportSeria.Text)) &&
                 (!String.IsNullOrEmpty(maskedPhone.Text) || !String.IsNullOrWhiteSpace(maskedPhone.Text)) && labelErrorEmail.Text == "Ок"))
            {
                if(checkBox1.Checked == true)
                {
                    Class.HashMD5 HMD5 = new Class.HashMD5();
                    string PasswordMD5 = HMD5.GetHashString(textBoxPassword.Text);   //хеширование пароля
                    string LoginMD5 = HMD5.GetHashString(textBoxLogin.Text);        //хеширование логина

                    connect.Open();

                    if (radioButton1.Checked == true)
                    {
                        string addUser = "INSERT INTO User (Login, Password, Surname, Name, Middlename, Phone, Email, IDGender, Birthday, SeriesPass, NumberPass, IDCountry)" +
                                          "VALUES('" + LoginMD5 + "', '" + PasswordMD5 + "', '" + textBoxSurname.Text + "', '" + textBoxname.Text + "', '" + textBoxMiddlename.Text + "', '+7" + maskedPhone.Text + "', '" + textBoxEmail.Text + "', " + 1 + ", '" + this.dateTimePickerBirthday.Text + "', '" + maskedPassportSeria.Text + "', '" + maskedTextBoxPassportNumber.Text + "', '" + comboBox1.SelectedIndex + "');";
                        MySqlCommand cmdAddUser = new MySqlCommand(addUser, connect);

                        cmdAddUser.ExecuteNonQuery();
                        connect.Close();

                        Authorization auth = new Authorization();
                        this.Hide();
                        auth.ShowDialog();
                    }
                    if (radioButton2.Checked == true)
                    {
                        string addUser = "INSERT INTO User (Login, Password, Surname, Name, Middlename, Phone, Email, IDGender, Birthday, SeriesPass, NumberPass, IDCountry)" +
                                          "VALUES('" + LoginMD5 + "', '" + PasswordMD5 + "', '" + textBoxSurname.Text + "', '" + textBoxname.Text + "', '" + textBoxMiddlename.Text + "', '+7" + maskedPhone.Text + "', '" + textBoxEmail.Text + "', " + 2 + ", '" + this.dateTimePickerBirthday.Text + "', '" + maskedPassportSeria.Text + "', '" + maskedTextBoxPassportNumber.Text + "', '" + comboBox1.SelectedIndex + "');";
                        MySqlCommand cmdAddUser = new MySqlCommand(addUser, connect);

                        cmdAddUser.ExecuteNonQuery();
                        connect.Close();

                        Authorization auth = new Authorization();
                        this.Hide();
                        auth.ShowDialog();
                    }
                }
                else
                {
                    if(MessageBox.Show("Подтвердите согласие на обработку данных", "Важно", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        checkBox1.Checked = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Введены не все поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RadioButton1_Click(object sender, EventArgs e)
        {
            buttonRegistration.Enabled = true;
        }

        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            string email = textBoxEmail.Text;
            while (true)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(email, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    string checkEmail = "SELECT COUNT(*) FROM User WHERE Email='" + textBoxEmail.Text + "'";
                    MySqlCommand cmdCheckEmail = new MySqlCommand(checkEmail, connect);

                    connect.Open();
                    cmdCheckEmail.Prepare();
                    int countEmail = Convert.ToInt32(cmdCheckEmail.ExecuteScalar());

                    if (countEmail == 0)
                    {
                        labelErrorEmail.Show();
                        labelErrorEmail.ForeColor = Color.FromArgb(12, 165, 47);
                        labelErrorEmail.Text = "Ок";
                        buttonRegistration.Enabled = true;
                        connect.Close();
                        break;
                    }
                    else
                    {
                        buttonRegistration.Enabled = false;
                        labelErrorEmail.Text = "Почта занята";
                        labelErrorEmail.ForeColor = Color.FromArgb(128, 0, 0);
                        connect.Close();
                        break;
                    }
                }
                else
                {
                    labelErrorEmail.Show();
                    labelErrorEmail.Text = "Некорректный email";
                    break;
                }
            }
        }

        private void TextBoxLogin_TextChanged(object sender, EventArgs e)
        {
            Class.HashMD5 HMD5 = new Class.HashMD5();
            string LoginMD5 = HMD5.GetHashString(textBoxLogin.Text);        //хеширование логина

            string checkLogin = "SELECT COUNT(*) FROM User WHERE Login='" + LoginMD5 + "'";
            MySqlCommand cmdCheckLogin = new MySqlCommand(checkLogin, connect);

            connect.Open();
            cmdCheckLogin.Prepare();
            int countLogin = Convert.ToInt32(cmdCheckLogin.ExecuteScalar());

            if (countLogin == 0 && textBoxLogin.Text != "")
            {
                labelErrorLogin.Show();
                labelErrorLogin.ForeColor = Color.FromArgb(12, 165, 47);
                labelErrorLogin.Text = "Ок";
                buttonRegistration.Enabled = true;
                connect.Close();
            }
            else
            {
                buttonRegistration.Enabled = false;
                labelErrorLogin.Text = "Логин занят";
                labelErrorLogin.ForeColor = Color.FromArgb(128, 0, 0);
                connect.Close();
            }
        }
    }
}