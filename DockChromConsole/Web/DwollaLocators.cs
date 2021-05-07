namespace DockChromConsole.Web
{
    public class DwollaLocators
    {
        public static string accountId;
        public static string BANK_IFRAME_XPATH = "plaid-link-iframe-1";
        public static string BANK_PASSWORD_TEXT_XPATH = "//*[@id=\"password\"]";
        public static string BANK_SELECT_ACCOUNT_TILE_XPATH = "//*[@id=\"plaid-link-container\"]/div/div[1]/div/div[2]/div[3]/div[1]/div[1]";
        public static string BANK_SUBMIT_BTN_XPATH = "//*[@id=\"plaid-link-container\"]/div/div[1]/div/div[2]/div[3]/div/form/button";
        public static string BANK_TILE_XPATH = "//*[@id=\"plaid-link-container\"]/div/div[1]/div/div/div[2]/div[2]/div/li[3]";
        public static string BANK_TILE_XPATH_BOA = "//*[@id=\"plaid-link-container\"]/div/div[1]/div/div/div[2]/div[2]/div/li[2]";
        public static string BANK_TILE_XPATH_WELLSFARGO = "//*[@id=\"plaid-link-container\"]/div/div[1]/div/div/div[2]/div[2]/div/li[1]";


        public static string BANK_USERNAME_TEXT_XPATH = "//*[@id=\"username\"]";
        public static string GET_STARTED_BTN_XPATH = "/html/body/div/div/div[1]/div/div/div[2]/div[2]/div/button[1]";
        public static string LINK_ACCOUNT_XPATH = "//*[@id=\"link-button\"]";
        public static string PASSWORD = "pass_good";
        public static string PLAID_ACCOUNTID = "/html/body/div/main/div[3]/div[4]";
        public static string PLAID_TOKEN_XPATH = "/html/body/div/main/div[3]/div[3]";
        public static string publicToken;

        public static string SELECTACCOUNT_CONTINUE_XPATH = "//*[@id=\"plaid-link-container\"]/div/div[1]/div/div[2]/div[3]/div[2]/button";
        public static string USERNAME = "user_good";
    }
}