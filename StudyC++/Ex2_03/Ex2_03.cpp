//Ex2_03.cpp
/* directions:
	setw is sets the field width to be used on output operations.
	This manipulator is declared in header <iomanip>.

	output:
	"  1234  5678"
*/

#include <iostream>;
#include <iomanip>;

using std::cout;
using std::endl;
using std::setw;

int main() {
	int num1 = 1234, num2 = 5678;

	cout << endl;
	cout << setw(6) << num1 << setw(6) << num2;
	cout << endl;

	system("pause"); // debug pause (F5)
	return 0;
}