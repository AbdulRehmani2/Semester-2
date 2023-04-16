using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange1.Classes;

namespace Challange1
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = new Product[20];
            int index = 0;
            while (true)
            {
                int choice = Menu();
                if (choice == 1)
                {
                    AddProduct(products, ref index);
                }
                else if (choice == 2)
                {
                    ShowProduct(products, index);
                    Console.ReadKey();
                }
                else if (choice == 3)
                {
                    float total = TotalWorth(products, index);
                    Console.WriteLine("The total worth is : {0}", total);
                    Console.ReadKey();
                }
                else if(choice == 4)
                {
                    break;
                }
            }
        }

        static int Menu()
        {
            Console.Clear();
            int choice = 0;
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Show Product");
            Console.WriteLine("3. Total Worth");
            Console.WriteLine("4. Exit");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }

        static void AddProduct(Product[] s, ref int index)
        {
            Console.Clear();
            s[index] = new Product();
            int id;
            string name;
            Console.Write("Enter the name of the Product : ");
            name = Console.ReadLine();
            Console.Write("Enter the ID : ");
            id = int.Parse(Console.ReadLine());
            if (IsValid(id, s, index))
            {
                s[index].name = name;
                s[index].id = id;
                Console.Write("Enter the catagory : ");
                s[index].catagory = Console.ReadLine();
                Console.Write("Enter the price : ");
                s[index].price = float.Parse(Console.ReadLine());
                Console.Write("Enter the Brand Name : ");
                s[index].brandName = Console.ReadLine();
                Console.Write("Enter the country : ");
                s[index].country = Console.ReadLine();
                index++;
            }
            else
            {
                Console.WriteLine("Cannot assign this ID");
                Console.ReadKey();
                AddProduct(s, ref index);
            }
        }

        static void ShowProduct(Product[] s, int index)
        {
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine("Name : {0}   ID : {1}   Price : {2}   Catagory : {3}   Brand Name {4}   Country {5}", s[i].name, s[i].id, s[i].price, s[i].catagory, s[i].brandName, s[i].country);
            }
        }

        static bool IsValid(int id, Product[] s, int index)
        {
            for (int i = 0; i < index; i++)
            {
                if (id == s[i].id)
                {
                    return false;
                }
            }
            return true;
        }

        static float TotalWorth(Product[] p, int index)
        {
            float total = 0;
            for(int i = 0; i < index; i++)
            {
                total = total + p[i].price;
            }
            return total;
        }
    }
}
