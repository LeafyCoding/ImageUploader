// -----------------------------------------------------------
// This program is private software, based on C# source code.
// To sell or change credits of this software is forbidden,
// except if someone approves it from the LeafyCoding INC. team.
// -----------------------------------------------------------
// Copyrights (c) 2016 LeafyYT-Downloader INC. All rights reserved.
// -----------------------------------------------------------

#region

using System;
using System.IO;
using Nini.Config;

#endregion

namespace Leafy_ImageUploader
{
    internal static class Config
    {
        private static IniConfigSource _configFile;

        public static void Init()
        {
            if (!File.Exists("config.ini"))
            {
                CreateConfig();
                Console.WriteLine("Config file not found, new config has been created, please fill in the info in config.ini.");
                Console.WriteLine("Press any key to close.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            ImgUploader.ftpAddr = ConfigSetting("[FTP]", "Address");
            ImgUploader.ftpAcc = ConfigSetting("[FTP]", "Username");
            ImgUploader.ftpPass = ConfigSetting("[FTP]", "Password");
            ImgUploader.httpAddr = ConfigSetting("[HTTP]", "Address");
        }

        private static void CreateConfig()
        {
            try
            {
                var NewConfig = ";config.ini|[FTP]|Address = |Username = |Password = |[HTTP]|Address = ";
                NewConfig = NewConfig.Replace("|", Environment.NewLine);
                File.WriteAllText(@"config.ini", NewConfig);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                Console.WriteLine("A config file could not be written, check current directory permissions.");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        private static string ConfigSetting(string key, string setting)
        {
            _configFile = new IniConfigSource("config.ini");

            return _configFile.Configs[key].Get(setting);
        }
    }
}