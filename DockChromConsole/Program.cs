using System;
using uitest.browser;

namespace DockChromConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var driver = UiSetup.InitDriverAndOpenWebPage("chrome", "https://bing.com");
            var title = driver.Title;
            if (driver.Title == null || !driver.Title.Equals("Bing"))
            {
                throw new InvalidOperationException("UITest failed");
            }
            Console.WriteLine("Test Successful: ");
            driver.Close();
            Console.WriteLine("Test Successful: Closed browser");
        }
    }
}
