using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            PizzaData();
        }

        static void Task1()
        {
            Console.Write("HELLO WORLD ! !");
            Console.Write("HELLO WORLD ! !");
            Console.ReadKey();
        }

        static void Task2()
        {
            Console.WriteLine("HELLO WORLD! !");
            Console.Write("HELLO WORLD! !");
            Console.ReadKey();
        }

        static void IntData()
        {
            int variable = 7;
            Console.WriteLine("Value : " + variable);
            Console.ReadKey();
        }

        static void StringData()
        {
            string str = "I am a String";
            Console.WriteLine("The string is : " + str);
            Console.ReadKey();
        }

        static void CharData()
        {
            char c = 'A';
            Console.WriteLine("The Character is : " + c);
            Console.ReadKey();
        }

        static void FloatData()
        {
            float number = 2.5F;
            Console.WriteLine("The number is : " + number);
            Console.ReadKey();
        }

        static void InputString()
        {
            string str;
            Console.Write("Enter the string : ");
            str = Console.ReadLine();
            Console.WriteLine("The string is : " + str);
            Console.ReadKey();
        }

        static void InputInt()
        {
            int number;
            Console.Write("Enter the number : ");
            number = int.Parse(Console.ReadLine());
            Console.WriteLine("The number is : " + number);
            Console.ReadKey();
        }

        static void InputFloat()
        {
            float number;
            Console.Write("Enter the number : ");
            number = float.Parse(Console.ReadLine());
            Console.WriteLine("The number is : " + number);
            Console.ReadKey();
        }

        static void Task3()
        {
            float length;
            float area;
            Console.Write("Enter the length : ");
            length = float.Parse(Console.ReadLine());
            area = length * length;
            Console.WriteLine("The area is : " + area);
            Console.ReadKey();
        }

        static void Task4()
        {
            int number;
            Console.Write("Enter the number : ");
            number = int.Parse(Console.ReadLine());
            if(number > 50)
            {
                Console.WriteLine("You Passed");
            }
            else
            {
                Console.WriteLine("You Failed");
            }
            Console.ReadKey();
        }

        static void Task5()
        {
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine("Welcome Jack");
            }
            Console.ReadKey();
        }

        static void Task6()
        {
            int number = 0;
            int sum = 0;
            while(number != -1)
            {
                Console.Write("Enter the number : ");
                sum = sum + number;
                number = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("The Sum is : " + sum);
            Console.ReadKey();
        }

        static void Task7()
        {
            int number = 0;
            int sum = 0;
            do
            {
                Console.Write("Enter the number : ");
                sum = sum + number;
                number = int.Parse(Console.ReadLine());
            }
            while (number != -1);
            Console.WriteLine("The Sum is : " + sum);
            Console.ReadKey();
        }

        static void Task8()
        {
            int[] arr = new int[3];
            for(int i = 0; i < 3; i++)
            {
                Console.Write("Enter the element : ");
                arr[i] = int.Parse(Console.ReadLine());
            }
            int largest = arr[0];
            for(int i = 1; i < 3; i++)
            {
                if(largest < arr[i])
                {
                    largest = arr[i];
                }
            }
            Console.WriteLine("The largest number is : " + largest);
            Console.ReadKey();
        }

        static void Lily()
        {
            int age, money = 0, toys = 0, moneyStolen = 0, totalMoney, toyPrice, evenCounter = 1;
            float price, result;
            Console.Write("Enter the age : ");
            age = int.Parse(Console.ReadLine());
            for(int i = 1; i <= age; i++)
            {
                if(i % 2 == 0)
                {
                    money = money + (10 * evenCounter);
                    moneyStolen++;
                    evenCounter++;
                }
                else
                {
                    toys++;
                }
            }
            Console.Write("Enter the price of washing machine : ");
            price = float.Parse(Console.ReadLine());
            Console.Write("Enter the price of toys : ");
            toyPrice = int.Parse(Console.ReadLine());
            totalMoney = money + (toys * toyPrice) - moneyStolen;
            result = price - totalMoney;
            if(result < 0)
            {
                Console.WriteLine("Yes! " + (-result));
            }
            else
            {
                Console.WriteLine("No! " + result);
            }
            Console.ReadKey();
        }

        static void Task9()
        {
            int number1, number2, sum;
            Console.Write("Enter the value : ");
            number1 = int.Parse(Console.ReadLine());
            Console.Write("Enter the value : ");
            number2 = int.Parse(Console.ReadLine());
            sum = Add(number1, number2);
            Console.WriteLine("The sum is : " + sum);
            Console.ReadKey();
        }

        static int Add(int number1, int number2)
        {
            return number1 + number2;
        }

        static void ReadData()
        {
            string path = "C:\\Users\\Dell\\source\\repos\\Lab1\\Lab1\\data.txt";
            if(File.Exists(path))
            {
                string line;
                StreamReader file = new StreamReader(path);
                while((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                file.Close();
            }
            else
            {
                Console.WriteLine("File does not exit");
            }
            Console.ReadKey();
        }

        static void AppendData()
        {
            string path = "C:\\Users\\Dell\\source\\repos\\Lab1\\Lab1\\data.txt";
            StreamWriter file = new StreamWriter(path, true);
            file.WriteLine("Hello");
            file.Flush();
            file.Close();
        }

        static void PizzaData()
        {
            string path = "C:\\Users\\Dell\\source\\repos\\Lab1\\Lab1\\pizzaData.txt";
            string line;
            int i = 0;
            string[] name = new string[2];
            int[] orderNumber = new int[2];
            string[] orderList = new string[2];
            int[,] orderPrice = {  };
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                while((line = file.ReadLine()) != null)
                {
                    name[i] = ParseString(line, 1);
                    orderNumber[i] = int.Parse(ParseString(line, 2));
                    orderList[i] = ParseString(line, 3);
                    i++;
                }
                file.Close();
            }
            /*Console.WriteLine(name[0]);
            Console.WriteLine(orderNumber[0]);
            Console.WriteLine(orderList[0]);
            Console.ReadKey();*/
            Parse(orderPrice, orderList[0]);
        }

        static string ParseString(string line, int field)
        {
            int spaceCount = 1;
            string item = "";
            for(int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                {
                    spaceCount++;
                }
                if(spaceCount == field)
                {
                    item = item + line[i];
                }
            }
            return item;
        }

        static void Parse(int[,] price, string orderList)
        {
            int number = orderList.Length-2;
            char[] order = new char[number];
            for(int i = 0; i < number-1; i++)
            {
                order[i] = orderList[i + 2];
            }
            /*Console.WriteLine(order);
            Console.ReadKey();*/
            for(int i = 0; i < price.Length; i++)
            {
                price[0, i] = Comma(order, i + 1);
            }
            for(int i = 0; i < price.Length; i++)
            {
                Console.WriteLine(price[0, i]);
            }
            Console.ReadKey();
            

        }

        static int Comma(char[] line, int field)
        {
            int commaCount = 1;
            string item = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    commaCount++;
                }
                if (commaCount == field)
                {
                    item = item + line[i];
                }
            }
            int result = int.Parse(item);
            return result;
        }
    }
}
