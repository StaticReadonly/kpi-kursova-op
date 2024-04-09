namespace Kursova
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var config = builder.Configuration;



            var app = builder.Build();



            app.Run();
        }
    }
}
