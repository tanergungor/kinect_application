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


        private Boolean _isStart = false;
        private int _frameNumber = 1;
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
                // Add the current exercise to the task list box
                taskContent.Items.Add(_task.ExerciseList[i].Id + "-" + _task.ExerciseList[i].Name);
            }
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
                // Make disable the stop button
                Stop.IsEnabled = false;
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















        public void DrawLine(Canvas canvas, Joint newF, Joint newS)
        {
            if (newF.Position.X < canvas.Width && 
                newF.Position.Y < canvas.Height && 
                newF.Position.X > 0 && 
                newF.Position.Y > 0)
            {
                Ellipse ellipse = new Ellipse { Fill = Brushes.Red, Width = 10, Height = 10 };
                Canvas.SetLeft(ellipse, newF.Position.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, newF.Position.Y - ellipse.Height / 2);
                canvas.Children.Add(ellipse);
            }

            if (newS.Position.X < canvas.Width && 
                newS.Position.Y < canvas.Height && 
                newS.Position.X > 0 && 
                newS.Position.Y > 0)
            {
                Ellipse ellipse = new Ellipse { Fill = Brushes.Red, Width = 10, Height = 10 };
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
                Line line = new Line { X1 = newF.Position.X, Y1 = newF.Position.Y, X2 = newS.Position.X, Y2 = newS.Position.Y, StrokeThickness = 5, Stroke = new SolidColorBrush(Colors.Red) };
                canvas.Children.Add(line);
            }
        }

        public void Draw(Canvas canvas, BodyJoints body)
        {
            if (body != null)
            {
                DrawLine(canvas, body.BodyJointsDictionary[JointType.Head.ToString()], body.BodyJointsDictionary[JointType.Neck.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.Neck.ToString()], body.BodyJointsDictionary[JointType.SpineShoulder.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineShoulder.ToString()], body.BodyJointsDictionary[JointType.ShoulderLeft.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineShoulder.ToString()], body.BodyJointsDictionary[JointType.ShoulderRight.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineShoulder.ToString()], body.BodyJointsDictionary[JointType.SpineMid.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.ShoulderLeft.ToString()], body.BodyJointsDictionary[JointType.ElbowLeft.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.ShoulderRight.ToString()], body.BodyJointsDictionary[JointType.ElbowRight.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.ElbowLeft.ToString()], body.BodyJointsDictionary[JointType.WristLeft.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.ElbowRight.ToString()], body.BodyJointsDictionary[JointType.WristRight.ToString()]);
                // DrawLine(canvas, body.BodyJointsDictionary[JointType.WristLeft], body.BodyJointsDictionary[JointType.HandLeft]);
                // DrawLine(canvas, body.BodyJointsDictionary[JointType.WristRight], body.BodyJointsDictionary[JointType.HandRight]);
                // DrawLine(canvas, body.BodyJointsDictionary[JointType.HandLeft], body.BodyJointsDictionary[JointType.HandTipLeft]);
                // DrawLine(canvas, body.BodyJointsDictionary[JointType.HandRight], body.BodyJointsDictionary[JointType.HandTipRight]);
                // DrawLine(canvas, body.BodyJointsDictionary[JointType.HandTipLeft], body.BodyJointsDictionary[JointType.ThumbLeft]);
                // DrawLine(canvas, body.BodyJointsDictionary[JointType.HandTipRight], body.BodyJointsDictionary[JointType.ThumbRight]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineMid.ToString()], body.BodyJointsDictionary[JointType.SpineBase.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineBase.ToString()], body.BodyJointsDictionary[JointType.HipLeft.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.SpineBase.ToString()], body.BodyJointsDictionary[JointType.HipRight.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.HipLeft.ToString()], body.BodyJointsDictionary[JointType.KneeLeft.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.HipRight.ToString()], body.BodyJointsDictionary[JointType.KneeRight.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.KneeLeft.ToString()], body.BodyJointsDictionary[JointType.AnkleLeft.ToString()]);
                DrawLine(canvas, body.BodyJointsDictionary[JointType.KneeRight.ToString()], body.BodyJointsDictionary[JointType.AnkleRight.ToString()]);
                // DrawLine(canvas, body.BodyJointsDictionary[JointType.AnkleLeft], body.BodyJointsDictionary[JointType.FootLeft]);
                // DrawLine(canvas, body.BodyJointsDictionary[JointType.AnkleRight], body.BodyJointsDictionary[JointType.FootRight]);

                /*
                SpineBaseX.Content = body.BodyJointsDictionary[JointType.SpineBase.ToString()].Position.X.ToString();
                SpineBaseY.Content = body.BodyJointsDictionary[JointType.SpineBase.ToString()].Position.Y.ToString();

                SpineShoulderX.Content = body.BodyJointsDictionary[JointType.SpineShoulder.ToString()].Position.X.ToString();
                SpineShoulderY.Content = body.BodyJointsDictionary[JointType.SpineShoulder.ToString()].Position.Y.ToString();

                Angle.Content = (body.BodyJointsDictionary[JointType.SpineShoulder.ToString()].Position.Y - body.BodyJointsDictionary[JointType.SpineBase.ToString()].Position.Y) / (body.BodyJointsDictionary[JointType.SpineShoulder.ToString()].Position.X - body.BodyJointsDictionary[JointType.SpineBase.ToString()].Position.X);
                */
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
                    // Check if the mode is body or not
                    if (_isStart)
                    {
                        IList<Body> _bodyList = new Body[frame.BodyFrameSource.BodyCount];

                        // Gets refreshed body data
                        frame.GetAndRefreshBodyData(_bodyList);

                        // For each body data traverse the joints
                        for(int i = 0; i < _bodyList.Count; i++)
                        {
                            // Check the current body data is empty or not
                            if (_bodyList[i] != null)
                            {
                                // Check whether or not the body is tracked
                                if (_bodyList[i].IsTracked)
                                {
                                    if (File.Exists(_filename))
                                    {
                                        using (StreamWriter streamWriter = File.AppendText(_filename))
                                        {
                                            BodyJoints bodyJoints = new BodyJoints(_frameNumber);

                                            foreach (var bodyJoint in _bodyList[i].Joints)
                                            {
                                                if (bodyJoint.Value.TrackingState != TrackingState.NotTracked)
                                                {
                                                    Joint newJ = new Joint();

                                                    ColorSpacePoint colorPoint = _kinectSensor.CoordinateMapper.MapCameraPointToColorSpace(bodyJoint.Value.Position);
                                                    // WARNING be careful about resolution ratio
                                                    newJ.Position.X = float.IsInfinity(colorPoint.X) ? 0 : (colorPoint.X / (float)(1920 / eCanvas.Width));
                                                    // WARNING be careful about resolution ratio
                                                    newJ.Position.Y = float.IsInfinity(colorPoint.Y) ? 0 : (colorPoint.Y / (float)(1080 / eCanvas.Height));

                                                    bodyJoints.BodyJointsDictionary.Add(bodyJoint.Key.ToString(), newJ);

                                                    // KEEP ORIGINAL OR CONVERTED DATA
                                                    streamWriter.WriteLine(_frameNumber + " " + bodyJoint.Key + " " + newJ.Position.X + " " + newJ.Position.Y);
                                                }
                                            }

                                            eCanvas.Children.RemoveRange(1, eCanvas.Children.Count);
                                            // Add the current body to the body list
                                            // _bodyJointsList.Add(new BodyJoints(frameNumber, _bodyList[i]));
                                            _bodyJointsList.Add(bodyJoints);
                                            // Draw the whole body
                                            Draw(eCanvas, _bodyJointsList[_frameNumber - 1]);

                                            _frameNumber++;
                                        }
                                    }
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
                    // Check if the mode is color or not
                    if (_isStart)
                    {
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
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the item is selected in the exercise list box
            if (taskContent.SelectedValue != null)
            {
                // Set the frame number to 1
                _frameNumber = 1;

                // Find the index of the '-' character in the string
                int index = taskContent.SelectedValue.ToString().IndexOf("-");

                // If it exist, split the string
                if (index > 0)
                {
                    String exerciseId = taskContent.Items[taskContent.SelectedIndex].ToString().Substring(0, index);

                    oCamera.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/EXERCISES/" + exerciseId + ".png"));

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
                }



                // take id, read file of the exerciseX
                


                // Make enable the start button
                Start.IsEnabled = true;
            }
        }

        // WARNING:: press start button, show the exercise video and say are you ready, then record the data
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            taskContent.IsEnabled = false;

            // Make disable the start button
            Start.IsEnabled = false;
            // Make enable the stop button
            Stop.IsEnabled = true;

            _isStart = true;
        }

        // WARNING::
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            // 
            eCanvas.Children.RemoveRange(1, eCanvas.Children.Count);

            // Make disable the stop button
            Stop.IsEnabled = false;
            // Make enable the replay button
            Replay.IsEnabled = true;

            // Update the image with the data from the kinect camera
            oCamera.Source = null;
            eCamera.Source = null;

            _isStart = false;
        }

        // WARNING::
        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            // Make disable the replay button
            Replay.IsEnabled = false;

            int frameNumber = 1;

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
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Close the screen
            this.Close();
        }
    }
}
