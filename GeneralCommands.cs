using Microsoft.VisualBasic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace lawDepoTest{
    public class GeneralCommands{
        public static IWebDriver mainDriver = new ChromeDriver();
        public static int itemsInCart;
        WebDriverWait wait = new WebDriverWait(mainDriver, TimeSpan.FromSeconds(3));

        //Naviate to site, hard-coded value
        public void AccessSite(){
            mainDriver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        //Close site and exit program
        public void CloseSite(){
            mainDriver.Quit();
        }

        /*
        Locates element for list of usernames
        Validates element is not null
        Splits the list of credentials by new lines and stores in array
        Parameters:
            login_credentials to specify credentials element ID
        Returns list of credentials 
        or
        Throws exception if list of credentials are null
        */
        public List<string> GetCredList(){
            List<string> credList = new List<string>();
            try {
                IWebElement creds = mainDriver.FindElement(By.Id("login_credentials"));
                if (creds != null){
                    string lgCreds = creds.Text;
                    credList = lgCreds.Split(new[] { '\r', '\n' }).ToList();
                }
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException("login creds null");
            }
            return credList;
        }

        /*
        Locates element based on parameter type
        Calls ClickMe
        Parameter: 
            element id, Xpath or classname
        Throws exception if locator is null
        */
        public void FindMe(string locator){
            IWebElement button;
            Thread.Sleep(TimeSpan.FromSeconds(3));
            try {
                if (locator.Contains("item") || locator.Contains("header")){
                    button = mainDriver.FindElement(By.XPath(locator));
                }
                else if (locator.Contains("link") || locator.Contains("_container")){
                    button = mainDriver.FindElement(By.ClassName(locator));
                }
                else{
                    button = mainDriver.FindElement(By.Id(locator));
                }
                ClickMe(button);
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException($"Element with XPath or ID '{locator}' not found");
            }
        }

        /*
        Locates element from given param
        Parameter: 
            WebElement, already specified from FindMe
        Throws exception if button does not trigger any change, validated through statecheck 
        */
        public void ClickMe(IWebElement button){
            try {
                button.Click();
                wait.Until(mainDriver => ((IJavaScriptExecutor)mainDriver).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (ElementClickInterceptedException) {
                throw new WebDriverException("button did not trigger any changes");
            }
        }

        /*
        Locates element based on locator already specified
        Throws exceptions for null, empty or lock_out_user
        Updates input field with value and validates if input field is changed
        Parameter: 
            element id, Xpath or classname
            value to compare to 
        Throws exceptions if element not found, input field is not updated
        */
        public void InputValue(By locator, string value){
            try {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                if (string.IsNullOrEmpty(value)) {
                    throw new WebDriverArgumentException("Empty or null values are not allowed");
                }
                else if (value == "locked_out_user"){
                    throw new WebDriverArgumentException("Locked out user login");
                }

                IWebElement entry = mainDriver.FindElement(locator);
                if (entry.Enabled) {
                    entry.SendKeys(value);
                    string updatedString = entry.GetAttribute("value");
                    if (updatedString != value){
                        throw new WebDriverException($"{locator} input was not updated");
                    }
                }
                else {
                    throw new WebDriverException("The text field is not enabled for typing.");
                }
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException("Field element not found");
            }
        }

        /*
        Compares 2 string values 
        Parameter: 
            value1 and value2 strings
        Throws exceptions if element not equal or not found
        */
        public void AssertSelf(string value1, string value2){
            string cartItems = "";
            try {
                if (value1.Contains("contents")){
                    cartItems = FinalCartCount(value1).ToString();
                    Assert.That(cartItems, Is.EqualTo(value2), "strings are equal");
                }
                else{
                    Assert.That(mainDriver.FindElement(By.Name(value1)).Text, Is.EqualTo(value2), "strings are equal");
                }
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException("Elements not equal");
            }
        }

        /*
        Validates string in url
        Parameter: 
            value string
        Throws exceptions if url does not contain value or not found
        */
        public void AssertURL(string urlVal){
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(urlVal));
            Assert.That(mainDriver.Url, Does.Contain(urlVal), "URL does not contain urlVal after login");
        }
        
        /*
        Locates element, retrives value, converts to int and compares to items
        Throws exceptions if the element is not found
        */
        public int GetCartCount(){
            By spanLocator = By.ClassName("shopping_cart_badge");
            try{
                IWebElement spanElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(spanLocator));
                return itemsInCart = int.Parse(spanElement.Text); 
            }
            catch (NoSuchElementException) {
                return itemsInCart = 0;
            }
        }

        public int FinalCartCount(string path){
            try{
                IWebElement divList = mainDriver.FindElement(By.XPath($"//*[@id='{path}']"));
                int numberOfItems = divList.FindElements(By.ClassName("cart_item")).Count();
                return numberOfItems;
            }
            catch (NoSuchElementException) {
                return 0;
            }
        }


    }
}