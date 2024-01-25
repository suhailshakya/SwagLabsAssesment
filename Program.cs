using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace lawDepoTest{
   public class Program {
      static void Main(){
         //objects to access class methods
         LoginTest loginTest = new LoginTest();
         GeneralCommands gc = new GeneralCommands();
         UpdateCartTest upCTest = new UpdateCartTest();
         CartCheckoutTest chCTest = new CartCheckoutTest();
         AccessItemInfoTest acItTest = new AccessItemInfoTest();
         SortItemsTest sort = new SortItemsTest();

         gc.AccessSite();
         var listCreds = gc.GetCredList();
         loginTest.LoginValid(listCreds);

         sort.AllSort();

         acItTest.ItemInfo();
         acItTest.BackToMainPage();

         upCTest.AddToCart();
         upCTest.ValidateCart();
         //upCt.RemoveFromCart();
         //upCt.ValidateCart();

         chCTest.AccessCart();
         chCTest.Checkout();
         chCTest.ValidInfoPay();

         gc.CloseSite();
      }
   }
}