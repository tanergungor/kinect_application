﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KINECT_APPLICATION.DataStructures
{
    class Person
    {
        private String _id;
        private String _name;
        private String _surname;
        private String _photo;
        private String _phone;
        private String _email;
        private DateTime _birthdate;
        private String _gender;
        private String _height;
        private String _weight;

        public Person()
        {
        }

        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }

            set
            {
                _surname = value;
            }
        }

        public string Photo
        {
            get
            {
                return _photo;
            }

            set
            {
                _photo = value;
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                _phone = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }

        public DateTime Birthdate
        {
            get
            {
                return _birthdate;
            }

            set
            {
                _birthdate = value;
            }
        }

        public string Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                _gender = value;
            }
        }

        public string Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        public string Weight
        {
            get
            {
                return _weight;
            }

            set
            {
                _weight = value;
            }
        }
    }
}
