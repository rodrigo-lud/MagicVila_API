namespace MagicVila_VilaAPI.logging
{
    public class LoggingV2 : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("*err: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("MagicVila_VilaAPI.logging V2 - Internal message");
                Console.WriteLine("      " + message);
            }
            else if (type == "warning")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("*war: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("MagicVila_VilaAPI.logging V2 - Internal message");
                Console.WriteLine("      " + message);
            }
            else if (type == "info")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("*inf: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("MagicVila_VilaAPI.logging V2 - Internal message");
                Console.WriteLine("      " + message);
            } 
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("*gen: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("MagicVila_VilaAPI.logging V2 - Internal message");
                Console.WriteLine("         " + message);
            }
        }
    }
}

