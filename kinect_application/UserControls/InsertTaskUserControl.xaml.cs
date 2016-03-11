using KINECT_APPLICATION.DataStructures;
using KINECT_APPLICATION.DB;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KINECT_APPLICATION.UserControls
{
    /// <summary>
    /// Interaction logic for InsertTaskUserControl.xaml
    /// </summary>
    public partial class InsertTaskUserControl : UserControl
    {
        // Create a singleton database connection object
        private DatabaseConnection _databaseConnection = DatabaseConnection.getDatabaseConnection();
        // Create a doctor object
        private Doctor _doctor = null;
        // Create a patient object
        private Patient _patient = null;

        internal InsertTaskUserControl(Doctor doctor, Patient patient)
        {
            InitializeComponent();

            // Set the doctor object
            _doctor = doctor;
            // Set the patient object
            _patient = patient;

            // Set the default task name
            Name.Text = "New Task For Patient " + _patient.Id + " - " + DateTime.Today.ToString();
            // Select all the exercises from the database
            _databaseConnection.SelectExercises(exerciseList);
        }

        // WARNING:: CHECK EMPTY FIELDS!
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            // Create a task object that is going to be updated
            Task task = new Task();
            // Get the task's patient id
            task.Id = _patient.Id;
            // Get the task name
            task.Name = Name.Text.ToString();
            // Create the empty task exercise list 
            task.ExerciseList = new List<Exercise>();

            // Traverse each exercise in the task list box
            for (int i = 0; i < taskContent.Items.Count; i++)
            {
                // Find the index of the '-' character in the string
                int index = taskContent.Items[i].ToString().IndexOf("-");

                // If it exist, split the string
                if (index > 0)
                {
                    // Create a new exercise object
                    Exercise exercise = new Exercise();
                    // Set the exercise id
                    exercise.Id = taskContent.Items[i].ToString().Substring(0, index);
                    // Add the exercise to the list
                    task.ExerciseList.Add(exercise);
                }
            }

            // Insert the task
            Boolean isTaskInserted = _databaseConnection.InsertTask(task);
            // Insert the new task exercise list
            Boolean isExercisesInserted = _databaseConnection.InsertExerciseRelations(_databaseConnection.SelectLastInsertedTask(_patient.Id), task.ExerciseList);

            // If the task is inserted
            if (isTaskInserted && isExercisesInserted)
            {
                // If the task is inserted, show the message
                MessageBox.Show("INSERT: Successful - The task is inserted!");
            }
            else
            {
                // If the task is not inserted, show the error message
                MessageBox.Show("INSERT: Unsuccessful - The task is not inserted!");
            }

            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the patient's information and tasks
            SelectPatientUserControl window = new SelectPatientUserControl(_doctor, _patient);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            // Check if the item is selected in the exercise list box
            if (exerciseList.SelectedValue != null)
            {
                // Find the selected item and get its value
                String currentItemText = exerciseList.SelectedValue.ToString();
                // Add the current item to the task list box
                taskContent.Items.Add(currentItemText);
                // Delete the current item from the exercise list box
                exerciseList.Items.RemoveAt(exerciseList.Items.IndexOf(exerciseList.SelectedItem));
            }
            else
            {
                // If the item is not selected in the exercise list box, show the error message
                MessageBox.Show("Please, Select the exercise from the exercise list!");
            }
        }

        private void RemoveExercise_Click(object sender, RoutedEventArgs e)
        {
            // Check if the item is selected in the task list box
            if (taskContent.SelectedValue != null)
            {
                // Find the selected item and get its value
                String currentItemText = taskContent.SelectedValue.ToString();
                // Add the current item to the exercise list box
                exerciseList.Items.Add(currentItemText);
                // Delete the current item from the task list box
                taskContent.Items.RemoveAt(taskContent.Items.IndexOf(taskContent.SelectedItem));
            }
            else
            {
                // If the item is not selected in the task list box, show the error message
                MessageBox.Show("Please, Select the exercise from the task list!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the patient's information and tasks
            SelectPatientUserControl window = new SelectPatientUserControl(_doctor, _patient);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }
    }
}
