namespace Socials.Client.Client.Helpers
{
    public class ResponseMessage
    {
        public bool OK { get; set; }
        public string? Message { get; set; } = "";

        public string? PrettyMessage => StringExtensions.FirstLetterToUpperCase(Message);
    }
}
