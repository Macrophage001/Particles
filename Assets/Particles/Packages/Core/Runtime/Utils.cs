using System;
using System.Diagnostics;
using Particles.Packages.Core.Runtime.Helpers;
using Debug = UnityEngine.Debug;

namespace Particles.Packages.Core.Runtime
{
    public class Logger
    {
        public static T Time<T>(string name, Func<T> fn)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = fn();
            stopwatch.Stop();
            Debug.Log($"{name}: {stopwatch.ElapsedMilliseconds}ms");
            return result;
        }

        public static void Time(string name, Action fn)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            fn();
            stopwatch.Stop();
            Debug.Log($"{name}: {stopwatch.ElapsedMilliseconds}ms");
        }
    }

    public class NumberUtils
    {

        private Func<int, string, (string, double, string)> FormatterGenerator()
        {
            return (_value, formatString) =>
            {
                int _mag = UnityEngine.Mathf.Clamp(((double)_value).Length(3), 0, Suffixes.Length);
                var _currencyNotation = Suffixes[_mag];
                return (_currencyNotation, MathUtility.Shrink(_value, _mag * 3), formatString);
            };
        }

        private static readonly string[] Suffixes = {
        String.Empty, "K", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "D", "U", "DD",
        "TD", "qD", "QD", "sD", "SD", "OD", "ND",
        "V", "UV", "DV", "TV", "qV", "QV", "sV", "SV", "OV", "NV",
        "TG", "UTG", "DTG", "TTG", "qTG", "QTG", "sTG", "STG", "OTG", "NTG",
        "QG", "UQG", "DQG", "TQG", "qQG", "QQG", "sQG", "SQG", "OQG", "NQG",
        "QqG", "UQqG", "DQqG", "TQqG", "qQqG", "QQqG", "sQqG", "SQqG", "OQqG", "NQqG",
        "saG", "UsaG", "DsaG", "TsaG", "qsaG", "QsaG", "ssaG", "SsaG", "OsaG", "NsaG",
        "SaG", "USaG", "DSaG", "TSaG", "qSaG", "QSaG", "sSaG", "SSaG", "QSaG", "NSaG",
        "OG", "UOG", "DOG", "TOG", "qOG", "QOG", "sOG", "SOG", "OOG", "NOG",
        "NG", "UNG", "DNG", "TNG", "qNG", "QNG", "sNG", "SNG", "ONG", "NNG",
        "C", "UC", "DC", "TC", "qC", "QC", "sC", "SC", "OC", "NC", "Inf"
    };
    }
}
