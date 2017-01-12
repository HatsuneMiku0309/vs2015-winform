// Ex2_06_01.cpp (p.67~68)
//�ܼƫ��A�M�κA�ഫ

/*
	�sĶ���ഫ�κA������
	//�B�I�B���O(�j>�p)
	1. long double
	2. double
	3. fload

	//�W����ƫ��O(�j>�p)
	4. unsigned long long
	5. long long

	//�`��ƫ��O(�j>�p)
	6. unsigned long
	7. long
	
	//��Ʀ�O(�j>�p)
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

	//output :�@4294967286 (x)
	//because int conversion unsigned int is not signed, so this type min = 0 and max = 4294967295 finally  4294967295+(1)-10 = 4294967286
	//�� 4294967295+(1) because of 4294967295 is max value but exist 0 value so in this type 0-1=4294967295 => 0 = 4294967295+1 = 4294967286
	return 0;
}

int ImplicitTypeConversionDataLoss(int normal_int, float decimal) {
	
	//data loss base concept(�[��)
	normal_int = decimal;
	
	cout << "ImplicitTypeConversionDataLoss" << endl;
	cout << normal_int << endl;
	cout << endl;

	return 0;
}

int ExplicitTypeConversionTest1(double value1, double value2) {
	
	//data loss base concept(�[��)
	int wholeNumber = static_cast<int>(value1) + static_cast<int>(value2);

	cout << "ExplicitTypeConversionTest1" << endl;
	cout << wholeNumber << endl;
	cout << endl;

	return 0;
}

int OldExplicitTypeConversion(float value1) {

	//���Q��ĳ�ϥΡA������F�ഫ���ت�
	//(the type to vonvert to) expression(��F��)
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

	//�]���O"�t��"�A�ҥH�b�o�V�k�첾�A�h�����1
	//�p�O����(�L��)�A�h��0

	//�����@�߸�0
	return 0;

}