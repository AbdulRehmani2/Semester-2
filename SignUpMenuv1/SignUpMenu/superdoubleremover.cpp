#include <iostream>
#include <vector>

using namespace std;

string doubleremover(string s);

int main()
{
    string s, result;
    cout << "Enter the string : ";
    cin >> s;
    result = doubleremover(s);
    cout << result;
}

string doubleremover(string s)
{
    vector<char> v;
    for (int i = 0; i < s.length(); i++)
    {
        v.insert(v.begin()+i, s[i]);
    }
    for (int j = 0; j < v.size(); j++)
    {
        for (int i = v.size()-1; i > 0 ; i++)
        {
            if (v[i] == v[i + 1])
            {
                v.erase(v.begin() + i);
                v.erase(v.begin() + i+1);
            }
        }
    }
    for(int i = 0; i < v.size(); i++)
    {
        cout << v[i];
    }
    s = "";
    if (v.size() == 0)
    {
        return "Empty String";
    }
    else
    {
        for (int i = 0; i < v.size(); i++)
        {
            s = s + v[i];
        }
    }
    return s;
}