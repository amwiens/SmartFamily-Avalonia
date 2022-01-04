﻿using Newtonsoft.Json.Converters;

using SmartFamily.Shell.Extensibility.Platforms;
using SmartFamily.Utils;

using System.Dynamic;
using System.Text.Json.Serialization;

namespace SmartFamily.GlobalSettings
{
    public class Settings
    {
        private dynamic _root = new ExpandoObject();

        private IDictionary<string, object> _rootIndex => (IDictionary<string, object>)_root;

        [JsonConverter(typeof(ExpandoObjectConverter))]
        public ExpandoObject Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
            }
        }

        private static string GlobalSettingsFile => Platform.SettingsDirectory != null ? Path.Combine(Platform.SettingsDirectory, "GlobalSettings.json") : null;

        public static Settings Instance { get; set; }

        static Settings()
        {
            Instance = Load();
        }

        private static Settings Load()
        {
            if (GlobalSettingsFile != null)
            {
                if (File.Exists(GlobalSettingsFile))
                {
                    var deserialized = SerializedObject.Deserialize<Settings>(GlobalSettingsFile);

                    if (deserialized != null)
                    {
                        return deserialized;
                    }
                }
            }

            var result = new Settings();

            result.Save();

            return result;
        }

        public void Save()
        {
            if (GlobalSettingsFile != null)
            {
                SerializedObject.Serialize(GlobalSettingsFile, this);
            }
        }

        private T GetSettingsImpl<T>() where T : new()
        {
            return SettingsSerializer.GetSettings<T>(() => Root, () => Save());
        }

        private void SetSettingsImpl<T>(T value) where T : new()
        {
            SettingsSerializer.SetSettings<T>(() => Root, () => Save(), value);
        }

        public static T GetSettings<T>() where T : new()
        {
            return Instance.GetSettingsImpl<T>();
        }

        public static void SetSettings<T>(T value) where T : new()
        {
            Instance.SetSettingsImpl(value);
        }
    }
}