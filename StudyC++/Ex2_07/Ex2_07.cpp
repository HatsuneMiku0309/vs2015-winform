// Ex2_07.cpp (p.78)
/*
	�۰��ܼƦb�O���餤�۰ʰt�m�쪺�Ŷ��٬����|(stack)�A���|�w�]���j�p��1MB�A�w���������j�����ݨD�A�Y�ݭn�i�N�M�ת�/STACK�ﶵ�]�w���һݤj�p�C
	�۰��ܼ�keyword = auto (auto is default so omission)
*/
#include "stdafx.h"
#include <iostream>

using namespace std;

int main()
{
	int count1 = 10;
	int count3 = 50;

	cout << endl
		 << "Value of outer count1 = " << count1
		 << endl;

	//������i�H�o�˪����s�W�ϰ�?
	{ // New scope starts here...
		int count1 = 20;
		int count2 = 30;

		cout << "Value of inner count1 = " << count1
			 << endl;

		count1 += 3;
		count3 += count2;

	}

	cout << "Value of outer ocunt1 = " << count1
		 << endl
		 << "Value of outer count3 = " << count3
		 << endl;
	
	// cout << count2 << endl; //uncomment(��������) to get an error

    return 0;
}

