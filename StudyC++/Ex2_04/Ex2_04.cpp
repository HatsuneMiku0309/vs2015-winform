//Ex2_04.cpp (p.53)
//Using escape sequences
//ex: \a , \n , \t...

/*
	In c++ best const syntax is const not #define.
	Const can debug , but #define can't
	Here are examples of use #define and const
*/

/* ASCII
	escape sequences  |  use
		\a				�o�XB�n
		\n				�s��
		\b				��h�@��
		\t				��������(tab)
*/

#include <iostream>
#include <stdlib.h>
#include <stdio.h>

#define newline '\n'

using std::cout;

int main() {
	//char newline = '\n';
	//const char newline = '\n';
	cout << newline;
	cout << "\"We\'ll make our escapes in sequence\", he said.";
	cout << "\n\tThe program\'s over, it\'s time to make a beep beep.\a\a";
	cout << newline;

	return 0;
}