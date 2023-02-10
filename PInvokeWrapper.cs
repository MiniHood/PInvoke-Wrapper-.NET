using System;
using System.Runtime.InteropServices;

namespace Pinvoke
{
    public static class PinvokeWrapper
    {
        public static T CallCppFunction<T>(string dllName, string functionName, params object[] parameters)
        {
            Type returnType = typeof(T);
            Type[] parameterTypes = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterTypes[i] = parameters[i].GetType();
            }

            IntPtr functionPointer = NativeMethods.GetProcAddress(NativeMethods.LoadLibrary(dllName), functionName);
            if (functionPointer == IntPtr.Zero)
            {
                throw new Exception("Failed to find the specified function in the DLL.");
            }

            Delegate delegateInstance = Marshal.GetDelegateForFunctionPointer(functionPointer, returnType);
            return (T)delegateInstance.DynamicInvoke(parameters);
        }
    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
    }
}
