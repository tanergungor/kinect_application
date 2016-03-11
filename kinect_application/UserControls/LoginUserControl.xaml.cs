using KINECT_APPLICATION.DataStructures;
using KINECT_APPLICATION.DB;
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

namespace KINECT_APPLICATION
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        // Create a singleton database connection object
        private DatabaseConnection _databaseConnection = DatabaseConnection.getDatabaseConnection();

        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Check if the e-mail or the password are empty or not
            if(!String.IsNullOrEmpty(Email.Text) && !String.IsNullOrEmpty(Password.Password))
            {
                // Check if the doctor exists in the database or not
                // If it exists, get the information
                Person doctor = _databaseConnection.Login(Email.Text, Password.Password);

                // If the doctor exists in the system, then log in to the system
                if (doctor.Id != null)
                {
                    // Delete the children of the main window content
                    MainWindow.MainWindowContent.Children.Clear();
                    // Create the new user control which shows the doctor's information and patients
                    SelectDoctorUserControl window = new SelectDoctorUserControl(doctor);
                    // Add the new user control to the main window content
                    MainWindow.MainWindowContent.Children.Add(window);
                }
                else
                {
                    // If the doctor doesn not exist in the system, show the message
                    MessageBox.Show("LOGIN: Unsuccessful - The user cannot found!");
                }
            }
            else
            {
                // If the e-mail or the password are empty, show the error message
                MessageBox.Show("LOGIN: Unsuccessful - Empty e-mail or password!");
            }
        }
    }
}
