using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Testing.Infrastructure.UnitTest
{
    public class UnitTest_GeneratorServices
    {

        private readonly ITestOutputHelper _output;

        public UnitTest_GeneratorServices(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("here is test startup");
        }
        [Fact]
        public void HashGenerator_NoDuplicateRecords()
        {
            //arranage
            Dictionary<string, int> result = new Dictionary<string, int>();

            //action
            for (int i = 0; i < 1000; i++)
            {
                string md5Hash = GeneratorService.GetMD5Hash(5);
                //_output.WriteLine($"{i.ToString()}, {md5Hash}");
                //assert
                result.Add(md5Hash, i);
            }
        }
    }
}
