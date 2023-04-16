using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signinup.Classes;
using System.IO;

namespace Signinup
{
    class Program
    {
        static void Main(string[] args)
        {
            User[] u = new User[10];
            int index = 0;
            int choice = 0;
            string path = "C:\\Users\\Dell\\source\\repos\\Signinup\\Signinup\\data.txt";
            LoadData(u, ref index, path);
            while (true)
            {
                choice = Menu();
                if (choice == 1)
                {
                    Console.Clear();
                    string name, pass;
                    u[index] = new User();
                    Console.Write("Enter the name :");
                    name = Console.ReadLine();
                    Console.Write("Enter the password :");
                    pass = Console.ReadLine();
                    if (IsValid(name, pass, u, index) == 9999)
                    {
                        u[index].username = name;
                        u[index].password = pass;
                        u[index].role = "Client";
                        index++;
                        SaveData(u, index, path);
                        Console.WriteLine("Signed Up Successfully");
                    }
                    else
                    {
                        Console.WriteLine("User already present");
                    }
                    Console.ReadKey();
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    string name, pass;
                    Console.Write("Enter the name :");
                    name = Console.ReadLine();
                    Console.Write("Enter the password :");
                    pass = Console.ReadLine();
                    int i = IsValid(name, pass, u, index);
                    if (i != 9999)
                    {
                        if (name == u[i].username && pass == u[i].password)
                        {
                            if (u[i].role == "Admin")
                            {
                                Console.WriteLine("Signed In Successfully");
                                Console.ReadKey();
                            }
                            else if (u[i].role == "Client")
                            {
                                Console.WriteLine("Signed In Successfully");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid");
                            Console.ReadKey();
                        }
                    }
                }
                else if(choice == 3)
                {
                    break;
                }
            }
        }

        static int IsValid(string name, string pass, User[] u, int index)
        {
            for(int i = 0; i < index; i++)
            {
                if(u[i].username == name)
                {
                    return i;
                }
            }
            return 9999;
        }
        static int Menu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("           1. Sign Up");
            Console.WriteLine("           2. Sign In");
            Console.WriteLine("           3. Exit");
            Console.Write("           Enter your choice : ");
            string str = Console.ReadLine();
            if (int.TryParse(str, out choice) == true)
            {
                return choice;
            }
            else
            {
                return 9999;
            } 
        }

        static void SaveData(User[] u, int index, string path)
        {
            StreamWriter file = new StreamWriter(path);
            for(int i = 0; i < index; i++)
            {
                file.WriteLine(u[i].username + "," + u[i].password + "," + u[i].role);
            }
            file.Flush();
            file.Close();
        }

        static void LoadData(User[] u, ref int index, string path)
        {
            string[] arr = new string[3];
            string record;
            index = 0;
            StreamReader file = new StreamReader(path);
            while(!file.EndOfStream)
            {
                u[index] = new User();
                record = file.ReadLine();
                arr = record.Split(',');
                u[index].username = arr[0];
                u[index].password = arr[1];
                u[index].role = arr[2];
                index++;
            }
            file.Close();
        }
    }
}
