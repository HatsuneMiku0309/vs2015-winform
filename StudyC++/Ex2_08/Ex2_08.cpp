// Ex2_08.cpp (p.82)
// Demonstrating variable scope

/*
	使用::範圍解析運算子可以取出被遮蔽的全域變數(globle)
*/

#include <iostream>

using std::cout;
using std::endl;

int count1 = 100;


int main()
{
	int count1 = 10;
	int count3 = 50;

	cout << endl
		<< "Value of outer count1 = " << count1
		<< endl;

	cout << "Value of global count1 = " << ::count1 << endl;

	//為什麼可以這樣直接新增區域?
	{ // New scope starts here...
		int count1 = 20; // This hides the outer count1
		int count2 = 30;

		cout << "Value of inner count1 = " << count1
			 << endl;

		cout << "Value of global count1 = " << ::count1 // From inner block
			 << endl;

		count1 += 3;
		count3 += count2;

	}

	cout << "Value of outer ocunt1 = " << count1
		<< endl
		<< "Value of outer count3 = " << count3
		<< endl;

	// cout << count2 << endl; //uncomment(取消註釋) to get an error

	return 0;
}