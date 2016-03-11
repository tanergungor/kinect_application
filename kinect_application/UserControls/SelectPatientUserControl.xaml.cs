using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

//Using namespaces 
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using KINECT_APPLICATION.DB;
using KINECT_APPLICATION.DataStructures;
using KINECT_APPLICATION.UserControls;
using System.Threading;
using System.IO;

namespace KINECT_APPLICATION
{
    /// <summary>
    /// Interaction logic for SelectPatientWindow.xaml
    /// </summary>
    public partial class SelectPatientUserControl : UserControl
    {
        // Create a singleton database connection object
        private DatabaseConnection _databaseConnection = DatabaseConnection.getDatabaseConnection();
        // Create a doctor object
        private Person _doctor = null;
        // Create a patient object
        private Person _patient = null;

        private Double _pain = 0;
        private Double _fatigue = 0;
        private Double _mood = 0;

        internal SelectPatientUserControl(Person doctor, Person patient)
        {
            InitializeComponent();

            // Set the doctor object
            _doctor = doctor;
            // Set the patient object
            _patient = patient;
            // Set the patient name
            Name.Text = _patient.Name;
            // Set the patient surname
            Surname.Text = _patient.Surname;
            // Set the patient photo
            Photo.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/PHOTOS/" + _patient.Id + ".png"));
            // Set the patient height
            Phone.Text = _patient.Phone;
            // Set the patient weight
            Email.Text = _patient.Email;
            // Set the patient gender
            Gender.Text = _patient.Gender;
            // Set the patient birthdate
            Birthdate.Text = _patient.Birthdate.ToString();
            // Set the patient height
            Height.Text = _patient.Height;
            // Set the patient weight
            Weight.Text = _patient.Weight;
            // Select all the tasks that is not done and belongs to the current patient and from database
            _databaseConnection.SelectTasks(taskList, _patient.Id, "0");
            // Select all the tasks that is done and belongs to the current patient from database
            _databaseConnection.SelectTasks(historytaskList, _patient.Id, "1");



            /* WARNING:: DataGridView setting Data is too slow

            int count = 0;

            // Traverse each exercise in the current task
            for (int i = 0; i < historytaskList.Items.Count; i++)
            {
                // Get the selected row from the task list
                DataRowView dataRow = (DataRowView)historytaskList.Items[i];
                // Get the task id whose index value is 0
                String taskID = dataRow.Row.ItemArray[0].ToString();

                List<Exercise> exerciseList = _databaseConnection.SelectExerciseRelations(taskID, _patient.Id);

                // Traverse each exercise in the current task
                for (int j = 0; j < exerciseList.Count; j++)
                {
                    // Sum the pain value of current exercise in the task
                    _pain = _pain + exerciseList[j].Pain;
                    // Sum the fatigue value of current exercise in the task
                    _fatigue = _fatigue + exerciseList[j].Fatigue;
                    // Sum the mood value of current exercise in the task
                    _mood = _mood + exerciseList[j].Mood;

                    count = count + 1;
                }
            }

            // Find the average pain value of the task
            _pain = _pain / count;
            // Find the average fatigue value of the task
            _fatigue = _fatigue / count;
            // Find the average mood value of the task
            _mood = _mood / count;

            // Set the pain progress bar
            Progress_Pain.Value = _pain;
            // Set the fatigue progress bar
            Progress_Fatigue.Value = _fatigue;
            // Set the mood progress bar
            Progress_Mood.Value = _mood;

            */
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Create a regular expression that only accepts the number
            Regex regex = new Regex("[^0-9]+");
            // Check if the text is matched with the regular expression created
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SelectTask_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected row from the task list
            DataRowView dataRow = (DataRowView)taskList.SelectedItem;
            // Get the task id whose index value is 0
            String taskID = dataRow.Row.ItemArray[0].ToString();
            // Select the task from the database according to the patient id
            Task task = _databaseConnection.SelectTask(taskID, _patient.Id);

            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the task's information
            SelectTaskUserControl window = new SelectTaskUserControl(_doctor, _patient, task);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }

        private void InsertTask_Click(object sender, RoutedEventArgs e)
        {
            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the task insertion page
            InsertTaskUserControl window = new InsertTaskUserControl(_doctor, _patient);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }

