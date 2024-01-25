using NUnit.Framework;
using OpenQA.Selenium;

namespace lawDepoTest{
    [TestFixture]
    public class LoginTest : GeneralCommands{

        /*
        Generates random number between 1 to 6 and obtains a userID
        Parametes: 
            userCreds used as a username
            password is harded to secret_sauce
        Uses userID and password to login and validate successful login
        */
        [Test]
        public void LoginValid(List<string> credList){
            Random rand = new Random();
            int rand1 = rand.Next(1, credList.Count());
            string userCreds = credList[rand1];

            InputValue(By.Name("user-name"), userCreds);
            InputValue(By.Name("password"), "secret_sauce");

            FindMe("login-button");
            AssertURL("inventory");
        }

        /*
        Parametes: 
            userCreds used as a invalidUser
            password is harded to invalidPass
        Uses userCreds and password to login and validate unsuccessful login
        */
        [Test]
        public void LoginInValid(){
            InputValue(By.Name("user-name"), "invalidUser");
            InputValue(By.Name("password"), "invalidPass");
            FindMe("login-button");
        }

    }
}