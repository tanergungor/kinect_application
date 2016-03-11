using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KINECT_APPLICATION.DataStructures
{
    public class BodyJoints
    {
        private Dictionary<String, Joint> _BodyJointsDictionary = new Dictionary<String, Joint>();
        private int _index = 0;

        internal BodyJoints()
        {
        }

        internal BodyJoints(int _index)
        {
            this._index = _index;
        }

        internal BodyJoints(int _index, Body body)
        {
            this._index = _index;

            // Head
            _BodyJointsDictionary.Add(JointType.Head.ToString(), body.Joints[JointType.Head]);
            // Neck
            _BodyJointsDictionary.Add(JointType.Neck.ToString(), body.Joints[JointType.Neck]);
            // Shoulder Left
            _BodyJointsDictionary.Add(JointType.ShoulderLeft.ToString(), body.Joints[JointType.ShoulderLeft]);
            // Hand Left
            _BodyJointsDictionary.Add(JointType.HandLeft.ToString(), body.Joints[JointType.HandLeft]);
            // Elbow Left
            _BodyJointsDictionary.Add(JointType.ElbowLeft.ToString(), body.Joints[JointType.ElbowLeft]);
            // Hand Right
            _BodyJointsDictionary.Add(JointType.HandRight.ToString(), body.Joints[JointType.HandRight]);
            // Elbow Right
            _BodyJointsDictionary.Add(JointType.ElbowRight.ToString(), body.Joints[JointType.ElbowRight]);
            // Shoulder Right
            _BodyJointsDictionary.Add(JointType.ShoulderRight.ToString(), body.Joints[JointType.ShoulderRight]);
            // Ankle Right
            _BodyJointsDictionary.Add(JointType.AnkleRight.ToString(), body.Joints[JointType.AnkleRight]);
            // Ankle Left
            _BodyJointsDictionary.Add(JointType.AnkleLeft.ToString(), body.Joints[JointType.AnkleLeft]);
            // Foot Left
            _BodyJointsDictionary.Add(JointType.FootLeft.ToString(), body.Joints[JointType.FootLeft]);
            // Foot Right
            _BodyJointsDictionary.Add(JointType.FootRight.ToString(), body.Joints[JointType.FootRight]);
            // Hand Tip Right
            _BodyJointsDictionary.Add(JointType.HandTipRight.ToString(), body.Joints[JointType.HandTipRight]);
            // Hand Tip Left
            _BodyJointsDictionary.Add(JointType.HandTipLeft.ToString(), body.Joints[JointType.HandTipLeft]);
            // Hip Left
            _BodyJointsDictionary.Add(JointType.HipLeft.ToString(), body.Joints[JointType.HipLeft]);
            // Hip Right
            _BodyJointsDictionary.Add(JointType.HipRight.ToString(), body.Joints[JointType.HipRight]);
            // Knee Right
            _BodyJointsDictionary.Add(JointType.KneeRight.ToString(), body.Joints[JointType.KneeRight]);
            // Knee Left
            _BodyJointsDictionary.Add(JointType.KneeLeft.ToString(), body.Joints[JointType.KneeLeft]);
            // Spine Base
            _BodyJointsDictionary.Add(JointType.SpineBase.ToString(), body.Joints[JointType.SpineBase]);
            // Spine Mid
            _BodyJointsDictionary.Add(JointType.SpineMid.ToString(), body.Joints[JointType.SpineMid]);
            // Spine Shoulder
            _BodyJointsDictionary.Add(JointType.SpineShoulder.ToString(), body.Joints[JointType.SpineShoulder]);
            // Thumb Left
            _BodyJointsDictionary.Add(JointType.ThumbLeft.ToString(), body.Joints[JointType.ThumbLeft]);
            // Thumb Right
            _BodyJointsDictionary.Add(JointType.ThumbRight.ToString(), body.Joints[JointType.ThumbRight]);
            // Wrist Right
            _BodyJointsDictionary.Add(JointType.WristRight.ToString(), body.Joints[JointType.WristRight]);
            // Wrist Left
            _BodyJointsDictionary.Add(JointType.WristLeft.ToString(), body.Joints[JointType.WristLeft]);
        }

        public int Index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }

        internal Dictionary<String, Joint> BodyJointsDictionary
        {
            get
            {
                return _BodyJointsDictionary;
            }

            set
            {
                _BodyJointsDictionary = value;
            }
        }
    }
}
