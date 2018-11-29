using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.CustomRepositories;
using ToDo.Services.Configuration;
using ToDo.Services.DTOs;
using ToDo.Services.Handlers;

namespace ToDo.Tests
{
    [TestFixture]
    public class Testing
    {
        public Mock<IItemRepository> mockRepository;
        IMapper _mapper;

        public Testing()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingServicesProfile>();
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            mockRepository = new Mock<IItemRepository>();

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
                        Category = new Category { Name = "Cat2", Parent = null },
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
        }

        [Test]
        public void GetAdultItems_FourDefaultValues_2ElementsWereReturned()
        { 
            var elementsCount = new ItemHandler(mockRepository.Object, _mapper)
                .GetAdultItems()
                .Items.Count();

            Assert.AreEqual(2, elementsCount);
        }

        [Test]
        public void GetAdultItems_FourDefaultValues_StatusIsFalse()
        {
            var itemStatus = new ItemHandler(mockRepository.Object, _mapper)
                .GetAdultItems()
                .Items.FirstOrDefault().Status;

            Assert.AreEqual(false, itemStatus);
        }

        [Test]
        public void GetAdultItems_FourDefaultValues_ValuesMatchToRequirements()
        {
            var item = new ItemHandler(mockRepository.Object, _mapper)
                .GetAdultItems()
                .Items.FirstOrDefault();
            Func<ItemDTO,  bool> IsMatchig =(i => (i.Title.Contains("xxx")
                                || i.Title.Contains("adult")
                                || string.Equals(
                                   i.Category.Name,
                                   "adult",
                                   StringComparison.CurrentCultureIgnoreCase)
                                || i.Description.Contains("xxx"))
                                && !i.Status);

            var match = IsMatchig(item);

            Assert.AreEqual(true, match);
        }

        [Test]
        public void GetAdultItems_FourDefaultValues_RightCountOfPriorities()
        {
            var countOfHighPriorities = new ItemHandler(mockRepository.Object, _mapper)
                .GetAdultItems()
                .PriorityCounts["High"];

            Assert.AreEqual(1, countOfHighPriorities);
        }
    }
}
