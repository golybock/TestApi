using Api.Models.Product.Color;

namespace Api.Database.RepositoryInterfaces.Product.Color;

public interface IColorRepository
{
    public Colors GetColor(int id);
    public List<Colors> GetColors();
    public int AddColor(Colors colors);
    public int UpdateColor(Colors colors);
    public bool DeleteColor(int id);
}