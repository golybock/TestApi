using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestsApi;

public class ApiTest
{
    private HttpClient _httpClient;

    public ApiTest()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }
    
    [Fact]
    public async Task GetProducts()
    {
        var response = await _httpClient.GetAsync("/Products/GetProducts");
        // var responseJson = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddNotValidProduct()
    {
        // в бд нет категории с индексом 0
        string badProduct = "{\"id\": 0,\"name\": \"string\",\"price\": 1,\"photoUrl\": \"string\",\"categoryId\": 0 }";
        StringContent product = new StringContent(badProduct, Encoding.UTF8, "application/json") ;
        var response = await _httpClient.PostAsync("/Products/AddProduct", product);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task AddValidProduct()
    {
        // продукт может не иметь категории
        string badProduct = "{\"id\": 0,\"name\": \"productName\",\"price\": 100,\"photoUrl\": \"photoUrl\",\"categoryId\": null }";
        StringContent product = new StringContent(badProduct, Encoding.UTF8, "application/json") ;
        var response = await _httpClient.PostAsync("/Products/AddProduct", product);
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }

    [Fact]
    public async Task AddFreeProduct()
    {
        // цена не может быть отрицательной
        string badProduct = "{\"id\": 0,\"name\": \"productName\",\"price\": -100,\"photoUrl\": \"photoUrl\",\"categoryId\": null }";
        StringContent product = new StringContent(badProduct, Encoding.UTF8, "application/json") ;
        var response = await _httpClient.PostAsync("/Products/AddProduct", product);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteNotValidProduct()
    {
        // такого продукта не существует
        int badProductId = 11321312;
        var response = await _httpClient.PostAsync($"/Products/DeleteProduct?productId={badProductId}", null);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    // работает единоразово, нужно менять id проудкта каждый раз
    [Fact]
    public async Task DeleteValidProduct()
    {
        // продукт точно существует
        int validProductId = 55;
        var response = await _httpClient.PostAsync($"/Products/DeleteProduct?productId={validProductId}", null);
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }
    
    [Fact]
    public async Task ValidUpdateProduct()
    {
        string badProduct = "{\"id\": 56,\"name\": \"productName\",\"price\": 15600,\"photoUrl\": \"photoUrl\",\"categoryId\": null }";
        StringContent product = new StringContent(badProduct, Encoding.UTF8, "application/json") ;
        var response = await _httpClient.PostAsync("/Products/UpdateProduct", product);
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }
    
    [Fact]
    public async Task NotValidUpdateProduct()
    {
        // обновляем на отрицательную цену
        string badProduct = "{\"id\": 56,\"name\": \"productName\",\"price\": -15600,\"photoUrl\": \"photoUrl\",\"categoryId\": null }";
        StringContent product = new StringContent(badProduct, Encoding.UTF8, "application/json") ;
        var response = await _httpClient.PostAsync("/Products/UpdateProduct", product);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateNotExistsProduct()
    {
        // обновляем не существующий продукт
        string badProduct = "{\"id\": 24242,\"name\": \"productName\",\"price\": 10,\"photoUrl\": \"photoUrl\",\"categoryId\": null }";
        StringContent product = new StringContent(badProduct, Encoding.UTF8, "application/json") ;
        var response = await _httpClient.PostAsync("/Products/UpdateProduct", product);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
}