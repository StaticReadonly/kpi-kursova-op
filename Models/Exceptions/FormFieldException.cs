namespace Models.Exceptions
{
    public class FormFieldException : Exception
    {
        public string Key { get; set; } = string.Empty;

        public FormFieldException(string key, string message) : base(message)
        {
            Key = key;
        }
    }
}
