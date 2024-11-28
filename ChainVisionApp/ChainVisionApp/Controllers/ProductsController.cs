using ChainVisionApp.Models;
using ChainVisionApp.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace ChainVisionApp.Controllers
{
    [Route("Products")]

    public class ProductsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChainVisionContext _cvContext;

        public ProductsController(ILogger<HomeController> logger, ChainVisionContext cvContext)
        {
            _logger = logger;
            _cvContext = cvContext;
        }

        public IActionResult Index()
        {
            var products = _cvContext.VwProductMaterialDetails
                .ToList()  // Materialize the query to a list
                .GroupBy(pd => new { pd.ProductId, pd.ProductName, pd.ImageUrl }) // Group by product information
                .Select(group => new ProductViewModel
                {
                    Id = group.Key.ProductId,
                    ProductName = group.Key.ProductName,
                    ImageUrl = group.Key.ImageUrl,
                    Ingredients = string.Join(", ", group.Select(pd => pd.MaterialName)) // Join all ingredients for the product
                })
                .OrderBy(_ => _.ProductName)
                .ToList(); // Execute the grouping and join operation in memory

            return View(products);
        }

        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var productDetails = _cvContext.VwNewsProductDisruptionDetails
                .Where(_ => _.ProductId == id)
                .ToList()
                .GroupBy(pd => new { pd.ProductId, pd.ProductName, pd.ImageUrl })
                .Select(g => new ProductNewsDetailViewModel
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    ImageUrl = g.Key.ImageUrl,
                    Ingredients = GetIngredientsByProductId(g.Key.ProductId),
                    NewsMaterialData = GetNewsByProductId(g.Key.ProductId)
                })
                .FirstOrDefault();

            if (productDetails == null)
            {
                productDetails = _cvContext.VwProductMaterialDetails
                .Where(_ => _.ProductId == id)
                .ToList()
                .GroupBy(pd => new { pd.ProductId, pd.ProductName, pd.ImageUrl })
                .Select(g => new ProductNewsDetailViewModel
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    ImageUrl = g.Key.ImageUrl,
                    Ingredients = string.Join(", ", g.Select(pd => pd.MaterialName)),
                    NewsMaterialData = null
                })
                .FirstOrDefault();
                
                if(productDetails == null)
                {
                    return NotFound();
                }
            }

            return View(productDetails);
        }

        private string GetIngredientsByProductId(int productId)
        {
            string ingredients = "";
            var ingredientList = _cvContext.VwProductMaterialDetails.Where(_ => _.ProductId == productId).ToList();
            foreach(var i in ingredientList)
            {
                ingredients += i.MaterialName + ", ";
            }
            return ingredients.Substring(0, ingredients.Length - 2);
        }

        private List<NewsMaterialData> GetNewsByProductId(int productId)
        {
            List<NewsMaterialData> newsMaterialDatas = new List<NewsMaterialData>();
            var relevantNews = _cvContext.VwNewsProductDisruptionDetails
                                .Where(_ => _.ProductId == productId)
                                .OrderByDescending(_ => _.PublishedDateUtc)
                                .ToList();
            foreach(var n in relevantNews)
            {
                string severityRating = _cvContext.SeverityRatings.Where(_ => _.Id == n.SeverityRatingId).Select(_ => _.Description).FirstOrDefault();
                string severityRatingColor = _cvContext.SeverityRatings.Where(_ => _.Id == n.SeverityRatingId).Select(_ => _.Hexcode).FirstOrDefault(); ;
                var newsDetails = new NewsMaterialData
                {
                    NewsId = n.NewsId,
                    ArticleId = n.ArticleId,
                    Title = n.Title,
                    Description = n.Description,
                    DisplayText = n.DisplayText,
                    SeverityRating = severityRating,
                    SeverityRatingColor = severityRatingColor,
                    PublichedDateUtc = n.PublishedDateUtc,
                    Country = n.Country,
                    ArticleUrl = n.ArticleUrl,
                    MaterialId = n.MaterialId,
                    MaterialName = n.MaterialName,
                };
                newsMaterialDatas.Add(newsDetails);
            }
            return newsMaterialDatas;
        }

        [Route("Search")]
        [HttpGet]
        public IActionResult Search(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                var products = _cvContext.VwProductMaterialDetails
                    .ToList()  // Materialize the query to a list
                    .GroupBy(pd => new { pd.ProductId, pd.ProductName, pd.ImageUrl }) // Group by product information
                    .Select(group => new ProductViewModel
                    {
                        Id = group.Key.ProductId,
                        ProductName = group.Key.ProductName,
                        ImageUrl = group.Key.ImageUrl,
                        Ingredients = string.Join(", ", group.Select(pd => pd.MaterialName)) // Join all ingredients for the product
                    })
                    .OrderBy(_ => _.ProductName)
                    .ToList();
                return Json(products);
            }
            else
            {
                var products = _cvContext.VwProductMaterialDetails
                    .Where(pd => pd.ProductName.Contains(query))
                    .ToList()
                    .GroupBy(pd => new { pd.ProductId, pd.ProductName, pd.ImageUrl })
                    .Select(group => new ProductViewModel
                    {
                        Id = group.Key.ProductId,
                        ProductName = group.Key.ProductName,
                        ImageUrl = group.Key.ImageUrl,
                        Ingredients = string.Join(", ", group.Select(pd => pd.MaterialName))
                    })
                    .OrderBy(p => p.ProductName)
                    .ToList();
                return Json(products);

            }

        }

        [Route("AddProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductViewModel newProduct)
        {
            try
            {
                if (newProduct != null)
                {
                    var prodExists = _cvContext.Products.Where(_ => _.ProductName == newProduct.ProductName).FirstOrDefault();
                    if (prodExists != null)
                    {
                        return Ok(new { status = false, message = $"{newProduct.ProductName} is already added" });
                    }
                    var productDb = new Product
                    {
                        ProductName = newProduct.ProductName,
                        ImageUrl = newProduct.ImageUrl
                    };
                    _cvContext.Products.Add(productDb);

                    var materials = newProduct.Ingredients.Replace(", ", ",").Split(",");

                    foreach (var i in materials)
                    {
                        var exists = _cvContext.Materials
                            .Where(_ => _.MaterialName == i).FirstOrDefault();
                        if (exists == null)
                        {
                            var materialDb = new Material
                            {
                                MaterialName = i
                            };
                            _cvContext.Materials.Add(materialDb);
                        }
                    }
                    await _cvContext.SaveChangesAsync();

                    var productId = _cvContext.Products.Where(_ => _.ProductName == newProduct.ProductName).Select(_ => _.Id).FirstOrDefault();
                    foreach (var i in materials)
                    {
                        var materialId = _cvContext.Materials.Where(_ => _.MaterialName == i).Select(_ => _.Id).FirstOrDefault();
                        var prodMatDb = new ProductMaterial
                        {
                            ProductId = productId,
                            MaterialId = materialId
                        };
                        _cvContext.ProductMaterials.Add(prodMatDb);
                    }
                    await _cvContext.SaveChangesAsync();
                    return Ok(new { status = true, message = "Product Created" });
                }
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = $"Error {e.Message}" });
            }
            return BadRequest(new { status = false, message = "Product Incomplete" });
        }
    }
}
