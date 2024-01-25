using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace lawDepoTest{
   public class Program {
      static void Main(){
         LoginTest loginTest = new LoginTest();
         GeneralCommands gc = new GeneralCommands();
         UpdateCartTest upCTest = new UpdateCartTest();
         CartCheckoutTest chCTest = new CartCheckoutTest();
         AccessItemInfoTest acItTest = new AccessItemInfoTest();
         SortItemsTest sort = new SortItemsTest();

         gc.AccessSite();
         var listCreds = gc.GetCredList();
         //run login with test with credentials list
         loginTest.LoginValid(listCreds);

         //test all 4 sorts
         sort.AllSort();

         //access item info
         acItTest.ItemInfo();
         acItTest.BackToMainPage();

         //add item to cart
         upCTest.AddToCart();
         upCTest.ValidateCart();
         //upCt.RemoveFromCart();
         //upCt.ValidateCart();

         //go to cart, checkout and pay
         chCTest.AccessCart();
         chCTest.Checkout();
         chCTest.ValidInfoPay();

         gc.CloseSite();
      }
   }
}