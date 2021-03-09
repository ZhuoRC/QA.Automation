using System.Collections.Generic;
using Xunit;

namespace Testing.Infrastructure.UnitTest
{
    public class UnitTest_FileServices
    {
        [Fact]
        public void LoadJson()
        {
            //arranage
            List<TestClass> items = new List<TestClass>();
            items.Add(new TestClass(1, "test element 001"));
            items.Add(new TestClass(2, "test element 002"));

            string jsonPath = "UnitTest_FileService_TestClass.json";

            FileService.SaveJson<TestClass>(items, jsonPath);

            //action
            List<TestClass> result = FileService.LoadJson<TestClass>(jsonPath);

            //assert
            Assert.Equal(items.Count, result.Count);
            Assert.Equal(items[1].Text, result[1].Text);

        }
    }

    public class TestClass
    {
        public TestClass(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }

        public int Id { get; set; }
        public string Text { get; set; }
    }
}
