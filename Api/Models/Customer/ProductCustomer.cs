using System.ComponentModel.DataAnnotations;

namespace Api.Models.Customer;

public class ProductCustomer
{
    public ProductCustomer()
    {
        Customer = new Customer();
        Product = new Product.Product();
    }

    public ProductCustomer(int id, Customer customer, Product.Product product)
    {
        Id = id;
        Customer = customer;
        Product = product;
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Customer required")]
    public Customer Customer { get; set; }
    [Required(ErrorMessage = "ProductRequired")]
    public Product.Product Product { get; set; }
}