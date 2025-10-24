using NUnit.Framework;

namespace Selenium.Framework.Core
{
    public class NUnitTestSetBase
    {
        [SetUp]
        public void BaseSetUp()
        {
            //ConsoleLogger.Log("here is the nunit base set up");
        }

        [TearDown]
        public void BaseTearDown()
        {
            //ConsoleLogger.Log("here is the nunit base tear down");
        }
    }
}
