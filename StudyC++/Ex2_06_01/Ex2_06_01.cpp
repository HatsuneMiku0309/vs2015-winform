// Ex2_06_01.cpp (p.67~68)
//變數型態和形態轉換

/*
	編譯器轉換形態的順序
	//浮點運算行別(大>小)
	1. long double
	2. double
	3. fload

	//超長整數型別(大>小)
	4. unsigned long long
	5. long long

	//常整數型別(大>小)
	6. unsigned long
	7. long
	
	//整數行別(大>小)
	8. unsigned int
	9. int
*/

#include "stdafx.h"
#include <iostream>

using std::cout;
using std::endl;
using std::cin;

int ImplicitTypeConversionError1(unsigned int u_int, int normal_int);
int ImplicitTypeConversionDataLoss(int normal_int, float decimal);
int ExplicitTypeConversionTest1(double value1, double value2);
int OldExplicitTypeConversion(float value1);
int CheckExpressionType(int x, double y);
int SignedRightPositions(char number);

int main()
{

	ImplicitTypeConversionError1(10u, 20);
	ImplicitTypeConversionDataLoss(0, 2.5f);
	ExplicitTypeConversionTest1(10.5,15.5);
	OldExplicitTypeConversion(10.5);
	CheckExpressionType(10,15.5);
	SignedRightPositions(-104);

    return 0;
}

int ImplicitTypeConversionError1(unsigned int u_int, int normal_int) {

	//implict type conversion error
	cout << "ImplicitTypeConversionError1" << endl;
	cout << u_int - normal_int << endl;
	cout << endl;

	//output :　4294967286 (x)
	//because int conversion unsigned int is not signed, so this type min = 0 and max = 4294967295 finally  4294967295+(1)-10 = 4294967286
	//● 4294967295+(1) because of 4294967295 is max value but exist 0 value so in this type 0-1=4294967295 => 0 = 4294967295+1 = 4294967286
	return 0;
}

int ImplicitTypeConversionDataLoss(int normal_int, float decimal) {
	
	//data loss base concept(觀念)
	normal_int = decimal;
	
	cout << "ImplicitTypeConversionDataLoss" << endl;
	cout << normal_int << endl;
	cout << endl;

	return 0;
}

int ExplicitTypeConversionTest1(double value1, double value2) {
	
	//data loss base concept(觀念)
	int wholeNumber = static_cast<int>(value1) + static_cast<int>(value2);

	cout << "ExplicitTypeConversionTest1" << endl;
	cout << wholeNumber << endl;
	cout << endl;

	return 0;
}

int OldExplicitTypeConversion(float value1) {

	//不被建議使用，不易表達轉換的目的
	//(the type to vonvert to) expression(表達式)
	int value = (int)value1;

	cout << "OldExplicitTypeConversion" << endl;
	cout << value << endl;
	cout << endl;

	return 0;
}

int CheckExpressionType(int x, double y) {

	cout << "CheckExpressionType" << endl;
	cout << "The type of x*y is " << typeid(x*y).name() << endl;
	cout << endl;

	return 0;
}

int SignedRightPositions(char number) { //char range -128~127

	// if type is int number = -104, bit = 1111 1111 1001 1000
	// so right positions 2 bit = 1111 1111 1110 0110 
	// finally number = -26 and char type the same

	// original char number bit = 1001 1000
	number <<= 2; // 1110 0110

	cout << "SignedRightPositions" << endl;
	cout << static_cast<int>(number) << endl; //output unable use char type...conversion to int 
	cout << endl;

	//因為是"負號"，所以在這向右位移，則左邊補1
	//如是正號(無號)，則補0

	//左移一律補0
	return 0;

}