using System;
using System.IO;

namespace Test1
{
    class ProdmgmtCSV
    {
        const string filename = "../../ProductData.csv";
        static void Main()
        {
            string menuFile = @"C:\Users\sirba\source\repos\Test\Test1\menu.txt";
            bool processing = true;
            do
            {
                string menu = File.ReadAllText(menuFile);
                Console.WriteLine(menu);
                string choice = Utilities.GetString("Enter your choice");
                processing = processMenu(choice);
            } while (processing);
        }

        private static bool processMenu(string choice)
        {
            IProductMgr pmg = ProductFactory.getComponent();
            switch (choice)
            {
                case "N":AddNewHelper(pmg);
                    return true;
                case "D":DeleteHelper(pmg);
                    return true;
                case "AD":DisplayAllHelper(pmg);
                    return true;
                case "U":UpdateHelper(pmg);
                    return true;
                case "F":FindHelper(pmg);
                    return true;
                default: Console.WriteLine("Invalid choice");
                    return false;
            }
        }

        private static void AddNewHelper(IProductMgr pmg)
        {
            int id = Utilities.GetInteger("Enter Id of the product");
            string Name = Utilities.GetString("Enter Name of the product");
            string Category = Utilities.GetString("Enter Category of the product");
            double price = Utilities.GetDouble("Enter price of the product");
            string Vendor = Utilities.GetString("Enter vendor details");
            Product product = new Product
            {
                ProductId = id,
                ProductName = Name,
                Category = Category,
                Price = price,
                VendorDetails = Vendor
            };
            pmg.WriteRecord(product, filename);
            Console.WriteLine("Product added to the file successfully");
        }

        private static void FindHelper(IProductMgr pmg)
        {
            int id = Utilities.GetInteger("Enter the id to find");
            pmg.FindRecord(id, filename);
        }

        private static void UpdateHelper(IProductMgr pmg)
        {
            //void updateRecord(int id, string filename, Product product);
            int id = Utilities.GetInteger("Enter Id of the product");
            string Name = Utilities.GetString("Enter Name of the product");
            string Category = Utilities.GetString("Enter Category of the product");
            double price = Utilities.GetDouble("Enter price of the product");
            string Vendor = Utilities.GetString("Enter vendor details");
            Product product = new Product
            {
                ProductId = id,
                ProductName = Name,
                Category = Category,
                Price = price,
                VendorDetails = Vendor
            };
            pmg.updateRecord(id, filename, product);
            Console.WriteLine("Product Updated successfully");
        }

        private static void DisplayAllHelper(IProductMgr pmg)
        {
            Console.WriteLine("The products available are listed below:");
            pmg.DisplayAllRecords(filename); 
        }

        private static void DeleteHelper(IProductMgr pmg)
        {
            int id = Utilities.GetInteger("Enter the Id to delete");
            pmg.DeleteRecord(id, filename);
            Console.WriteLine("Product deleted");
        }
    }
}
