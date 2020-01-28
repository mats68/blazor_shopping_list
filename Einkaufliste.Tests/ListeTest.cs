using Einkaufsliste;
using System;
using System.Linq;
using Xunit;

namespace Einkaufliste.Tests
{
    public class ListeTest
    {
        public class ListeTest1
        {
            public EinkaufServiceTest einkaufServiceTest { get; set; }

            public ListeTest1()
            {
                Init();
            }

            internal async void Init()
            {
                einkaufServiceTest = new EinkaufServiceTest();
                await einkaufServiceTest.GetList();
            }

            [Fact]
            public void AddItem()
            {
                Assert.Equal(3, einkaufServiceTest.List.Count());
                einkaufServiceTest.AddEinkauf(new Einkauf() { Name = "Banane" });
                Assert.Equal(4, einkaufServiceTest.List.Count());
            }
        }
    }
}
