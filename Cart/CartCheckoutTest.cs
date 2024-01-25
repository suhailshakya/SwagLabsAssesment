using NUnit.Framework;
using OpenQA.Selenium;

namespace lawDepoTest{
    [TestFixture]
    public class CartCheckoutTest : GeneralCommands{
    
        [Test]
        public void AccessCart(){
            //access cart
            FindMe("shopping_cart_link");
            //wait for all elements and fields to load
            AssertURL("cart");
            //get list of items in cart and compare to count from updateCart
            AssertSelf("cart_contents_container", itemsInCart.ToString());
        }

        [Test]
        public void Checkout(){
            //checkout from cart
            FindMe("checkout");
            //wait for all elements and fields to load
            AssertURL("checkout-step-one");
        }

        [Test]
        public void ValidInfoPay(){
            //input into and final pay
            InputValue(By.Id("first-name"), "Law");
            InputValue(By.Name("lastName"), "Depot");
            InputValue(By.Id("postal-code"), "M5G1C6");
            FindMe("continue");
            //wait for all elements and fields to load
            AssertURL("checkout-step-two");
            //verify total, AssertSelf()
            FindMe("finish");
            AssertURL("checkout-complete");
        }

        [Test]
        public void InvalidInfoPay(){
            //input into and final pay
            InputValue(By.Name("firstname"), "");
            InputValue(By.Name("lastname"), "");
            InputValue(By.Name("postalcode"), "");
            FindMe("continue");
        }

    }
}