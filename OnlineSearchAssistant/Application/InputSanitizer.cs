using System;

namespace OnlineSearchAssistant.Application
{
    public interface IInputSanitizer
    {
        public bool IsAbsoluteUrl(string text);
    }

    public class InputSanitizer : IInputSanitizer
    {
        public bool IsAbsoluteUrl(string text)
        {
            return Uri.IsWellFormedUriString(text, UriKind.Absolute);
        }
    }
}
