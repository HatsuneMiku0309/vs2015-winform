// Ex2_01.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

using std::cout;
using std::endl;

int main()
{
	short i = 0;
	int apples, oranges;
	int fruit;
	apples = 5; oranges = 6;
	fruit = apples*oranges;

	cout << "Oranges are not the only fruit.... " << endl
		 << "- and we have " << fruit << " fruits in all." << endl;

	while (true) {
		cout << i++ << endl;
	}

    return 0;
}

