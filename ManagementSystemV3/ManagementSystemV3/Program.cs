using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemV3.Classes;
using System.IO;

namespace Signinup
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>();
            List<Book> books = new List<Book>();
            int choice;
            int subChoice;
            int accountTrack;
            string path = "data.txt";
            string path1 = "Books.txt";
            LoadData(users,  path);
            LoadBooks(books);
            while (true)
            {
                Console.Clear();
                choice = Menu();
                if (choice == 1)
                {
                    Console.Clear();
                    string name, pass;
                    User item = new User();
                    Console.SetCursorPosition(10, 5);
                    Console.Write("Enter the name : ");
                    name = Console.ReadLine();
                    Console.SetCursorPosition(10, 6);
                    Console.Write("Enter the password : ");
                    pass = Console.ReadLine();
                    if (IsValid(name, pass, users) == 9999)
                    {
                        while(item.role != "Admin" && item.role != "Client")
                        {
                            Console.SetCursorPosition(10, 7);
                            Console.Write("Enter the role : ");
                            item.role = Console.ReadLine();
                        }
                        item.username = name;
                        item.password = pass;
                        users.Add(item);
                        SaveData(users, path);
                        Console.SetCursorPosition(10, 8);
                        Console.WriteLine("Signed Up Successfully");
                    }
                    else
                    {
                        Console.SetCursorPosition(10, 8);
                        Console.WriteLine("User already present");
                    }
                    Console.ReadKey();
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    string name, pass;
                    Console.SetCursorPosition(10, 5);
                    Console.Write("Enter the name :");
                    name = Console.ReadLine();
                    Console.SetCursorPosition(10, 6);
                    Console.Write("Enter the password :");
                    pass = Console.ReadLine();
                    int i = IsValid(name, pass, users);
                    if (i != 9999)
                    {
                        if (name == users[i].username && pass == users[i].password)
                        {
                            accountTrack = i;
                            if (users[i].role == "Admin")
                            {
                                while(true)
                                {
                                    subChoice = AdminMenu();
                                    if(subChoice == 1)
                                    {
                                        ViewBooks(books);
                                    }
                                    else if(subChoice == 2)
                                    {
                                        Book temp = new Book();
                                        Console.SetCursorPosition(10, 5);
                                        Console.WriteLine("Enter the name of the book : ");
                                        temp.name = Console.ReadLine();
                                        if(SearchBooks(books, temp.name, 1) == 9999)
                                        {
                                            Console.SetCursorPosition(10, 6);
                                            Console.WriteLine("Enter the name of the author : ");
                                            temp.author = Console.ReadLine();
                                            books.Add(temp);
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(10, 7);
                                            Console.WriteLine("Book already present");
                                        }
                                        SaveBooks(books, path1);
                                        Console.ReadKey();
                                    }
                                    else if(subChoice == 3)
                                    {
                                        Console.Clear();
                                        int mode;
                                        string searchName;
                                        Console.SetCursorPosition(10, 5);
                                        Console.WriteLine("1. Search by bookname");
                                        Console.SetCursorPosition(10, 6);
                                        Console.WriteLine("2. Search by authorname");
                                        Console.SetCursorPosition(10, 7);
                                        Console.Write("Enter your choice : ");
                                        mode = int.Parse(Console.ReadLine());
                                        Console.Clear();
                                        Console.SetCursorPosition(10, 5);
                                        Console.Write("3. Enter the name : ");
                                        searchName = Console.ReadLine();
                                        int index = SearchBooks(books, searchName, mode);
                                        if(index != 9999)
                                        {
                                            Console.SetCursorPosition(10, 6);
                                            Console.WriteLine(books[index].name + "      " + books[index].author);
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(10, 6);
                                            Console.WriteLine("Book not found");
                                        }
                                        Console.ReadKey();
                                    }
                                    else if(subChoice == 4)
                                    {
                                        string bookName;
                                        Console.SetCursorPosition(10, 5);
                                        Console.Write("Enter the name of the book : ");
                                        bookName = Console.ReadLine();
                                        int index = SearchBooks(books, bookName, 1);
                                        if (index != 9999)
                                        {
                                            books.RemoveAt(index);
                                            Console.SetCursorPosition(10, 6);
                                            Console.WriteLine("Book removed successfully");
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(10, 6);
                                            Console.WriteLine("Book not present");
                                        }
                                        SaveBooks(books, path1);
                                        Console.ReadKey();
                                    }
                                    else if(subChoice == 5)
                                    {
                                        Console.Clear();
                                        string password;
                                        Console.SetCursorPosition(10, 5);
                                        Console.Write("Enter the old password : ");
                                        password = Console.ReadLine();
                                        if(password == users[i].password)
                                        {
                                            Console.SetCursorPosition(10, 6);
                                            Console.WriteLine("Enter the new password : ");
                                            users[i].password = Console.ReadLine();
                                            Console.SetCursorPosition(10, 7);
                                            Console.WriteLine("Password changed successfully");
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(10, 7);
                                            Console.WriteLine("Wrong password");
                                        }
                                        Console.ReadKey();
                                        SaveData(users, path);
                                    }
                                    else if(subChoice == 6)
                                    {
                                        string path2 = "BackUpBooks.txt";
                                        string path3 = "BackUpData.txt";
                                        SaveBooks(books, path2);
                                        SaveData(users, path3);
                                        Console.SetCursorPosition(10, 5);
                                        Console.Write("BackUp Successfull");
                                        Console.ReadKey();
                                    }
                                    else if(subChoice == 7)
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (users[i].role == "Client")
                            {
                                Console.SetCursorPosition(10, 7);
                                Console.WriteLine("Signed In Successfully");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(10, 7);
                            Console.WriteLine("Invalid");
                            Console.ReadKey();
                        }
                    }
                }
                else if (choice == 3)
                {
                    break;
                }
            }
        }

        static int IsValid(string name, string pass, List<User> u)
        {
            for (int i = 0; i < u.Count; i++)
            {
                if (u[i].username == name)
                {
                    return i;
                }
            }
            return 9999;
        }
        static int Menu()
        {
            int choice;
            Console.WriteLine("                                                 _      _ _                             _____           _                  ");
            Console.WriteLine("                                                | |    (_) |                           / ____|         | |                 ");
            Console.WriteLine("                                                | |     _| |__  _ __ __ _ _ __ _   _  | (___  _   _ ___| |_ ___ _ __ ___   ");
            Console.WriteLine("                                                | |    | | '_ \\| '__/ _` | '__| | | |  \\___ \\| | | / __| __/ _ \\ '_ ` _ \\  ");
            Console.WriteLine("                                                | |____| | |_) | | | (_| | |  | |_| |  ____) | |_| \\__ \\ ||  __/ | | | | | ");
            Console.WriteLine("                                                |______|_|_.__/|_|  \\__,_|_|   \\__, | |_____/ \\__, |___/\\__\\___|_| |_| |_| ");
            Console.WriteLine("                                                                                __/ |          __/ |                       ");
            Console.WriteLine("                                                                               |___/          |___/                        ");
            Console.WriteLine();
            Console.WriteLine("************************************************************************************************************************************************************************ ");
            Console.WriteLine();
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

        static void SaveData(List<User> u, string path)
        {
            StreamWriter file = new StreamWriter(path);
            for (int i = 0; i < u.Count; i++)
            {
                file.WriteLine(u[i].username + "," + u[i].password + "," + u[i].role);
            }
            file.Flush();
            file.Close();
        }

        static void LoadData(List <User> u, string path)
        {
            string[] arr = new string[3];
            string record;
            StreamReader file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                User info = new User();
                record = file.ReadLine();
                arr = record.Split(',');
                info.username = arr[0];
                info.password = arr[1];
                info.role = arr[2];
                u.Add(info);
            }
            file.Close();
        }

        static int AdminMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("                                                            _           _         __  __                   ");
            Console.WriteLine("                                                   /\\      | |         (_)       |  \\/  |                  ");
            Console.WriteLine("                                                  /  \\   __| |_ __ ___  _ _ __   | \\  / | ___ _ __  _   _  ");
            Console.WriteLine("                                                 / /\\ \\ / _` | '_ ` _ \\| | '_ \\  | |\\/| |/ _ \\ '_ \\| | | | ");
            Console.WriteLine("                                                / ____ \\ (_| | | | | | | | | | | | |  | |  __/ | | | |_| | ");
            Console.WriteLine("                                               /_/    \\_\\__,_|_| |_| |_|_|_| |_| |_|  |_|\\___|_| |_|\\__,_| ");
            Console.WriteLine();
            Console.WriteLine("************************************************************************************************************************************************************************ ");
            Console.WriteLine("           1. View Books");
            Console.WriteLine("           2. Add Books");
            Console.WriteLine("           3. Search Books");
            Console.WriteLine("           4. Delete Books");
            Console.WriteLine("           5. Change Password");
            Console.WriteLine("           6. Backup");
            Console.WriteLine("           7. Logout");
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

        static void ViewBooks(List<Book> books)
        {
            Console.Clear();
            for (int i = 0; i < books.Count; i++)
            {
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("Book Name");
                Console.SetCursorPosition(37, 4);
                Console.WriteLine("Author Name");
                Console.SetCursorPosition(10, 5 + i);
                Console.Write(books[i].name);
                Console.SetCursorPosition(37, 5 + i);
                Console.WriteLine(books[i].author);
            }
            Console.ReadKey();
        }

        static void SaveBooks(List<Book> books, string path)
        {
            StreamWriter file = new StreamWriter(path);
            for (int i = 0; i < books.Count; i++)
            {
                file.Write(books[i].name + ',');
                file.WriteLine(books[i].name);
            }
            file.Flush();
            file.Close();
        }

        static void LoadBooks(List<Book> books)
        {
            string path = "Books.txt";
            string[] split = new string[2];
            string record;
            StreamReader file = new StreamReader(path);
            if(File.Exists(path))
            {
                while (!file.EndOfStream)
                {
                    record = file.ReadLine();
                    Book temp = new Book();
                    split = record.Split(',');
                    temp.name = split[0];
                    temp.author = split[1];
                    books.Add(temp);
                }
            }
            file.Close();
        }

        static int SearchBooks(List<Book> books, string name, int mode)
        {
            for(int i = 0; i < books.Count; i++)
            {
                if(mode == 1)
                {
                    if (books[i].name == name)
                    {
                        return i;
                    }
                }
                else if(mode == 2)
                {
                    if(books[i].author == name)
                    {
                        return i;
                    }
                }
            }
            return 9999;
        }
    }
}
