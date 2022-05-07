using UnityEngine;
using System.Diagnostics;

public class Debugger
{
    public enum LEVEL {INFO, WARNING, ERROR}
    public static void log(string message, LEVEL level = LEVEL.INFO)
    {
        System.Reflection.MethodBase method = new StackTrace().GetFrame(1).GetMethod();
        string log = "[" + method.ReflectedType.Name + "::" + method.Name + "] --- " + message;

        switch (level)
        {
            case LEVEL.INFO:
                UnityEngine.Debug.Log(log);
                break;
            case LEVEL.WARNING:
                UnityEngine.Debug.LogWarning(log);
                break;
            case LEVEL.ERROR:
                UnityEngine.Debug.LogError(log);
                break;
        }
    }
}
