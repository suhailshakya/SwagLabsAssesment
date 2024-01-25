using NUnit.Framework;
using OpenQA.Selenium;

namespace lawDepoTest{
    [TestFixture]
    public class CartCheckoutTest : GeneralCommands{
    
        /*
        Navigate to cart
        Validate correct URL is reached
        Validate the amount of items in the cart
        Parameters:
            XPath for cart
            cart is contained in URL
            XPath for cart contents and number of current items in cart
        */
        [Test]
        public void AccessCart(){
            FindMe("shopping_cart_link");
            AssertURL("cart");
            AssertSelf("cart_contents_container", itemsInCart.ToString());
        }

        /*
        Navigate to checkout from cart
        Validate correct URL is reached        
        Parameters:
            ID for checkout
            checkout is contained in URL
        */
        [Test]
        public void Checkout(){
            FindMe("checkout");
            AssertURL("checkout-step-one");
        }

        /*
        Validate correct URL is reached        
        Parameters:
            ID for firstname, lastname, postal-code
            checkout is contained in URL
        */
        [Test]
        public void ValidInfoPay(){
            InputValue(By.Id("first-name"), "Law");
            InputValue(By.Name("lastName"), "Depot");
            InputValue(By.Id("postal-code"), "M5G1C6");
            FindMe("continue");
            AssertURL("checkout-step-two");
            FindMe("finish");
            AssertURL("checkout-complete");
        }

        /*
        Throws exception when empty field is sent as parameter
        Validate correct URL is not reached       
        Parameters:
            empty parameters
        */
        [Test]
        public void InvalidInfoPay(){
            InputValue(By.Name("firstname"), "");
            InputValue(By.Name("lastname"), "");
            InputValue(By.Name("postalcode"), "");
            FindMe("");
        }

    }
}