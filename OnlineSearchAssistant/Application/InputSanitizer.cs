using System;

namespace OnlineSearchAssistant.Application
{
    public interface IInputSanitizer
    {
        public bool IsUrl(string text);
    }

    public class InputSanitizer : IInputSanitizer
    {
        public bool IsUrl(string text)
        {
            return Uri.IsWellFormedUriString(text, UriKind.RelativeOrAbsolute);
        }
    }
}
