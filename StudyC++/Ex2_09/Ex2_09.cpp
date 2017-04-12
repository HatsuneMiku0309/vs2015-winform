// Ex2_09.cpp (p.88)
// Demonstrating type-safe and non-type-safe enumerations

/**
    �i�ܦC�|(enum)��ث��A
*/

#include <iostream>

using std::cout;
using std::endl;

/**
    �p�G�ŧi�H�U��C�|enum
        ex. 
            enum t1 {test, test1}
            enum t2 {test, test2}
    �N�|������C�|�l�ܩ�ʳ��϶��A�ӳy���C�|�l�Ĭ�

    �i�ϥ�C++ 11�w�����A�C�|
        ex.
            enum class t1 {test}
            enum class t2 {test}
    �N�|��C�|�l��class��~�]�� t1::test �P t2::test�O���P��(���k�������w �C�|�W�� )
*/
enum Suit : long
{
    Clubs, Diamonds, Hearts, Spades
};

int main() {
    Suit suit = Clubs;
    Suit another = Suit::Diamonds;

    cout << "suit value: " << suit << endl;
    cout << "Add 10 to another: " << another + 10 << endl;

    enum class Color : char { Red, Orange, Yellow };
    Color skyColor(Color::Yellow);
    // Color testColor(Yellow); // Uncomment for an error

    cout << endl
        << "Skey color value: "
        << static_cast<long>(skyColor) << endl;

    cout << "Incremented sky color: "
        << static_cast<long>(skyColor) + 10L << endl;


    system("PAUSE");
    return 0;
}