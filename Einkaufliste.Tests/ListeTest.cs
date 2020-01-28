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
                var count = einkaufServiceTest.List.Count();
                einkaufServiceTest.AddEinkauf(new Einkauf() { Name = "Banane" });
                Assert.Equal(count+1, einkaufServiceTest.List.Count());
            }

            [Fact]
            public void AddEmptyItemNoAppend()
            {
                var count = einkaufServiceTest.List.Count();
                einkaufServiceTest.AddEinkauf(new Einkauf() { Name = "" });
                Assert.Equal(count, einkaufServiceTest.List.Count());
            }

        }
    }
}
