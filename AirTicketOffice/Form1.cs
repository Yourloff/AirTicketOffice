using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AirTicketOffice.Properties;
using MySql.Data.MySqlClient;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Data;
using System.Linq;

namespace AirTicketOffice
{
    public partial class Form1 : Form
    {
        Point moveStart;
        private int imageIndex;
        private string[] imageList;
        public Form1()
        {
            InitializeComponent();
           // SQLDT.StartProc();
            Registration reg = new Registration();
            this.Hide();
            reg.ShowDialog();
            flowLayoutPanel2.Hide();
            pictureBox2.Hide();
            panel8.Hide();
            LoadUser();
            LoadTickets();
            timer1.Tick += Timer1_Tick;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
            this.MouseDown += panel1_MouseDown;
            this.MouseMove += panel1_MouseMove;

            connect.Open();
            string dayBase = "SELECT Day FROM ditetimepicker;";
            string monthBase = "SELECT Month FROM ditetimepicker;";
            MySqlCommand cmddayBase = new MySqlCommand(dayBase, connect);
            MySqlCommand cmdmonthBase = new MySqlCommand(monthBase, connect);
            object dayBaseObj = cmddayBase.ExecuteScalar();
            int dayBaseInt = Convert.ToInt32(dayBaseObj);
            object monthBaseObj = cmdmonthBase.ExecuteScalar();
            int monthBaseInt = Convert.ToInt32(monthBaseObj);

            connect.Close();
            DateTime Now = DateTime.Now;
            int Year = Now.Year;
            DateTime dt = new DateTime(Year, monthBaseInt, dayBaseInt);

            TimeSpan TimeRemaining = dt - DateTime.Now;
            label11.Text = TimeRemaining.Days + " дней";
        }

        private static string myConnectionString = "Database = AirTO; Data Source = localhost; User Id=root; charset= utf8; Password =";
        private MySqlConnection connect = new MySqlConnection(myConnectionString);

