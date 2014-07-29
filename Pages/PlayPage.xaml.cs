//------------------------------------------------------------------------------
// <copyright file="ButtonSample.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    using Microsoft.Kinect;
    using Microsoft.Kinect.VisualGestureBuilder;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Forms;

    /// <summary>
    /// Interaction logic for ButtonSample
    /// </summary>
    public partial class ButtonSample : System.Windows.Controls.UserControl
    {
        VisualGestureBuilderDatabase vgbd;
        VisualGestureBuilderFrameSource vgbfs;
        VisualGestureBuilderFrameReader vgbr;
        Gesture gesture;
        KinectSensor sensor;
        BodyFrameReader bfr;
        Body closest_b; 

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonSample" /> class.
        /// </summary>
        public ButtonSample()
        {
            Debug.WriteLine("1");

            sensor = KinectSensor.GetDefault();
            bfr = sensor.BodyFrameSource.OpenReader();
            bfr.FrameArrived += bfr_FrameArrived;

            this.sensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            vgbd = new VisualGestureBuilderDatabase("KontrollerGestures.gbd");
            vgbfs = new VisualGestureBuilderFrameSource(KinectSensor.GetDefault(), 0);
            Debug.WriteLine("1");
            foreach (var g in vgbd.AvailableGestures)
            {
                if (g.Name.Equals("running") || g.Name.Equals("jump") || g.Name.Equals("crouch") || g.Name.Equals("punch"))
                {
                    gesture = g;
                    vgbfs.AddGesture(gesture);
                }
            }
            vgbr = vgbfs.OpenReader();
            vgbfs.GetIsEnabled(gesture);
            vgbr.FrameArrived += vgbr_FrameArrived;
            sensor.Open();
            this.InitializeComponent();
        }

        private void ButtonSample_Closing(object sender, CancelEventArgs e)
        {
            if (this.sensor != null)
            {
                this.sensor.Close();
                this.sensor = null;
            }
        }

        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            // yall ready for this? buuuhduhduh..
        }

        void bfr_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            //Check to see if VGB has a valid tracking id, if not find a new body to track
            if (!vgbfs.IsTrackingIdValid)
            {

                using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
                {
                    if (bodyFrame != null)
                    {
                        Body[] bodies = new Body[6];
                        bodyFrame.GetAndRefreshBodyData(bodies);
                        Body closestBody = null;
                        //iterate through the bodies and pick the one closest to the camera
                        foreach (Body b in bodies)
                        {
                            if (b.IsTracked)
                            {
                                if (closestBody == null)
                                {
                                    closestBody = b;
                                    this.closest_b = b;
                                }
                                else
                                {
                                    Joint newHeadJoint = b.Joints[JointType.Head];
                                    Joint oldHeadJoint = closestBody.Joints[JointType.Head];
                                    if (newHeadJoint.TrackingState == TrackingState.Tracked && newHeadJoint.Position.Z < oldHeadJoint.Position.Z)
                                    {
                                        closestBody = b;
                                        this.closest_b = b;

                                    }
                                }
                                
                                
                            }

                        }

                        //if we found a tracked body, update the trackingid for vgb
                        if (closestBody != null)
                        {
                            vgbfs.TrackingId = closestBody.TrackingId;
                        }
                    }
                }
            }
        }
        const int VK_W = 0x57;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        int press()
        {

            //Press the key
            keybd_event((byte)VK_W, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            return 0;

        }
        int release()
        {
            //Release the key
            keybd_event((byte)VK_W, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            return 0;
        }

        void vgbr_FrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    //This check is almost certainly not needed for this sample, left in for debugging help
                    if (vgbfs.IsTrackingIdValid)
                    {
                        foreach (var result in frame.GestureResults)
                        {
                            Gesture gesture = result.Key;
                            var gestureResult = result.Value as DiscreteGestureResult;


                            if (gestureResult.Detected == true && gesture.Name.Equals("punch") && gestureResult.FirstFrameDetected)
                            {
                                Debug.WriteLine("Gesture detected! " + gesture.Name);
                                SendKeys.SendWait("o");
                                Thread.Sleep(1);
                            }

                            if (gestureResult.Detected == true && gesture.Name.Equals("jump") && gestureResult.FirstFrameDetected)
                            {
                                Debug.WriteLine("Gesture detected! " + gesture.Name);
                                SendKeys.SendWait(" ");
                                Thread.Sleep(1);
                            }

                            if (gestureResult.Detected == true && gesture.Name.Equals("crouch") && gestureResult.FirstFrameDetected)
                            {
                                Debug.WriteLine("Gesture detected! " + gesture.Name);
                                SendKeys.SendWait("m");
                                Thread.Sleep(1);
                            }

                            if (gestureResult.Detected == true && gesture.Name.Equals("running") && gestureResult.FirstFrameDetected)
                            {
                                Debug.WriteLine("Gesture detected! "+gesture.Name);
                                SendKeys.SendWait("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww"); //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz
                                //Thread.Sleep(1);                                                                      :(
                                //press();
                                //PressKey('w', true);
                            }
                        }
                        //DiscreteGestureResult result = (DiscreteGestureResult)frame.GetDiscreteGestureResult(gesture);

                        //If it is detected, and it this this gesture was not detected on the last frame, then call the gesture as hit
                        //If you didn't require "FirstFrameDetected" every frame for the gesture would count as a unique instance
                        //This case just uses "Detected" as a bool, we could tune our detection threshold by using result.Confidence and set our own min value
                        
                    }
                }
            }
        }
         
        public static void PressKey(char ch, bool press)
        {
            byte vk = hwatAPI.VkKeyScan(ch);
            ushort scanCode = (ushort)hwatAPI.MapVirtualKey(vk, 0);

            if (press)
                KeyDown(scanCode);
            else
                KeyUp(scanCode);
        }
        // WHAT IS HAPPENING
        public static void KeyDown(ushort scanCode)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = hwatAPI.INPUT_KEYBOARD;
            inputs[0].ki.dwFlags = 0;
            inputs[0].ki.wScan = (ushort)(scanCode & 0xff);

            uint intReturn = hwatAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
            if (intReturn != 1)
            {
                throw new Exception("Could not send key: " + scanCode);
            }
        }

        public static void KeyUp(ushort scanCode)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = hwatAPI.INPUT_KEYBOARD;
            inputs[0].ki.wScan = scanCode;
            inputs[0].ki.dwFlags = hwatAPI.KEYEVENTF_KEYUP;
            uint intReturn = hwatAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
            if (intReturn != 1)
            {
                throw new Exception("Could not send key: " + scanCode);
            }
        }
       


    }
}
