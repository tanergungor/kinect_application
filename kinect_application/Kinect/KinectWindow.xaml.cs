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
using Microsoft.Kinect;
using KINECT_APPLICATION.DataStructures;
using KINECT_APPLICATION.DB;
using System.IO;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace KINECT_APPLICATION
{
    /// <summary>
    /// Interaction logic for KinectWindow.xaml
    /// </summary>
    public partial class KinectWindow : Window
    {
        // Create a singleton database connection object
        private DatabaseConnection _databaseConnection = DatabaseConnection.getDatabaseConnection();
        // Create a doctor object
        private Person _doctor = null;
        // Create a patient object
        private Person _patient = null;
        // Create a task object
        private Task _task = null;


        private Boolean _isStart = false, _isStop = false;
        private int _frameNumber = 1;
        private BodyJoints _bodyJoints = null;
        private String _filename = null;
        private KinectSensor _kinectSensor;
        private List<BodyJoints> _bodyJointsList;
        private MultiSourceFrameReader _multiSourceFrameReader;

        internal KinectWindow(Person doctor, Person patient, Task task)
        {
            InitializeComponent();

            _bodyJointsList = new List<BodyJoints>();

            // Set the doctor object
            _doctor = doctor;
            // Set the patient object
            _patient = patient;
            // Set the task object
            _task = task;

            // Select all the exercises that belongs to the current task from the database
            _task.ExerciseList = _databaseConnection.SelectExerciseRelations(_task.Id, _patient.Id);

            // Traverse each exercise in the current task
            for (int i = 0; i < _task.ExerciseList.Count; i++)
            {
                if (_task.ExerciseList[i].Status == 1)
                {
                    // Add the current exercise to the task list box
                    taskContent.Items.Add(_task.ExerciseList[i].Id + "-" + _task.ExerciseList[i].Name + "_DONE");
                }
                else
                {
                    // Add the current exercise to the task list box
                    taskContent.Items.Add(_task.ExerciseList[i].Id + "-" + _task.ExerciseList[i].Name);
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Create a regular expression that only accepts the number
            Regex regex = new Regex("[^0-9]+");
            // Check if the text is matched with the regular expression created
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (Connect.Content.Equals("Disconnect"))
            {
                Status.Foreground = Brushes.Red;
                Status.Content = "Disconnected";
                Connect.Content = "Connect";
                eCanvas.Children.RemoveRange(1, eCanvas.Children.Count);
                oCamera.Source = new BitmapImage();
                eCamera.Source = new BitmapImage();
                closeKinectSensor();

                // Unselect the all exercise in the task list box
                taskContent.UnselectAll();
                // Make disable the task list box
                taskContent.IsEnabled = false;
                // Make disable the start button
                Start.IsEnabled = false;
                // Make disable the replay button
                Replay.IsEnabled = false;
            }
            else
            {
                // Get the default Kinect sensor
                _kinectSensor = KinectSensor.GetDefault();

                // Open the current Kinect sensor
                if (_kinectSensor != null)
                {
                    _kinectSensor.Open();
                }

                // Wait for sensor to be connected
                while (!_kinectSensor.IsAvailable)
                {
                    Status.Foreground = Brushes.Orange;
                    Status.Content = "Waiting";
                }

                // Check if the Kinect sensor is available or not
                if (_kinectSensor.IsAvailable)
                {
                    Status.Foreground = Brushes.Green;
                    Status.Content = "Connected";
                    Connect.Content = "Disconnect";

                    _multiSourceFrameReader = _kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body | FrameSourceTypes.Color);
                    _multiSourceFrameReader.MultiSourceFrameArrived += multiSourceFrameReaderArrived;
                }

                taskContent.IsEnabled = true;
            }
        }

        private void closeKinectSensor()
        {
            if (_kinectSensor != null)
            {
                _kinectSensor.Close();
            }

            if (_multiSourceFrameReader != null)
            {
                _multiSourceFrameReader.Dispose();
            }
        }











        public void DrawTrajectory(Canvas canvas, BodyJoints fBody, BodyJoints sBody)
        {
            if (fBody != null && sBody != null)
            {
                if (fBody.BodyJointsDictionary.ContainsKey(JointType.Head.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.Head.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.Head.ToString()], sBody.BodyJointsDictionary[JointType.Head.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.Neck.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.Neck.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.Neck.ToString()], sBody.BodyJointsDictionary[JointType.Neck.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.SpineShoulder.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.SpineShoulder.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.SpineShoulder.ToString()], sBody.BodyJointsDictionary[JointType.SpineShoulder.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.ShoulderRight.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.ShoulderRight.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.ShoulderRight.ToString()], sBody.BodyJointsDictionary[JointType.ShoulderRight.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.ShoulderLeft.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.ShoulderLeft.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.ShoulderLeft.ToString()], sBody.BodyJointsDictionary[JointType.ShoulderLeft.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.SpineBase.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.SpineBase.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.SpineBase.ToString()], sBody.BodyJointsDictionary[JointType.SpineBase.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.SpineMid.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.SpineMid.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.SpineMid.ToString()], sBody.BodyJointsDictionary[JointType.SpineMid.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.ElbowLeft.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.ElbowLeft.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.ElbowLeft.ToString()], sBody.BodyJointsDictionary[JointType.ElbowLeft.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.WristLeft.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.WristLeft.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.WristLeft.ToString()], sBody.BodyJointsDictionary[JointType.WristLeft.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.ElbowRight.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.ElbowRight.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.ElbowRight.ToString()], sBody.BodyJointsDictionary[JointType.ElbowRight.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.WristRight.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.WristRight.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.WristRight.ToString()], sBody.BodyJointsDictionary[JointType.WristRight.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.HipLeft.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.HipLeft.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.HipLeft.ToString()], sBody.BodyJointsDictionary[JointType.HipLeft.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.HipRight.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.HipRight.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.HipRight.ToString()], sBody.BodyJointsDictionary[JointType.HipRight.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.KneeLeft.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.KneeLeft.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.KneeLeft.ToString()], sBody.BodyJointsDictionary[JointType.KneeLeft.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.KneeRight.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.KneeRight.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.KneeRight.ToString()], sBody.BodyJointsDictionary[JointType.KneeRight.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.AnkleLeft.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.AnkleLeft.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.AnkleLeft.ToString()], sBody.BodyJointsDictionary[JointType.AnkleLeft.ToString()], Brushes.Black, 1);
                }

                if (fBody.BodyJointsDictionary.ContainsKey(JointType.AnkleRight.ToString()) && sBody.BodyJointsDictionary.ContainsKey(JointType.AnkleRight.ToString()))
                {
                    DrawLine(canvas, fBody.BodyJointsDictionary[JointType.AnkleRight.ToString()], sBody.BodyJointsDictionary[JointType.AnkleRight.ToString()], Brushes.Black, 1);
                }
            }
        }








        public void DrawLine(Canvas canvas, Joint newF, Joint newS, SolidColorBrush colorBrush, int markerSize)
        {
            if (newF.Position.X < canvas.Width && 
                newF.Position.Y < canvas.Height && 
                newF.Position.X > 0 && 
                newF.Position.Y > 0)
            {
                Ellipse ellipse = new Ellipse { Fill = colorBrush, Width = markerSize, Height = markerSize };
                Canvas.SetLeft(ellipse, newF.Position.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, newF.Position.Y - ellipse.Height / 2);
                canvas.Children.Add(ellipse);
            }

            if (newS.Position.X < canvas.Width && 
                newS.Position.Y < canvas.Height && 
                newS.Position.X > 0 && 
                newS.Position.Y > 0)
            {
                Ellipse ellipse = new Ellipse { Fill = colorBrush, Width = markerSize, Height = markerSize };
                Canvas.SetLeft(ellipse, newS.Position.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, newS.Position.Y - ellipse.Height / 2);
                canvas.Children.Add(ellipse);
            }

            if (newF.Position.X < canvas.Width && 
                newF.Position.Y < canvas.Height && 
                newF.Position.X > 0 && 
                newF.Position.Y > 0 &&
                newS.Position.X < canvas.Width && 
                newS.Position.Y < canvas.Height && 
                newS.Position.X > 0 && 
                newS.Position.Y > 0)
            {
                Line line = new Line { X1 = newF.Position.X, Y1 = newF.Position.Y, X2 = newS.Position.X, Y2 = newS.Position.Y, StrokeThickness = (markerSize / 2), Stroke = colorBrush };
                canvas.Children.Add(line);
            }
        }

        public void Draw(Canvas canvas, BodyJoints body)
        {
            if (body != null)
            {
                if (body.BodyJointsDictionary.ContainsKey(JointType.Head.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.Neck.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.Head.ToString()], body.BodyJointsDictionary[JointType.Neck.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.Neck.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.SpineShoulder.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.Neck.ToString()], body.BodyJointsDictionary[JointType.SpineShoulder.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.SpineShoulder.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.ShoulderLeft.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineShoulder.ToString()], body.BodyJointsDictionary[JointType.ShoulderLeft.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.SpineShoulder.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.ShoulderRight.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineShoulder.ToString()], body.BodyJointsDictionary[JointType.ShoulderRight.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.SpineShoulder.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.SpineMid.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineShoulder.ToString()], body.BodyJointsDictionary[JointType.SpineMid.ToString()], Brushes.Green, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.ShoulderLeft.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.ElbowLeft.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.ShoulderLeft.ToString()], body.BodyJointsDictionary[JointType.ElbowLeft.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.ShoulderRight.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.ElbowRight.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.ShoulderRight.ToString()], body.BodyJointsDictionary[JointType.ElbowRight.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.ElbowLeft.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.WristLeft.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.ElbowLeft.ToString()], body.BodyJointsDictionary[JointType.WristLeft.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.ElbowRight.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.WristRight.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.ElbowRight.ToString()], body.BodyJointsDictionary[JointType.WristRight.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.SpineMid.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.SpineBase.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineMid.ToString()], body.BodyJointsDictionary[JointType.SpineBase.ToString()], Brushes.Green, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.SpineBase.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.HipLeft.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineBase.ToString()], body.BodyJointsDictionary[JointType.HipLeft.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.SpineBase.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.HipRight.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineBase.ToString()], body.BodyJointsDictionary[JointType.HipRight.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.HipLeft.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.KneeLeft.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.HipLeft.ToString()], body.BodyJointsDictionary[JointType.KneeLeft.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.HipRight.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.KneeRight.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.HipRight.ToString()], body.BodyJointsDictionary[JointType.KneeRight.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.KneeLeft.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.AnkleLeft.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.KneeLeft.ToString()], body.BodyJointsDictionary[JointType.AnkleLeft.ToString()], Brushes.Red, 5);
                }

                if (body.BodyJointsDictionary.ContainsKey(JointType.KneeRight.ToString()) && body.BodyJointsDictionary.ContainsKey(JointType.AnkleRight.ToString()))
                {
                    DrawLine(canvas, body.BodyJointsDictionary[JointType.KneeRight.ToString()], body.BodyJointsDictionary[JointType.AnkleRight.ToString()], Brushes.Red, 5);
                }
            }
        }






        private void multiSourceFrameReaderArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Open body frame
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                // Check if the frame is empty or not
                if (frame != null)
                {
                    IList<Body> _bodyList = new Body[frame.BodyFrameSource.BodyCount];

                    // Gets refreshed body data
                    frame.GetAndRefreshBodyData(_bodyList);

                    // For each body data traverse the joints
                    for (int i = 0; i < _bodyList.Count; i++)
                    {
                        // Check the current body data is empty or not
                        // Check whether or not the body is tracked
                        if (_bodyList[i] != null && _bodyList[i].IsTracked)
                        {
                            _bodyJoints = new BodyJoints();

                            foreach (var bodyJoints in _bodyList[i].Joints)
                            {
                                if (bodyJoints.Value.TrackingState != TrackingState.NotTracked)
                                {
                                    Joint newJ = new Joint();

                                    ColorSpacePoint colorPoint = _kinectSensor.CoordinateMapper.MapCameraPointToColorSpace(bodyJoints.Value.Position);
                                    // WARNING be careful about resolution ratio
                                    newJ.Position.X = float.IsInfinity(colorPoint.X) ? 0 : (colorPoint.X / (float)(1920 / eCanvas.Width));
                                    // WARNING be careful about resolution ratio
                                    newJ.Position.Y = float.IsInfinity(colorPoint.Y) ? 0 : (colorPoint.Y / (float)(1080 / eCanvas.Height));

                                    _bodyJoints.BodyJointsDictionary.Add(bodyJoints.Key.ToString(), newJ);
                                }
                            }

                            if (_isStart && File.Exists(_filename))
                            {
                                using (StreamWriter streamWriter = File.AppendText(_filename))
                                {
                                    foreach (var joint in _bodyJoints.BodyJointsDictionary)
                                    {
                                        // KEEP ORIGINAL OR CONVERTED DATA
                                        streamWriter.WriteLine(_frameNumber + " " + joint.Key + " " + joint.Value.Position.X + " " + joint.Value.Position.Y);
                                    }

                                    _frameNumber++;
                                }
                            }
                        }
                    }
                }
            }


            // Open color frame
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                // Check if the frame is empty or not
                if (frame != null)
                {
                    if(!_isStop)
                    {
                        // Clear the screen from previous body joints
                        eCanvas.Children.RemoveRange(1, eCanvas.Children.Count);
                        // Draw the whole body
                        Draw(eCanvas, _bodyJoints);
                        // Update the image with the data from the kinect camera
                        eCamera.Source = ToBitmap(frame);
                    }
                }
            }
        }

        private ImageSource ToBitmap(ColorFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            PixelFormat format = PixelFormats.Bgr32;

            byte[] pixels = new byte[width * height * ((PixelFormats.Bgr32.BitsPerPixel + 7) / 8)];

            if (frame.RawColorImageFormat == ColorImageFormat.Bgra)
            {
                frame.CopyRawFrameDataToArray(pixels);
            }
            else
            {
                frame.CopyConvertedFrameDataToArray(pixels, ColorImageFormat.Bgra);
            }

            int stride = width * format.BitsPerPixel / 8;

            BitmapSource bitmap = BitmapSource.Create(width, height, 96, 96, format, null, pixels, stride);

            ScaleTransform scale = new ScaleTransform((eCanvas.Width / bitmap.PixelWidth), (eCanvas.Height / bitmap.PixelHeight));

            TransformedBitmap tbitmap = new TransformedBitmap(bitmap, scale);

            return tbitmap;
        }


























        // WARNING:: Check if the exercise is done or not
        // take id, read file of the exerciseX
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the item is selected in the exercise list box
            if (taskContent.SelectedValue != null)
            {
                _isStop = false;

                // Set the frame number to 1
                _frameNumber = 1;

                // Find the index of the '-' character in the string
                int index = taskContent.SelectedValue.ToString().IndexOf("-");

                // If it exist, split the string
                if (index > 0)
                {
                    String exerciseId = taskContent.Items[taskContent.SelectedIndex].ToString().Substring(0, index);

                    // WARNING:: maybe you can carry it outside of if
                    oCamera.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/RESOURCES/EXERCISES/" + exerciseId + ".png"));

                    if (_task.ExerciseList[taskContent.SelectedIndex].Status == 0)
                    {
                        _filename = "DATA/PATIENT_" + _patient.Id + "/TASK_" + _task.Id;

                        if (!Directory.Exists(_filename))
                        {
                            Directory.CreateDirectory(_filename);
                        }

                        _filename = _filename + "/EXERCISE_" + exerciseId;

                        if (!File.Exists(_filename))
                        {
                            // Create a file to write to.
                            using (StreamWriter streamWriter = File.CreateText(_filename))
                            {
                                MessageBox.Show("File is created: " + _filename);
                            }
                        }

                        Start.IsEnabled = true;

                        Replay.IsEnabled = false;

                        Pain.IsEnabled = true;
                        Pain.Text = "0";
                        Fatigue.IsEnabled = true;
                        Fatigue.Text = "0";
                        Mood.IsEnabled = true;
                        Mood.Text = "0";
                    }
                    else
                    {
                        eCanvas.Children.RemoveRange(1, eCanvas.Children.Count);

                        Start.Content = "Start";

                        Start.IsEnabled = false;

                        Replay.IsEnabled = true;

                        Pain.IsEnabled = false;
                        Pain.Text = _task.ExerciseList[taskContent.SelectedIndex].Pain.ToString();
                        Fatigue.IsEnabled = false;
                        Fatigue.Text = _task.ExerciseList[taskContent.SelectedIndex].Fatigue.ToString();
                        Mood.IsEnabled = false;
                        Mood.Text = _task.ExerciseList[taskContent.SelectedIndex].Mood.ToString().ToString();

                        // Update the image with the data from the kinect camera
                        eCamera.Source = null;

                        _isStart = false;
                        _isStop = true;
                    }
                } 
            }
        }

        // WARNING:: press start button, show the exercise video and say are you ready, then record the data
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if(Start.Content.Equals("Start"))
            {
                taskContent.IsEnabled = false;

                Start.Content = "Stop";

                _isStart = true;
            }
            else
            {
                Start.Content = "Start";

                Start.IsEnabled = false;

                Replay.IsEnabled = true;

                Finish.IsEnabled = true;

                eCanvas.Children.RemoveRange(1, eCanvas.Children.Count);

                // Update the image with the data from the kinect camera
                eCamera.Source = null;

                _isStart = false;
                _isStop = true;
            }
        }

















        // WARNING:: READFILE NOT THE DATA STRUCTURE
        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            taskContent.IsEnabled = false;

            // Make disable the replay button
            Replay.IsEnabled = false;
            Finish.IsEnabled = false;

            int frameNumber = 1;

            // Find the index of the '-' character in the string
            int index = taskContent.SelectedValue.ToString().IndexOf("-");

            // If it exist, split the string
            if (index > 0)
            {
                String exerciseId = taskContent.Items[taskContent.SelectedIndex].ToString().Substring(0, index);

                _filename = "DATA/PATIENT_" + _patient.Id + "/TASK_" + _task.Id + "/EXERCISE_" + exerciseId;

                if (File.Exists(_filename))
                {
                    _bodyJointsList.Clear();

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
                                bodyJoints = new BodyJoints();
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

                        streamReader.Close();
                    }
                }
            }

            if (_bodyJointsList.Count != 0)
            {
                int bodyJointsListSize = _bodyJointsList.Count();

                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(0.03),
                };

                timer.Tick += (nSender, args) =>
                {
                    eCanvas.Children.RemoveRange(1, eCanvas.Children.Count);

                    if (frameNumber == bodyJointsListSize)
                    {
                        taskContent.IsEnabled = true;
                        Replay.IsEnabled = true;

                        if (_task.ExerciseList[taskContent.SelectedIndex].Status == 0)
                        {
                            Finish.IsEnabled = true;
                        }

                        timer.Stop();

                        return;
                    }

                    // Draw the whole body
                    Draw(eCanvas, _bodyJointsList.ElementAt(frameNumber));

                    frameNumber++;
                };

                timer.Start();
            }
            else
            {
                taskContent.IsEnabled = true;
                Replay.IsEnabled = true;
            }
        }



        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            Finish.IsEnabled = false;

            int selectedIndex = taskContent.SelectedIndex;


            _task.ExerciseList[selectedIndex].Pain = double.Parse(Pain.Text);
            _task.ExerciseList[selectedIndex].Fatigue = double.Parse(Fatigue.Text);
            _task.ExerciseList[selectedIndex].Mood = double.Parse(Mood.Text);

            // CALCULATE RESULT!

            // Update the current exercise's status to 1
            _databaseConnection.UpdateExerciseRelation(_task.Id, _task.ExerciseList[selectedIndex]);

            taskContent.Items.Clear();

            // Select all the exercises that belongs to the current task from the database
            _task.ExerciseList = _databaseConnection.SelectExerciseRelations(_task.Id, _patient.Id);

            // Traverse each exercise in the current task
            for (int i = 0; i < _task.ExerciseList.Count; i++)
            {
                if (_task.ExerciseList[i].Status == 1)
                {
                    // Add the current exercise to the task list box
                    taskContent.Items.Add(_task.ExerciseList[i].Id + "-" + _task.ExerciseList[i].Name + "_DONE");
                }
                else
                {
                    // Add the current exercise to the task list box
                    taskContent.Items.Add(_task.ExerciseList[i].Id + "-" + _task.ExerciseList[i].Name);
                }
            }

            taskContent.SelectedIndex = selectedIndex;

            // Traverse each exercise in the current task
            for (int i = 0; i < _task.ExerciseList.Count; i++)
            {
                // Check if all the exercises are done
                if (_task.ExerciseList[i].Status == 0)
                {
                    // If all exercises are not done, then do not change the task status
                    return;
                }
            }

            // If all exercises are done, then change the task status also to 1
            _task.Status = "1";
            _databaseConnection.UpdateTask(_task);
        }
















        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Close the screen
            this.Close();
        }
    }
}
