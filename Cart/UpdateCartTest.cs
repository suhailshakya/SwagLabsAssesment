using NUnit.Framework;
using OpenQA.Selenium;

namespace lawDepoTest{
    [TestFixture]
    public class UpdateCartTest : GeneralCommands{
        //count items in cart
        public static int itemCount = 0;

        [Test]
        public void AddToCart(){
            //get count of cart, 0
            FindMe("add-to-cart-sauce-labs-bike-light");
            FindMe("add-to-cart-sauce-labs-onesie");
            //check for +2 difference in cart, AssertSelf
        }

        [Test]
        public void RemoveFromCart(){
            //get count of cart
            FindMe("remove-sauce-labs-bike-light");
            //check for -1 difference in cart, AssertSelf
        }

        [Test]
        public void ValidateCart(){
            itemCount = GetCartCount();
            Console.WriteLine("amount of items in cart: " + itemCount);
        }

    }
}