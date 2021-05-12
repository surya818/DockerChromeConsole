using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DockChromConsole.Web;
using OpenQA.Selenium;
using uitest.browser;

namespace DockChromConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");
            UiSetup.cleanupBrowser();
            var obj = new DwollaUI().LoginToBankAndExtractAchAccountInformation("fde3d2728b2f2df0b8119b7c16bf62");
            Console.WriteLine("AccessToken: "+obj.GetValueOrDefault("AccessToken"));
            Console.WriteLine("ExternalAccountIds: " + obj.GetValueOrDefault("ExternalAccountIds"));
            /*
            string filepath = "/app/DockerChromeConsole/DockChromConsole/Web/plaidpoc.html";
            string absoluteFilePath = Path.GetFullPath(filepath);
            Console.WriteLine(absoluteFilePath);
            
            var driver = UiSetup.InitDriverAndOpenWebPage("chrome", "file:///"+absoluteFilePath);
            var title = driver.Title;
            /*
             * if (driver.Title == null || !driver.Title.Equals("Bing"))
            {
                throw new InvalidOperationException("UITest failed");
            }
            Console.WriteLine("Test Successful: ");
            driver.Close();
            Console.WriteLine("Test Successful: Closed browser");
             

            if (driver.Title == null || !driver.Title.Contains("Payment Poc"))
            {
                throw new InvalidOperationException("UITest failed");
            }
            Console.WriteLine(driver.FindElement(By.Id("link-button")).Enabled+" :Link Button Enabled");
            Console.WriteLine("Test Successful: ");
            driver.Close();
            Console.WriteLine("Test Successful: Closed browser");
            */

        }
    }
}
