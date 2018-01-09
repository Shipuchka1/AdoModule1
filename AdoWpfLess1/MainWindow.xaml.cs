using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//для работы с App.Config/Web.config
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using static AdoWpfLess1.MainWindow;
using System.Diagnostics;

namespace AdoWpfLess1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string conectionString = "";
        private string conectionStringOleDb = "";
        public MainWindow()
        {
            InitializeComponent();
            conectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conectionStringOleDb = ConfigurationManager.ConnectionStrings["OleDbConnection"].ConnectionString;
        

    }

        public class equipment
        {
            public int EquipmentId { get; set; }
            public string GarageRoom { get; set; }
        }


    private void ConnectToServerButton_Click(object sender, RoutedEventArgs e)
        {
            //SqlConnection connect = new SqlConnection(conectionString);
            //try
            //{
            //    connect.Open();
            //    ConnectMessage.Content += "Подключение открыто\n";

            //}

            //catch(Exception ex)
            //{
            //    connect.Close();
            //    ConnectMessage.Content += "Подключение закрыто\n" + ex.Message; 
            //}

            List<equipment> eq = new List<equipment>();
            Stopwatch stp = new Stopwatch();
            stp.Start();
            using (SqlConnection con = new SqlConnection(conectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection =  con;
                cmd.CommandText = "select * from newEquipment";

                SqlDataReader sdr = cmd.ExecuteReader();

                while(sdr.Read())
                {
                    equipment eq1 = new equipment();
                    eq1.EquipmentId = Int32.Parse(sdr[0].ToString());
                    eq1.GarageRoom = sdr[1].ToString();

                    eq.Add(eq1);
                }
                stp.Stop();
                MessageBox.Show(stp.ElapsedMilliseconds.ToString());
               
                //ConnectMessage.Content += "Подключение открыто\n";

                //ConnectMessage.Content += "Свойство подключения\n";
                //ConnectMessage.Content += "\t--> cтрока подключения\n" + con.ConnectionString + "\n";
                //ConnectMessage.Content += "\t--> база данных\n" + con.Database + "\n";
                //ConnectMessage.Content += "\t--> сервер\n" + con.ServerVersion + "\n";
                //ConnectMessage.Content += "\t--> состояние\n" + con.State + "\n";
                //ConnectMessage.Content += "\t--> рабочая станция\n" + con.WorkstationId + "\n";
                //ConnectMessage.Content += "Подключение закрыто\n" + "\n";

            }
            ListViewEquipment.ItemsSource = eq;
            //using (OleDbConnection con = new OleDbConnection(conectionStringOleDb))
            //{
            //    con.Open();
            //    ConnectMessage.Content += "Подключение открыто\n";

            //    ConnectMessage.Content += "Свойство подключения\n";
            //    ConnectMessage.Content += "\t--> cтрока подключения\n" + con.ConnectionString + "\n";
            //    ConnectMessage.Content += "\t--> база данных\n" + con.Database + "\n";
            //    ConnectMessage.Content += "\t--> сервер\n" + con.ServerVersion + "\n";
            //    ConnectMessage.Content += "\t--> состояние\n" + con.State + "\n";

            //    ConnectMessage.Content += "Подключение закрыто\n" + "\n";
            //}
        }
    }
}
