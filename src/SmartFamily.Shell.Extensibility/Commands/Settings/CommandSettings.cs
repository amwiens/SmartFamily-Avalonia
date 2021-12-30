namespace SmartFamily.Commands.Settings
{
    public class CommandSettings
    {
        public Dictionary<string, Command> Commands { get; set; }

        public CommandSettings()
        {
            Commands = new Dictionary<string, Command>();
        }
    }
}