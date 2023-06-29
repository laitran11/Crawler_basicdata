
using DataTelephone;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Net.WebRequestMethods;


string? DownLoad(string folder, string url)
{
    string root = Path.Combine(Directory.GetCurrentDirectory(), "images",folder);
    using (WebClient client = new WebClient())
    {
        string fileName  = Path.GetFileName(url);
        try
        {
            client.DownloadFile(url, Path.Combine(root,fileName));
            return fileName;
        }
        catch
        {
            return null;
        }
    }
}





Product GetProduct(string url, Category? category)
{
    HtmlWeb web = new HtmlWeb();
    HtmlDocument doc = web.Load(url);

    var parent = doc.DocumentNode.SelectSingleNode("//div[@class=\"xxxx\"]");

    var h1 = parent.SelectSingleNode("./div[@class=\"product-title hidden-xs\"]/h1");
    //Console.WriteLine(h1.InnerText.Trim());

    var sku = parent.SelectSingleNode("./div[@class=\"product-title hidden-xs\"]/span");
    //Console.WriteLine(sku.InnerText.Replace("SKU:","").Trim());

    var bonus = parent.SelectSingleNode("./span[@class=\"hide\"]");
    //Console.WriteLine(bonus.InnerText);

    var minPrice = parent.SelectSingleNode("./div[@class=\"product-price hidden-xs\"]");
    //Console.WriteLine(minPrice.GetAttributeValue("data-price-min", null));

    var sizes = parent.SelectNodes(".//div[@class=\"select-swap sizeEachSwatch \"]/select/option");


    Product product = new Product
    {
        Category = category,
        //CategoryId = 3,
        Name = h1.InnerText.Trim(),
        Code = sku.InnerText.Replace("SKU:", "").Trim(),
        minPrice = Convert.ToInt32(minPrice.GetAttributeValue("data-price-min", null)),
        Bonus = Convert.ToInt32(bonus.InnerText),
    };

    List<ProductSize> productSizes = new List<ProductSize>();
    foreach (var size in sizes)
    {
        if (size.GetAttributeValue("value", null) != "")
        {
            //Console.WriteLine(size.GetAttributeValue("data-trade", null));
            ProductSize productSize = new ProductSize
            {
                ProductId = product.Id,
                Size = size.GetAttributeValue("data-trade", null),
                IsSoldOut = false
            };
            productSizes.Add(productSize);
        }
    }
    product.ProductSizes = productSizes;
    //}
    //using (StoreContext context = new StoreContext())
    //{
    //    ProductSizeRepository repository = new ProductSizeRepository(context);
    //    Console.WriteLine(repository.Add(productSizes));
    //}
    
    var parentcolor = doc.GetElementbyId("variant-swatch-0");
    var colors = parentcolor.SelectNodes("./div[@class=\"select-swap  colorEachSwatch\"]/div");
    List<Colors> productColors = new List<Colors>();
    foreach(var color in colors)
    {
        Colors productColor = new Colors
        {
            ProductId = product.Id,
            Color = color.GetAttributeValue("data-value", null)
        };
        productColors.Add(productColor);
        //Console.WriteLine(color.GetAttributeValue("data-value", null));
    }
    product.ProductColors = productColors;


    var slider = doc.GetElementbyId("sliderproduct");
    var img = slider.SelectNodes("./li/img");
    List<ProductImage> productImages = new List<ProductImage>();
    foreach (var node in img)
    {
        var imgs = node.GetAttributeValue("src", null);
        if (!imgs.StartsWith("https://"))
        {
            imgs = "https:" + imgs;
        }
        string? fileName = DownLoad("imgs", imgs);
        if (!string.IsNullOrEmpty(fileName))
        {
            productImages.Add(new ProductImage
            {
                Product = product,
                ImageUrl = fileName,
            });
        }
        //Console.WriteLine(imgs);
    }
    product.ProductImages = productImages;
    return product;
}

//string url = "https://hnossfashion.com/products/ao-kieu-somi-vat-dap-hnaki074";
//Product product = GetProduct(url, null);


//using (StoreContext context = new StoreContext())
//{
//    Console.WriteLine(context.Database.ExecuteSqlRaw("EXEC ClearStore")); 
//    ProductRepository repository = new ProductRepository(context);
//    Console.WriteLine(repository.Add(product));
//}


List<Product> GetProducts(string url, Category category)
{
    HtmlWeb web = new HtmlWeb();
    HtmlDocument doc = web.Load(url);
    var parent = doc.DocumentNode.SelectSingleNode("//div[@class= \"content-product-list product-list nw21-collection-product-list-content filter clearfix\"]");

    var items = parent.SelectNodes(".//div[@class=\"product-img image-resize nw21-col-product-image\"]/a");


    if (items != null)
    {
        List<Product> list = new List<Product>();
        foreach (var item in items)
        {
            var href = item.GetAttributeValue("href", null);
            if (!href.StartsWith("https://hnossfashion.com/"))
            {
                href = "https://hnossfashion.com" + href;
            }
            Console.WriteLine(href);
            list.Add(GetProduct(href, category));
        }
        return list;
    }
    return null;

}


//string url = "https://hnossfashion.com/products/ao-kieu-somi-vat-dap-hnaki074";
//HtmlWeb web = new HtmlWeb();
//HtmlDocument doc = web.Load(url);

    




//List<Product> list = GetProducts(url, null);

//using (StoreContext context = new StoreContext())
//{
//    Console.WriteLine(context.Database.ExecuteSqlRaw("EXEC ClearStore"));
//    ProductRepository repository = new ProductRepository(context);
//    Console.WriteLine(repository.Add(list));
//}


 string url = "https://hnossfashion.com/collections/san-pham-moi?itm_source=homepage&itm_medium=menu&itm_campaign=normal&itm_content=newin";

HtmlWeb web = new HtmlWeb();
HtmlDocument doc = web.Load(url);

//HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul[@class=\"menu-collection\"]/li/a");

//foreach (var node in nodes)
//{
//    Console.WriteLine(node.InnerText.Trim());
//}

var ul = doc.GetElementbyId("nw21-menu-left--top");
var nodes = ul.SelectNodes("./li/a");

List<Category> categories = new List<Category>();


foreach (var node in nodes)
{
    var href = node.GetAttributeValue("href", null);
    if (!href.StartsWith("https://hnossfashion.com/"))
    {
        href = "https://hnossfashion.com" + href;

    }
    Console.WriteLine(href);
        //Console.WriteLine(node.InnerText.Trim());
        Category category = new Category
        {
            Name = node.InnerText.Trim(),
            Url = href,
            ParentId = 1
        };
        categories.Add(category);
        List<Product> products = GetProducts(category.Url, category);
        category.Products = products;
    
}

using (StoreContext context = new StoreContext())
{
    Console.WriteLine(context.Database.ExecuteSqlRaw("EXEC ClearStore"));
    CategoryRepository repository = new CategoryRepository(context);
    int ret = repository.Add(categories);
    Console.WriteLine(ret);
}



