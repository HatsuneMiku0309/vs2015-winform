// Ex2_06_02.cpp (p.68)
// AUTO keyword

#include "stdafx.h"
#include <iostream>

using namespace std;

int main()
{
	//variable
	auto n = 16;		// type is int
	auto pi = 3.14159;	// type is double
	auto x = 3.5f;		// type is float
	auto found = false; // type is bool

	//constant
	const auto E = 2.71828L;	// type is const long double
	const auto DOZEN = 12;		// type is const int

	//use auto keyword define variable is initial value can an expression(¹Bºâ¦¡)
	auto factor(n*pi*pi);

    return 0;
}

