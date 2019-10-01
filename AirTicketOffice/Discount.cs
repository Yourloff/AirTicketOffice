using System;
using System.Drawing;
using System.Windows.Forms;
using AirTicketOffice.Properties;
using Word = Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System.IO;

namespace AirTicketOffice
{
    public partial class Discount : Form
    {
        Point moveStart;
        private readonly string TemplateFileName = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "AeroTicket.docx";

        public Discount()
        {
            InitializeComponent();
        }
        private static Random rnd = new Random();
        private static string myConnectionString = "Database = AirTO; Data Source = localhost; User Id=root; charset= utf8; Password =";
        private MySqlConnection connect = new MySqlConnection(myConnectionString);

        private void Discount_Load(object sender, EventArgs e)
        {
            //DateTime Now = DateTime.Now;

            //int Month = Now.Month;
            //int Year = Now.Year;
            //int Day = Now.Day;

            //DateTime ToBeContinue = new DateTime(Year, Month + 1, Day);
            //DateTime ToBeContinue1 = new DateTime(Year, Month, Day + 1);
            //DateTime ToBeContinue2 = new DateTime(Year, Month, Day + 10);
            //dateTimePicker1.MinDate = DateTime.Now;
            //dateTimePicker1.MaxDate = ToBeContinue2;
            //dateTimePicker2.MaxDate = ToBeContinue;
            //dateTimePicker2.MinDate = ToBeContinue1;
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

        private void Buttoncapt_Click(object sender, EventArgs e)
        {
            var name = Settings.Default["Name"].ToString();
            var surname = Settings.Default["Surname"].ToString();
            Class.HashMD5 HMD5 = new Class.HashMD5();
            string LoginMD5 = HMD5.GetHashString(Settings.Default["Login"].ToString());
           connect.Open();
            string numberPassUser = "SELECT NumberPass FROM User WHERE Login = '" + LoginMD5 + "';";
            MySqlCommand cmdNumberPassUser = new MySqlCommand(numberPassUser, connect);
            object numberPassUserObj = cmdNumberPassUser.ExecuteScalar();
            var doc = Convert.ToString(numberPassUserObj);

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[rnd.Next(chars.Length)];
            }
            var numberBooking = new String(stringChars);
            var dateNow = DateTime.Now;

            var h = rnd.Next(0, 23);
            var m = rnd.Next(0, 59);

            var dateDeparture = dateTimePicker1.Value.ToString("dd.MM.yyyy");
            var dateArrival = dateTimePicker2.Value.ToString("dd.MM.yyyy"); ;
            var dateClockDeparture = dateTimePicker1.Value.ToString("hh:mm");
            var dateClockArrival = $"{h}:{m}";

            var countrydepart = comboBox1.SelectedItem.ToString().TrimStart();
            var countryArrival = comboBox2.SelectedItem.ToString().TrimStart();
            string rndAirplane = "SELECT NameAircraft FROM Aircraft WHERE IDAircraft = " + rnd.Next(1, 8) + ";";
            MySqlCommand cmdRndAirplane = new MySqlCommand(rndAirplane, connect);
            object rndAirplaneObj = cmdRndAirplane.ExecuteScalar();
            connect.Close();
            var Aircraft = Convert.ToString(rndAirplaneObj);
            var priceT = rnd.Next(7000, 10000);
            var priceTF = rnd.Next(4000, 5000);
            var priceMain = priceT + priceTF;
            
            this.TopMost = false;
                var wordApp = new Word.Application();
                wordApp.Visible = false;
           try
           {
                var wordDocument = wordApp.Documents.Open(TemplateFileName);
                ReplaceWordStub("{name}", name, wordDocument);
                ReplaceWordStub("{surname}", surname, wordDocument);
                ReplaceWordStub("{doc}", doc, wordDocument);
                ReplaceWordStub("{numberBooking}", numberBooking, wordDocument);
                ReplaceWordStub("{numberBooking}", numberBooking, wordDocument);
                ReplaceWordStub("{dateNow}", dateNow.ToString(), wordDocument);
                int iT = Convert.ToInt32(Settings.Default["iTicket"]);
                ++iT;
                Settings.Default["iTicket"] = iT.ToString();
                Settings.Default.Save();
                
                ReplaceWordStub("{dateDeparture}", dateDeparture, wordDocument);
                ReplaceWordStub("{countrydepart}", countrydepart, wordDocument);
                ReplaceWordStub("{dateClockDeparture}", dateClockDeparture, wordDocument);

                ReplaceWordStub("{dateArrival}", Convert.ToString(dateArrival), wordDocument);
                ReplaceWordStub("{countryArrival}", countryArrival, wordDocument);
                ReplaceWordStub("{dateClockArrival}", dateClockArrival, wordDocument);

                ReplaceWordStub("{Aircraft}", Aircraft, wordDocument);
                ReplaceWordStub("{priceT}", Convert.ToString(priceT), wordDocument);
                ReplaceWordStub("{priceTF}", Convert.ToString(priceTF), wordDocument);
                ReplaceWordStub("{priceMain}", Convert.ToString(priceMain) , wordDocument);

                if (Convert.ToInt32(Settings.Default["iTicket"]) <= 9)
                {
                    Settings.Default["numTicket"] = "00000" + Settings.Default["iTicket"].ToString();
                    var numTicket = Settings.Default["numTicket"].ToString();
                    connect.Open();
                    string addUser = "INSERT INTO adventure (IDAdventure, numBooking, dateNow, Name, Surname, Aircraft, dateD, dateA, countryD, countryA, price)" +
                                          "VALUES('" + numTicket + "', '" + numberBooking + "', '" + dateNow + "', '" + name + "', '" + surname + "', '" + Aircraft + "', '" + dateDeparture + "', '" + dateArrival + "', '" + countrydepart + "', '" + countryArrival + "', '" + priceMain + "');";
                    MySqlCommand cmdAddUser = new MySqlCommand(addUser, connect);
                    cmdAddUser.ExecuteNonQuery();
                    connect.Close();
                    ReplaceWordStub("{numTicket}", numTicket, wordDocument);
                }
                if (Convert.ToInt32(Settings.Default["iTicket"]) >= 10 && Convert.ToInt32(Settings.Default["iTicket"]) <= 99)
                {
                    Settings.Default["numTicket"] = "0000" + Settings.Default["iTicket"].ToString();
                    var numTicket = Settings.Default["numTicket"].ToString();
                    connect.Open();
                    string addUser = "INSERT INTO adventure (IDAdventure, numBooking, dateNow, Name, Surname, Aircraft, dateD, dateA, countryD, countryA, price)" +
                                          "VALUES('" + numTicket + "', '" + numberBooking + "', '" + dateNow + "', '" + name + "', '" + surname + "', '" + Aircraft + "', '" + dateDeparture + "', '" + dateArrival + "', '" + countrydepart + "', '" + countryArrival + "', '" + priceMain + "');";
                    MySqlCommand cmdAddUser = new MySqlCommand(addUser, connect);
                    cmdAddUser.ExecuteNonQuery();
                    connect.Close();
                    ReplaceWordStub("{numTicket}", numTicket, wordDocument);
                }
                if (Convert.ToInt32(Settings.Default["iTicket"]) >= 100 && Convert.ToInt32(Settings.Default["iTicket"]) <= 999)
                {
                    Settings.Default["numTicket"] = "000" + Settings.Default["iTicket"].ToString();
                    var numTicket = Settings.Default["numTicket"].ToString();
                    connect.Open();
                    string addUser = "INSERT INTO adventure (IDAdventure, numBooking, dateNow, Name, Surname, Aircraft, dateD, dateA, countryD, countryA, price)" +
                                          "VALUES('" + numTicket + "', '" + numberBooking + "', '" + dateNow + "', '" + name + "', '" + surname + "', '" + Aircraft + "', '" + dateDeparture + "', '" + dateArrival + "', '" + countrydepart + "', '" + countryArrival + "', '" + priceMain + "');";
                MySqlCommand cmdAddUser = new MySqlCommand(addUser, connect);
                    cmdAddUser.ExecuteNonQuery();
                    connect.Close();
                    ReplaceWordStub("{numTicket}", numTicket, wordDocument);
                }
                if (Convert.ToInt32(Settings.Default["iTicket"]) >= 1000 && Convert.ToInt32(Settings.Default["iTicket"]) <= 9999)
                {
                    Settings.Default["numTicket"] = "00" + Settings.Default["iTicket"].ToString();
                    var numTicket = Settings.Default["numTicket"].ToString();
                    connect.Open();
                    string addUser = "INSERT INTO adventure (IDAdventure, numBooking, dateNow, Name, Surname, Aircraft, dateD, dateA, countryD, countryA, price)" +
                                          "VALUES('" + numTicket + "', '" + numberBooking + "', '" + dateNow + "', '" + name + "', '" + surname + "', '" + Aircraft + "', '" + dateDeparture + "', '" + dateArrival + "', '" + countrydepart + "', '" + countryArrival + "', '" + priceMain + "');";
                MySqlCommand cmdAddUser = new MySqlCommand(addUser, connect);
                    cmdAddUser.ExecuteNonQuery();
                    connect.Close();
                    ReplaceWordStub("{numTicket}", numTicket, wordDocument);
                }
                if (Convert.ToInt32(Settings.Default["iTicket"]) >= 10000 && Convert.ToInt32(Settings.Default["iTicket"]) <= 99999)
                {
                    Settings.Default["numTicket"] = "0" + Settings.Default["iTicket"].ToString();
                    var numTicket = Settings.Default["numTicket"].ToString();
                    connect.Open();
                    string addUser = "INSERT INTO adventure (IDAdventure, numBooking, dateNow, Name, Surname, Aircraft, dateD, dateA, countryD, countryA, price)" +
                                          "VALUES('" + numTicket + "', '" + numberBooking + "', '" + dateNow + "', '" + name + "', '" + surname + "', '" + Aircraft + "', '" + dateDeparture + "', '" + dateArrival + "', '" + countrydepart + "', '" + countryArrival + "', '" + priceMain + "');";
                MySqlCommand cmdAddUser = new MySqlCommand(addUser, connect);
                    cmdAddUser.ExecuteNonQuery();
                    connect.Close();
                    ReplaceWordStub("{numTicket}", numTicket, wordDocument);
                }
                if (Convert.ToInt32(Settings.Default["iTicket"]) >= 100000 && Convert.ToInt32(Settings.Default["iTicket"]) <= 999999)
                {
                    Settings.Default["numTicket"] = Settings.Default["iTicket"].ToString();
                    var numTicket = Settings.Default["numTicket"].ToString();
                    connect.Open();
                    string addUser = "INSERT INTO adventure (IDAdventure, numBooking, dateNow, Name, Surname, Aircraft, dateD, dateA, countryD, countryA, price)" +
                                          "VALUES('" + numTicket + "', '" + numberBooking + "', '" + dateNow + "', '" + name + "', '" + surname + "', '" + Aircraft + "', '" + dateDeparture + "', '" + dateArrival + "', '" + countrydepart + "', '" + countryArrival + "', '" + priceMain + "');";
                MySqlCommand cmdAddUser = new MySqlCommand(addUser, connect);
                    cmdAddUser.ExecuteNonQuery();
                    connect.Close();
                    ReplaceWordStub("{numTicket}", numTicket, wordDocument);
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Word Documents (*.docx)|*.docx|All files (*.*)|*.*";
                sfd.FileName = surname + name + "AirTicket.docx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    wordDocument.SaveAs(sfd.FileName);
                    MessageBox.Show("Успешно!");

                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        wordDocument.PrintPreview();
                    }
                }
                wordDocument.Close();
            }
            catch(Exception ex)
            {
               MessageBox.Show("Произошла ошибка: " + ex.Message);
                connect.Close();
            }
            finally
            {
               wordApp.Quit();
                connect.Close();
            }
        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText:stubToReplace, ReplaceWith: text);
        }
    }
}
