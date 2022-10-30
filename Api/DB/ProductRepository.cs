using Api.Models;
using Npgsql;

namespace Api.DB;

public class ProductRepository
{
     public string ConnectionString;
    NpgsqlConnection connection;

    public ProductRepository(string connectionString)
    {
        ConnectionString = connectionString;
        connection = new NpgsqlConnection(ConnectionString);
        // открываем подкючение к бд
        connection.Open();
    }

    public List<Product> GetAllProducts()
    {
        // для хранения продуктов из бд в виде объектов
        List<Product> products = new List<Product>();
        
        // запрос
        string query = @"select * from products";
        
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
        string query = @"select * from products where id = $1";
        
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
    
    public int DeleteProduct(int productId)
    {
        // запрос
        string query = @"delete from products where id = ($1)";
        
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
        return cmd.ExecuteNonQuery();
    }

    public bool AddProduct(Product product)
    {
        // запрос
        string query =
            @"insert into products(name, price, photo_url, category_id)
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
            return false;
        }

        return true;
    }

    public int UpdateProduct(Product product)
    {
        // запрос
        string query =
            @"update products set
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
            return cmd.ExecuteNonQuery();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return 0;
        }
    }

    // на всякий случай деструктор
    ~ProductRepository()
    {
        connection.Close();
    }
}