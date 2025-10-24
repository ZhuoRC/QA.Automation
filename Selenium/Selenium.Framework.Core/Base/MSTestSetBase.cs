using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Selenium.Framework.Core
{
    public class MSTestSetBase
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void BaseInitialize()
        {
            //ConsoleLogger.Log("here is the mstest base set up");
        }

        [TestCleanup]
        public void BaseCleanup()
        {
            //ConsoleLogger.Log("here is the mstest base tear down");
        }
    }
}
