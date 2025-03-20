using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingFinances : MonoBehaviour
{
    public List<Product> products = new List<Product>();

    public void AddProduct(Product product)
    {
        products.Add(product);
        Debug.Log($"{product.ProductName} added to tracking.");
        Debug.Log($"{product.ProductName} price is {product.Price}.");
    }
    public void ModifyProduct(int index, float newPrice)
    {
        Product tempProduct = products[index];
        tempProduct.Price = newPrice;
        products[index] = tempProduct;
    }

    void Start()
    {
        Product laptop = new Product
        {
            Price = 6900.69f,
            NumberOfTimesSold = 69,
            ProductName = "Laptop",
        };
        AddProduct(laptop);
    }
}
