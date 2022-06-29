using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kasir
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new FormLogin());
        }

        public static MySqlConnection conn = null;
        public static Boolean bisaLogin;
        public static String serverVar;
        public static String uidVar;
        public static String databaseVar;

        public static void setConn(String server, String uid, String database)
        {
            serverVar = server;
            uidVar = uid;
            databaseVar = database;
            conn = new MySqlConnection
            ($"server = {server}; uid = {uid}; database = {database};");
            openConn();
        }

        public static void openConn()
        {
            try
            {
                conn.Open();
                bisaLogin = true;
            }
            catch (Exception ex)
            {
                bisaLogin = false;
                MessageBox.Show("Tidak bisa menghubungkan database!!!");
            }
        }
    }
}
