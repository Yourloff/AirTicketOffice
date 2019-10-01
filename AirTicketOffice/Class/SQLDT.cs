using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AirTicketOffice
{
    class SQLDT
    {
        public static void StartProc()
        {
            try
            {
                ProcessStartInfo processStart = new ProcessStartInfo();
                processStart.FileName = "MySQLx.x\\mysql-5.5\\bin\\mysqld.exe";
                processStart.CreateNoWindow = false;
                processStart.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(processStart);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                StopProc();
            }
        }
        public static void StopProc()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "MySQLx.x\\StopServer.bat";
                proc.StartInfo.WorkingDirectory = "AirTicketOffice\\MySQLx.x";
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
