using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace lawDepoTest{
   public class Program {
      static void Main(){
         LoginTest loginTest = new LoginTest();
         GeneralCommands gc = new GeneralCommands();
         UpdateCartTest upCt = new UpdateCartTest();
         CartCheckoutTest acCt = new CartCheckoutTest();

         gc.AccessSite();
         var listCreds = gc.GetCredList();
         //run login with test with credentials list
         loginTest.LoginValid(listCreds);
         upCt.AddToCart();
         upCt.ValidateCart();
         //upCt.RemoveFromCart();
         //upCt.ValidateCart();
         acCt.AccessCart();
         acCt.Checkout();
         acCt.ValidInfoPay();

         gc.CloseSite();
         
      }
   }
}