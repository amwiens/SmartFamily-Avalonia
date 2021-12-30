namespace SmartFamily
{
    public enum Language
    {
        English,
    }

    public static class LanguageHelper
    {
        public static Language[] Languages { get; } =
        {
            Language.English,
        };
    }
}