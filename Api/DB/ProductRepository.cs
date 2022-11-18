using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Api.DB;

public class ProductRepository : IProductsService
{
    private string _connectionString;
    NpgsqlConnection connection;

    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
        // открываем подкючение к бд
        connection.Open();
    }

    public List<Product> GetProducts()
    {
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
                    product.Name = reader.GetValue(reader.GetOrdinal("name")).ToString();
                    product.Price = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("price")));
                    product.PhotoUrl = reader.GetValue(reader.GetOrdinal("photo_url")).ToString();
                    // получение категории
                    var category = reader.GetValue(reader.GetOrdinal("category_id"));
                    if (category == DBNull.Value)
                        product.CategoryId = null;
                    else
                        product.CategoryId = Convert.ToInt32(category);

                    // сохранение объекта в списке
                    products.Add(product);
                }
            }
        }
        return products;
    }

    public Product? GetProductById(int id)
    {
        Product? product = new Product();
        
        // запрос
        string query = @"select * from product where id = $1";
        
        // выполняем команду
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = id}
            }
        };
        try
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    // перенос значений из строки базы данных в объект класса
                    product.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    product.Name = reader.GetValue(reader.GetOrdinal("name")).ToString();
                    product.Price = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("price")));
                    product.PhotoUrl = reader.GetValue(reader.GetOrdinal("photo_url")).ToString();
                    var category = reader.GetValue(reader.GetOrdinal("category_id"));
                    if (category == DBNull.Value)
                        product.CategoryId = null;
                    else
                        product.CategoryId = Convert.ToInt32(category);
                }
            }
        }
        catch (Exception e)
        {
            return null;
        }

        return product;
    }

    public List<ProductCategory> GetCategories()
    {
        List<ProductCategory> productCategories = new List<ProductCategory>();
        // запрос
        string query = @"select * from product_category";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
        // пробуем выолнить
        try
        {
            // создаем reader
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                // проход по всем строкам таблицы
                while (reader.Read())
                {
                    ProductCategory productCategory = new ProductCategory();
                    // перенос значений из строки базы данных в объект класса
                    productCategory.Id = reader.GetInt32(reader.GetOrdinal("id"));
                    productCategory.Name = reader.GetValue(reader.GetOrdinal("name")).ToString();
                    productCategories.Add(productCategory);
                }
            }
        }
        catch (Exception e)
        {
            return productCategories;
        }

        return productCategories;
    }

    public IActionResult AddCategory(ProductCategory productCategory)
    {
        // запрос
        string query =
            @"insert into product_category(name)
                           values ($1)";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = productCategory.Name},
            }
        };
        // пробуем выолнить
        try
        {
            cmd.ExecuteNonQuery();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            // Console.WriteLine(e.ToString());
            return new BadRequestResult();
        }

        return new AcceptedResult();
    }

    public IActionResult DeleteProduct(int productId)
    {
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
        return cmd.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
    }

    public IActionResult AddProduct(Product product)
    {
        // запрос
        string query =
            @"insert into product(name, price, photo_url, category_id)
                           values ($1, $2, $3, $4)";
        // дополняем запрос параметрами
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new() {Value = product.Name},
                new() {Value = product.Price},
                new() {Value = product.PhotoUrl == null ? DBNull.Value : product.PhotoUrl},
                new() {Value = product.CategoryId == null ? DBNull.Value : product.CategoryId}
            }
        };
        // пробуем выолнить
        try
        {
            cmd.ExecuteNonQuery();
        }
        // что-то не так с данными
        catch (Exception e)
        {
            // Console.WriteLine(e.ToString());
            return new BadRequestResult();
        }

        return new AcceptedResult();
    }

    public IActionResult UpdateProduct(Product product)
    {
        // запрос
        string query =
            @"update product set
                    name = $2,
                    price = $3,
                    photo_url = $4,
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
                new() {Value = product.Price},
                new() {Value = product.PhotoUrl},
                new() {Value = product.CategoryId == null ? DBNull.Value : product.CategoryId}
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
    }

    // на всякий случай деструктор
    ~ProductRepository()
    {
        connection.Close();
    }
}