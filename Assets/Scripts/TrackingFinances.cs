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
    public void ModifyProduct(int index, float newPrice, int newSoldNum)
    {
        Product tempProduct = products[index];
        tempProduct.Price = newPrice;
        tempProduct.NumberOfTimesSold = newSoldNum;
        
        products[index] = tempProduct;
        Debug.Log("Product " + index + " shoulde be " + newPrice + " but is " + products[index].Price + " intead");
    }

    void Start()
    {
        Product globe = new Product
        {
            Price = 500f,
            NumberOfTimesSold = 0,
            ProductName = "Globe",
        };
        AddProduct(globe);
        Product laptop = new Product
        {
            Price = 1000f,
            NumberOfTimesSold = 0,
            ProductName = "Laptop",
        };
        AddProduct(laptop);
        Product apple = new Product
        {
            Price = 10f,
            NumberOfTimesSold = 0,
            ProductName = "Apple",
        };
        AddProduct(apple);
        
    }
}
