using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Dell\\source\\repos\\Lab1\\Lab1\\pizzaData.txt";
            string line;
            int size = 0;
            int order = 0;
            int orderPrice = 0;
            Console.Write("Enter the no of orders : ");
            order = int.Parse(Console.ReadLine());
            Console.Write("Enter the minimum price : ");
            orderPrice = int.Parse(Console.ReadLine());
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    size++;
                }
                file.Close();
            }
            int i = 0;
            string[] name = new string[size];
            int[] orderNumber = new int[size];
            string[] orderList = new string[size];
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    name[i] = ParseString(line, 1);
                    orderNumber[i] = int.Parse(ParseString(line, 2));
                    orderList[i] = ParseString(line, 3);
                    i++;
                }
                file.Close();
            }
            for(int j = 0; j < size; j++)
            {
                orderList[j] = orderList[j].Substring(1, orderList[j].Length - 1);
                orderList[j] = orderList[j].Substring(0, orderList[j].Length - 1);
                orderList[j] = orderList[j].Trim();
            }
            for(int j = 0; j < size; j++)
            {
            int counter = 0;
                for(int k = 0; k < orderList[j].Length; k++)
                {
                    if(Parse(orderList[j], k+1, orderList[j].Length) >= orderPrice)
                    {
                        counter++;
                    }
                }
                if(counter >= order)
                {
                    Console.WriteLine(name[j]);
                }
            }
            Console.ReadKey();
        }
        static string ParseString(string line, int field)
        {
            int spaceCount = 1;
            string item = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (spaceCount == field)
                {
                    item = item + line[i];
                }
                if (line[i] == ' ')
                {
                    spaceCount++;
                }
            }
            return item;
        }

        static int Parse(string line, int field, int length)
        {
            int commaCount = 1;
            string item = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    commaCount++;
                }
                else if (commaCount == field)
                {
                    item = item + line[i];
                }
            }
            if(item != "")
            {
                int result = int.Parse(item);
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}
