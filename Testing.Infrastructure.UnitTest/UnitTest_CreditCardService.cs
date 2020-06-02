using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Testing.Infrastructure.UnitTest
{
    public class UnitTest_CreditCardService 
    {
        private readonly ITestOutputHelper _output;

        public UnitTest_CreditCardService(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("here is test startup");
        }
       
        [Theory]
        [InlineData("1234")]
        [InlineData("12345678")]
        [InlineData("123456789012")]
        [InlineData("1234567890123456")]
        public void MaskedPAN(string pan)
        {
            //arrange

            //act 
            string maskedPAN = CreditCardService.MaskPAN(pan);
            _output.WriteLine($"{pan} ==> {maskedPAN}");
            //assert
            switch (pan.Length)
            {
                case int n when n<=4:
                    Assert.Equal(pan, maskedPAN);
                    break;
                case int n when n <= 8:
                    Assert.Equal(4, maskedPAN.Count(c => c == '*'));
                    Assert.Equal("****", maskedPAN.Substring(0,4));
                    break;
                case int n when n > 8:
                    Assert.Equal(pan.Length-8, maskedPAN.Count(c => c == '*'));
                    Assert.NotEqual("****", maskedPAN.Substring(0, 4));
                    Assert.Equal(pan.Substring(pan.Length-4), maskedPAN.Substring(maskedPAN.Length-4));
                    break;
                default:
                    break;
            }

        }


    }
}
