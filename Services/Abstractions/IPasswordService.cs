namespace Services.Abstractions
{
    public interface IPasswordService
    {
        public string HashPassword(string pass);
        public bool VerifyPassword(string pass, string userPass);
    }
}
