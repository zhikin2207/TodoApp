using Moq;
using NUnit.Framework;
using System;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.CustomRepositories;
using ToDo.Services.Handlers;

namespace ToDo.Tests
{
    [TestFixture]
    public class Testing
    {
        [Test]
        public void GetAdultItems_FourDefaultValues_2ElementsWereReturned()
        {
            var mockRepository = new Mock<IItemRepository>();
            mockRepository
                .Setup(x => x.GetAll())
                .Returns(new[]
                {
                    new Item {
                        Title = "Item1",
                        Description = "xxx",
                        Priority = Priority.High,
                        Category = new Category { Name = "Cat1", Parent = null },
                        DueDate = DateTime.Now, Status = false },
                    new Item {
                        Title = "adult",
                        Description = "Text",
                        Priority = Priority.Low,
                        Category = new Category { Name = "Cat1", Parent = null },
                        DueDate = DateTime.Now, Status = true },
                    new Item {
                        Title = "Iasdxxxa",
                        Description = "Text",
                        Priority = Priority.Medium,
                        Category = new Category { Name = "adult", Parent = null },
                        DueDate = DateTime.Now,
                        Status = false },
                    new Item {
                        Title = "Item4",
                        Description = "Text",
                        Priority = Priority.SuperLow,
                        Category = new Category { Name = "adult", Parent = null },
                        DueDate = DateTime.Now,
                        Status = false }
                 });
            
            var elementsCount = new ItemHandler(mockRepository.Object)
                .GetAdultItems()
                .items;
                //.Count;
            Assert.AreEqual(2, elementsCount);
        }
    }
}
