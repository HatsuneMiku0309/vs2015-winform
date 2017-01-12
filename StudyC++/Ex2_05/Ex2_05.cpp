// Ex2_05.cpp (p.56)
//Calculating(計算) how many rolls of wallpaper are reqwuired for a room

#include "stdafx.h"
#include <iostream>


using namespace std;
/*
using std::cout;
using std::endl;
using std::cin;
*/

int main()
{
	const bool DEBUG = false;

	double height = 0.0, width = 0.0, length = 0.0;
	double perimeter = 0.0;
	const double ROLL_WIDTH = 21.0;
	const double ROLL_LENGTH = 33.0; // 12.0 * 33.0 公制轉英制

	int stripsPerRoll = 0;
	int StripsReqd = 0;
	int nrolls = 0;

	cout << endl
		 //<< "Enter the height of the room in inches: ";
		 << "Enter the height of the room in meter: ";
	
	cin >> height; //care this output operator(操作子)

	cout << endl
		 //<< "Now enter the length and width in inches: ";
		 << "Now enter the length and width in meter: ";
	cin >> length >> width;

	//stripsPerRoll = ROLL_LENGTH / height; // 33/8 = 4
	stripsPerRoll = static_cast<int>(ROLL_LENGTH / height); // explicit type conversion to int type
	if (DEBUG) {
		cout << stripsPerRoll << endl;
	}

	perimeter = 2.0 * (length + width); // 2 * (8+8) = 32
	if (DEBUG) {
		cout << perimeter << endl;
	}

	StripsReqd = perimeter / ROLL_WIDTH; // 32 / 21 = 1
	if (DEBUG) {
		cout << StripsReqd << endl;
	}

	nrolls = StripsReqd / stripsPerRoll; // 1 / 4
	cout << endl
		 << "For your room you need " << nrolls << " rolls of wallpaper."
		 << endl;

    return 0;
}

