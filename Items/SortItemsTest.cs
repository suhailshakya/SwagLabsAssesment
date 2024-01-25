using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace lawDepoTest{
    [TestFixture]
    public class SortItemsTest : GeneralCommands{

        /*
        iterates through all sort buttons
        parametes: 
            main sort button looks for id: select_container 
            sort option uses Xpath with identifier from 1 to 4
        throws exception if there is an issue with any button
        */
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