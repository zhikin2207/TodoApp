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
            var itemWithAdultTitle = 
                WithAdultTitle(
                NotAdultItem(new Item()));
            var items = new[]
            {
                NotAdultItem(new Item()),
                itemWithAdultTitle
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultTitle);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);

            var handler = new ItemHandler(_mockRepository.Object, _mapper);
            

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id,
                actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultCategory_Selected()
        {
            var itemWithAdultCategory =
                WithAdultCategory(
                NotAdultItem(
                NotAdultItem(new Item())));
            var items = new[]
            {
                NotAdultItem(new Item()),
                itemWithAdultCategory
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultCategory);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id,
                actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultDescription_Selected()
        {
            var itemWithAdultDescription =
                WithAdultDescription(
                NotAdultItem(new Item()));
            var items = new[]
            {
                NotAdultItem(new Item()),
                itemWithAdultDescription
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultDescription);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id,
                actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndStatusTrue_NotSelected()
        {
            var itemWithAdultTitleAndStatusTrue =
                WithAdultTitle(
                WithStatusTrue(
                NotAdultItem(new Item())));
            var items = new[]
            {
                NotAdultItem(new Item()),
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
                NotAdultItem(new Item())));
            var items = new[]
            {
                NotAdultItem(new Item()),
                itemWithAdultTitleAndStatusFalse
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultTitleAndStatusFalse);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id,
                actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndAdultCategory_Selected()
        {
            var itemWithAdultTitleAndAdultCategory =
                WithAdultTitle(
                WithAdultCategory(
                NotAdultItem(new Item())));
            var items = new[]
            {
                NotAdultItem(new Item()),
                itemWithAdultTitleAndAdultCategory
            };
            var expectedItem = _mapper.Map<ItemDTO>(itemWithAdultTitleAndAdultCategory);
            _mockRepository
                 .Setup(m => m.GetItemsWithCategoryAndTags())
                 .Returns(items);
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(expectedItem.Id,
                actualItems.Items.Single().Id);
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndAdultCategoryAndTrueStatus_NotSelected()
        {
            var itemWithAdultTitleAndAdultCategoryAndStatusTrue =
                WithAdultTitle(
                WithStatusTrue(
                WithAdultCategory(NotAdultItem(new Item()))));
            var items = new[]
            {
                NotAdultItem(new Item()),
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

        private Item NotAdultItem(Item item)
        {
            item.Id = Guid.NewGuid();
            item.Title = "Item1";
            item.Description = "Text";
            item.Priority = Priority.High;
            item.Category = new Category { Name = "Cat1", Parent = null };
            item.DueDate = DateTime.Now;
            item.Status = false;

            return item;
        }
    } 
}
