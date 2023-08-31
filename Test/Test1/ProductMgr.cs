using System;
using System.Collections.Generic;
using System.IO;


namespace Test1
{
        //Create an interface 
        interface IProductMgr
        {
            void WriteRecord(Product prod, string filename);
            List<Product> readAllRecords(string filename);
            void DeleteRecord(int id, string filename);
            void DisplayAllRecords(string filename);
            void updateRecord(int id, string filename, Product product);
            void FindRecord(int id, string filename);
        }
        //Create a child class for the interface that implements all the methods
        class ProductMgr : IProductMgr
        {
            static IProductMgr pmg = ProductFactory.getComponent();
            public void WriteRecord(Product prod, string filename)
            {
                var line = $"{prod.ProductId},{prod.ProductName},{prod.Category},{prod.Price},{prod.VendorDetails}\n";
                File.AppendAllText(filename, line);
            }

            public List<Product> readAllRecords(string filename)
            {
                List<Product> prodList = new List<Product>();
                string[] lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    string[] words = line.Split(',');
                    var prod = new Product
                    {
                        ProductId = int.Parse(words[0]),
                        ProductName = words[1],
                        Category = words[2],
                        Price = double.Parse(words[3]),
                        VendorDetails = words[4]
                    };
                    prodList.Add(prod);
                }
                return prodList;
            }

            private static void bulkInsertRecords(List<Product> records, string filename)
            {
                foreach (var prod in records)
                {
                    pmg.WriteRecord(prod, filename);
                }
            }
            public void DeleteRecord(int id, string filename)
            {
                var content = pmg.readAllRecords(filename);
                var records = content;
                for (int i = 0; i < records.Count; i++)
                {
                    if (records[i].ProductId == id)
                    {
                        records.RemoveAt(i);
                        break;
                    }
                }
                File.Delete(filename);
                bulkInsertRecords(records, filename);
            }
            public void FindRecord(int id, string filename)
            {
                var records = pmg.readAllRecords(filename);
                for (int i = 0; i < records.Count; i++)
                {
                    if (records[i].ProductId == id)
                    {
                        Console.WriteLine($"{records[i].ProductName} has id {records[i].ProductId} ,belongs to {records[i].Category} costs {records[i].Price} and provisioned by {records[i].VendorDetails} vendor");
                        break;
                    }
                }
            }
            public void updateRecord(int id, string filename, Product product)
            {
                var records = pmg.readAllRecords(filename);
                for (int i = 0; i < records.Count; i++)
                {
                    if (records[i].ProductId == id)
                    {
                        records[i].ProductName = product.ProductName;
                        records[i].Category = product.Category;
                        records[i].Price = product.Price;
                        records[i].VendorDetails = product.VendorDetails;
                        break;
                    }
                }
                File.Delete(filename);
                bulkInsertRecords(records, filename);
            }

            public void DisplayAllRecords(string filename)
            {
                List<Product> prodlist = pmg.readAllRecords(filename);
                foreach (var item in prodlist)
                {
                    Console.WriteLine($"{item.ProductName} has id {item.ProductId}");
                }
            }
        }

        class ProductNotFoundException : Exception
        {
            public ProductNotFoundException() : base("The Product ID did not match any product - Retry")
            {

            }
            public ProductNotFoundException(string message) : base(message) { }
        }
}
