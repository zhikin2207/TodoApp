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
            var itemWithAdultTitle = WithAdultTitle(new ItemDTO());
            var items = new[]
            {
                new ItemDTO(),
                itemWithAdultTitle
            };
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(itemWithAdultTitle,
                actualItems.Items.Single());
        }

        [Test]
        public void GetAdultItems_WithAdultCategory_Selected()
        {
            var itemWithAdultCategory = WithAdultCategory(new ItemDTO());
            var items = new[]
            {
                new ItemDTO(),
                itemWithAdultCategory
            };
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(itemWithAdultCategory,
                actualItems.Items.Single());
        }

        [Test]
        public void GetAdultItems_WithAdultDescription_Selected()
        {
            var itemWithAdultDescription = WithAdultDescription(new ItemDTO());
            var items = new[]
            {
                new ItemDTO(),
                itemWithAdultDescription
            };
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(itemWithAdultDescription,
                actualItems.Items.Single());
        }

        [Test]
        public void GetAdultItems_WithFalseStatus_Selected()
        {
            var itemWithFalseStatus = WithStatusFalse(new ItemDTO());
            var items = new[]
            {
                new ItemDTO(),
                itemWithFalseStatus
            };
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(itemWithFalseStatus,
                actualItems.Items.Single());
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndStatusTrue_NotSelected()
        {
            var itemWithAdultTitleAndStatusTrue = WithAdultTitle(
                WithStatusTrue(new ItemDTO()));
            var items = new[]
            {
                new ItemDTO(),
                itemWithAdultTitleAndStatusTrue
            };
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(0, actualItems.Items.Count());
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndStatusFalse_Selected()
        {
            var itemWithAdultTitleAndStatusFalse = WithAdultTitle(
                WithStatusFalse(new ItemDTO()));
            var items = new[]
            {
                new ItemDTO(),
                itemWithAdultTitleAndStatusFalse
            };
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(itemWithAdultTitleAndStatusFalse,
                actualItems.Items.Single());
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndAdultCategory_Selected()
        {
            var itemWithAdultTitleAndAdultCategory = WithAdultTitle(
                WithAdultCategory(new ItemDTO()));
            var items = new[]
            {
                new ItemDTO(),
                itemWithAdultTitleAndAdultCategory
            };
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(itemWithAdultTitleAndAdultCategory,
                actualItems.Items.Single());
        }

        [Test]
        public void GetAdultItems_WithAdultTitleAndAdultCategoryAndTrueStatus_NotSelected()
        {
            var itemWithAdultTitleAndAdultCategoryAndStatusTrue = WithAdultTitle(
                WithStatusTrue(
                    WithAdultCategory(new ItemDTO())));
            var items = new[]
            {
                new ItemDTO(),
                itemWithAdultTitleAndAdultCategoryAndStatusTrue
            };
            var handler = new ItemHandler(_mockRepository.Object, _mapper);

            var actualItems = handler.GetAdultItems();

            Assert.AreEqual(itemWithAdultTitleAndAdultCategoryAndStatusTrue,
                actualItems.Items.Single());
        }

        private ItemDTO WithAdultTitle(ItemDTO item)
        {
            item.Title = "xxx";

            return item;
        }

        private ItemDTO WithAdultCategory(ItemDTO item)
        {
            item.Category = new CategoryDTO { Name = "adult" };

            return item;
        }
        
        private ItemDTO WithAdultDescription(ItemDTO item)
        {
            item.Description = "TextxxxText";

            return item;
        }

        private ItemDTO WithStatusFalse(ItemDTO item)
        {
            item.Status = false;

            return item;
        }

        private ItemDTO WithStatusTrue(ItemDTO item)
        {
            item.Status = false;

            return item;
        }
    } 
}
