﻿using System;
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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

//Using namespaces 
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using KINECT_APPLICATION.DataStructures;
using KINECT_APPLICATION.DB;

namespace KINECT_APPLICATION
{
    /// <summary>
    /// Interaction logic for AddPatientWindow.xaml
    /// </summary>
    public partial class InsertPatientUserControl : UserControl
    {
        // Create a singleton database connection object
        private DatabaseConnection _databaseConnection = DatabaseConnection.getDatabaseConnection();
        // Create a doctor object
        private Doctor _doctor = null;

        internal InsertPatientUserControl(Doctor doctor)
        {
            InitializeComponent();

            // Set the doctor object
            _doctor = doctor;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Create a regular expression that only accepts the number
            Regex regex = new Regex("[^0-9]+");
            // Check if the text is matched with the regular expression created
            e.Handled = regex.IsMatch(e.Text);
        }

        // WARNING:: CHECK EMPTY FIELDS!
        private void InsertPatient_Click(object sender, RoutedEventArgs e)
        {
            // Create a patient object that is going to be inserted
            Patient patient = new Patient();
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
            patient.Birthdate = (DateTime) Birthdate.SelectedDate;
            // Get the patient height
            patient.Height = Height.Text.ToString();
            // Get the patient weight
            patient.Weight = Weight.Text.ToString();

            // Insert the patient in the database
            Boolean isInserted = _databaseConnection.InsertPatient(patient);

            // If the patient's information is inserted
            if (isInserted)
            {
                // If the patient's information is inserted, show the message
                MessageBox.Show("INSERT: Successful - The patient's information is inserted!");
            }
            else
            {
                // If the patient's information is not inserted, show the error message
                MessageBox.Show("INSERT: Unsuccessful - The patient's information is not inserted!");
            }

            // Delete the children of the main window content
            MainWindow.MainWindowContent.Children.Clear();
            // Create the new user control which shows the doctor's information and patients
            SelectDoctorUserControl window = new SelectDoctorUserControl(_doctor);
            // Add the new user control to the main window content
            MainWindow.MainWindowContent.Children.Add(window);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
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