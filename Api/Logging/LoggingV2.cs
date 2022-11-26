using System;
namespace Api.Logging
{
	public class LoggingV2 : ILogging
	{
		public LoggingV2()
		{
		}

        public void Log(string message, LogType type)
		{
			if (type == LogType.Error)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.WriteLine("ERROR - " + message);
				Console.BackgroundColor = ConsoleColor.Black;

            }
			else if (type == LogType.Info)
			{
				Console.WriteLine("INFO - " + message);
			}
			else Console.WriteLine("Message - " + message); 

		}

    }
	
}

