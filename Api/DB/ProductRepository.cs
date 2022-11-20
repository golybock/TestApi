using Api.Models.Client;
using Api.Models.Customer;
using Api.Models.Order;
using Api.Models.Product;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Api.DB;

public class ProductRepository : IProductsService, IOrderService, ICustomerService, IClientService
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public IActionResult GetProducts()
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        List<Product> products = new List<Product>();
        
        // запрос
        string query = @"select * from product";
        
        // выполняем команду
        using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    Product product = new Product();
                    // перенос значений из строки базы данных в объект класса
                    product.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    product.Name = reader.GetString(reader.GetOrdinal("name"));
                    product.CurrentPrice = reader.GetDecimal(reader.GetOrdinal("current_price"));
                    product.Sale = reader.GetDecimal(reader.GetOrdinal("sale"));
                    product.Category.Id = reader.GetInt32(reader.GetOrdinal("category_id"));
                    // сохранение объекта в списке
                    products.Add(product);
                }
            }
        }
        
        connection.Close();
        
        return new OkObjectResult(products);
    }

    public IActionResult GetProductById(int id)
    {
        // открываем подкючение к бд
        connection.Open();
        
        // для хранения продуктов из бд в виде объектов
        Product product = new Product();
        
        // запрос
        string query = @"select * from product where id = $1";
        
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
                    product.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    product.Name = reader.GetString(reader.GetOrdinal("name"));
                    product.CurrentPrice = reader.GetDecimal(reader.GetOrdinal("current_price"));
                    product.Sale = reader.GetDecimal(reader.GetOrdinal("sale"));
                    product.Category.Id = reader.GetInt32(reader.GetOrdinal("category_id"));
                }
            }
        }
        
        connection.Close();
        
        return new OkObjectResult(product);
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
            return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
        
    }

    public IActionResult DeleteProduct(Product product)
    {
        return DeleteProduct(product.Id);
    }

    public IActionResult AddProduct(Product product)
    {
        connection.Open();
        // запрос
        string query =
            @"insert into product(name, current_price, sale, category_id)
                           values ($1, $2, $3, $4)";
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
            return new AcceptedResult();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            // Console.WriteLine(e.ToString());
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult UpdateProduct(Product product)
    {
        connection.Close();
        // запрос
        string query =
            @"update product set
                    name = $2,
                    current_price = $3,
                    sale = $4,
                    category_id = $5
            where id = $1";
        // дополняем запрос параметрами
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

    public IActionResult DeleteProductCategory(Category category)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_category where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = category.Id},
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

    public IActionResult DeleteBrand(Brand brand)
    {
        connection.Open();
        // запрос
        string query = @"delete from brand where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = brand.Id},
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
        throw new NotImplementedException();
    }

    public IActionResult GetProductBrands(Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetBrands()
    {
        throw new NotImplementedException();
    }

    public IActionResult AddBrandToProduct(Product product, Brand brand)
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
                new() {Value = product.Id},
                new() {Value = brand.Id}
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

    public IActionResult AddBrandsToProduct(Product product, List<Brand> brands)
    {
        try
        {
            foreach (var brand in brands)
            {
                AddBrandToProduct(product, brand);
            }
            return new AcceptedResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }

    public IActionResult DeleteBrandFromProduct(Product product, Brand brand)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_brand where product_id = $1 and brand_id = $2";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = product.Id},
                new() { Value = brand.Id}
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

    public IActionResult SetProductBrands(Product product, List<Brand> brands)
    {
        try
        {
            ClearProductBrands(product);
            AddBrandsToProduct(product, brands);
            return new AcceptedResult();
        }
        catch
        {
            return new BadRequestResult();
        }

    }

    public IActionResult ClearProductBrands(Product product)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_brand where product_id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = product.Id},
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

    public IActionResult AddProductPhoto(Product product, ProductPhoto productPhoto)
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
                new() {Value = product.Id},
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
            // Console.WriteLine(e.ToString());
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult EditProductPhoto(ProductPhoto productPhoto)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProductPhoto(ProductPhoto productPhoto)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_photo where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productPhoto.Id},
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

    public IActionResult EditProductPhoto(Product product, ProductPhoto productPhoto)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddProductPhotos(Product product, List<ProductPhoto> productPhotos)
    {
        try
        {
            foreach (var photo in productPhotos)
            {
                AddProductPhoto(product, photo);
            }

            return new AcceptedResult();
        }
        catch
        {
            return new BadRequestResult();
        }
    }

    public IActionResult SetProductPhotos(Product product, List<ProductPhoto> productPhotos)
    {
        throw new NotImplementedException();
    }

    public IActionResult ClearProductPhotos(Product product)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_photo where product_id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = product.Id},
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

    public IActionResult AddNewProductPrice(Product product, ProductPrice productPrice)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProductPrice(ProductPrice productPrice)
    {
        connection.Open();
        // запрос
        string query = @"delete from product_price where id = $1 and product_id = $2";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = productPrice.Id}
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
        throw new NotImplementedException();
    }

    public IActionResult GetOrders()
    {
        throw new NotImplementedException();
    }

    public IActionResult AddOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteOrder(Order order)
    {
        return DeleteOrder(order.Id);
    }

    public IActionResult DeleteOrder(int id)
    {
        connection.Open();
        // запрос
        string query = @"delete from order where id = $1";
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
        throw new NotImplementedException();
    }

    public IActionResult GetOrderProducts(Order order)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddProductToOrder(Order order, Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddProductsToOrder(Order order, List<Product> products)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProductFromOrder(Order order, Product product)
    {
        connection.Open();
        // запрос
        string query = @"delete from order_products where order_id = $1 and product_id = $2";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() { Value = order.Id},
                new() {Value = product.Id},
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
    
    public IActionResult SetOrderProducts(Order order, List<OrderProducts> orderProducts)
    {
        throw new NotImplementedException();
    }

    public IActionResult ClearOrderProducts(Order order)
    {
        connection.Open();
        // запрос
        string query = @"delete from order_products where order_id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = order.Id},
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

    public IActionResult AddStatus(OrderStatus orderStatus)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteStatus(OrderStatus orderStatus)
    {
        connection.Open();
        // запрос
        string query = @"delete from order_status where id = $1";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = orderStatus.Id},
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

    public IActionResult EditOrderStatus(OrderStatus orderStatus)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddOrderStatus(Order order, OrderStatus orderStatus)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteOrderStatus(Order order, OrderStatus orderStatus)
    {
        connection.Open();
        // запрос
        string query = @"delete from order_statuses where order_id = $1 and order_status_id = $2";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = order.Id},
                new() {Value = orderStatus.Id},
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
        throw new NotImplementedException();
    }

    public IActionResult AddCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public IActionResult EditCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteCustomer(Customer customer)
    {
        return DeleteCustomer(customer.Id);
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

    public IActionResult GetCustomerProducts(Customer customer)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetClient(int id)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetClient(string token)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetClientOrders(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddClient(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult EditClient(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteClient(Client client)
    {
        return DeleteClient(client.Id);
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