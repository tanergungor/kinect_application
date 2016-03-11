using KINECT_APPLICATION.DataStructures;
using KINECT_APPLICATION.DB;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace KINECT_APPLICATION.UserControls
{
    /// <summary>
    /// Interaction logic for SelectTaskUserControl.xaml
    /// </summary>
    public partial class CheckTaskUserControl : UserControl
    {
        // Create a singleton database connection object
        private DatabaseConnection _databaseConnection = DatabaseConnection.getDatabaseConnection();
        // Create a doctor object
        private Doctor _doctor = null;
        // Create a patient object
        private Patient _patient = null;
        // Create a task object
        private Task _task = null;


        private KinectWindow _instance = null;
        private List<BodyJoints> _bodyJointsList = null;
        private int _frameNumber = 1;
        private String _filename = null;
        private Double _pain = 0;
        private Double _fatigue = 0;
        private Double _mood = 0;


        internal CheckTaskUserControl(Doctor doctor, Patient patient, Task task)
        {
            InitializeComponent();

            // Set the doctor object
            _doctor = doctor;
            // Set the patient object
            _patient = patient;
            // Set the task object
            _task = task;


            _bodyJointsList = new List<BodyJoints>();
            _instance = new KinectWindow(_doctor, _patient, _task);

            // Set the default task name
            Name.Text = _task.Name;
            // Select all the exercises from the database
            _task.ExerciseList = _databaseConnection.SelectExerciseRelations(_task.Id, _patient.Id);

            // Traverse each exercise in the current task
            for (int i = 0; i < _task.ExerciseList.Count; i++)
            {
                // Add current task to the task list box
                taskContent.Items.Add(_task.ExerciseList[i].Id + "-" + _task.ExerciseList[i].Name);
                // Sum the pain value of current exercise in the task
                _pain = _pain + _task.ExerciseList[i].Pain;
                // Sum the fatigue value of current exercise in the task
                _fatigue = _fatigue + _task.ExerciseList[i].Fatigue;
                // Sum the mood value of current exercise in the task
                _mood = _mood + _task.ExerciseList[i].Mood;
            }

            // Find the average pain value of the task
            _pain = _pain / _task.ExerciseList.Count;
            // Find the average fatigue value of the task
            _fatigue = _fatigue / _task.ExerciseList.Count;
            // Find the average mood value of the task
            _mood = _mood / _task.ExerciseList.Count;

            // Set the pain progress bar
            ProgressPain.Value = _pain;
            // Set the fatigue progress bar
            ProgressFatigue.Value = _fatigue;
            // Set the mood progress bar
            ProgressMood.Value = _mood;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the patient's information and tasks
            SelectPatientUserControl window = new SelectPatientUserControl(_doctor, _patient);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);

            _instance.Close();
        }












































        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (_bodyJointsList.Count != 0)
            {
                int bodyJointsListSize = _bodyJointsList.Count();

                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(0.03),
                };

                taskContent.IsEnabled = false;

                FrameSlider.IsEnabled = false;

                Play.IsEnabled = false;

                timer.Tick += (nSender, args) =>
                {
                    oCanvas.Children.RemoveRange(1, oCanvas.Children.Count);
                    FrameSliderNumber.Content = _frameNumber;
                    FrameSlider.Value = _frameNumber;

                    if (_frameNumber == bodyJointsListSize)
                    {
                        taskContent.IsEnabled = true;

                        FrameSlider.IsEnabled = true;

                        Play.IsEnabled = true;

                        _frameNumber = 1;

                        FrameSliderNumber.Content = _frameNumber;

                        FrameSlider.Value = _frameNumber;

                        timer.Stop();

                        return;
                    }

                    // Draw the whole body
                    _instance.Draw(oCanvas, _bodyJointsList.ElementAt(_frameNumber - 1));

                    _frameNumber++;
                };

                timer.Start();
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            oCanvas.Children.RemoveRange(1, oCanvas.Children.Count);

            _bodyJointsList.Clear();

            FrameSlider.IsEnabled = true;

            Play.IsEnabled = true;

            _frameNumber = 1;

            FrameSliderNumber.Content = _frameNumber;

            // Check if the item is selected in the exercise list box
            if (taskContent.SelectedValue != null)
            {
                // Find the index of the '-' character in the string
                int index = taskContent.SelectedValue.ToString().IndexOf("-");

                // If it exist, split the string
                if (index > 0)
                {
                    String exerciseId = taskContent.Items[taskContent.SelectedIndex].ToString().Substring(0, index);

                    _filename = "DATA/PATIENT_" + _patient.Id + "/TASK_" + _task.Id + "/EXERCISE_" + exerciseId;

                    if (File.Exists(_filename))
                    {
                        using (StreamReader streamReader = new StreamReader(_filename))
                        {
                            BodyJoints bodyJoints = null;
                            String line = null;
                            int count = 0;

                            // Read and display lines from the file until the end of the file is reached
                            while ((line = streamReader.ReadLine()) != null)
                            {
                                String[] words = line.Split(' ');

                                if (count == 0)
                                {
                                    bodyJoints = new BodyJoints(int.Parse(words[0]));
                                }

                                Joint joint = new Joint();
                                joint.Position.X = (float)Double.Parse(words[2]);
                                joint.Position.Y = (float)Double.Parse(words[3]);

                                bodyJoints.BodyJointsDictionary.Add(words[1], joint);
                                count++;

                                if (count == 25)
                                {
                                    _bodyJointsList.Add(bodyJoints);

                                    count = 0;
                                }
                            }

                            FrameSlider.Maximum = _bodyJointsList.Count;
                        }
                    }
                    else
                    {
                        MessageBox.Show("File does not exist!", "Error");
                    }
                }

                // Find the selected item and get its value
                int currentItemIndex = taskContent.SelectedIndex;
                // Set the pain progress bar
                ProgressPain.Value = _task.ExerciseList[currentItemIndex].Pain;
                // Set the fatigue progress bar
                ProgressFatigue.Value = _task.ExerciseList[currentItemIndex].Fatigue;
                // Set the mood progress bar
                ProgressMood.Value = _task.ExerciseList[currentItemIndex].Mood;
            }
        }




        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _frameNumber = (int) FrameSlider.Value;

            FrameSliderNumber.Content = _frameNumber;

            oCanvas.Children.RemoveRange(1, oCanvas.Children.Count);

            if(_frameNumber > 1 && _frameNumber < _bodyJointsList.Count)
            {
                // Draw the whole body
                _instance.Draw(oCanvas, _bodyJointsList.ElementAt(_frameNumber));
            }
        }
    }
}