        private void Timer1_Tick(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connect.Open();
            Class.HashMD5 HMD5 = new Class.HashMD5();
            string LoginMD5 = HMD5.GetHashString(Settings.Default["Login"].ToString());


            string Dday = "SELECT Day FROM ditetimepicker";
            string Mmonth = "SELECT Month FROM ditetimepicker";
            MySqlCommand cmdDday = new MySqlCommand(Dday, connect);
            MySqlCommand cmdMmonth = new MySqlCommand(Mmonth, connect);

            int DdayInt = Convert.ToInt32(cmdDday.ExecuteScalar());
            int MmonthInt = Convert.ToInt32(cmdMmonth.ExecuteScalar());

            DateTime Now = DateTime.Now;
            int Year = Now.Year;

            DateTime dt = new DateTime(Year, MmonthInt, DdayInt);

            TimeSpan TimeRemaining = dt - DateTime.Now;
            label11.Text = TimeRemaining.Days + " дней";

            connect.Close();
        }
        private void LoadUser()
        {
            connect.Open();
            string getUserList = "SELECT Surname, Name, Middlename, Phone, Email, IDGender, Birthday, SeriesPass, NumberPass, IDCountry FROM user ORDER BY IDUser;";
            MySqlCommand cmdGetUserList = new MySqlCommand(getUserList, connect);

            MySqlDataReader reader = cmdGetUserList.ExecuteReader();
            List<string[]> data = new List<string[]>();
            try
            {
                while (reader.Read())
                {
                    data.Add(new string[10]);

                    data[data.Count - 1][0] = reader[0].ToString();      //фамилия
                    data[data.Count - 1][1] = reader[1].ToString();      //имя
                    data[data.Count - 1][2] = reader[2].ToString();      //отчество
                    data[data.Count - 1][3] = reader[3].ToString();      //телефон
                    data[data.Count - 1][4] = reader[4].ToString();      //почта
                    data[data.Count - 1][5] = reader[5].ToString();      //пол
                    data[data.Count - 1][6] = reader[6].ToString();      //дата рождения
                    data[data.Count - 1][7] = reader[7].ToString();     //серия
                    data[data.Count - 1][8] = reader[8].ToString();     //номер
                    data[data.Count - 1][9] = reader[9].ToString();     //страна
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            reader.Close();
            connect.Close();
            // где 1 и 1 - это номер столбца и строки
            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
            }
            labelCount.Text = "Количество записей: " + (dataGridView1.RowCount - 1).ToString();
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
            Settings.Default["Login"] = "";
            Settings.Default["Surname"] = "";
            Settings.Default["Name"] = "";
            Settings.Default["Middlename"] = "";
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

        private void ПрофильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profile prf = new Profile();
            prf.ShowDialog();
        }

        public void FilterData(string valueToFilter)
        {
            connect.Open();
            dataGridView1.Rows.Clear();
            string getUserList = "SELECT Surname, Name, Middlename, Phone, Email, IDGender, Birthday, SeriesPass, NumberPass, IDCountry " +
                "FROM user WHERE Surname = '" + valueToFilter + "';";
            MySqlCommand cmdGetUserList = new MySqlCommand(getUserList, connect);

            MySqlDataReader reader = cmdGetUserList.ExecuteReader();
            List<string[]> data = new List<string[]>();
            try
            {
                while (reader.Read())
                {
                    data.Add(new string[10]);

                    data[data.Count - 1][0] = reader[0].ToString();      //фамилия
                    data[data.Count - 1][1] = reader[1].ToString();      //имя
                    data[data.Count - 1][2] = reader[2].ToString();      //отчество
                    data[data.Count - 1][3] = reader[3].ToString();      //телефон
                    data[data.Count - 1][4] = reader[4].ToString();      //почта
                    data[data.Count - 1][5] = reader[5].ToString();      //пол
                    data[data.Count - 1][6] = reader[6].ToString();      //дата рождения
                    data[data.Count - 1][7] = reader[7].ToString();     //серия
                    data[data.Count - 1][8] = reader[8].ToString();     //номер
                    data[data.Count - 1][9] = reader[9].ToString();     //страна
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            reader.Close();
            connect.Close();
            // где 1 и 1 - это номер столбца и строки
            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
            }
        }

        private void ButtonFilter_Click(object sender, EventArgs e)
        {
            if (textBoxSurname.Text != "")
            {
                string valueToFilter = textBoxSurname.Text.ToString();
                FilterData(valueToFilter);
                labelCount.Text = "Количество записей: " + (dataGridView1.RowCount - 1).ToString();
            }
            else
            {
                if (textBoxSurname.Text == "")
                {
                    dataGridView1.Rows.Clear();
                    LoadUser();
                    labelCount.Text = "Количество записей: " + (dataGridView1.RowCount - 1).ToString();
                }
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            textBoxSurname.Clear();
            textBoxName.Clear();
            textBoxMiddlename.Clear();
            textBoxNumberPassport.Clear();
            dataGridView1.Rows.Clear();
            LoadUser();
            labelCount.Text = "Количество записей: " + (dataGridView1.RowCount - 1).ToString();
        }

        private void tb_TextCh(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            for (int _i = 0; _i < dataGridView1.Rows.Count - 1; _i++)
            {
                bool _S = false, _N = false, _M = false, _Num = false;

                if (dataGridView1.Rows[_i].Cells[0].Value.ToString() == textBoxSurname.Text) _S = true;
                else if (textBoxSurname.Text == "") _S = true;
                if (dataGridView1.Rows[_i].Cells[1].Value.ToString() == textBoxName.Text) _N = true;
                else if (textBoxName.Text == "") _N = true;
                if (dataGridView1.Rows[_i].Cells[2].Value.ToString() == textBoxMiddlename.Text) _M = true;
                else if (textBoxMiddlename.Text == "") _M = true;
                if (dataGridView1.Rows[_i].Cells[8].Value.ToString() == textBoxNumberPassport.Text) _Num = true;
                else if (textBoxNumberPassport.Text == "") _Num = true;

                if (textBoxSurname.Text != "" || textBoxName.Text != "" || textBoxMiddlename.Text != "" || textBoxNumberPassport.Text != "")
                {
                    if (_S && _N && _M && _Num) dataGridView1.Rows[_i].Selected = true;
                }
            }
        }
        private static int a = 0;
        private void PictureBox3_Click(object sender, EventArgs e)
        {
            a++;
            if (a % 2 == 0)
            {
                flowLayoutPanel2.Hide();
            }
            else
            {
                flowLayoutPanel2.Show();
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                comboBox1.Text = comboBox1.Items[0].ToString();
                label6.Text = "Красноярское Центральное Агентство \nВоздушных Сообщений";
                label7.Text = "ул. 9 Мая, 63, Красноярск";
            }
            if (radioButton2.Checked == true)
            {
                comboBox1.Text = comboBox1.Items[1].ToString();
                label6.Text = "Красноярское Центральное Агентство \nВоздушных Сообщений";
                label7.Text = "ул. Тельмана, 30Г, Советский район, микрорайон\nЗелёная Роща, Красноярск, гипермаркет Алпи";
            }
            if (radioButton3.Checked == true)
            {
                comboBox1.Text = comboBox1.Items[2].ToString();
                label6.Text = "Представительство UTair";
                label7.Text = "ул. Авиаторов, 19, Красноярск, оф. 101";
            }
            if (radioButton4.Checked == true)
            {
                comboBox1.Text = comboBox1.Items[3].ToString();
                label6.Text = "Красноярское Центральное Агентство \nВоздушных Сообщений";
                label7.Text = "Аэровокзальная ул., 17, Красноярск";
            }
            if (radioButton5.Checked == true)
            {
                comboBox1.Text = comboBox1.Items[4].ToString();
                label6.Text = "Аэрофлот Российские Авиалинии";
                label7.Text = "ул. Карла Маркса, 73А, Красноярск, этаж 1";
            }
            if (radioButton6.Checked == true)
            {
                comboBox1.Text = comboBox1.Items[5].ToString();
                label6.Text = "Красноярское Центральное Агентство \nВоздушных Сообщений";
                label7.Text = "ул. Александра Матросова, 4, Красноярск";
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        radioButton1.Checked = true;
                        label6.Text = "Красноярское Центральное Агентство \nВоздушных Сообщений";
                        label7.Text = "ул. 9 Мая, 63, Красноярск";
                        break;
                    }
                case 1:
                    {
                        radioButton2.Checked = true;
                        label6.Text = "Красноярское Центральное Агентство \nВоздушных Сообщений";
                        label7.Text = "ул. Тельмана, 30Г, Советский район, микрорайон\nЗелёная Роща, Красноярск, гипермаркет Алпи";
                        break;
                    }
                case 2:
                    {
                        radioButton3.Checked = true;
                        label6.Text = "Представительство UTair";
                        label7.Text = "ул. Авиаторов, 19, Красноярск, оф. 101";
                        break;
                    }
                case 3:
                    {
                        radioButton4.Checked = true;
                        label6.Text = "Красноярское Центральное Агентство \nВоздушных Сообщений";
                        label7.Text = "Аэровокзальная ул., 17, Красноярск";
                        break;
                    }
                case 4:
                    {
                        radioButton5.Checked = true;
                        label6.Text = "Аэрофлот Российские Авиалинии";
                        label7.Text = "ул. Карла Маркса, 73А, Красноярск, этаж 1";
                        break;
                    }
                case 5:
                    {
                        radioButton6.Checked = true;
                        label6.Text = "Красноярское Центральное Агентство \nВоздушных Сообщений";
                        label7.Text = "ул. Александра Матросова, 4, Красноярск";
                        break;
                    }
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            imageIndex--;
            if (imageIndex < 0)
                imageIndex = imageList.Length - 1;

            pictureBoxMain.Image = Image.FromFile(imageList[imageIndex]);
        }

        public string SelectDir()
        {
            string Dir = treeView1.SelectedNode.Text;
            return Dir;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string D = SelectDir();
            if (treeView1.SelectedNode.Text == D)
            {
                imageList = Directory.GetFiles("image\\" + D + "", "*.jp*g");
                imageIndex = 0;
                pictureBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxMain.Image = Image.FromFile(imageList[imageIndex]);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            imageIndex++;
            if (imageIndex > imageList.Length - 1)
                imageIndex = 0;

            pictureBoxMain.Image = Image.FromFile(imageList[imageIndex]);
        }

        private void TabPage3_MouseClick(object sender, MouseEventArgs e)
        {
            treeView1.Focus();
        }

        private void Panel7_MouseEnter(object sender, EventArgs e)
        {

            panel1.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel1.Parent = this;
        }
        int cl = 0;
        private string fileRow;
        private string[] fileDataField;

        public bool DataLoaded { get; private set; }

        private void Panel7_Click(object sender, EventArgs e)
        {
            cl++;
            if (cl % 2 == 0)
            {
                pictureBox2.Hide();
                panel8.Hide();
            }
            else
            {
                pictureBox2.Show();
                panel8.Show();
            }

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Discount dsc = new Discount();
            dsc.ShowDialog();
        }

        private void ВремяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dateTimePicker dtp = new dateTimePicker();
            dtp.Show();
        }

        private void ДобавитьИзображенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "image\\");
        }

        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pictureBoxMain.ContextMenuStrip = contextMenuStrip1;
        }

