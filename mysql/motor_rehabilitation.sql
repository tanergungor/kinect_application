-- phpMyAdmin SQL Dump
-- version 4.5.3.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Mar 17, 2016 at 10:33 AM
-- Server version: 5.7.10
-- PHP Version: 5.6.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `motor_rehabilitation`
--

-- --------------------------------------------------------

--
-- Table structure for table `mr_exercises`
--

CREATE TABLE `mr_exercises` (
  `exercise_id` int(10) UNSIGNED NOT NULL,
  `exercise_name` varchar(64) NOT NULL,
  `exercise_time` time NOT NULL,
  `exercise_path` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `mr_exercises`
--

INSERT INTO `mr_exercises` (`exercise_id`, `exercise_name`, `exercise_time`, `exercise_path`) VALUES
(20008, 'Exercise 01', '00:01:30', '/PATH/EXERCISE/01/'),
(20021, 'Exercise 02', '00:00:45', '/PATH/EXERCISE/02/'),
(20034, 'Exercise 03', '00:01:15', '/PATH/EXERCISE/03/'),
(20047, 'Exercise 04', '00:00:45', '/PATH/EXERCISE/04/'),
(20060, 'Exercise 05', '00:00:30', '/PATH/EXERCISE/05/');

-- --------------------------------------------------------

--
-- Table structure for table `mr_people`
--

CREATE TABLE `mr_people` (
  `person_id` int(10) UNSIGNED NOT NULL,
  `person_name` varchar(64) NOT NULL DEFAULT '',
  `person_surname` varchar(64) NOT NULL DEFAULT '',
  `person_phone` varchar(32) DEFAULT NULL,
  `person_email` varchar(64) NOT NULL DEFAULT '',
  `person_password` varchar(64) NOT NULL DEFAULT '',
  `person_gender` char(1) DEFAULT NULL,
  `person_birthdate` datetime DEFAULT NULL,
  `person_height` double UNSIGNED DEFAULT NULL,
  `person_weight` double UNSIGNED DEFAULT NULL,
  `person_activation_key` varchar(64) DEFAULT NULL,
  `person_registered_date` datetime DEFAULT CURRENT_TIMESTAMP,
  `person_status` int(11) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `mr_people`
--

INSERT INTO `mr_people` (`person_id`, `person_name`, `person_surname`, `person_phone`, `person_email`, `person_password`, `person_gender`, `person_birthdate`, `person_height`, `person_weight`, `person_activation_key`, `person_registered_date`, `person_status`) VALUES
(10011, 'Taner', 'Güngör', '+90 554 779 42 51', 'tanergungor@yahoo.com', '111111', 'M', '1989-10-05 00:00:00', 65, 178, NULL, '2016-02-05 16:44:00', 1),
(10037, 'Ayla', 'Gündogdu', '+90 554 779 42 52', 'aylagungogdu@yahoo.com', '000000', 'F', '2016-01-01 00:00:00', 150, 55, NULL, '2016-02-05 16:45:21', 0);

-- --------------------------------------------------------

--
-- Table structure for table `mr_relations`
--

CREATE TABLE `mr_relations` (
  `relation_task_id` int(10) UNSIGNED NOT NULL,
  `relation_exercise_id` int(10) UNSIGNED NOT NULL,
  `relation_exercise_time` time DEFAULT NULL,
  `relation_exercise_result` tinyint(1) DEFAULT NULL,
  `relation_exercise_pain` double DEFAULT '0',
  `relation_exercise_fatigue` double DEFAULT '0',
  `relation_exercise_mood` double DEFAULT '0',
  `relation_exercise_status` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `mr_relations`
--

INSERT INTO `mr_relations` (`relation_task_id`, `relation_exercise_id`, `relation_exercise_time`, `relation_exercise_result`, `relation_exercise_pain`, `relation_exercise_fatigue`, `relation_exercise_mood`, `relation_exercise_status`) VALUES
(30005, 20008, NULL, NULL, 1, 1, 10, 1),
(30018, 20008, NULL, NULL, 0, 0, 0, 0),
(30018, 20021, NULL, NULL, 0, 0, 0, 0),
(30018, 20034, NULL, NULL, 0, 0, 0, 0),
(30018, 20047, NULL, NULL, 0, 0, 0, 0),
(30018, 20060, NULL, NULL, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `mr_tasks`
--

CREATE TABLE `mr_tasks` (
  `task_id` int(10) UNSIGNED NOT NULL,
  `task_person_id` int(10) UNSIGNED NOT NULL,
  `task_name` varchar(64) NOT NULL DEFAULT '',
  `task_time` time DEFAULT NULL,
  `task_result` tinyint(1) DEFAULT NULL,
  `task_review` longtext,
  `task_status` int(11) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `mr_tasks`
--

INSERT INTO `mr_tasks` (`task_id`, `task_person_id`, `task_name`, `task_time`, `task_result`, `task_review`, `task_status`) VALUES
(30005, 10037, 'Task 01 For Patient 10037', NULL, NULL, '', 1),
(30018, 10037, 'Task 02 For Patient 10037', NULL, NULL, '', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `mr_exercises`
--
ALTER TABLE `mr_exercises`
  ADD PRIMARY KEY (`exercise_id`);

--
-- Indexes for table `mr_people`
--
ALTER TABLE `mr_people`
  ADD PRIMARY KEY (`person_id`);

--
-- Indexes for table `mr_relations`
--
ALTER TABLE `mr_relations`
  ADD PRIMARY KEY (`relation_task_id`,`relation_exercise_id`),
  ADD KEY `relation_task_id` (`relation_task_id`),
  ADD KEY `relation_exercise_id` (`relation_exercise_id`);

--
-- Indexes for table `mr_tasks`
--
ALTER TABLE `mr_tasks`
  ADD PRIMARY KEY (`task_id`),
  ADD KEY `mr_task_foreign_key_1` (`task_person_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `mr_exercises`
--
ALTER TABLE `mr_exercises`
  MODIFY `exercise_id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20061;
--
-- AUTO_INCREMENT for table `mr_people`
--
ALTER TABLE `mr_people`
  MODIFY `person_id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10038;
--
-- AUTO_INCREMENT for table `mr_tasks`
--
ALTER TABLE `mr_tasks`
  MODIFY `task_id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30019;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `mr_relations`
--
ALTER TABLE `mr_relations`
  ADD CONSTRAINT `mr_relation_foreign_key_1` FOREIGN KEY (`relation_task_id`) REFERENCES `mr_tasks` (`task_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `mr_relation_foreign_key_2` FOREIGN KEY (`relation_exercise_id`) REFERENCES `mr_exercises` (`exercise_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `mr_tasks`
--
ALTER TABLE `mr_tasks`
  ADD CONSTRAINT `mr_task_foreign_key_1` FOREIGN KEY (`task_person_id`) REFERENCES `mr_people` (`person_id`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
