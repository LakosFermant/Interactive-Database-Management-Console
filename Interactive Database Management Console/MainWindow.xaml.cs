using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Interactive_Database_Management_Console
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        //We intialize the MySQL connection
        public MySqlConnection dblogin;

        //Next, we create the string that will hold the connection info
        public string connectorInfo;

        //We create a closed bool variable that will help us check if a connection is open or not
        public bool IsClosed { get; }

        //Start the program now
        public MainWindow()
        {
            InitializeComponent();
        }

        //Login Button
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            

            //If the the username and password is correct, they should login successfully
            try
            {
                //Setting string values to connector
                connectorInfo = "server=" + serverInput.Text + ";uid=" + userInput.Text + ";pwd=" + passInput.Password + ";";

                //We assign the MySQL object with the connection string
                dblogin = new MySqlConnection(connectorInfo);

                //Then we use that information in connectorinfo to try to login to a MySQL server; If it worked, then display message saying it worked
                dblogin.Open();
                MessageBox.Show("Successfully logged in", "Login Attempt Succeeded");
            }
            
            //If not, then it didnt work
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Attempt Failed");
            }
        }

        //Send Query button
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //We create a new SQL command with the login information to use and the query to send to the database
                //We then create a datatable that will house the information that needs to be displayed to the DataGrid
                //If there is any data to be shown after sending a query, then we fill the DataAdapter with it and set the DataGrid source to that of the DataTable
                MySqlCommand newCmd = new MySqlCommand(queryInput.Text, dblogin);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(newCmd);
                da.Fill(dt);
                tableData.AutoGenerateColumns = true;
                tableData.ItemsSource = dt.DefaultView; 
            }

            //If any of the above didnt work, output why
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nMake sure you are logged into a MySQL server first", "Failed Query Attmept");
            }
        }

        //Log out button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Close the connection if its open and alert the user 
            try
            {
                dblogin.Close();
                MessageBox.Show("Successfully logged out", "Logout Attempt Succeeded");
            }

            //If they weren't even logged in, alert user about it
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nYou are currently not logged in", "Logout Attempt Failed");
            }
            
        }

        //Quit Button
        private void QuitApp_Click(object sender, RoutedEventArgs e)
        {
            //Quit Application
           System.Windows.Application.Current.Shutdown();
        }

        //Keeping these here, only because the build wont compile unless there is a definition of these elements in code. They are just gonna stay empty and down here
        //Visual Studio is wierd, dont ask me
        private void TableData_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ServerInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}
