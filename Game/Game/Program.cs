using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EZInput;
using System.Threading;
using Game.Classes;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            entity enemy = new entity();
            entity player = new entity();
            player.health = 10;
            enemy.health = 50;
            int score = 0;
            string path = "level.txt";
            StreamReader file = new StreamReader(path);
            char[,] maze = new char[42, 165];
            player.model = new char[,] { { ' ', ' ', '_', '|', ' ', '|', '_', ' ', ' ' }, { '_', '|', '#', '|', ' ', '|', '#', '|', '_' }, { '|', '#', '|', '_', '_', '_', '|', '#', '|' } };
            enemy.model = new char[,] {{ '_', '_', '_', '.', '.', '_', '_', '_'},
                         { ' ', '|', '_', '=', '=', '_', '|', ' '},
                         { ' ', ' ', '(', '|', '|', ')', ' ', ' '}};
            player.bullets = new List<List<int>>();
            enemy.bullets = new List<List<int>>();
            player.bullets.Add(new List<int>());
            player.bullets.Add(new List<int>());
            enemy.bullets.Add(new List<int>());
            enemy.bullets.Add(new List<int>());
            ReadMaze(maze, file);
            string direction = "left";
            player.x = 50;
            player.y = 37;
            enemy.x = 30;
            enemy.y = 10;
            int frame = 0;
            int choice = 0;
            int subChoice = 0;
            while(true)
            {
                logo();
                Console.ReadKey();
                choice = mainMenu();
                if (choice == 2)
                {
                    while (true)
                    {
                        subChoice = options();
                        if (subChoice == 1)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("        Press left and right to move the character");
                            Console.WriteLine("        Press Space and CTRL to shoot");
                            Console.ReadKey();
                        }
                        else if (subChoice == 2)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("        You are to shoot the aliens as they fly from the sky.");
                            Console.WriteLine("        The enemies will progressively become strong.");
                            Console.ReadKey();
                        }
                        else if (subChoice == 3)
                        {
                            break;
                        }
                    }
                }
                else if(choice == 3)
                {
                    break;
                }
                else if (choice == 1)
                {
                    PrintMaze(maze);
                    PrintEnemy(enemy);
                    PrintPlayer(player);
                    while (true)
                    {
                        frame++;
                        if (Keyboard.IsKeyPressed(Key.LeftArrow) && maze[player.y, player.x - 1] == ' ')
                        {
                            ClearPlayer(player.x, player.y);
                            ClearMaze(player, maze);
                            player.x--;
                            PrintPlayer(player);
                            PrintPlayerMaze(player, maze);
                        }
                        if (Keyboard.IsKeyPressed(Key.RightArrow) && maze[player.y, player.x + 9] == ' ')
                        {
                            ClearPlayer(player.x, player.y);
                            ClearMaze(player, maze);
                            player.x++;
                            PrintPlayer(player);
                            PrintPlayerMaze(player, maze);
                        }
                        if (Keyboard.IsKeyPressed(Key.Space))
                        {
                            
                            player.bullets[0].Add(player.x + 4);
                            player.bullets[1].Add(player.y - 1);
                            Console.SetCursorPosition(player.x + 4, player.y - 1);
                            Console.Write(".");
                        }
                        EnemyMovement(enemy, maze, player, ref direction, frame);
                        BulletMovement(player.bullets, maze, enemy, ref score);
                        CreateBullet(enemy.bullets, enemy.x, enemy.y, frame);
                        EnemyBulletMovement(enemy.bullets, maze, ref player.health);
                        DisplayHealth(player, score);
                        Thread.Sleep(30);
                        if(enemy.health == 0)
                        {
                            Console.Clear();
                            player.health = 10;
                            enemy.health = 50;
                            Console.SetCursorPosition(70, 15);
                            Console.Write("You Win");
                            int number = 0;
                            while (number < 50)
                            {
                                number++;
                                BulletMovement(player.bullets, maze, enemy, ref score);
                                EnemyBulletMovement(enemy.bullets, maze, ref player.health);
                            }
                            Thread.Sleep(2000);
                            break;
                        }
                    }
                }
            }
            
        }


        static void logo()
        {
            Console.SetCursorPosition(20, 10);
            Console.Write( "_________          _______    _        _______  _______ _________   _______ _________ _______  _        ______  " );
            Console.SetCursorPosition(20, 11);
            Console.Write( "\\__   __/|\\     /|(  ____ \\  ( \\      (  ___  )(  ____ \\__   __/  (  ____ \\ \\__   __/(  ___  )( (    /|(  __  \\ " );
            Console.SetCursorPosition(20, 12);
            Console.Write( "   ) (   | )   ( || (    \\/  | (      | (   ) || (    \\/   ) (     | (    \\/   ) (   | (   ) ||  \\  ( || (  \\  )" );
            Console.SetCursorPosition(20, 13);
            Console.Write( "   | |   | (___) || (__      | |      | (___) || (_____    | |     | (_____    | |   | (___) ||   \\ | || |   ) |" );
            Console.SetCursorPosition(20, 14);
            Console.Write( "   | |   |  ___  ||  __)     | |      |  ___  |(_____  )   | |     (_____  )   | |   |  ___  || (\\ \\) || |   | |" );
            Console.SetCursorPosition(20, 15);
            Console.Write( "   | |   | (   ) || (        | |      | (   ) |      ) |   | |           ) |   | |   | (   ) || | \\   || |   ) |" );
            Console.SetCursorPosition(20, 16);
            Console.Write( "   | |   | )   ( || (____/\\  | (____/\\| )   ( |/\\____) |   | |     /\\____) |   | |   | )   ( || )  \\  || (__/  )" );
            Console.SetCursorPosition(20, 17);
            Console.Write( "   )_(   |/     \\|(_______/  (_______/|/     \\|\\_______)   )_(     \\_______)   )_(   |/     \\||/    )_)(______/ " );
            Console.Write( "                                                                                                                " );
            Console.SetCursorPosition(20, 19);
        }


        static void ClearMaze(entity player, char[,] maze)
        {
            maze[player.y, player.x] = ' ';
            maze[player.y, player.x + 1] = ' ';
            maze[player.y, player.x + 2] = ' ';
            maze[player.y, player.x + 3] = ' ';
            maze[player.y, player.x + 4] = ' ';
            maze[player.y, player.x + 5] = ' ';
            maze[player.y, player.x + 6] = ' ';
            maze[player.y, player.x + 7] = ' ';
            maze[player.y, player.x + 8] = ' ';
        }

        static void PrintPlayerMaze(entity player, char[,] maze)
        {
            maze[player.y, player.x] = 'p';
            maze[player.y, player.x + 1] = 'p';
            maze[player.y, player.x + 2] = 'p';
            maze[player.y, player.x + 3] = 'p';
            maze[player.y, player.x + 4] = 'p';
            maze[player.y, player.x + 5] = 'p';
            maze[player.y, player.x + 6] = 'p';
            maze[player.y, player.x + 7] = 'p';
            maze[player.y, player.x + 8] = 'p';
        }

        static void ReadMaze(char[,] maze, StreamReader file)
        {
            Console.Clear();
            string temp = "";
            for (int i = 0; i < 41; i++)
            {
                temp = file.ReadLine();
                for (int j = 0; j < temp.Length; j++)
                {
                    maze[i, j] = temp[j];
                }
            }
        }
        static void PrintMaze(char[,] maze)
        {
            Console.Clear();
            for (int i = 0; i < 42; i++)
            {
                for(int j = 0; j < 165; j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
        }

        static int mainMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("      1. Start Game");
            Console.WriteLine("      2. Options");
            Console.WriteLine("      3. Exit");
            int choice = 0;
            Console.Write("      Enter your Choice : ");
            while(true)
            {
                string str = Console.ReadLine();
                if(int.TryParse(str, out choice))
                {
                    break;
                }
            }
            return choice;
        }

        static int options()
        {
            Console.Clear();
            int choice = 0;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("     1.Keys");
            Console.WriteLine("     2.Instruction");
            Console.WriteLine("     3.Exit");
            Console.Write("     Enter your choice : ");
            while (true)
            {
                string str = Console.ReadLine();
                if (int.TryParse(str, out choice))
                {
                    break;
                }
            }
            return choice;
        }

        static void PrintEnemy(entity enemy)
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Console.SetCursorPosition(enemy.x + j, enemy.y + i);
                    Console.Write(enemy.model[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void PrintPlayer(entity player)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.SetCursorPosition(player.x + j, player.y + i);
                    Console.Write(player.model[i, j]);
                }
                Console.WriteLine();
            }
        }

        

        static void EnemyMovement(entity enemy, char[,] maze, entity player, ref string direction, int frame)
        {
            if (frame % 5 == 0)
            {
                if (maze[enemy.y, enemy.x + 8] != '*')
                {
                    if(enemy.x < player.x)
                    {
                        ClearEnemy(enemy.x, enemy.y);
                        maze[enemy.y + 2, enemy.x] = ' ';
                        maze[enemy.y + 2, enemy.x + 1] = ' ';
                        maze[enemy.y + 2, enemy.x + 2] = ' ';
                        maze[enemy.y + 2, enemy.x + 3] = ' ';
                        maze[enemy.y + 2, enemy.x + 4] = ' ';
                        maze[enemy.y + 2, enemy.x + 5] = ' ';
                        maze[enemy.y + 2, enemy.x + 6] = ' ';
                        enemy.x++;
                        maze[enemy.y+2, enemy.x] = 'E';
                        maze[enemy.y+2, enemy.x+1] = 'E';
                        maze[enemy.y+2, enemy.x+2] = 'E';
                        maze[enemy.y+2, enemy.x+3] = 'E';
                        maze[enemy.y+2, enemy.x+4] = 'E';
                        maze[enemy.y + 2, enemy.x + 5] = 'E';
                        maze[enemy.y + 2, enemy.x + 6] = 'E';
                        PrintEnemy(enemy);
                    }
                }
                if (maze[enemy.y, enemy.x - 1] != '*')
                {
                    if (enemy.x > player.x)
                    {
                        ClearEnemy(enemy.x, enemy.y);
                        maze[enemy.y + 2, enemy.x] = ' ';
                        maze[enemy.y + 2, enemy.x + 1] = ' ';
                        maze[enemy.y + 2, enemy.x + 2] = ' ';
                        maze[enemy.y + 2, enemy.x + 3] = ' ';
                        maze[enemy.y + 2, enemy.x + 4] = ' ';
                        maze[enemy.y + 2, enemy.x + 5] = ' ';
                        maze[enemy.y + 2, enemy.x + 6] = ' ';
                        enemy.x--;
                        maze[enemy.y + 2, enemy.x] = 'E';
                        maze[enemy.y + 2, enemy.x + 1] = 'E';
                        maze[enemy.y + 2, enemy.x + 2] = 'E';
                        maze[enemy.y + 2, enemy.x + 3] = 'E';
                        maze[enemy.y + 2, enemy.x + 4] = 'E';
                        maze[enemy.y + 2, enemy.x + 5] = 'E';
                        maze[enemy.y + 2, enemy.x + 6] = 'E';
                        PrintEnemy(enemy);
                    }
                }
            }
        }

        static void CreateBullet(List<List<int>> bullet, int x, int y, int frame)
        {
            if(frame % 30 == 0)
            {
                bullet[0].Add(x + 4);
                bullet[1].Add(y + 3);
                Console.SetCursorPosition(x + 4, y + 3);
                Console.Write('|');
            }
        }

        static void ClearPlayer(int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.SetCursorPosition(x + j, y + i);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static void ClearEnemy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
            Console.SetCursorPosition(x + 7, y);
            Console.Write(' ');
        }

        static void BulletMovement(List<List<int>> bullet, char[,] maze, entity enemyOne, ref int score)
        {
            for(int i = 0; i < bullet[0].Count; i++)
            {
                if (maze[bullet[1][i] - 1, bullet[0][i]] == 'E')
                {
                    enemyOne.health--;
                    score++;
                }
                if (maze[bullet[1][i] - 1, bullet[0][i]] == '*' || maze[bullet[1][i] - 1, bullet[0][i]] == 'E')
                {
                    Console.SetCursorPosition(bullet[0][i], bullet[1][i]);
                    Console.Write(" ");
                    bullet[0].RemoveAt(i);
                    bullet[1].RemoveAt(i);
                }
                else
                {
                    Console.SetCursorPosition(bullet[0][i], bullet[1][i]);
                    maze[bullet[1][i], bullet[0][i]] = ' ';
                    Console.Write(" ");
                    bullet[1][i]--;
                    Console.SetCursorPosition(bullet[0][i], bullet[1][i]);
                    maze[bullet[1][i], bullet[0][i]] = '.';
                    Console.Write('.');
                }
            }
        }

        static void EnemyBulletMovement(List<List<int>> bullet, char[,] maze, ref int health)
        {
            for(int i = 0; i < bullet[0].Count; i++)
            {
                if (maze[bullet[1][i] + 1, bullet[0][i]] == 'p')
                {
                    health--;
                }
                if (maze[bullet[1][i] + 1, bullet[0][i]] == '*' || maze[bullet[1][i] + 1, bullet[0][i]] == 'p')
                {
                    Console.SetCursorPosition(bullet[0][i], bullet[1][i]);
                    Console.Write(' ');
                    bullet[0].RemoveAt(i);
                    bullet[1].RemoveAt(i);
                }
                
                else
                {
                    Console.SetCursorPosition(bullet[0][i], bullet[1][i]);
                    Console.Write(' ');
                    maze[bullet[1][i], bullet[0][i]] = ' ';
                    bullet[1][i]++;
                    Console.SetCursorPosition(bullet[0][i], bullet[1][i]);
                    maze[bullet[1][i], bullet[0][i]] = '.';
                    Console.Write('|');
                }
            }
        }

        static void DisplayHealth(entity player, int score)
        {
            Console.SetCursorPosition(5, 41);
            Console.Write("Score is : " + score);
            Console.SetCursorPosition(5, 42);
            Console.Write("Health is : " + player.health);
        }
    }
}
