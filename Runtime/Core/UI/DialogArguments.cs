namespace NineLives.Framework.Core.UI
{
    public class DialogArguments
    {
        public DialogArguments(string caption, string? message, params DialogButtonInfo[] buttonInfos)
        {
            Title = caption;
            Message = message;
            Buttons = buttonInfos;
        }

        public string Title { get; set; }
        public string? Message { get; set; }
        public DialogButtonInfo[] Buttons { get; set; }
    }
}