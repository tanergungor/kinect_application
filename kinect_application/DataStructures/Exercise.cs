using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KINECT_APPLICATION.DataStructures
{
    class Exercise
    {
        private String _id;
        private String _name;
        private String _path;
        private Double _pain;
        private Double _fatigue;
        private Double _mood;

        public Exercise()
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

        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
            }
        }

        public double Pain
        {
            get
            {
                return _pain;
            }

            set
            {
                _pain = value;
            }
        }

        public double Fatigue
        {
            get
            {
                return _fatigue;
            }

            set
            {
                _fatigue = value;
            }
        }

        public double Mood
        {
            get
            {
                return _mood;
            }

            set
            {
                _mood = value;
            }
        }
    }
}
