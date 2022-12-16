
namespace Api.Database.RepositoryInterfaces.Product.Category;

public interface ICategoryRepository
{
    public Models.Product.Category.Category GetCategory(int id);
    public List<Models.Product.Category.Category> GetCategories();
    public 
}