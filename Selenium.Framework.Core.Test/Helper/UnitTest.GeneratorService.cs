using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Framework.Core.Helper;
using System.Collections.Generic;
using System.Threading;

namespace Selenium.Framework.Core.Test
{
    [TestClass]
    public class UnitTestGenerator
    {


        [TestInitialize()]
        public void Startup()
        {
         
        }

        [TestCleanup()]
        public void Cleanup()
        {
        }


        [TestMethod]
        public void HashGenerator_NoDuplicateRecords()
        {
            //arranage
            Dictionary<string, int> result = new Dictionary<string, int>();

            //action
            for (int i = 0; i < 1000; i++)
            {
                //assert
                result.Add(GeneratorService.GetMD5Hash(), i);
            } 


           
        }

    }
}

