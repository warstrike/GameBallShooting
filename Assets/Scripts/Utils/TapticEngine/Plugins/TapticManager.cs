using System.Runtime.InteropServices;
using RDG;
using UnityEngine;

namespace TapticPlugin
{
    
    public enum NotificationFeedback
    {
        Success,
        Warning,
        Error
    }


    public enum ImpactFeedback
    {
        Light,
        Medium,
        Heavy
    }

    public static class TapticManager
    {

        private static bool isAndroid()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
            return false;
#endif
        }

        public static void Notification(NotificationFeedback feedback)
        {
            if(GamePrefs.GetVibrate())
            _unityTapticNotification((int)feedback);
        }

        public static void Impact(ImpactFeedback feedback)
        {
            if (GamePrefs.GetVibrate())
            {
                if (isAndroid())
                {

                int time=10;
                int power = 60;
                if (feedback == ImpactFeedback.Light)
                {
                    time = 30;
                }
                if (feedback == ImpactFeedback.Medium)
                {
                    time = 100;
                    power = 100;
                }
                if (feedback == ImpactFeedback.Heavy)
                {
                    time = 400;
                    power = 200;
                }

                Vibration.Vibrate(time,power);
  
                    
                }
                else
                {
                    _unityTapticImpact((int)feedback);
                }
                
            }
        }

        public static void Selection()
        {
            if (GamePrefs.GetVibrate()) _unityTapticSelection();
        }

        public static bool IsSupport()
        {
            return _unityTapticIsSupport();
        }

        #region DllImport

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _unityTapticNotification(int type);
        [DllImport("__Internal")]
        private static extern void _unityTapticSelection();
        [DllImport("__Internal")]
        private static extern void _unityTapticImpact(int style);
        [DllImport("__Internal")]
        private static extern bool _unityTapticIsSupport();
#else
        private static void _unityTapticNotification(int type) { }

        private static void _unityTapticSelection() { }

        private static void _unityTapticImpact(int style) { }

        private static bool _unityTapticIsSupport() { return false; }
#endif

        #endregion // DllImport
    }

}