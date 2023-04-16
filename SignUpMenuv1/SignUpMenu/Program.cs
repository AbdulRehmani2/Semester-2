using System;
using System.IO;
using System.Threading;

namespace SignUpMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            int index = 0;
            string[] name = new string[10];
            string[] password = new string[10];
            string[] books = new string[100];
            string[] author = new string[100];
            int bookIndex = 0;
            int currentIndex = 0;
            LoadData(name, password, ref index);
            LoadBooks(books, author, ref bookIndex);
            while(true)
            {
                int choice = 9999;
                while(choice == 9999)
                {
                    Console.Clear();
                    choice = Menu();
                }
                if (choice == 1)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(10, 5);
                        Console.Write("Enter your name : ");
                        name[index] = Console.ReadLine();
                        if (IsValid(name, name[index], index))
                        {
                            break;
                        }
                        Console.SetCursorPosition(10, 6);
                        Console.WriteLine("Enter a valid username");
                        Console.ReadKey();
                    }
                    Console.SetCursorPosition(10, 6);
                    Console.Write("Enter your password : ");
                    password[index] = Console.ReadLine();
                    index++;
                }
                else if (choice == 2)
                {
                    string username;
                    string pass;
                    Console.Clear();
                    Console.SetCursorPosition(10, 5);
                    Console.Write("Enter your name : ");
                    username = Console.ReadLine();
                    Console.SetCursorPosition(10, 6);
                    Console.Write("Enter your password : ");
                    pass = Console.ReadLine();
                    currentIndex = SignIn(username, pass, name, password, index);
                    if (currentIndex != 9999)
                    {
                        Console.SetCursorPosition(10, 7);
                        Console.WriteLine("Signed In successfully");
                        while (true)
                        {
                            int menuChoice = 9999;
                            while(menuChoice == 9999)
                            {
                                Console.Clear();
                                menuChoice = AdminMenu();
                            }
                            if(menuChoice == 1)
                            {
                                ViewBooks(books, author, bookIndex);
                            }
                            else if(menuChoice == 2)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(10, 5);
                                Console.Write("Enter the name of the book : ");
                                string bookname = Console.ReadLine();
                                if(SearchBook(books, bookname) != 9999)
                                {
                                    Console.SetCursorPosition(10, 6);
                                    Console.WriteLine("Book already present");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.SetCursorPosition(10, 6);
                                    Console.Write("Enter the name of the author : ");
                                    string authorname = Console.ReadLine();
                                    AddBooks(books, author, ref bookIndex, authorname, bookname);
                                    Console.SetCursorPosition(10, 7);
                                    Console.WriteLine("Book Added Successfully");
                                    SaveBooks(books, author, bookIndex);
                                    Console.ReadKey();
                                }
                            }
                            else if(menuChoice == 3)
                            {
                                Console.SetCursorPosition(10, 5);
                                Console.WriteLine("1. Search by Book name");
                                Console.SetCursorPosition(10, 6);
                                Console.WriteLine("2. Search by Author name");
                                Console.SetCursorPosition(10, 7);
                                Console.Write("Enter your choice : ");
                                int option = int.Parse(Console.ReadLine());
                                Console.Clear();
                                if(option == 1)
                                {
                                    Console.SetCursorPosition(10, 5);
                                    Console.Write("Enter the name of the book : ");
                                    string bookname = Console.ReadLine();
                                    int idx = SearchBook(books, bookname);
                                    if (idx != 9999)
                                    {
                                        Console.SetCursorPosition(10, 6);
                                        Console.WriteLine(books[idx] + "\t" + author[idx]);
                                        Console.ReadKey();
                                    }

                                }
                                else if (option == 2)
                                {
                                    Console.SetCursorPosition(10, 5);
                                    Console.Write("Enter the name of the author : ");
                                    string authorname = Console.ReadLine();
                                    for(int i = 0; i < bookIndex; i++)
                                    {
                                        if(authorname == author[i])
                                        {
                                            Console.SetCursorPosition(10, 6);
                                            Console.WriteLine(books[i] + "\t" + author[i]);
                                        }
                                    }
                                    Console.ReadKey();
                                }
                            }
                            else if (menuChoice == 4)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(10, 5);
                                Console.Write("Enter the name of the book : ");
                                string bookname = Console.ReadLine();
                                int idx = SearchBook(books, bookname);
                                if (idx == 9999)
                                {
                                    Console.SetCursorPosition(10, 6);
                                    Console.WriteLine("Book not present");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    RemoveBooks(books, author, ref bookIndex, bookname, idx);
                                    SaveBooks(books, author, bookIndex);
                                    Console.SetCursorPosition(10, 6);
                                    Console.WriteLine("Book Removed Successfully");
                                    Console.ReadKey();
                                }
                            }
                            else if (menuChoice == 5)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(10, 5);
                                Console.Write("Enter the old password : ");
                                string pas = Console.ReadLine();
                                if(pas == password[currentIndex])
                                {
                                    Console.SetCursorPosition(10, 6);
                                    Console.Write("Enter the new password : ");
                                    string newPassword = Console.ReadLine();
                                    password[currentIndex] = newPassword;
                                    SaveData(name, password, index);
                                }
                                else
                                {
                                    Console.SetCursorPosition(10, 6);
                                    Console.WriteLine("Wrong Password");
                                    Console.ReadKey();
                                }
                            }
                            else if (menuChoice == 6)
                            {
                                Console.Clear();
                                string path = "backupBook.txt";
                                BackUpFiles(path, books, author, bookIndex);
                                path = "backupData.txt";
                                BackUpFiles(path, name, password, index);
                                Console.SetCursorPosition(10, 5);
                                Console.WriteLine("Creating backup...");
                                Thread.Sleep(500);
                                Console.SetCursorPosition(10, 6);
                                Console.WriteLine("BackUp Complete");
                                Console.ReadKey();
                            }
                            else if(menuChoice == 7)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(10, 7);
                        Console.WriteLine("Sign In Failed");
                        Console.ReadKey();
                    }
                }
                else if (choice == 3)
                {
                    SaveData(name, password, index);
                    break;
                }
            }
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
            if(int.TryParse(str, out choice) == true)
            {
                return choice;
            }
            else
            {
                return 9999;
            }
        }

        static int AdminMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("                                                            _           _         __  __                   ");
            Console.WriteLine("                                                   /\\      | |         (_)       |  \\/  |                  ") ;
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
            if(int.TryParse(str, out choice) == true)
            {
                return choice;
            }
            else
            {
                return 9999;
            }
        }

        static bool IsValid(string[] name, string user, int index)
        {
            for(int i = 0; i < index; i++)
            {
                if(user == name[i])
                {
                    return false;
                }
            }
            return true;
        }

        static void ViewBooks(string[] books, string[] author, int bookIndex)
        {
            Console.Clear();
            for(int i = 0; i < bookIndex; i++)
            {
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("Book Name");
                Console.SetCursorPosition(37, 4);
                Console.WriteLine("Author Name");
                Console.SetCursorPosition(10, 5+i);
                Console.Write(books[i]);
                Console.SetCursorPosition(37, 5+i);
                Console.WriteLine(author[i]);
            }
            Console.ReadKey();
        }

        static void AddBooks(string[] books, string[] author, ref int bookIndex, string authorname, string bookname)
        {
            author[bookIndex] = authorname;
            books[bookIndex] = bookname;
            bookIndex++;
        }

        static void RemoveBooks(string[] books, string[] author, ref int bookIndex, string bookname, int idx)
        {
            for(int i = idx; i < bookIndex-1; i++)
            {
                books[i] = books[i + 1];
                author[i] = author[i + 1];
            }
            bookIndex--;
        }

        static int SearchBook(string[] array, string name)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if(name == array[i])
                {
                    return i;
                }
            }
            return 9999;
        }

        static void BackUpFiles(string path, string[] array, string[] array2, int index)
        {
            StreamWriter file = new StreamWriter(path);
            for(int i = 0; i < index; i++)
            {
                file.WriteLine(array[i] + "," + array2[i]);
            }
            file.Close();
        }

        static int SignIn(string username, string pass, string[] name, string[] password, int index)
        {
            for(int i = 0; i < index; i++)
            {
                if(username == name[i] && pass == password[i])
                {
                    return i;
                }
            }
            return 9999;
        }

        static void SaveData(string[] name, string[] password, int index)
        {
            string path = "data.txt";
            StreamWriter file = new StreamWriter(path);
            for(int i = 0; i < index; i++)
            {
                file.Write(name[i] + ",");
                file.WriteLine(password[i]);
            }
            file.Flush();
            file.Close();
        }

        static void SaveBooks(string[] books, string[] author, int bookIndex)
        {
            string path = "Books.txt";
            StreamWriter file = new StreamWriter(path);
            for (int i = 0; i < bookIndex; i++)
            {
                file.Write(books[i] + ',');
                file.WriteLine(author[i]);
            }
            file.Flush();
            file.Close();
        }
        
        static string ParseData(string record ,int field)
        {
            int comma = 1;
            string item = "";
            for(int i = 0; i < record.Length; i++)
            {
                if(record[i] == ',')
                {
                    comma++;
                }
                else if(comma == field)
                {
                    item = item + record[i];
                }
            }
            return item;
        }

        static void LoadData(string[] name, string[] password, ref int index)
        {
            string path = "data.txt";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                string record;
                while ((record = file.ReadLine()) != null)
                {
                    name[index] = ParseData(record, 1);
                    password[index] = ParseData(record, 2);
                    index++;
                }
                file.Close();
            }
            else
            {
                Console.WriteLine("File does not Exists");
            }
        }

        static void LoadBooks(string[] books, string[] author, ref int bookIndex)
        {
            string path = "Books.txt";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                string record;
                while ((record = file.ReadLine()) != null)
                {
                    books[bookIndex] = ParseData(record, 1);
                    author[bookIndex] = ParseData(record, 2);
                    bookIndex++;
                }
                file.Close();
            }
            else
            {
                Console.WriteLine("File does not Exists");
            }
        }
    }
}
