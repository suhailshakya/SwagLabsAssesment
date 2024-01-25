using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace lawDepoTest{
    [TestFixture]
    public class SortItemsTest : GeneralCommands{
        [Test]
        public void AllSort(){
            try{
                for (int i = 1; i <= 4; i++){
                    FindMe("select_container");
                    FindMe($"//*[@id='header_container']/div[2]/div/span/select/option[{i}]");
                }
            }
            catch (Exception ex){
                Console.WriteLine("Error : " + ex.Message);
                throw;
            }
        }

    }
}