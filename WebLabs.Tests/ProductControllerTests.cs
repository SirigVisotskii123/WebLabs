﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebLabs.Controllers;
using WebLabs.DAL.Entities;
using Xunit;

namespace WebLabs.Tests
{
    public class ProductControllerTests
    {
        [Theory]
        [InlineData(1, 3, 1)] // 1-я страница, кол. объектов 3, id первого объекта 1
        [InlineData(2, 2, 4)] // 2-я страница, кол. объектов 2, id первого объекта 4
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            var controller = new ProductController();
            controller._dishes = new List<Dish>
            {
            new Dish{ DishId=1},
            new Dish{ DishId=2},
            new Dish{ DishId=3},
            new Dish{ DishId=4},
            new Dish{ DishId=5}
            };
            // Act
            var result = controller.Index(page) as ViewResult;
            var model = result?.Model as List<Dish>;
            // Assert
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].DishId);
        }
    }
}
