// -----------------------------------------------------------
// This program is private software, based on C# source code.
// To sell or change credits of this software is forbidden,
// except if someone approves it from the LeafyCoding INC. team.
// -----------------------------------------------------------
// Copyrights (c) 2016 Leafy-ImageUploader INC. All rights reserved.
// -----------------------------------------------------------

#region

using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

#endregion

namespace Leafy_ImageUploader
{
    internal static class ImgUploader
    {
        internal static string ftpAcc = string.Empty;
        internal static string ftpPass = string.Empty;
        internal static string ftpAddr = string.Empty;
        internal static string httpAddr = string.Empty;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern bool SetClipboardData(uint uFormat, IntPtr data);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static void Main(string[] args)
        {
            Console.Title = "Leafy Image Uploader 🍂";
            Config.Init();

            ShowWindow(GetConsoleWindow(), 0);

            var FullName = args.FirstOrDefault();
            var FileName = args.FirstOrDefault()?.Split('\\').LastOrDefault();
            var FileExtension = FileName?.Split('.').LastOrDefault();
            var NewFileName = DateTime.Now.ToString("dd-MMM-yy_HH:mm:ss") + "." + FileExtension;

            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpAcc, ftpPass);
                if (FullName != null)
                    client.UploadFile(ftpAddr + NewFileName, "STOR", FullName);
            }

            OpenClipboard(IntPtr.Zero);

            if (!httpAddr.EndsWith("/"))
            {
                httpAddr = httpAddr + "/";
            }
            var URL = httpAddr + NewFileName;
            Process.Start(URL);

            var ptr = Marshal.StringToHGlobalUni(URL);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);
        }
    }
}