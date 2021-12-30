﻿using System.Reflection;
using System.Runtime.InteropServices;

namespace SmartFamily.Shell.Extensibility.Platforms
{
    public class Platform
    {
        private static string _baseDirectory;
        public static string AppName { get; set; }

        public static string SettingsDirectory => BaseDirectory != null ? Path.Combine(BaseDirectory, "Settings") : null;

        public static void Initialize()
        {
            if (!Directory.Exists(BaseDirectory))
            {
                Directory.CreateDirectory(BaseDirectory);
            }

            if (!Directory.Exists(SettingsDirectory))
            {
                Directory.CreateDirectory(SettingsDirectory);
            }

            if (!Directory.Exists(ExtensionsFolder))
            {
                Directory.CreateDirectory(ExtensionsFolder);
            }
        }

        public static PlatformID PlatformIdentifier
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return PlatformID.Win32NT;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return PlatformID.Unix;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return PlatformID.MacOSX;
                }

                throw new Exception("Unknown platform");
            }
        }

        public static string ExecutionPath => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static string ExtensionsFolder => Path.Combine(ExecutionPath, "Extensions");

        public static string BaseDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_baseDirectory))
                {
                    var envPath = Environment.GetEnvironmentVariable("SMARTFAMILY_CACHE_PATH");

                    if (envPath != null)
                    {
                        _baseDirectory = envPath;
                    }
                    else
                    {
                        string userDir = string.Empty;

                        switch (PlatformIdentifier)
                        {
                            case PlatformID.Win32NT:
                                userDir = Environment.GetEnvironmentVariable("UserProfile");
                                break;

                            default:
                                userDir = Environment.GetEnvironmentVariable("HOME");
                                break;
                        }

                        _baseDirectory = AppName != null ? Path.Combine(userDir, AppName) : null;
                    }
                }

                return _baseDirectory;
            }
            set
            {
                _baseDirectory = value;
            }
        }
    }
}