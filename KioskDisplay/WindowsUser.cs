using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace KioskDisplay
{
    public static class WindowsUser
    {
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);

        const int WTS_CURRENT_SESSION = -1;
        static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        public static void SetShell(string path)
        {
            var userRegistry = Registry.CurrentUser;
            userRegistry = userRegistry.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            userRegistry.SetValue("Shell", path);
        }

        public static string GetShell()
        {
            var userRegistry = Registry.CurrentUser;
            userRegistry = userRegistry.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            return (string)userRegistry.GetValue("Shell");
        }

        public static void Logoff()
        {
            if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE,
                 WTS_CURRENT_SESSION, false))
            {
                throw new Win32Exception();
            }
        }
    }
}
