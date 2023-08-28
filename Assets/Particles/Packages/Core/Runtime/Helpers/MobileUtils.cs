using UnityEngine;

namespace Particles.Packages.Core.Runtime.Helpers
{
    public static class MobileUtils
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        public static AndroidJavaClass m_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        public static AndroidJavaObject m_CurrentActivity =
            m_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        public static AndroidJavaObject m_Vibrator = m_CurrentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
        public static AndroidJavaClass m_UnityPlayer;
        public static AndroidJavaObject m_CurrentActivity;
        public static AndroidJavaObject m_Vibrator;
#endif

        public static bool IsAndroid()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }

        public static void Vibrate(long _milliseconds = 63)
        {
            if (IsAndroid())
            {
                m_Vibrator.Call("vibrate", _milliseconds);
            }
            else
            {
                Handheld.Vibrate();
            }
        }

        public static void Cancel()
        {
            if (IsAndroid())
                m_Vibrator.Call("cancel");
        }
    }
}
