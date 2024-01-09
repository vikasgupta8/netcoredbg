// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace NetCoreDbg
{
    enum RetCode : int
    {
        OK = 0,
        Fail = 1,
        Exception = 2
    }

    public class Utils
    {
        [UnmanagedCallersOnly]
        internal unsafe static RetCode StringToUpper(IntPtr _srcString, IntPtr *pdstString)
        {
	    string srcString = Marshal.PtrToStringUni(_srcString);
	    ref IntPtr dstString = ref *pdstString;
	    
            dstString = IntPtr.Zero;

            try
            {
                dstString = Marshal.StringToBSTR(srcString.ToUpper());
            }
            catch
            {
                return RetCode.Exception;
            }

            return RetCode.OK;
        }

        [UnmanagedCallersOnly]
        internal static IntPtr SysAllocStringLen(int size)
        {
            string empty = new String('\0', size);
            return Marshal.StringToBSTR(empty);
        }

        [UnmanagedCallersOnly]
        internal static void SysFreeString(IntPtr ptr)
        {
            Marshal.FreeBSTR(ptr);
        }

        [UnmanagedCallersOnly]
        internal static IntPtr CoTaskMemAlloc(int size)
        {
            return Marshal.AllocCoTaskMem(size);
        }

        [UnmanagedCallersOnly]
        internal static void CoTaskMemFree(IntPtr ptr)
        {
            Marshal.FreeCoTaskMem(ptr);
        }
    }
}
