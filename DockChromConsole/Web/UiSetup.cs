using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

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
        public static IWebDriver getDriver { get; private set; }

        public static IWebDriver InitDriver(string browser)
        {
            ChromeOptions options = new ChromeOptions();
            
            switch (browser)
            {
                case "chrome":
                    if (IsWindows())
                    {
                        getDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                    }
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-dev-shm-using");
                    var runtime = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                    if (runtime.ToLower().Contains("linux"))
                    {
                        getDriver = new ChromeDriver("/usr/bin", options);
                    }
                    else
                    {
                        getDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    }
                    break;

                case "firefox":
                    
                    var ffoptions = new FirefoxOptions();
                    ffoptions.BrowserExecutableLocation = @"/usr/bin/firefox";
                    ffoptions.LogLevel = FirefoxDriverLogLevel.Default;

                    FirefoxDriverService service =
                        FirefoxDriverService.CreateDefaultService(@"/usr/bin","geckodriver");
                    service.FirefoxBinaryPath = @"/usr/bin/firefox";
                    getDriver = new FirefoxDriver(service,ffoptions,TimeSpan.FromSeconds(30));
                    break;

                default:
                    if (IsWindows())
                    {
                        getDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                    }
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-dev-shm-using");
                    runtime = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                    if (runtime.ToLower().Contains("linux"))
                    {
                        getDriver = new ChromeDriver("/usr/bin", options);
                    }
                    else
                    {
                        getDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    }
                    break;
            }

            getDriver.Manage().Window.Maximize();
            getDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return getDriver;
        }

        public static IWebDriver InitDriverAndOpenWebPage(string browser, string url)
        {
            getDriver= InitDriver(browser);
            getDriver.Url = url;
            return getDriver;
        }

        public static bool IsWindows()
        {
            bool isWindowsOs = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            return isWindowsOs;
        
        }
}
}

