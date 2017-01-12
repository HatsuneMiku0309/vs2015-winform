// Ex2_06.cpp (p.64)
//Exercising the comma operator

/*
    優先權    |  運算子                    |  結合性
	1           ::                           None
	
	2           () [] -> .                   left
	            postfix++  postfix--
				typeid
				const_cast dynamic_cast
				static_cast(明確轉換)
				reinterpret_cast
    
	3           !(邏輯運算子)  ~(1的補數)     right
	            ++  --  (type cast)
				+ - * &(一元運算子)
				sizeof  decltype
				new  delete

    4           .*  ->*                      left

	5           *  /  %                      left
	
	6           +  -                         left

	7           <<  >>                       left

	8           ==  !=                       left

	9           &(二進位比較)                 left

	10          ^                            left

	11          |(二進位比較)                 left

	12          &&                           left

	13          ||                           left

	14          ?:(三元條件運算子)            right

	15          =  *=  /=  %=  +=  -=  &=    right
	            ^=  |=  <<=  >>=

	16          Throw                        right

	17          ,                            left


	----------------directions----------------------
	優先權最高的運算子顯示在表格的第一行。同一行若有多種運算子則表示他們具有相同的優先權。
	當運算子沒有括號，且運算子的優先權相同時，則由他們的結合性(associativity)來決定執行順序。
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
		 << "拿掉num4 = num1...的括號"
		 << endl;

	num4 = num1 = 10, num2 = 20, num3 = 30; // get left value

	cout << endl
		<< "The value of a series of expressions "
		<< "is the value of the rightmost: "
		<< num4
		<< endl;

    return 0;
}

