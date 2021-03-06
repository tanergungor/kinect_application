﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows.Controls;
using KINECT_APPLICATION.DataStructures;

namespace KINECT_APPLICATION.DB
{
    class DatabaseConnection
    {
        private MySqlConnection _connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        private static DatabaseConnection _databaseConnection = new DatabaseConnection();

        private DatabaseConnection()
        {
        }

        public static DatabaseConnection getDatabaseConnection()
        {
            return _databaseConnection;
        }

        public static void setDatabaseConnection(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }




















        public Person Login(String doctorEmail, String doctorPassword)
        {
            Person doctor = new Person();

            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT person_id, person_name, person_surname, person_phone, person_email, person_gender, person_birthdate FROM `mr_people` WHERE person_email = @person_email AND person_password = @person_password AND person_status = 1;", _connection);
                command.Parameters.AddWithValue("person_email", doctorEmail);
                command.Parameters.AddWithValue("person_password", doctorPassword);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get the current patient's ID
                        doctor.Id = reader["person_id"].ToString();

                        // Get the current patient's name
                        doctor.Name = reader["person_name"].ToString();

                        // Get the current patient's surname
                        doctor.Surname = reader["person_surname"].ToString();

                        doctor.Photo = "pack://application:,,,/Resources/PHOTOS/" + doctor.Id + ".png";

                        // Get the current patient's phone
                        doctor.Phone = reader["person_phone"].ToString();

                        // Get the current patient's email
                        doctor.Email = reader["person_email"].ToString();

                        // Get the current patient's gender
                        if (reader["person_gender"].ToString().Equals("F"))
                        {
                            doctor.Gender = "Female";
                        }
                        else
                        {
                            doctor.Gender = "Male";
                        }

                        // Get the current patient's birthdate
                        doctor.Birthdate = (DateTime) reader["person_birthdate"];
                    }

