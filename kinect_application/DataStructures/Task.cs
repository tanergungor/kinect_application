using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KINECT_APPLICATION.DataStructures
{
    class Task
    {
        private List<Exercise> _exerciseList;
        private String _id;
        private String _patientId;
        private String _name;
        private String _status;
        private String _review;

        public Task()
        {
        }

        public List<Exercise> ExerciseList
        {
            get
            {
                return _exerciseList;
            }

            set
            {
                _exerciseList = value;
            }
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

        public string PatientId
        {
            get
            {
                return _patientId;
            }

            set
            {
                _patientId = value;
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

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        public string Review
        {
            get
            {
                return _review;
            }

            set
            {
                _review = value;
            }
        }
    }
}
