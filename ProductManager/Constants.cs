namespace ProductManager
{
    internal class Constants
    {
        public class Database
        {
            public const string ConnectionStringName = "DefaultConnection";
        }

        public class Routes
        {
            public const string Template = "api/[controller]";
        }

        public class Serilog
        {
            public const string Path = @"D:\Home\LogFiles\Application\ProductManagerLogs.txt";

            public const long FileSizeLimitBytes = 1000000;

            public const bool RollOnFileSizeLimit = true;

            public const bool Shared = true;

            public const int FlushToDiskIntervalInSeconds = 1;
        }

        public class Swagger
        {
            public const string Name = "Product Manager";

            public const string Url = "/swagger/v1/swagger.json";

            public const string Version = "v1";
        }
    }
}