                    return doctor;
                }
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close(); 
            }

            return null;
        }





        public Person SelectDoctor(String doctorID)
        {
            Person doctor = new Person();

            try
            {
                _connection.Open();

                // Get the current doctor's information
                MySqlCommand command = new MySqlCommand("SELECT person_name, person_surname, person_gender, person_birthdate, person_height, person_weight FROM `mr_people` WHERE person_id = @person_id AND person_status = 1;", _connection);
                command.Parameters.AddWithValue("person_id", doctorID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get the current doctor's name
                        doctor.Id = doctorID;

                        // Get the current doctor's name
                        doctor.Name = reader["person_name"].ToString();

                        // Get the current doctor's surname
                        doctor.Surname = reader["person_surname"].ToString();

                        doctor.Photo = "pack://application:,,,/Resources/PHOTOS/" + doctor.Id + ".png";

                        // Get the current doctor's phone
                        doctor.Phone = reader["person_phone"].ToString();

                        // Get the current doctor's email
                        doctor.Email = reader["person_email"].ToString();

                        // Get the current doctor's gender
                        if (reader["person_gender"].ToString().Equals("F"))
                        {
                            doctor.Gender = "Female";
                        }
                        else
                        {
                            doctor.Gender = "Male";
                        }

                        // Get the current doctor's birthdate
                        doctor.Birthdate = (DateTime) reader["person_birthdate"];
                    }

                    return doctor;
                }
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close(); 
            }

            return null;
        }






        public Boolean UpdatePatient(Person patient)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("UPDATE `mr_people` SET `person_name` = @person_name, `person_surname` = @person_surname, `person_phone` = @person_phone, `person_email` = @person_email, `person_gender` = @person_gender, `person_birthdate` = @person_birthdate, `person_height` = @person_height, `person_weight` = @person_weight WHERE `person_id` = @person_id;", _connection);
                command.Parameters.AddWithValue("person_id", patient.Id);
                command.Parameters.AddWithValue("person_name", patient.Name);
                command.Parameters.AddWithValue("person_surname", patient.Surname);
                command.Parameters.AddWithValue("person_phone", patient.Phone);
                command.Parameters.AddWithValue("person_email", patient.Email);
                command.Parameters.AddWithValue("person_gender", patient.Gender[0]);
                command.Parameters.AddWithValue("person_birthdate", patient.Birthdate);
                command.Parameters.AddWithValue("person_height", patient.Height);
                command.Parameters.AddWithValue("person_weight", patient.Weight);
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }



        public Boolean UpdateDoctor(Person doctor)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("UPDATE `mr_people` SET `person_name` = @person_name, `person_surname` = @person_surname, `person_phone` = @person_phone, `person_email` = @person_email, `person_gender` = @person_gender, `person_birthdate` = @person_birthdate WHERE `person_id` = @person_id;", _connection);
                command.Parameters.AddWithValue("person_id", doctor.Id);
                command.Parameters.AddWithValue("person_name", doctor.Name);
                command.Parameters.AddWithValue("person_surname", doctor.Surname);
                command.Parameters.AddWithValue("person_phone", doctor.Phone);
                command.Parameters.AddWithValue("person_email", doctor.Email);
                command.Parameters.AddWithValue("person_gender", doctor.Gender[0]);
                command.Parameters.AddWithValue("person_birthdate", doctor.Birthdate);
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }



















        public Person SelectPatient(String patientID)
        {
            Person patient = new Person();

            try
            {
                _connection.Open();

                // Get the current patient's information
                MySqlCommand command = new MySqlCommand("SELECT person_id, person_name, person_surname, person_phone, person_email, person_gender, person_birthdate, person_height, person_weight FROM `mr_people` WHERE person_id = @person_id AND person_status = 0;", _connection);
                command.Parameters.AddWithValue("person_id", patientID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get the current patient's name
                        patient.Id = reader["person_id"].ToString();

                        // Get the current patient's name
                        patient.Name = reader["person_name"].ToString();

                        // Get the current patient's surname
                        patient.Surname = reader["person_surname"].ToString();

                        patient.Photo = "pack://application:,,,/Resources/PHOTOS/" + patient.Id + ".png";

                        // Get the current patient's phone
                        patient.Phone = reader["person_phone"].ToString();

                        // Get the current patient's email
                        patient.Email = reader["person_email"].ToString();

                        // Get the current patient's gender
                        if (reader["person_gender"].ToString().Equals("F"))
                        {
                            patient.Gender = "Female";
                        }
                        else
                        {
                            patient.Gender = "Male";
                        }

                        // Get the current patient's birthdate
                        patient.Birthdate = (DateTime) reader["person_birthdate"];

                        // Get the current patient's height
                        patient.Height = reader["person_height"].ToString();

                        // Get the current patient's weight
                        patient.Weight = reader["person_weight"].ToString();
                    }

                    return patient;
                }
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }







        public Boolean InsertPatient(Person patient)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO `mr_people`(`person_name`, `person_surname`, `person_phone`, `person_email`, `person_gender`, `person_birthdate`, `person_height`, `person_weight`, `person_registered_date`, `person_status`) VALUES (@person_name, @person_surname, @person_phone, @person_email, @person_gender, @person_birthdate, @person_height, @person_weight, NOW(), 0); ", _connection);
                command.Parameters.AddWithValue("person_name", patient.Name);
                command.Parameters.AddWithValue("person_surname", patient.Surname);
                command.Parameters.AddWithValue("person_phone", patient.Phone);
                command.Parameters.AddWithValue("person_email", patient.Email);
                command.Parameters.AddWithValue("person_gender", patient.Gender[0]);
                command.Parameters.AddWithValue("person_birthdate", patient.Birthdate);
                command.Parameters.AddWithValue("person_height", patient.Height);
                command.Parameters.AddWithValue("person_weight", patient.Weight);
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }

        public String SelectLastInsertedPatient()
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT MAX(`person_id`) AS `person_id` FROM `mr_people`;", _connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader["person_id"].ToString();
                    }
                }
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }











        public Boolean UpdateTask(Task task)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE `mr_tasks` SET `task_name` = @task_name, `task_review` = @task_review, `task_status` = @task_status WHERE `task_id` = @task_id;", _connection);
                command.Parameters.AddWithValue("task_id", task.Id);
                command.Parameters.AddWithValue("task_name", task.Name);
                command.Parameters.AddWithValue("task_review", task.Review);
                command.Parameters.AddWithValue("task_status", task.Status);
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }

        public Boolean DeleteExerciseRelations(String taskID)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("DELETE FROM `mr_relations` WHERE `relation_task_id` = @relation_task_id;", _connection);
                command.Parameters.AddWithValue("relation_task_id", taskID);
                command.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }








        public Boolean UpdateExerciseRelation(String taskId, Exercise exercise)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE `mr_relations` SET `relation_exercise_status` = '1', `relation_exercise_pain` = @relation_exercise_pain, `relation_exercise_fatigue` = @relation_exercise_fatigue, `relation_exercise_mood` = @relation_exercise_mood WHERE `relation_task_id` = @relation_task_id AND `relation_exercise_id` = @relation_exercise_id;", _connection);
                command.Parameters.AddWithValue("relation_task_id", taskId);
                command.Parameters.AddWithValue("relation_exercise_id", exercise.Id);
                command.Parameters.AddWithValue("relation_exercise_pain", exercise.Pain);
                command.Parameters.AddWithValue("relation_exercise_fatigue", exercise.Fatigue);
                command.Parameters.AddWithValue("relation_exercise_mood", exercise.Mood);
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }













        public Boolean InsertTask(Task task)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO `mr_tasks`(`task_person_id`, `task_name`, `task_status`) VALUES (@task_person_id, @task_name, 0); ", _connection);
                command.Parameters.AddWithValue("task_person_id", task.Id);
                command.Parameters.AddWithValue("task_name", task.Name);
                command.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }




        public String SelectLastInsertedTask(String patientID)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT `task_id` FROM `mr_tasks` WHERE `task_person_id` = @task_person_id ORDER BY `task_id` DESC; ", _connection);
                command.Parameters.AddWithValue("task_person_id", patientID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader["task_id"].ToString();
                    }
                }
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }




        public Boolean InsertExerciseRelations(String taskID, List<Exercise> list)
        {
            try
            {
                _connection.Open();

                for (int i = 0; i < list.Count; i++)
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO `mr_relations`(`relation_task_id`, `relation_exercise_id`) VALUES (@relation_task_id, @relation_exercise_id); ", _connection);
                    command.Parameters.AddWithValue("relation_task_id", taskID);
                    command.Parameters.AddWithValue("relation_exercise_id", list[i].Id);
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }














        public List<Exercise> SelectExerciseRelations(String taskID, String patientID)
        {
            List<Exercise> list = new List<Exercise>();

            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT `exercise_id`, `exercise_name`, `relation_exercise_pain`, `relation_exercise_fatigue`, `relation_exercise_mood`, `relation_exercise_status` FROM `mr_tasks`, `mr_relations`, `mr_exercises` WHERE `task_id` = `relation_task_id` AND `exercise_id` = `relation_exercise_id` AND `task_id` = @task_id AND `task_person_id` = @person_id;", _connection);
                command.Parameters.AddWithValue("task_id", taskID);
                command.Parameters.AddWithValue("person_id", patientID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise();
                        exercise.Id = reader["exercise_id"].ToString();
                        exercise.Name = reader["exercise_name"].ToString();
                        exercise.Pain = (double)reader["relation_exercise_pain"];
                        exercise.Fatigue = (double)reader["relation_exercise_fatigue"];
                        exercise.Mood = (double)reader["relation_exercise_mood"];
                        exercise.Status = (int)reader["relation_exercise_status"];

                        // Add the exercise ID nad name
                        list.Add(exercise);
                    }
                }

                return list;

            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }




















        public void SelectTasks(DataGrid list, String patientID, String statusID)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT task_id, task_person_id, task_name, task_review FROM `mr_tasks` WHERE task_person_id = @task_person_id AND task_status = @task_status;", _connection);
                command.Parameters.AddWithValue("task_person_id", patientID);
                command.Parameters.AddWithValue("task_status", statusID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "LoadDataBinding");
                list.DataContext = dataset;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }
        }



























        public void SelectPatients(DataGrid list)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT `person_id`, `person_name`, `person_surname`, `person_email`, `person_gender`, `person_birthdate`, `person_registered_date` FROM `mr_people` WHERE person_status = '0';", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "LoadDataBinding");
                list.DataContext = dataSet;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }
        }

        public Boolean DeletePatient(String patientID)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("DELETE FROM `mr_people` WHERE person_id = @person_id AND person_status = 0;", _connection);
                command.Parameters.AddWithValue("person_id", patientID);
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }





























        public Task SelectTask(String taskID, String patientID)
        {
            Task task = new Task();

            try
            {
                _connection.Open();
                // Get the current patient's information

                MySqlCommand command = new MySqlCommand("SELECT task_id, task_person_id, task_name, task_review, task_status FROM `mr_tasks` WHERE task_id = @task_id;", _connection);
                command.Parameters.AddWithValue("task_id", taskID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get the current task's ID
                        task.Id = reader["task_id"].ToString();

                        // Get the current task's person's ID
                        task.PatientId = reader["task_person_id"].ToString();

                        // Get the current task's name
                        task.Name = reader["task_name"].ToString();

                        // Get the current task's review
                        task.Review = reader["task_review"].ToString();

                        // Get the current task's status
                        task.Status = reader["task_status"].ToString();
                    }

                }

                return task;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }



        public Boolean DeleteTask(String taskID, String patientID)
        {
            try
            {
                _connection.Open();

                // Get the current patient's information
                MySqlCommand command = new MySqlCommand("DELETE FROM `mr_tasks` WHERE task_id = @task_id AND task_person_id = @task_person_id;", _connection);
                command.Parameters.AddWithValue("task_id", taskID);
                command.Parameters.AddWithValue("task_person_id", patientID);
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }

        public void SelectExercises(DataGrid list)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT `exercise_id`, `exercise_name`, `exercise_path` FROM `mr_exercises`;", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "LoadDataBinding");
                list.DataContext = dataSet;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }
        }





        public void SelectExercises(ListBox list)
        {
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT `exercise_id`, `exercise_name` FROM `mr_exercises`;", _connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the exercise ID nad name
                        list.Items.Add(reader["exercise_id"].ToString() + "-" + reader["exercise_name"].ToString());
                    }
                }

            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                _connection.Close();
            }
        }






    }
}