using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace lawDepoTest{
    [TestFixture]
    public class AccessItemInfoTest : GeneralCommands{
        [Test]
        public void ItemInfo(){
            string item = "item_0_title_link";
            //get the item name
            FindMe(item);
            //compare URL has the correct item number --> correct image and name
            AssertURL(item.Substring(5, 1));
            //compare the previously attained item name and compare to the item name on the current page
            //AssertSelf();
        }

        public void BackToMainPage(){
            FindMe("back-to-products");
        }

    }
}