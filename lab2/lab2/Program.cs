using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Classes;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Student s1 = new Student();
            Console.Write("Enter the name : ");
            s1.name = Console.ReadLine();
            Console.Write("Enter the roll no : ");
            s1.rollNo = int.Parse(Console.ReadLine());
            Console.Write("Enter the cpga : ");
            s1.cgpa = float.Parse(Console.ReadLine());
            Console.WriteLine("Name : {0} \nPassword : {1} \nCGPA : {2}", s1.name, s1.rollNo, s1.cgpa);
            Console.ReadKey();*/

            /*Student s2 = new Student();
            s2.name = "Musa";
            s2.rollNo = 10;
            s2.cgpa = 3.5F;
            Console.WriteLine("Name : {0} \nPassword : {1} \nCGPA : {2}", s2.name, s2.rollNo, s2.cgpa);
            Console.ReadKey();*/

            Student[] students = new Student[10];
            int index = 0;

            while (true)
            {
                int choice = Menu();
                if (choice == 1)
                {
                    AddStudent(students, ref index);
                }
                else if(choice == 2)
                {
                    ShowStudent(students, index);
                    Console.ReadKey();
                }
                else if(choice == 3)
                {
                    TopStudent(s, index);
                }
            }
        }

        static int Menu()
        {
            int choice = 0;
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Show Students");
            Console.WriteLine("3. Top Students");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }

        static bool IsValid(int rollNo, Student[] s, int index)
        {
            for(int i = 0; i < index; i++)
            {
                if (rollNo == s[i].rollNo)
                {
                    return false;
                }
            }
            return true;
        }

        static void AddStudent(Student[] s, ref int index)
        {
            s[index] = new Student();
            int rollNo;
            string name;
            Console.Write("Enter the name of the student : ");
            name = Console.ReadLine();
            Console.Write("Enter the Roll no : ");
            rollNo = int.Parse(Console.ReadLine());
            if(IsValid(rollNo, s, index))
            {
                s[index].name = name;
                s[index].rollNo = rollNo;
                Console.Write("Enter the cgpa : ");
                s[index].cgpa = float.Parse(Console.ReadLine());
                Console.Write("Enter the name of the department : ");
                s[index].department = Console.ReadLine();
                Console.Write("Is the student hostalide : ");
                s[index].isHostalide = char.Parse(Console.ReadLine());
                index++;
            }
            else
            {
                Console.WriteLine("Cannot assign this roll no.");
                Console.ReadKey();
                AddStudent(s, ref index);
            }
        }

        static void ShowStudent(Student[] s, int index)
        {
            for(int i = 0; i < index; i++)
            {
                Console.Write("Name : {0} Roll No : {1} CGPA : {2} Hostalide : {3} Department {4}", s[i].name, s[i].rollNo, s[i].cgpa, s[i].isHostalide, s[i].department);
            }
        }

        static void TopStudent(Student[] s, int index)
        {
            if(index == 0)
            {
                Console.WriteLine("No Data Present");
            }
            else if(index == 1)
            {
                ShowStudent(s, 1);
            }
            else if(index == 2)
            {
                int idx = 0;
                for (int i = 0; i < 2; i++)
                {
                    float largest = s[i].cgpa;
                    idx = i;
                    for (int j = 0; j < index; j++)
                    {
                        if (largest < s[j].cgpa)
                        {
                            largest = s[j].cgpa;
                            idx = j;
                        }
                    }
                    Student temp = s[idx];
                    s[idx] = s[i];
                    s[i] = temp;
                }
            }
            else
            {
                int idx = 0;
                for (int i = 0; i < 3; i++)
                {
                    float largest = s[i].cgpa;
                    idx = i;
                    for (int j = 0; j < index; j++)
                    {
                        if (largest < s[j].cgpa)
                        {
                            largest = s[j].cgpa;
                            idx = j;
                        }
                    }
                    Student temp = s[idx];
                    s[idx] = s[i];
                    s[i] = temp;
                }
                ShowStudent(s, 3);
                Console.ReadKey();
            }
        }
    }   
}