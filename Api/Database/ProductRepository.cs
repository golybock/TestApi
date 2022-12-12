using Api.Models.Client;
using Api.Models.Customer;
using Api.Models.Order;
using Api.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Api.Database;

public class ProductRepository : MainDatabaseClass
{
    protected ProductRepository(string connectionString) : base(connectionString)
    {
    }
    
    public List<Product> GetProducts()
    {
        connection.Open();
        
        List<Product> products = new List<Product>();
        
        string query = @"select * from product";
        
        using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product();

                    product.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    product.Name = reader.GetString(reader.GetOrdinal("name"));
                    product.CurrentPrice = reader.GetDecimal(reader.GetOrdinal("current_price"));
                    product.Sale = reader.GetDecimal(reader.GetOrdinal("sale"));
                    product.Category.Id = reader.GetInt32(reader.GetOrdinal("category_id"));

                    products.Add(product);
                }
            }
        }
        
        connection.Close();
        
        // дополняем данными
        foreach (var pr in products)
        {
            pr.Category = GetCategory(pr.Category.Id);
            
            pr.ProductPhotos = photos.Value as List<ProductPhoto>;
            
            pr.ProductBrands = brands.Value as List<Brand>;
        }
        
        return products;
    }

    public Product GetProductById(int id)
    {
        connection.Open();
        
        Product product = new Product();
        
        string query = @"select * from product where id = $1";
        
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };

        using (cmd)
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    product.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    product.Name = reader.GetString(reader.GetOrdinal("name"));
                    product.CurrentPrice = reader.GetDecimal(reader.GetOrdinal("current_price"));
                    product.Sale = reader.GetDecimal(reader.GetOrdinal("sale"));
                    product.Category.Id = reader.GetInt32(reader.GetOrdinal("category_id"));
                }
            }
        }
        
        connection.Close();
        
        // дополняем объект данными
        product.Category = GetCategory(product.Category.Id);
        
        var photos = GetProductPhotos(product.Id) as OkObjectResult;
        product.ProductPhotos = photos.Value as List<ProductPhoto>;

        var brands = GetProductBrands(product.Id) as OkObjectResult;
        product.ProductBrands = brands.Value as List<Brand>;

        return product;
    }
    
    public IActionResult DeleteProduct(int productId)
    {
        connection.Open();
        // запрос
        string query = @"delete from product where id = ($1)";
        
        // создаем команду
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productId}
            }
        };
        // выполняем команду
        // получаем ответ от бд: 0 - строки не обновлены, 1+ - строки обновлены
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new OkResult() : new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
        finally
        {
            connection.Close();
        }
        
    }
    
    public IActionResult AddProduct(Product product)
    {
        connection.Open();
        // запрос
        string query = 
            @"insert into product(name, current_price, sale, category_id) values ($1, $2, $3, $4)";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = product.Name},
                new() {Value = product.CurrentPrice},
                new() {Value = product.Sale},
                new() {Value = product.Category.Id}
            }
        };
        // пробуем выолнить
        try
        {
            cmd.ExecuteNonQuery();
            return new OkResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            // возвращаем ошибку
            return new BadRequestObjectResult(e);
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult UpdateProduct(Product product)
    {
        connection.Close();
        
        string query =
            @"update product set
                    name = $2,
                    current_price = $3,
                    sale = $4,
                    category_id = $5
            where id = $1";
        
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                // если новые параметры пустые, заполняем их старыми значениями
                new() {Value = product.Id},
                new() {Value = product.Name},
                new() {Value = product.CurrentPrice},
                new() {Value = product.Sale},
                new() {Value = product.Category.Id}
            }
        };
        
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new OkResult() : new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
        finally
        {
            connection.Close();
        }
    }

    public Category GetCategory(int id)
    {
        connection.Open();

        Category category = new Category();
        
        string query = @"select * from product_category where id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        using (cmd)
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    category.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    category.Name = reader.GetString(reader.GetOrdinal("name"));
                    
                    var description = reader.GetValue(reader.GetOrdinal("description"));
                    category.Description = description == DBNull.Value ? null : description.ToString();
                }
            }
        }
        
        connection.Close();
        
        return category;
    }

    public List<Category> GetCategories()
    {
        connection.Open();
        
        List<Category> categories = new List<Category>();
        
        string query = @"select * from product_category";
        
        using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    category.Name = reader.GetString(reader.GetOrdinal("name"));
                    
                    var description = reader.GetValue(reader.GetOrdinal("description"));
                    category.Description = description == DBNull.Value ? null : description.ToString();
                    
                    categories.Add(category);
                }
            }
        }
        
        connection.Close();
        
        return categories;
    }

    // категория продукта
    public IActionResult AddProductCategory(Category category)
    {
        connection.Open();
        // запрос
        string query =
            @"insert into product_category(name, description)
                           values ($1, $2)";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = category.Name},
                new() {Value = category.Description == null ? DBNull.Value : category.Description}
            }
        };
        // пробуем выолнить
        try
        {
            cmd.ExecuteNonQuery();
            return new AcceptedResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult EditProductCategory(Category category)
    {
        connection.Open();
        // запрос
        string query =
            @"update product_category set name = $2, description = $3 where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = category.Id},
                new() {Value = category.Name},
                new() {Value = category.Description == null ? DBNull.Value : category.Description}
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult DeleteProductCategory(int productCategoryId)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_category where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productCategoryId},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }
    
    public IActionResult AddBrand(Brand brand)
    {
        connection.Open();
        // запрос
        string query =
            @"insert into brand(name, photo_url)
                           values ($1, $2)";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = brand.Name},
                new() {Value = brand.PhotoUrl == null ? DBNull.Value : brand.PhotoUrl}
            }
        };
        // пробуем выолнить
        try
        {
            cmd.ExecuteNonQuery();
            return new AcceptedResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult DeleteBrand(int brandId)
    {
        connection.Open();
        // запрос
        string query = @"delete from brand where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = brandId},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult EditBrand(Brand brand)
    {
        connection.Open();
        // запрос
        string query =
            @"update brand set
                   name = $2, photo_url = $3 where id = $1";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = brand.Id},
                new() { Value = brand.Name},
                new() { Value = brand.PhotoUrl == null ? DBNull.Value : brand.PhotoUrl}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult GetProductBrands(int id)
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        List<Brand> brands = new List<Brand>();
        
        // запрос
        string query = @"select b.id as id, b.name as name, b.photo_url as photo_url
                            from product_brand
                            join brand b on b.id = product_brand.brand_id
                            where product_id = $1";
        
        // выполняем команду
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        using(cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    Brand brand = new Brand();
                    // перенос значений из строки базы данных в объект класса
                    brand.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    brand.Name = reader.GetString(reader.GetOrdinal("name"));
                    var photoUrl = reader.GetValue(reader.GetOrdinal("photo_url"));
                    brand.PhotoUrl = photoUrl == DBNull.Value ? null : photoUrl.ToString();
                    brands.Add(brand);
                }
            }
        }
        
        connection.Close();
        
        return new OkObjectResult(brands);
    }

    public IActionResult GetBrands()
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        List<Brand> brands = new List<Brand>();
        
        // запрос
        string query = @"select * from brand";
        
        // выполняем команду
        using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    Brand brand = new Brand();
                    // перенос значений из строки базы данных в объект класса
                    brand.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    brand.Name = reader.GetString(reader.GetOrdinal("name"));
                    var photoUrl = reader.GetValue(reader.GetOrdinal("photo_url"));
                    brand.PhotoUrl = photoUrl == DBNull.Value ? null : photoUrl.ToString();
                    brands.Add(brand);
                }
            }
        }
        
        connection.Close();
        
        return new OkObjectResult(brands);
    }

    public IActionResult AddBrandToProduct(ProductBrand productBrand)
    {
        connection.Open();
        // запрос
        string query =
            @"insert into product_brand(product_id, brand_id)
                           values ($1, $2)";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productBrand.ProductId},
                new() {Value = productBrand.BrandId}
            }
        };
        // пробуем выолнить
        try
        {
            cmd.ExecuteNonQuery();
            return new AcceptedResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult AddBrandsToProduct(List<ProductBrand> productBrands)
    {
        try
        {
            foreach (var productBrand in productBrands)
            {
                AddBrandToProduct(productBrand);
            }
            return new AcceptedResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }

    public IActionResult DeleteBrandFromProduct(int productBrandId)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_brand where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productBrandId}
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult SetProductBrands(List<ProductBrand> productBrands)
    {
        try
        {
            ClearProductBrands(productBrands.ElementAt(0).ProductId);
            AddBrandsToProduct(productBrands);
            return new AcceptedResult();
        }
        catch
        {
            return new BadRequestResult();
        }
    }
    
    public IActionResult ClearProductBrands(int productId)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_brand where product_id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productId},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult GetProductPhotos(int id)
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        List<ProductPhoto> productPhotos = new List<ProductPhoto>();

        // запрос
        string query = @"select * from product_photo where product_id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    ProductPhoto productPhoto = new ProductPhoto();
                    productPhoto.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    productPhoto.PhotoPath = reader.GetString(reader.GetOrdinal("photo_url"));
                    productPhoto.ProductId = reader.GetInt32(reader.GetOrdinal("product_id"));
                    productPhotos.Add(productPhoto);
                }
            }
        }
        
        connection.Close();
        
        return new OkObjectResult(productPhotos);
    }

    public IActionResult AddProductPhoto(ProductPhoto productPhoto)
    {
        connection.Open();
        // запрос
        string query =
            @"insert into product_photo(product_id, photo_url)
                           values ($1, $2)";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productPhoto.ProductId},
                new() {Value = productPhoto.PhotoPath}
            }
        };
        // пробуем выолнить
        try
        {
            cmd.ExecuteNonQuery();
            return new AcceptedResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult DeleteProductPhoto(int productPhotoId)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_photo where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productPhotoId},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult EditProductPhoto(ProductPhoto productPhoto)
    {
        connection.Open();
        // запрос
        string query =
            @"update product_photo set
                   photo_url = $2 where id = $1";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = productPhoto.Id},
                new() {Value = productPhoto.PhotoPath}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult AddProductPhotos(List<ProductPhoto> productPhotos)
    {
        try
        {
            foreach (var photo in productPhotos)
            {
                AddProductPhoto(photo);
            }

            return new AcceptedResult();
        }
        catch
        {
            return new BadRequestResult();
        }
    }

    public IActionResult SetProductPhotos(List<ProductPhoto> productPhotos)
    {
        try
        {
            ClearProductPhotos(productPhotos.ElementAt(0).ProductId);
            AddProductPhotos(productPhotos);
            return new AcceptedResult();
        }
        catch
        {
            return new BadRequestResult();
        }
    }
    
    public IActionResult ClearProductPhotos(int productId)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_photo where product_id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productId},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult AddNewProductPrice(ProductPrice productPrice)
    {
        try
        {
            connection.Open();
            // запрос
            string query = @"insert into product_price(price, datetime, product_id, customer_id)
                            VALUES ($1, $2, $3, $4)";
        
            // дополняем запрос параметрами
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
            {
                Parameters =
                {
                    new() { Value = productPrice.Price},
                    new() { Value = DateTime.Now},
                    new() { Value = productPrice.ProductId},
                    new() { Value = productPrice.CustomerId}
                }
            };
            // пробуем выолнить
            try
            {
                return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
            }
            // что-то не так с данными
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.ToString());
            }
            finally
            {
                connection.Close();
            }
            
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
    }

    public IActionResult DeleteProductPrice(int productPriceId)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_price where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = productPriceId}
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult GetOrder(int id)
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        Order order = new Order();
        
        // запрос
        string query = @"select * from client_order where id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    // перенос значений из строки базы данных в объект класса
                    order.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    order.DateTimeOfCreation = reader.GetDateTime(reader.GetOrdinal("datetime_of_creation"));
                    order.TotalCost = reader.GetDecimal(reader.GetOrdinal("total_cost"));
                    order.ClientId = reader.GetInt32(reader.GetOrdinal("client_id"));
                }
            }
        }
        
        connection.Close();
        
        OkObjectResult products = GetOrderProducts(order.Id) as OkObjectResult;
        order.OrderProductsList = products.Value as List<OrderProduct>;
        
        OkObjectResult statuses = GetOrderStatuses(order.Id) as OkObjectResult;
        order.OrderStatusesList = statuses.Value as List<OrderStatuses>;

        return new OkObjectResult(order);
    }
    
    [Authorize]
    public IActionResult GetOrders()
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        List<Order> orders = new List<Order>();
        
        // запрос
        string query = @"select * from client_order";
        
        // выполняем команду
        using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    Order order = new Order();
                    // перенос значений из строки базы данных в объект класса
                    order.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    order.DateTimeOfCreation = reader.GetDateTime(reader.GetOrdinal("datetime_of_creation"));
                    order.TotalCost = reader.GetDecimal(reader.GetOrdinal("total_cost"));
                    order.ClientId = reader.GetInt32(reader.GetOrdinal("client_id"));
                    // сохранение объекта в списке
                    orders.Add(order);
                }
            }
        }
        
        connection.Close();

        foreach (var order in orders)
        {
            OkObjectResult products = GetOrderProducts(order.Id) as OkObjectResult;
            order.OrderProductsList = products.Value as List<OrderProduct>;
            
            OkObjectResult statuses = GetOrderStatuses(order.Id) as OkObjectResult;
            order.OrderStatusesList = statuses.Value as List<OrderStatuses>;
        }
        
        return new OkObjectResult(orders);
    }

    public IActionResult AddOrder(Order order)
    {
        connection.Open();
        // запрос
        string query = @"insert into client_order(client_id, datetime_of_creation, total_cost) VALUES ($1, $2, $3) returning id";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = order.ClientId},
                new() { Value = DateTime.Now},
                new() { Value = order.TotalCost}
            }
        };
        int orderId = 0;
        // пробуем выолнить
        try
        {
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orderId = reader.GetInt32(reader.GetOrdinal("id"));
            }

            return new OkObjectResult((orderId));
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
            AddOrderStatus(new OrderStatuses() { OrderId = orderId , StatusId = 1});
        }
    }
    
    public IActionResult DeleteOrder(int id)
    {
        connection.Open();
        // запрос
        string query = @"delete from client_order where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = id},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult EditOrder(Order order)
    {
        connection.Open();
        // запрос
        string query =
            @"update client_order set
                   client_id = $2, total_cost = $3 where id = $1";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = order.Id},
                new() {Value = order.ClientId},
                new() {Value = order.TotalCost}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult GetOrderProducts(int id)
    {
        // открываем подкючение к бд
        connection.Open();

        List<OrderProduct> orderProductsList = new List<OrderProduct>();

        // запрос
        string query = @"select * from order_products where order_id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    OrderProduct orderProducts = new OrderProduct();
                    orderProducts.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    orderProducts.OrderId = reader.GetInt32(reader.GetOrdinal("order_id"));
                    orderProducts.ProductId = reader.GetInt32(reader.GetOrdinal("product_id"));
                    orderProducts.Count = reader.GetInt32(reader.GetOrdinal("count"));
                    orderProducts.PriceForOne = reader.GetDecimal(reader.GetOrdinal("price_for_one"));
                    orderProductsList.Add(orderProducts);
                }
            }
        }
        
        connection.Close();

        return new OkObjectResult(orderProductsList);
    }

    public IActionResult AddProductToOrder(OrderProduct orderProducts)
    {
        connection.Open();
        // запрос
        string query = @"insert into order_products(order_id, product_id, count, price_for_one) VALUES ($1, $2, $3, $4)";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = orderProducts.OrderId},
                new() { Value = orderProducts.ProductId},
                new() { Value = orderProducts.Count},
                new() { Value = orderProducts.PriceForOne}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult AddProductsToOrder(List<OrderProduct> orderProductsList)
    {
        try
        {
            foreach (var pr in orderProductsList)
            {
                AddProductToOrder(pr);
            }

            return new OkResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
    }

    public IActionResult DeleteProductFromOrder(OrderProduct orderProducts)
    {
        connection.Open();
        // запрос
        string query = @"delete from order_products where order_id = $1 and product_id = $2";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = orderProducts.OrderId},
                new() {Value = orderProducts.ProductId},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult SetOrderProducts(List<OrderProduct> orderProductsList)
    {
        try
        {
            // очищаем список продуктов в заказе
            ClearOrderProducts(orderProductsList.ElementAt(0).OrderId);
            // добавляем продукты в заказ
            AddProductsToOrder(orderProductsList);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
    }

    public IActionResult ClearOrderProducts(int id)
    {
        connection.Open();
        // запрос
        string query = @"delete from order_products where order_id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = id},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult GetStatuses()
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        List<OrderStatus> orderStatuses = new List<OrderStatus>();

        // запрос
        string query = @"select * from order_status";
        
        // выполняем команду
        using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    OrderStatus orderStatus = new OrderStatus();
                    orderStatus.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    orderStatus.Name = reader.GetString(reader.GetOrdinal("name"));
                    var description = reader.GetValue(reader.GetOrdinal("description"));
                    orderStatus.Description = description == DBNull.Value ? null : description.ToString();
                    orderStatuses.Add(orderStatus);
                }
            }
        }
        
        connection.Close();
        
        return new OkObjectResult(orderStatuses);
    }

    public IActionResult AddStatus(OrderStatus orderStatus)
    {
        connection.Open();
        // запрос
        string query = @"insert into order_status(name, description) VALUES ($1, $2)";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = orderStatus.Name},
                new() { Value = orderStatus.Description == null ? DBNull.Value : orderStatus.Description}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }
    
    public IActionResult EditStatus(OrderStatus orderStatus)
    {
        connection.Open();
        // запрос
        string query =
            @"update order_status set
                   name = $2 where id = $1";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = orderStatus.Id},
                new() {Value = orderStatus.Name}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult DeleteStatus(int orderStatusId)
    {
        connection.Open();
        // запрос
        string query = @"delete from order_status where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = orderStatusId},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult GetOrderStatuses(int id)
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        List<OrderStatuses> orderStatuses = new List<OrderStatuses>();

        // запрос
        string query = @"select * from order_statuses where order_id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    OrderStatuses orderStatus = new OrderStatuses();
                    orderStatus.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    orderStatus.OrderId = reader.GetInt32(reader.GetOrdinal("order_id"));
                    orderStatus.StatusId = reader.GetInt32(reader.GetOrdinal("order_status_id"));
                    orderStatuses.Add(orderStatus);
                }
            }
        }
        
        connection.Close();

        return new OkObjectResult(orderStatuses);
    }

    public IActionResult AddOrderStatus(OrderStatuses orderStatus)
    {
        connection.Open();
        // запрос
        string query = @"insert into order_statuses(order_id, order_status_id) VALUES ($1, $2)";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = orderStatus.OrderId},
                new() { Value = orderStatus.StatusId}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult DeleteOrderStatus(int orderStatusesId)
    {
        connection.Open();
        // запрос
        string query = @"delete from order_statuses where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = orderStatusesId}
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult GetCustomer(int id)
    {
        // открываем подкючение к бд
        connection.Open();

        Customer customer = new Customer();
        
        // запрос
        string query = @"select * from customer where id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    // перенос значений из строки базы данных в объект класса
                    customer.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    customer.Name = reader.GetString(reader.GetOrdinal("name"));
                    customer.PhotoUrl = reader.GetString(reader.GetOrdinal("photo_url"));
                }
            }
        }
        
        connection.Close();

        return new OkObjectResult(customer);
    }

    public IActionResult AddCustomer(Customer customer)
    {
        connection.Open();
        // запрос
        string query = @"insert into customer(name, photo_url) VALUES ($1, $2)";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = customer.Name},
                new() { Value = customer.PhotoUrl == null ? DBNull.Value : customer.PhotoUrl}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult EditCustomer(Customer customer)
    {
        connection.Open();
        // запрос
        string query =
            @"update customer set
                   name = $2, photo_url = $3 where id = $1";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = customer.Id},
                new() {Value = customer.Name},
                new() {Value = customer.PhotoUrl == null ? DBNull.Value : customer.PhotoUrl}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }
    
    public IActionResult DeleteCustomer(int id)
    {
        connection.Open();
        // запрос
        string query = @"delete from customer where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = id},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult GetCustomerProducts(int id)
    {
        // открываем подкючение к бд
        connection.Open();

        List<ProductCustomer> productCustomers = new List<ProductCustomer>();

        // запрос
        string query = @"select * from product_customer where customer_id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    ProductCustomer productCustomer = new ProductCustomer();
                    productCustomer.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    productCustomer.Customer.Id = reader.GetInt32(reader.GetOrdinal("customer_id"));
                    productCustomer.Product.Id = reader.GetInt32(reader.GetOrdinal("product_id"));
                    productCustomers.Add(productCustomer);
                }
            }
        }
        
        connection.Close();

        foreach (var product in productCustomers)
        {
            OkObjectResult customer = GetCustomer(product.Customer.Id) as OkObjectResult;
            product.Customer = customer.Value as Customer;
            
            OkObjectResult prod = GetProductById(product.Product.Id) as OkObjectResult;
            product.Product = prod.Value as Product;
        }

        return new OkObjectResult(productCustomers);
    }

    public IActionResult GetClient(int id)
    {
        // открываем подкючение к бд
        connection.Open();

        Client client = new Client();
        
        // запрос
        string query = @"select * from client where id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    client.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    client.DateTimeOfRegistration = reader.GetDateTime(reader.GetOrdinal("datetime_of_registration"));
                    client.Token = reader.GetString(reader.GetOrdinal("token"));
                    
                    var email = reader.GetValue(reader.GetOrdinal("email"));
                    var phoneNumber = reader.GetValue(reader.GetOrdinal("phone_number"));
                    var password = reader.GetValue(reader.GetOrdinal("password"));
                    var firstName = reader.GetValue(reader.GetOrdinal("first_name"));
                    var lastName = reader.GetValue(reader.GetOrdinal("last_name"));

                    client.Email = email == DBNull.Value ? null : email.ToString();
                    client.PhoneNumber = phoneNumber == DBNull.Value ? null : phoneNumber.ToString();
                    client.Password = password == DBNull.Value ? null : password.ToString();
                    client.FirstName = firstName == DBNull.Value ? null : firstName.ToString();
                    client.LastName = lastName == DBNull.Value ? null : lastName.ToString();
                }
            }
        }
        
        connection.Close();

        return new OkObjectResult(client);
    }

    public IActionResult GetClient(string token)
    {
        // открываем подкючение к бд
        connection.Open();

        Client client = new Client();
        
        // запрос
        string query = @"select * from client where token = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = token}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    client.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    client.DateTimeOfRegistration = reader.GetDateTime(reader.GetOrdinal("datetime_of_registration"));
                    client.Token = reader.GetString(reader.GetOrdinal("token"));
                    
                    var email = reader.GetValue(reader.GetOrdinal("email"));
                    var phoneNumber = reader.GetValue(reader.GetOrdinal("phone_number"));
                    var password = reader.GetValue(reader.GetOrdinal("password"));
                    var firstName = reader.GetValue(reader.GetOrdinal("first_name"));
                    var lastName = reader.GetValue(reader.GetOrdinal("last_name"));

                    client.Email = email == DBNull.Value ? null : email.ToString();
                    client.PhoneNumber = phoneNumber == DBNull.Value ? null : phoneNumber.ToString();
                    client.Password = password == DBNull.Value ? null : password.ToString();
                    client.FirstName = firstName == DBNull.Value ? null : firstName.ToString();
                    client.LastName = lastName == DBNull.Value ? null : lastName.ToString();
                }
            }
        }
        
        connection.Close();

        return new OkObjectResult(client);
    }

    public IActionResult GetClientByLogin(string login)
    {
        // открываем подкючение к бд
        connection.Open();

        Client client = new Client();
        
        // запрос
        string query = @"select * from client where email = $1 or phone_number = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = login}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    client.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    client.DateTimeOfRegistration = reader.GetDateTime(reader.GetOrdinal("datetime_of_registration"));

                    var token = reader.GetValue(reader.GetOrdinal("token"));
                    var email = reader.GetValue(reader.GetOrdinal("email"));
                    var phoneNumber = reader.GetValue(reader.GetOrdinal("phone_number"));
                    var password = reader.GetValue(reader.GetOrdinal("password"));
                    var firstName = reader.GetValue(reader.GetOrdinal("first_name"));
                    var lastName = reader.GetValue(reader.GetOrdinal("last_name"));

                    client.Token = token == DBNull.Value ? null : token.ToString();
                    client.Email = email == DBNull.Value ? null : email.ToString();
                    client.PhoneNumber = phoneNumber == DBNull.Value ? null : phoneNumber.ToString();
                    client.Password = password == DBNull.Value ? null : password.ToString();
                    client.FirstName = firstName == DBNull.Value ? null : firstName.ToString();
                    client.LastName = lastName == DBNull.Value ? null : lastName.ToString();
                }
            }
        }
        
        connection.Close();

        return new OkObjectResult(client);
    }

    public IActionResult GetClientOrders(int id)
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        List<Order> orders = new List<Order>();
        
        // запрос
        string query = @"select * from client_order where client_id = $1";

        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = {new() {Value = id}}
        };
        
        // выполняем команду
        using (cmd)
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    Order order = new Order();
                    // перенос значений из строки базы данных в объект класса
                    order.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    order.DateTimeOfCreation = reader.GetDateTime(reader.GetOrdinal("datetime_of_creation"));
                    order.TotalCost = reader.GetDecimal(reader.GetOrdinal("total_cost"));
                    orders.Add(order);
                }
            }
        }
        
        connection.Close();

        foreach (var order in orders)
        {
            OkObjectResult products = GetOrderProducts(order.Id) as OkObjectResult;
            order.OrderProductsList = products.Value as List<OrderProduct>;
        }
        
        return new OkObjectResult(orders);
    }

    public IActionResult AddClient(Client client)
    {
        connection.Open();
        // запрос
        string query =
            @"insert into client(
                   datetime_of_registration,
                   email, phone_number,
                   password, first_name,
                   last_name, token)
                           values ($1, $2, $3, $4, $5, $6, $7)";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = client.DateTimeOfRegistration == null ? DBNull.Value : client.DateTimeOfRegistration.Value},
                new() {Value = client.Email == null ? DBNull.Value : client.Email},
                new(){Value = client.PhoneNumber == null ? DBNull.Value : client.PhoneNumber},
                new(){Value = client.Password == null ? DBNull.Value : client.Password},
                new(){Value = client.FirstName == null ? DBNull.Value : client.FirstName},
                new(){Value = client.LastName == null ? DBNull.Value : client.LastName},
                new(){Value = client.Token}
            }
        };
        // пробуем выолнить
        try
        {
            cmd.ExecuteNonQuery();
            return new AcceptedResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult EditClient(Client client)
    {
        connection.Open();
        // запрос
        string query =
            @"update client set
                   datetime_of_registration = $2,
                   email = $3, phone_number = $4,
                   password =$5, first_name = $6,
                   last_name = $7, token = $8 where id = $1";
        
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = client.Id},
                new() {Value = client.DateTimeOfRegistration == null ? DBNull.Value : client.DateTimeOfRegistration.Value},
                new() {Value = client.Email == null ? DBNull.Value : client.Email},
                new() {Value = client.PhoneNumber == null ? DBNull.Value : client.PhoneNumber},
                new() {Value = client.Password == null ? DBNull.Value : client.Password},
                new() {Value = client.FirstName == null ? DBNull.Value : client.FirstName},
                new() {Value = client.LastName == null ? DBNull.Value : client.LastName},
                new() {Value = client.Token}
            }
        };
        // пробуем выолнить
        try
        {
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new NotFoundResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult DeleteClient(int id)
    {
        connection.Open();
        // запрос
        string query = @"delete from client where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = id},
            }
        };
        // пробуем выолнить
        try
        {
            // выполняем команду
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }
    
}