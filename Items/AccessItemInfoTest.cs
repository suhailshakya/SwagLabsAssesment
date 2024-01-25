using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace lawDepoTest{
    [TestFixture]
    public class AccessItemInfoTest : GeneralCommands{
        
        /*
        Access item page 
        Parametes: 
            Xpath for the item is specified and stored as string item
        Validates the Xpath is found and clickable
        Compares the item number to the number added to the URL after navigation
        */
        [Test]
        public void ItemInfo(){
            string item = "//*[@id='item_0_title_link']";
            FindMe(item);
            AssertURL(item.Substring(5, 1));
        }

        /*
        Navigates back to inventory page
        */
        public void BackToMainPage(){
            FindMe("back-to-products");
        }

    }
}