using Microsoft.AspNetCore.Mvc;
using WebLabs.DAL.Entities;
using WebLabs.Models;

namespace WebLabs.Controllers
{
    public class ProductController : Controller
    {
        public List<Dish> _dishes;
        List<DishGroup> _dishGroups;

        int _pageSize;

        public ProductController()
        {
            _pageSize = 3;
            SetupData();
        }
        public IActionResult Index(int? group, int pageNo = 1)
        {
            var dishesFiltered = _dishes.Where(d => !group.HasValue || d.DishGroupId == group.Value);
            // Поместить список групп во ViewData
            ViewData["Groups"] = _dishGroups;
            // Получить id текущей группы и поместить в TempData
            ViewData["CurrentGroup"] = group ?? 0;
            return View(ListViewModel<Dish>.GetModel(dishesFiltered, pageNo,_pageSize));
            //var items = _dishes
            //.Skip((pageNo - 1) * _pageSize)
            //.Take(_pageSize)
            //.ToList();
            //return View(items);
        }
        //public IActionResult Index()
        //{
        //    return View(_dishes);
        //}
        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _dishGroups = new List<DishGroup>
            {
            new DishGroup {DishGroupId=1, GroupName="Стартеры"},
            new DishGroup {DishGroupId=2, GroupName="Салаты"},
            new DishGroup { DishGroupId = 3, GroupName = "Супы" },
            new DishGroup
            {
                DishGroupId = 4,
                GroupName = "Основные блюда"},
            new DishGroup { DishGroupId = 5, GroupName = "Напитки" },
                new DishGroup { DishGroupId = 6, GroupName = "Десерты" }
            };
                        _dishes = new List<Dish>
            {
            new Dish {DishId = 1, DishName="Суп-харчо",
            Description="Очень острый, невкусный",
            Calories =200, DishGroupId=3, Image="Суп.jfif" },
            new Dish { DishId = 2, DishName="Борщ",
            Description="Много сала, без сметаны",
            Calories =330, DishGroupId=3, Image="Борщ.jpg" },
            new Dish { DishId = 3, DishName="Котлета пожарская",
            Description="Хлеб - 80%, Морковь - 20%",Calories =635, DishGroupId=4, Image="Котлета.jpg" },
            new Dish { DishId = 4, DishName="Макароны по-флотски",
            Description="С охотничьей колбаской",
            Calories =524, DishGroupId=4, Image="Макароны.jpg" },
            new Dish { DishId = 5, DishName="Компот",
            Description="Быстро растворимый, 2 литра",
            Calories =180, DishGroupId=5, Image="Компот.jpg" }
            };
        }
    }
}