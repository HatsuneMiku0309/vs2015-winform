// Ex2_09.cpp (p.88)
// Demonstrating type-safe and non-type-safe enumerations

/**
    展示列舉(enum)兩種型態
*/

#include <iostream>

using std::cout;
using std::endl;

/**
    如果宣告以下兩列舉enum
        ex. 
            enum t1 {test, test1}
            enum t2 {test, test2}
    將會直接把列舉子至於封閉區間，而造成列舉子衝突

    可使用C++ 11安全型態列舉
        ex.
            enum class t1 {test}
            enum class t2 {test}
    將會把列舉子至class內~因此 t1::test 與 t2::test是不同的(此法必須指定 列舉名稱 )
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