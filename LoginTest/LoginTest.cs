using NUnit.Framework;
using OpenQA.Selenium;

namespace lawDepoTest{
    [TestFixture]
    public class LoginTest : GeneralCommands{

        [Test]
        public void LoginValid(List<string> credList){
            //pick random creds from list
            Random rand = new Random();
            int rand1 = rand.Next(1, credList.Count());
            string userCreds = credList[rand1];

            //check for empty username and password input field
            InputValue(By.Name("user-name"), userCreds);
            InputValue(By.Name("password"), "secret_sauce");

            //click login
            FindMe("login-button");
            AssertURL("inventory");
        }

        [Test]
        public void LoginInValid(){
            InputValue(By.Name("user-name"), "invalidUser");
            InputValue(By.Name("password"), "invalidPass");
            FindMe("login-button");
        }

    }
}