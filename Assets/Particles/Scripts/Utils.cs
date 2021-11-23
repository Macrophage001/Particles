using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Particles.Scripts
{
    public abstract class AbstractLogger
    {
        #if DISABLE_DEBUG_LOGGING
            [Conditional("__NEVER_DEFINED__")]
        #endif
        public abstract void Log(string _string);
        
        #if DISABLE_DEBUG_LOGGING
            [Conditional("__NEVER_DEFINED__")]
        #endif
        public abstract void Error(string _string);
        
        #if DISABLE_DEBUG_LOGGING
            [Conditional("__NEVER_DEFINED__")]
        #endif
        public abstract void Warning(string _string);
    }
    
    public class Logger : AbstractLogger
    {
        private static Logger _instance;
        public static Logger m_Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Logger();
                return _instance;
            }
        }
        public override void Log(string _string)
        {
            Debug.Log(_string);
        }

        public override void Error(string _string)
        {
            Debug.LogError(_string);
        }

        public override void Warning(string _string)
        {
            Debug.LogWarning(_string);
        }
    }

    public static class Utils
    {
    }
}