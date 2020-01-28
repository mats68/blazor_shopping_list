using Einkaufsliste;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            public async Task AddItem()
            {
                var count = einkaufServiceTest.List.Count();
                await einkaufServiceTest.AddEinkauf(new Einkauf() { Name = "Banane" });
                Assert.Equal(count+1, einkaufServiceTest.List.Count());
                Assert.Equal("Banane", einkaufServiceTest.CurrentItem.Name);
            }

            [Fact]
            public async Task ToggleItem()
            {
                var item = einkaufServiceTest.List[1];
                Assert.Equal("Brot", item.Name);
                Assert.False(item.IsDone);
                einkaufServiceTest.CurrentItem = item;
                await einkaufServiceTest.ToggleIsDone(item);
                Assert.True(item.IsDone);
            }

            [Fact]
            public async Task AddEmptyItemNoAppend()
            {
                var count = einkaufServiceTest.List.Count();
                await einkaufServiceTest.AddEinkauf(new Einkauf() { Name = "" });
                Assert.Equal(count, einkaufServiceTest.List.Count());
            }

            [Fact]
            public async Task DeleteItem()
            {
                var count = einkaufServiceTest.List.Count();
                var item = einkaufServiceTest.List[1];
                einkaufServiceTest.CurrentItem = item;
                await einkaufServiceTest.DeleteEinkauf();
                Assert.Equal(count-1, einkaufServiceTest.List.Count());
                Assert.Null(einkaufServiceTest.CurrentItem);
            }

        }
    }
}
