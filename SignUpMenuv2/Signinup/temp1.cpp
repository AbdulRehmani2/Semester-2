#include <iostream>

using namespace std;

int add(int number1, int number2);
int difference(int number1, int number2);
int product(int number1, int number2);
int divide(int number1, int number2);

int main()
{
    int number1;
    int number2;
    cout << "Enter first integer: ";
    cin >> number1;
    cout << "Enter second integer: ";
    cin >> number2;
    int result = add(number1, number2);
    cout << "Result is " << result << endl;
    result = difference(number1, number2);
    cout << "Result is " << result << endl;
    result = product(number1, number2);
    cout << "Result is " << result << endl;
    result = divide(number1, number2);
    cout << "Result is " << result << endl;
}

int add(int number1, int number2)
{
    return number1 + number2;
}

int difference(int number1, int number2)
{
    return number1 - number2;
}

int product(int number1, int number2)
{
    return number1 * number2;
}

int divide(int number1, int number2)
{
    return number1 / number2;
}