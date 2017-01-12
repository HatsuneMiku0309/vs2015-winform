// Ex2_07.cpp (p.78)
/*
	自動變數在記憶體中自動配置到的空間稱為堆疊(stack)，堆疊預設的大小為1MB，已足夠滿足大部份需求，若需要可將專案的/STACK選項設定為所需大小。
	自動變數keyword = auto (auto is default so omission)
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

	//為什麼可以這樣直接新增區域?
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
	
	// cout << count2 << endl; //uncomment(取消註釋) to get an error

    return 0;
}

