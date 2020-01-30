using Einkaufsliste;
using Einkaufsliste.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Einkaufliste.Tests
{
    public class ListeTest
    {
        public class ListeTest1
        {
            public EinkaufService einkaufServiceTest { get; set; }

            public ListeTest1()
            {
                // await Init();
                Init();
                //Task.Run(() => Init());
            }

            internal void Init()
            {
                LocalStorageServiceFake localStorageServiceFake = new LocalStorageServiceFake();
                einkaufServiceTest = new EinkaufService(localStorageServiceFake);
            }

            async Task AddItems()
            {
                await einkaufServiceTest.AddEinkauf(new Einkauf() { Id = 1, Name = "Radieschen" });
                await einkaufServiceTest.AddEinkauf(new Einkauf() { Id = 2, Name = "Brot" });
                await einkaufServiceTest.AddEinkauf(new Einkauf() { Id = 3, Name = "Käse" });
            }

            [Fact]
            public async Task AddItem()
            {
                await AddItems();
                var count = einkaufServiceTest.List.Count();
                await einkaufServiceTest.AddEinkauf(new Einkauf() { Name = "Banane" });
                Assert.Equal(count + 1, einkaufServiceTest.List.Count());
                Assert.Equal("Banane", einkaufServiceTest.CurrentItem.Name);
                Assert.Equal(4, einkaufServiceTest.List[3].Id);
            }

            [Fact]
            public async Task ToggleItem()
            {
                await AddItems();
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
                await AddItems();
                var count = einkaufServiceTest.List.Count();
                await einkaufServiceTest.AddEinkauf(new Einkauf() { Name = "" });
                Assert.Equal(count, einkaufServiceTest.List.Count());
            }

            [Fact]
            public async Task DeleteItem()
            {
                await AddItems();
                var count = einkaufServiceTest.List.Count();
                var item = einkaufServiceTest.List[1];
                einkaufServiceTest.CurrentItem = item;
                await einkaufServiceTest.DeleteEinkauf();
                Assert.Equal(count - 1, einkaufServiceTest.List.Count());
                Assert.Null(einkaufServiceTest.CurrentItem);
            }

        }
    }
}
