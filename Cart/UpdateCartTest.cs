using NUnit.Framework;
using OpenQA.Selenium;

namespace lawDepoTest{
    [TestFixture]
    public class UpdateCartTest : GeneralCommands{
        //keep count of items in the cart
        public static int itemCount = 0;

        /*
        Add items to cart
        Parametes: 
            string id to specify the item 
        */
        [Test]
        public void AddToCart(){
            FindMe("add-to-cart-sauce-labs-bike-light");
            FindMe("add-to-cart-sauce-labs-onesie");
        }

        /*
        Remove items to cart
        Parametes: 
            string id to specify the item 
        */
        [Test]
        public void RemoveFromCart(){
            FindMe("remove-sauce-labs-bike-light");
        }

        /*
        Validates the amount of items in the cart matches, items added and removed from cart
        Parametes: 
            string id to specify the item 
        */
        [Test]
        public void ValidateCart(){
            itemCount = GetCartCount();
            Console.WriteLine("amount of items in cart: " + itemCount);
        }

    }
}