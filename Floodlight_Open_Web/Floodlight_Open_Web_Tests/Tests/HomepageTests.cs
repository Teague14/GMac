using System;
using Floodlight_Open_Web.Helpers;
using Floodlight_Open_Web_Tests.Pages;
using NUnit.Framework;

namespace Floodlight_Open_Web_Tests
{
    [TestFixture]
    public class HomepageTests:BaseClass
    {
        [SetUp]
        public void FixtureSetup()
        {
            Init(BrowserType.Chrome);
        }

        [TearDown]
        public void FixtureTearDown()
        {
            Close();
        }

        [Test]
        public void HomepageClickJoin()
        {
            HomePage homePage = new HomePage();
            JoinPage joinPage = homePage.ClickJoinButton();
            joinPage.ClickContinueButton();
        }
    }
}
