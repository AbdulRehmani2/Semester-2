#include <iostream>
#include <vector>

using namespace std;

int main()
{
    int size;
    cin >> size;
    vector<vector<int>> arr;
    arr.resize(size);
    for(int i = 0; i < size; i++)
    {
        int temp;
        arr[i].resize(size);
        for(int j = 0; j < size; j++)
        {
            cin >> arr[i][j];
        }
    }
    int sum1 = 0, sum2 = 0;
    int temp = size-1;
    for(int i = 0; i < size; i++)
    {
        for(int j = 0; j < size; j++)
        {
            if(i == j)
            {
                sum1 = sum1 + arr[i][j];
            }
        }
        sum2 = sum2 + arr[i][temp];
        temp--;
    }
    cout << sum1 << " " << sum2;
}