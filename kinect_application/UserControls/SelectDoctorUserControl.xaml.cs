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
using System.Text.RegularExpressions;

//Using namespaces 
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using KINECT_APPLICATION.DataStructures;
using KINECT_APPLICATION.DB;
using System.IO;
using System.Reflection;

namespace KINECT_APPLICATION
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class SelectDoctorUserControl : UserControl
    {
        // Create a singleton database connection object
        private DatabaseConnection _databaseConnection = DatabaseConnection.getDatabaseConnection();
        // Create a doctor object
        private Person _doctor = null;

        internal SelectDoctorUserControl(Person doctor)
        {
            InitializeComponent();

            // Set the doctor object
            _doctor = doctor;
            // Set the doctor name
            Name.Text = _doctor.Name;
            // Set the doctor surname
            Surname.Text = _doctor.Surname;
            // Set the doctor photo
            Photo.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/PHOTOS/" + _doctor.Id + ".png"));
            // Set the doctor phone
            Phone.Text = _doctor.Phone;
            // Set the doctor e-mail
            Email.Text = _doctor.Email;
            // Set the doctor gender
            Gender.Text = _doctor.Gender;
            // Set the doctor birthdate
            Birthdate.Text = _doctor.Birthdate.ToString();

            if(_doctor.Gender.Equals("Male"))
            {
                Welcome.Text = "Welcome, Mr. " + _doctor.Name + " " + _doctor.Surname;
            }
            else
            {
                Welcome.Text = "Welcome, Ms. " + _doctor.Name + " " + _doctor.Surname;
            }

            // Select all the patients that belongs to the current doctor from database
            _databaseConnection.SelectPatients(patientList);
            // Select all the exercises in the database
            _databaseConnection.SelectExercises(exerciseList);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Create a regular expression that only accepts the number
            Regex regex = new Regex("[^0-9]+");
            // Check if the text is matched with the regular expression created
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SelectPatient_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected row from the patient list
            DataRowView dataRow = (DataRowView)patientList.SelectedItem;
            // Get the patient id whose index value is 0
            String patientID = dataRow.Row.ItemArray[0].ToString();
            // Select the patient from the database according to the patient id
            Person patient = _databaseConnection.SelectPatient(patientID);

            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the patient's information and tasks
            SelectPatientUserControl window = new SelectPatientUserControl(_doctor, patient);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }

        private void InsertPatient_Click(object sender, RoutedEventArgs e)
        {
            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the patient's information that is going to be filled
            InsertPatientUserControl window = new InsertPatientUserControl(_doctor);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            // Show the warning message to the doctor, if s/he is sure to delete the patient from the list
            // If the patient is deleted from the database, it is imposible to recover the patient's information
            if (MessageBox.Show("Do you really want to delete the patient from the list?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                // Get the selected row from the patient list
                DataRowView dataRow = (DataRowView)patientList.SelectedItem;
                // Get the patient id whose index value is 0
                String patientId = dataRow.Row.ItemArray[0].ToString();

                // Delete the patient from the database
                Boolean IsDeleted = _databaseConnection.DeletePatient(patientId);
                // Select all the patients from the database and set them to the list
                _databaseConnection.SelectPatients(patientList);

                // Check if the patient is deleted
                if (IsDeleted)
                {
                    // Delete the patient photo 
                    File.Delete(System.IO.Path.Combine("C:/Users/Taner/Desktop/kinect_application/kinect_application/Resources/PHOTOS/", patientId + ".png"));

                    // If the patient is deleted, show the message
                    MessageBox.Show("DELETE: Successful - The patient is deleted!");
                }
                else
                {
                    // If the patient is not deleted, show the error message
                    MessageBox.Show("DELETE: Unsuccessful - The patient is not deleted!!");
                }
            }
        }





        private void SelectExercise_Click(object sender, RoutedEventArgs e)
        {
            // SelectExerciseUserControl window = new SelectExerciseUserControl();
            // window.Show();
        }

        private void DeleteExercise_Click(object sender, RoutedEventArgs e)
        {
            // DeleteExerciseUserControl window = new DeleteExerciseUserControl();
            // window.Show();
        }

        private void InsertExercise_Click(object sender, RoutedEventArgs e)
        {
            // AddExerciseUserControl window = new AddExerciseUserControl();
            // window.Show();
        }





        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            // Create open file dialog 
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            openFileDialog.Filter = "PNG Files (*.png)|*.png";

            // Display open file dialog by calling show dialog method 
            Nullable<bool> result = openFileDialog.ShowDialog();

            // Get the selected file name if it is not not
            if (result == true)
            {
                // Get the path of the doctor photo
                _doctor.Photo = openFileDialog.FileName;
                // Set the doctor photo
                Photo.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        // WARNING:: CHECK EMPTY FIELDS!
        private void UpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            // Create a doctor object that is going to be updated
            Person doctor = new Person();
            // Get the doctor id
            doctor.Id = _doctor.Id;
            // Get the doctor name
            doctor.Name = Name.Text.ToString();
            // Get the doctor surname
            doctor.Surname = Surname.Text.ToString();
            // Get the doctor phone
            doctor.Phone = Phone.Text.ToString();
            // Get the doctor e-mail
            doctor.Email = Email.Text.ToString();
            // Get the doctor gender
            doctor.Gender = Gender.Text.ToString();
            // Get the doctor birthdate
            doctor.Birthdate = (DateTime) Birthdate.SelectedDate;

            // Update the doctor in the database
            Boolean IsUpdated = _databaseConnection.UpdateDoctor(doctor);

            // If the doctor's information is updated
            if (IsUpdated)
            {
                using (var fileStream = new FileStream(System.IO.Path.Combine("C:/Users/Taner/Desktop/kinect_application/kinect_application/Resources/PHOTOS/", _doctor.Id + ".png"), FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(new Uri(_doctor.Photo)));
                    encoder.Save(fileStream);
                }

                // If the doctor's information is updated, show the message
                MessageBox.Show("UPDATE: Successful - The doctor's information is updated!");
                // Delete the children of the main window content
                MainWindow.MainWindowContent.Children.Clear();
                // Create the new user control which shows the doctor's information and patients
                SelectDoctorUserControl window = new SelectDoctorUserControl(doctor);
                // Add the new user control to the main window content
                MainWindow.MainWindowContent.Children.Add(window);
            }
            else
            {
                // If the doctor's information is not updated, show the error message
                MessageBox.Show("UPDATE: Unsuccessful - The doctor's information is not updated!");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the login page
            LoginUserControl window = new LoginUserControl();
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }
    }
}
