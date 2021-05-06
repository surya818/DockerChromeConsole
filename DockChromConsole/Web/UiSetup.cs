using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace uitest.browser
{
    #region Namespace Imports

    using System;
    using System.IO;
    using System.Reflection;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;

    #endregion


    public class UiSetup
    {
        public static string AlpineBinPath = "/usr/bin";
        public static string AlpineFirefoxBinPath = "/usr/bin/firefox";

        public static IWebDriver InitDriver(string browser)
        {
            IWebDriver driver;
            var options = new ChromeOptions();

            switch (browser)
            {
                case "chrome":
                    driver = ChromeDriverInit(options);
                    break;

                case "firefox":

                    driver = FirefoxInit();
                    break;
                default:
                    driver = ChromeDriverInit(options);
                    break;
            }

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return driver;
        }

        public static IWebDriver InitDriverAndOpenWebPage(string url, string browser = null)
        {
            var driver = InitDriver(browser);
            driver.Url = url;
            return driver;
        }

        public static bool IsLinux()
        {
            var isWindowsOs = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            return isWindowsOs;
        }


        public static bool IsWindows()
        {
            var isWindowsOs = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            return isWindowsOs;
        }

        public static IWebElement WaitForElement(IWebDriver driver, string xPath)
        {
            var waitDriver = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            waitDriver.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(xPath)));
            var element = driver.FindElement(By.XPath(xPath));
            return element;
        }

        private static IWebDriver ChromeDriverInit(ChromeOptions options)
        {
            if (IsWindows())
            {
                return new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            }

            if (IsLinux())
            {
                options.BinaryLocation = Path.Combine(AlpineBinPath, "chromium-browser");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-using");
                Console.WriteLine("Binary location: "+options.BinaryLocation);
                return new ChromeDriver(AlpineBinPath, options);
            }

            return new ChromeDriver();
        }

        private static IWebDriver FirefoxInit()
        {
            var ffoptions = new FirefoxOptions();
            ffoptions.BrowserExecutableLocation = AlpineFirefoxBinPath;
            ffoptions.LogLevel = FirefoxDriverLogLevel.Default;

            var service = FirefoxDriverService.CreateDefaultService(AlpineBinPath, "geckodriver");
            service.FirefoxBinaryPath = AlpineFirefoxBinPath;
            return new FirefoxDriver(service, ffoptions, TimeSpan.FromSeconds(30));
        }
    }
}