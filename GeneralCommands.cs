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
        public static double totalCartPrice = 0.0;
        WebDriverWait wait = new WebDriverWait(mainDriver, TimeSpan.FromSeconds(3));

        public void AccessSite(){
            mainDriver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        public void CloseSite(){
            mainDriver.Quit();
        }

        public List<string> GetCredList(){
            List<string> credList = new List<string>();
            //get list of creds
            try {
                IWebElement creds = mainDriver.FindElement(By.Id("login_credentials"));
                if (creds != null){
                    //split all creds in div and store in list
                    string lgCreds = creds.Text;
                    credList = lgCreds.Split(new[] { '\r', '\n' }).ToList();
                }
            }
            //catch if credentials are null
            catch (NoSuchElementException) {
                throw new NoSuchElementException("login creds null");
            }
            return credList;
        }

        public void FindMe(string locator){
            IWebElement button;
            Thread.Sleep(TimeSpan.FromSeconds(3));
            try {
                //if the string value is looking for an XPath
                if (locator.Contains("item") || locator.Contains("header")){
                    button = mainDriver.FindElement(By.XPath(locator));
                }
                else if (locator.Contains("link") || locator.Contains("_container")){
                    button = mainDriver.FindElement(By.ClassName(locator));
                }
                else{
                    //if the string is looking for an id
                    button = mainDriver.FindElement(By.Id(locator));
                }
                ClickMe(button);
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException($"Element with XPath or ID '{locator}' not found");
            }
        }

        public void ClickMe(IWebElement button){
            try {
                button.Click();
                //validate there was changes on the page after the click
                wait.Until(mainDriver => ((IJavaScriptExecutor)mainDriver).ExecuteScript("return document.readyState").Equals("complete"));
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(button));
            }
            catch (ElementClickInterceptedException) {
                throw new WebDriverException("button did not trigger any changes");
            }
        }

        //validate field exists
        //throw exception if element null
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

        //validates 2 elements equality
        public void AssertSelf(string value1, string value2){
            string cartItems = "";
            try {
                //validating cart items
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

        public void AssertURL(string urlVal){
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(urlVal));
            Assert.That(mainDriver.Url, Does.Contain(urlVal), "URL does not contain urlVal after login");
        }

        //get the count of items in cart
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

        //count items in cart in checkout
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

        public void DropDownMenu(string button1, string button2){
            FindMe(button1);
            FindMe(button2);
        }


    }
}