        private void ДобавитьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "image\\");
        }

        private void LoadTickets()
        {
            connect.Open();
            string getTicketList = "SELECT IDAdventure, numBooking, dateNow, Name, Surname, Aircraft, dateD, dateA, countryD, countryA, price FROM Adventure ORDER BY IDAd;";
            MySqlCommand cmdgetTicketList = new MySqlCommand(getTicketList, connect);

            MySqlDataReader reader = cmdgetTicketList.ExecuteReader();
            List<string[]> data = new List<string[]>();
            try
            {
                while (reader.Read())
                {
                    data.Add(new string[11]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                    data[data.Count - 1][4] = reader[4].ToString();
                    data[data.Count - 1][5] = reader[5].ToString();
                    data[data.Count - 1][6] = reader[6].ToString();
                    data[data.Count - 1][7] = reader[7].ToString();
                    data[data.Count - 1][8] = reader[8].ToString();
                    data[data.Count - 1][9] = reader[9].ToString();
                    data[data.Count - 1][10] = reader[10].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            reader.Close();
            connect.Close();
            // где 1 и 1 - это номер столбца и строки
            foreach (string[] s in data)
            {
                dataGridView2.Rows.Add(s);
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            string text = "";

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    text += dataGridView2.Rows[i].Cells[j].Value.ToString().Replace(';', '\0') + ";";
                }
                text += "\n";
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Сохранить CSV";
            sfd.FileName = "Tickets";
            sfd.Filter = "Comma-Separated Values (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.ShowDialog();

            string path = sfd.FileName;
            File.WriteAllText(path, text, Encoding.UTF8);
            MessageBox.Show("CSV файл записан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Открыть CSV";
            ofd.Filter = "Comma-Separated Values (*.csv)|*.csv|All files (*.*)|*.*";

            dataGridView3.Rows.Clear();
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string csvPath = ofd.FileName;

                    if (System.IO.File.Exists(csvPath))
                    {
                        System.IO.StreamReader fileReader = new System.IO.StreamReader(csvPath, false);

                        //Reading Data
                        while (fileReader.Peek() != -1)
                        {
                            fileRow = fileReader.ReadLine();
                            fileDataField = fileRow.Split(';');
                            dataGridView3.Rows.Add(fileDataField);
                        }
                        fileReader.Dispose();
                        fileReader.Close();
                    }
                    else
                    {
                        MessageBox.Show("CSV Bestand niet gevonden.");
                    }
                }
                DataLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear(); LoadTickets();
        }
    }
}