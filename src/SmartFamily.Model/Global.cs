using System.Diagnostics;

namespace SmartFamily.Model
{
    public static class Global
    {
        public const string AppName = "Smart Family";

        /// <summary>
        /// Returns the path of the executable that started the currently executing process.
        /// Returns null when the path is not available.
        /// </summary>
        public static string? ProcessPath => Environment.ProcessPath;

        /// <summary>
        /// Returns the directory of the executable that started the currently executing process.
        /// Returns null when the path is not available.
        /// </summary>
        public static string? ProcessDirectory => Path.GetDirectoryName(ProcessPath);

        /// <summary>
        /// Gets the NetBIoS name of this local computer.
        /// </summary>
        public static string MachineName => Environment.MachineName;

        /// <summary>
        /// Gets the current platform identifier and version number.
        /// </summary>
        public static string OSVersion => Environment.OSVersion.VersionString;

        private static string? _appVersion;

        public static string AppVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_appVersion))
                {
                    _appVersion = GetVersion();
                }
                return _appVersion;
            }
        }

        private static string GetVersion()
        {
            try
            {
                // https://docs.microsoft.com/en-us/dotnet/core/deploying/single-file
                var fvi = FileVersionInfo.GetVersionInfo(Environment.ProcessPath);

#pragma warning disable CS8603 // Possible null reference return.
                if (fvi != null)
                {
                    if (fvi.FileVersion == fvi.ProductVersion)
                    {
                        return fvi.FileVersion;
                    }
                    else
                    {
                        return $"{fvi.FileVersion} ({fvi.ProductVersion})";
                    }
                }
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Exception ex)
            {
                return $"ERROR in version: {ex.Message}";
            }
        }

        public static Version ParseVersion(string versionStr)
        {
            if (Version.TryParse(versionStr, out var version))
            {
                return version;
            }

            if (versionStr.Contains(' '))
            {
                if (Version.TryParse(versionStr.Split(' ')[0], out version))
                {
                    return version;
                }
                throw new ArgumentException();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Get the file size in MB. Returns <c>-1</c> if the file is not found.
        /// </summary>
        public static long GetFileSize(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path).Length / 1_048_576;
            }
            return -1;
        }
    }
}