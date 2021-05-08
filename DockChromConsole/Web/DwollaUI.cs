using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Minority.InternalTesting.RegressionTest.Contract.ACH.templates;
using OpenQA.Selenium;
using uitest.browser;

namespace DockChromConsole.Web
{
    #region Namespace Imports

    #endregion


    public class DwollaUI
    {
        private static readonly string _linuxPodDwollaHtmlPath = "/app/Ach/templates";
        private IWebDriver _driver;

        public static string InitializeDwollaUi(string plaidPublicKey)
        {
            CleanDwollaHtmlFilesInTmp();
            var tmpDirPath = "../../release/netcoreapp2.2/Ach/templates";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                tmpDirPath = _linuxPodDwollaHtmlPath;
            }

            var tmpDirAbsolutePath = Path.GetFullPath(tmpDirPath);
            Console.WriteLine(tmpDirAbsolutePath);
            var dwollaTemplateFilePath = Path.Combine(tmpDirAbsolutePath, "dwollapoc.html");
            var tmpFileName = "dwollapoc-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            var dwollaProcessedFilePath = Path.Combine(tmpDirAbsolutePath, tmpFileName);
            Console.WriteLine("Template path: "+dwollaTemplateFilePath);
            Console.WriteLine("ProcessedFile: "+dwollaProcessedFilePath);
            ProcessDwollaTemplateFile(dwollaTemplateFilePath, dwollaProcessedFilePath, plaidPublicKey);
            return dwollaProcessedFilePath;
        }

        public static void ProcessDwollaTemplateFile(string templateFile, string processedFile, string plaidPublicKey)
        {
            string line;
            var reader = new StreamReader(templateFile);
            var writer = new StreamWriter(processedFile);

            //Continue to read until you reach end of file
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("key:"))
                {
                    line = line.Replace("<plaidPublicKey>", plaidPublicKey);
                }

                writer.WriteLine(line);
            }

            reader.Close();
            writer.Close();
        }

        public Dictionary<string,object> LoginToBankAndExtractAchAccountInformation(string plaidPublicToken)
        {
            Environment.SetEnvironmentVariable("DISPLAY", ":1");
            LoadPlaidScreen(plaidPublicToken);
            ClickLinkAccount();
            SwitchToIframe();
            ClickGetStarted();
            SelectBank();
            LoginToBank();
            SelectAccountAndContinue();
            var token = ExtractPlaidPublicToken();
            var accountId = ExtractPlaidAccountId();
            var accountsList = new List<string>();
            accountsList.Add(accountId);
            var request = new Dictionary<string, object>();
            request.Add("AccessToken", token);
            request.Add("ExternalAccountIds", accountsList);
            Console.WriteLine("Request: "+request.ToString());
            return request;
        }

        private static void CleanDwollaHtmlFilesInTmp()
        {
            var tmpDirPath = "../../../../Minority.InternalTesting.RegressionTest.Contract/ACH/templates";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                tmpDirPath = _linuxPodDwollaHtmlPath;
            }

            var d = new DirectoryInfo(tmpDirPath); //Assuming Test is your Folder
            var files = d.GetFiles("dwollapoc-*.html"); //Getting Text files

            foreach (var file in files)
            {
                var filepath = Path.Join(file.DirectoryName, file.Name);
                File.SetAttributes(filepath, FileAttributes.Normal);
                File.Delete(filepath);
            }
        }


        private static void SetPlaidTokenInDwollaHtml(string dwollaHtmlPath, string plaidPublicToken)
        {
            var fileContent = File.ReadAllText(dwollaHtmlPath);
            fileContent = fileContent.Replace("<plaidPublicKey>", plaidPublicToken);
            File.WriteAllText(dwollaHtmlPath, fileContent);
        }


        private static void Sleep(int seconds) => Thread.Sleep(seconds * 1000);

        private void ClickGetStarted()
        {
            Sleep(3);
            IReadOnlyCollection<IWebElement> butts = _driver.FindElements(By.TagName("button"));
            int i = 0;
            foreach (var butt in butts)
            {
                Console.WriteLine("Button text of elm for button  "+i+++" is "+butt.Text);
            }
            IWebElement getStartedBtn = _driver.FindElement(By.XPath(DwollaLocators.GET_STARTED_BTN_XPATH));
            getStartedBtn.Click();

        }

        private void ClickLinkAccount()
        {
            var linkAccountBtn = UiSetup.WaitForElement(_driver, DwollaLocators.LINK_ACCOUNT_XPATH);
            Console.WriteLine("Link Account Btn Displayed: "+linkAccountBtn.Displayed);
            linkAccountBtn.Click();
            Console.WriteLine("Link Account Btn Clicked: " + linkAccountBtn.Displayed);

        }

        private string ExtractPlaidAccountId()
        {
            _driver.SwitchTo().DefaultContent();
            Sleep(2);
            var account = UiSetup.WaitForElement(_driver, DwollaLocators.PLAID_ACCOUNTID).Text;
            _driver.Close();
            return account;
        }

        private string ExtractPlaidPublicToken()
        {
            _driver.SwitchTo().DefaultContent();
            Sleep(2);
            var token = UiSetup.WaitForElement(_driver, DwollaLocators.PLAID_TOKEN_XPATH).Text;
            return token;
        }


        private void LoadPlaidScreen(string plaidPublicToken)
        {
            var dwollaFilePath = InitializeDwollaUi(plaidPublicToken);
            dwollaFilePath = "file://" + dwollaFilePath;
            _driver = UiSetup.InitDriverAndOpenWebPage(dwollaFilePath);
            Sleep(2);
        }

        private void LoginToBank()
        {
            UiSetup.WaitForElement(_driver, DwollaLocators.BANK_USERNAME_TEXT_XPATH).SendKeys(DwollaLocators.USERNAME);
            UiSetup.WaitForElement(_driver, DwollaLocators.BANK_PASSWORD_TEXT_XPATH).SendKeys(DwollaLocators.PASSWORD);
            UiSetup.WaitForElement(_driver, DwollaLocators.BANK_SUBMIT_BTN_XPATH).Click();
        }


        private void SelectAccountAndContinue()
        {
            UiSetup.WaitForElement(_driver, DwollaLocators.BANK_SELECT_ACCOUNT_TILE_XPATH).Click();
            UiSetup.WaitForElement(_driver, DwollaLocators.SELECTACCOUNT_CONTINUE_XPATH).Click();
        }

        private void SelectBank() => UiSetup.WaitForElement(_driver, DwollaLocators.BANK_TILE_XPATH).Click();

        private void SwitchToIframe()
        {
            Sleep(3);
            IWebElement iFrame = _driver.FindElement(By.Id(DwollaLocators.BANK_IFRAME_XPATH));
            _driver.SwitchTo().Frame(iFrame);
            Sleep(5);
        }
    }
}