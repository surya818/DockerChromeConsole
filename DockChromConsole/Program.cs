using System;
using System.IO;
using OpenQA.Selenium;
using uitest.browser;

namespace DockChromConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string filepath = "Web/plaidpoc.html";
            string absoluteFilePath = Path.GetFullPath(filepath);
            Console.WriteLine(absoluteFilePath);
            
            var driver = UiSetup.InitDriverAndOpenWebPage("firefox", "file:///"+absoluteFilePath);
            var title = driver.Title;
            /*
             * if (driver.Title == null || !driver.Title.Equals("Bing"))
            {
                throw new InvalidOperationException("UITest failed");
            }
            Console.WriteLine("Test Successful: ");
            driver.Close();
            Console.WriteLine("Test Successful: Closed browser");
             */

            if (driver.Title == null || !driver.Title.Contains("Payment Poc"))
            {
                throw new InvalidOperationException("UITest failed");
            }
            Console.WriteLine(driver.FindElement(By.Id("link-button")).Enabled+" :Link Button Enabled");
            Console.WriteLine("Test Successful: ");
            driver.Close();
            Console.WriteLine("Test Successful: Closed browser");
            
        }
    }
}
