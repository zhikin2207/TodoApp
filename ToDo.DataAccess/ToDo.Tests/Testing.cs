using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
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
        private Mock<IItemRepository> _mockRepository;
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingServicesProfile>();
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [SetUp]
        public void TestSetup()
        {
            _mockRepository = new Mock<IItemRepository>();
        }

        [Test]
        public void GetAdultItems_WithAdultTitles_Selected()
        {
            var itemWithAdultTitle = WithAdultTitle(NotAdultItem());
            
            var items = new[]
            {
                NotAdultItem(),
                itemWithAdultTitle
            };

            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultTitle);

            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);

            var handler = new ItemHandler(_mockRepository.Object, _mapper);
            
            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id, actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultCategory_Selected()
        {
            var itemWithAdultCategory = WithAdultCategory(NotAdultItem());
            var items = new[]
            {
                NotAdultItem(),
                itemWithAdultCategory
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultCategory);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id, actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultDescription_Selected()
        {
            var itemWithAdultDescription = WithAdultDescription(NotAdultItem());
            var items = new[]
            {
                NotAdultItem(),
                itemWithAdultDescription
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultDescription);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id, actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndStatusTrue_NotSelected()
        {
            var itemWithAdultTitleAndStatusTrue =
                WithAdultTitle(
                WithStatusTrue(
                NotAdultItem()));
            var items = new[]
            {
                NotAdultItem(),
                itemWithAdultTitleAndStatusTrue
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultTitleAndStatusTrue);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(0, actualItems.Items.Count());
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndStatusFalse_Selected()
        {
            var itemWithAdultTitleAndStatusFalse =
                WithAdultTitle(
                WithStatusFalse(
                NotAdultItem()));
            var items = new[]
            {
                NotAdultItem(),
                itemWithAdultTitleAndStatusFalse
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultTitleAndStatusFalse);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id, actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndAdultCategory_Selected()
        {
            var itemWithAdultTitleAndAdultCategory =
                WithAdultTitle(
                WithAdultCategory(
                NotAdultItem()));
            var items = new[]
            {
                NotAdultItem(),
                itemWithAdultTitleAndAdultCategory
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultTitleAndAdultCategory);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id, actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndAdultCategoryAndTrueStatus_NotSelected()
        {
            var itemWithAdultTitleAndAdultCategoryAndStatusTrue =
                WithAdultTitle(
                WithStatusTrue(
                WithAdultCategory(NotAdultItem())));
            var items = new[]
            {
                NotAdultItem(),
                itemWithAdultTitleAndAdultCategoryAndStatusTrue
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultTitleAndAdultCategoryAndStatusTrue);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(0, actualItems.Items.Count());
        }

        [Test]
        public void GetAdultItems_WithTrueStatus_NotSelected()
        {
            var itemWithStatusTrue = WithStatusTrue(NotAdultItem());
            var items = new[]
            {
                NotAdultItem(),
                itemWithStatusTrue
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithStatusTrue);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(0, actualItems.Items.Count());
        }

        [Test]
        public void GetAdultItems_2AdultTitles_OrderedByDate()
        {
            var items = new[]
            {
                WithAdultTitle(NotAdultItem()),
                WithAdultTitle(NotAdultItem())
            };
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems().Items.ToList();

            Assert.AreEqual(true, actualItems[0].DueDate < actualItems[1].DueDate);
        }

        private Item WithAdultTitle(Item item)
        {
            item.Title = "xxx";

            return item;
        }

        private Item WithAdultCategory(Item item)
        {
            item.Category = new Category { Name = "adult" };

            return item;
        }
        
        private Item WithAdultDescription(Item item)
        {
            item.Description = "TextxxxText";

            return item;
        }

        private Item WithStatusFalse(Item item)
        {
            item.Status = false;

            return item;
        }

        private Item WithStatusTrue(Item item)
        {
            item.Status = true;

            return item;
        }

        private Item NotAdultItem()
        {
            var item = new Item();

            item.Id = Guid.NewGuid();
            item.Title = "Item1";
            item.Description = "Text";
            item.Priority = Priority.High;
            item.Category = new Category { Name = "Cat1", Parent = null };
            item.DueDate = DateTime.Now;
            Thread.Sleep(1000);
            item.Status = false;

            return item;
        }
    } 
}
