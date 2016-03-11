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
    public partial class SelectTaskUserControl : UserControl
    {
        // Create a singleton database connection object
        private DatabaseConnection _databaseConnection = DatabaseConnection.getDatabaseConnection();
        // Create a doctor object
        private Doctor _doctor = null;
        // Create a patient object
        private Patient _patient = null;
        // Create a task object
        private Task _task = null;

        internal SelectTaskUserControl(Doctor doctor, Patient patient, Task task)
        {
            InitializeComponent();

            // Set the doctor object
            _doctor = doctor;
            // Set the patient object
            _patient = patient;
            // Set the task object
            _task = task;

            // Set the task name
            Name.Text = _task.Name;
            // Select all the exercises from the database
            _databaseConnection.SelectExercises(exerciseList);
            // Select all the exercises that belongs to the current task from the database
            _task.ExerciseList = _databaseConnection.SelectExerciseRelations(_task.Id, _patient.Id);

            // Traverse each exercise in the current task
            for(int i = 0; i < _task.ExerciseList.Count; i++)
            {
                // Add the current exercise to the task list box
                taskContent.Items.Add(_task.ExerciseList[i].Id + "-" + _task.ExerciseList[i].Name);
                // Remove the current exercise from the exercise list box
                exerciseList.Items.Remove(taskContent.Items[i]);
            }
        }

        // WARNING:: CHECK EMPTY FIELDS!
        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            // Create a task object that is going to be updated
            Task task = new Task();
            // Get the task id
            task.Id = _task.Id;
            // Get the task's patient id
            task.PatientId = _patient.Id;
            // Get the task name
            task.Name = Name.Text.ToString();
            // Get the task review
            task.Review = "";
            // Get the task status
            task.Status = "0";
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

            // Update the task
            Boolean isTaskUpdated = _databaseConnection.UpdateTask(task);
            // Delete the old task exercise list
            Boolean isExercisesDeleted = _databaseConnection.DeleteExerciseRelations(task.Id);
            // Insert the new task exercise list
            Boolean isExercisesInserted = _databaseConnection.InsertExerciseRelations(task.Id, task.ExerciseList);

            // If the task is updated
            if (isTaskUpdated && isExercisesDeleted && isExercisesInserted)
            {
                // If the task is updated, show the message
                MessageBox.Show("UPDATE: Successful - The task is updated!");
            }
            else
            {
                // If the task is not updated, show the error message
                MessageBox.Show("UPDATE: Unsuccessful - The task is not updated!");
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
