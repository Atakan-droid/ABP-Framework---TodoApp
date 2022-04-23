using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TodoApp
{
    [Collection(TodoAppTestConsts.CollectionDefinitionName)]
    public class TodoAppService_Tests:TodoAppApplicationTestBase
    {
        [Fact]
        public async Task Full_Test()
        {
            var service =  GetRequiredService<ITodoAppService>();

            var items=await service.GetListAsync();
            //Assert.Equal(items.Count, 0);
            items.ShouldBeEmpty();

            await service.CreateAsync("my test 1");
            items = await service.GetListAsync();
            items.ShouldNotBeEmpty();
            items.First().Text.ShouldBe("my test 2");


            await service.DeleteAsync(items.First().Id);

            items = await service.GetListAsync();
            items.ShouldBeEmpty();

        }
    }
}
