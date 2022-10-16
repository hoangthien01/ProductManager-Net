
using Microsoft.AspNetCore.Mvc;
using ProductManager.Models;
using ProductManager.Services;
namespace ProductManager.Controllersace
{
    public class ProductController : Controller
    {
        // public List<Product> Products {get;set;}
        private readonly IProductService service;
        public ProductController(IProductService service)
        {
            this.service = service;
        }

        // public ProductController()
        // {
        //     // Products = new List<Product>()
        //     // {
        //     //     new Product() { Id =1, Name = "Iphone 10", Price = 500, Quantity = 30},
        //     //     new Product() { Id =2, Name = "Iphone 11", Price = 600, Quantity = 40},
        //     //     new Product() { Id =3, Name = "Iphone 12", Price = 700, Quantity = 50},
        //     // };
            
        // }
        public IActionResult Index()
        {   
            var categories = service.GetCategories();
            var products = service.GetProducts();
            ViewBag.Categories= categories;
            return View(products);
        }
        public IActionResult Create()
        {   

            var categories = service.GetCategories();
            return View(categories);
        }
        public IActionResult Update(int id)
        {   
            var product = service.GetProductById(id);
            if ( product == null) return RedirectToAction("Create");

            var categories = service.GetCategories();
            ViewBag.Product = product;
            return View(categories);
        }
        public IActionResult Delete(int id)
        {   
            
            service.DeleteProduct(id);
            
            return RedirectToAction("Index");
        }
        public IActionResult Save(Product product)
        {   
            if (service.GetProductById(product.Id)==null)
            service.CreateProduct(product);
            else service.UpdateProduct(product);
            return RedirectToAction("Index");
        }

    }
}