        private void CheckTask_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected row from the task list
            DataRowView dataRow = (DataRowView)historytaskList.SelectedItem;
            // Get the task id whose index value is 0
            String taskID = dataRow.Row.ItemArray[0].ToString();
            // Select the task from the database according to the patient id
            Task task = _databaseConnection.SelectTask(taskID, _patient.Id);

            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the task's information
            CheckTaskUserControl window = new CheckTaskUserControl(_doctor, _patient, task);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Show the warning message to the doctor, if s/he is sure to delete the patient from the list
            // If the patient is deleted from the database, it is imposible to recover the patient's information
            if (MessageBox.Show("Do you really want to delete the task from the list?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                // Get the selected row from the task list
                DataRowView dataRow = (DataRowView)taskList.SelectedItem;
                // Get the task id whose index value is 0
                String taskID = dataRow.Row.ItemArray[0].ToString();

                // Delete the task from the database
                Boolean IsDeleted = _databaseConnection.DeleteTask(taskID, _patient.Id);
                // Select all the tasks from the database and set them to the list
                _databaseConnection.SelectTasks(taskList, _patient.Id, "0");

                // Check if the task is deleted
                if (IsDeleted)
                {
                    // If the task is deleted, show the message
                    MessageBox.Show("DELETE: Successful - The task is deleted!");
                }
                else
                {
                    // If the task is not deleted, show the error message
                    MessageBox.Show("DELETE: Unsuccessful - The task is not deleted!!");
                }
            }
        }

        private void StartTask_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected row from the task list
            DataRowView dataRow = (DataRowView)taskList.SelectedItem;
            // Get the task id whose index value is 0
            String taskID = dataRow.Row.ItemArray[0].ToString();
            // Select the task from the database according to the patient id
            Task task = _databaseConnection.SelectTask(taskID, _patient.Id);

            // Create the new window which interacts with KINECT
            KinectWindow window = new KinectWindow(_doctor, _patient, task);
            // Show the screen
            window.Show();
        }













        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            // Create Open File Dialog 
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            openFileDialog.Filter = "PNG Files (*.png)|*.png";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = openFileDialog.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                _patient.Photo = openFileDialog.FileName;

                Photo.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        // WARNING:: CHECK EMPTY FIELDS!
        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            // Create a patient object that is going to be updated
            Person patient = new Person();
            // Get the patient id
            patient.Id = _patient.Id;
            // Get the patient name
            patient.Name = Name.Text.ToString();
            // Get the patient surname
            patient.Surname = Surname.Text.ToString();
            // Get the patient phone
            patient.Phone = Phone.Text.ToString();
            // Get the patient e-mail
            patient.Email = Email.Text.ToString();
            // Get the patient gender
            patient.Gender = Gender.Text.ToString();
            // Get the patient birthdate
            patient.Birthdate = (DateTime)Birthdate.SelectedDate;
            // Get the patient height
            patient.Height = Height.Text.ToString();
            // Get the patient weight
            patient.Weight = Weight.Text.ToString();

            // Update the patient in the database
            Boolean IsUpdated = _databaseConnection.UpdatePatient(patient);

            // If the patient's information is updated
            if (IsUpdated)
            {
                using (var fileStream = new FileStream(System.IO.Path.Combine("C:/Users/Taner/Desktop/kinect_application/kinect_application/Resources/PHOTOS/", _patient.Id + ".png"), FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(new Uri(_patient.Photo)));
                    encoder.Save(fileStream);
                }

                // If the patient's information is updated, show the message
                MessageBox.Show("UPDATE: Successful - The patient's information is updated!");
                // Delete the children of the main window content
                MainWindow.MainWindowContent.Children.Clear();
                // Create the new user control which shows the patient's information and tasks
                SelectPatientUserControl window = new SelectPatientUserControl(_doctor, patient);
                // Add the new user control to the main window content
                MainWindow.MainWindowContent.Children.Add(window);
            }
            else
            {
                // If the patient's information is not updated, show the error message
                MessageBox.Show("UPDATE: Unsuccessful - The patient's information is not updated!");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the doctor's information and patients
            SelectDoctorUserControl window = new SelectDoctorUserControl(_doctor);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }
    }
}
