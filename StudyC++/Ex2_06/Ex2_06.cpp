// Ex2_06.cpp (p.64)
//Exercising the comma operator

/*
    �u���v    |  �B��l                    |  ���X��
	1           ::                           None
	
	2           () [] -> .                   left
	            postfix++  postfix--
				typeid
				const_cast dynamic_cast
				static_cast(���T�ഫ)
				reinterpret_cast
    
	3           !(�޿�B��l)  ~(1���ɼ�)     right
	            ++  --  (type cast)
				+ - * &(�@���B��l)
				sizeof  decltype
				new  delete

    4           .*  ->*                      left

	5           *  /  %                      left
	
	6           +  -                         left

	7           <<  >>                       left

	8           ==  !=                       left

	9           &(�G�i����)                 left

	10          ^                            left

	11          |(�G�i����)                 left

	12          &&                           left

	13          ||                           left

	14          ?:(�T������B��l)            right

	15          =  *=  /=  %=  +=  -=  &=    right
	            ^=  |=  <<=  >>=

	16          Throw                        right

	17          ,                            left


	----------------directions----------------------
	�u���v�̰����B��l��ܦb��檺�Ĥ@��C�P�@��Y���h�عB��l�h��ܥL�̨㦳�ۦP���u���v�C
	��B��l�S���A���A�B�B��l���u���v�ۦP�ɡA�h�ѥL�̪����X��(associativity)�ӨM�w���涶�ǡC
*/

#include "stdafx.h"
#include <iostream>

using namespace std;

int main()
{
	long num1 = 0L, num2(0L), num3(0L), num4(0L); // (0L) = 0L

	num4 = (num1 = 10L, num2 = 20, num3 = 30); // get right value
	
	cout << endl
		 << "The value of a series of expressions "
		 << "is the value of the rightmost: "
		 << num4
		 << endl;

	cout << endl
		 << "����num4 = num1...���A��"
		 << endl;

	num4 = num1 = 10, num2 = 20, num3 = 30; // get left value

	cout << endl
		<< "The value of a series of expressions "
		<< "is the value of the rightmost: "
		<< num4
		<< endl;

    return 0;
}

