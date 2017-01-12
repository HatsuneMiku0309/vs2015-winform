Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' 呼叫function
        Dim returnValue As String = Calc(TextBox1.Text)

        'Dim returnValue As String = Calc2(TextBox1.Text)

        'Dim returnValue As String = Calc3(TextBox1.Text)

        Label1.Text = returnValue

    End Sub

    Public Function Calc(Expression As String) As String

        Dim re() As Char = {"+"c, "-"c, "*"c, "/"c}
        Dim re2() As Char = {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c}

        ' 計算的順序
        Dim countSort() As Char = {"*"c, "/"c, "+"c, "-"c}

        Dim numberSplit() As String
        Dim opNumber() As Integer
        Dim SignsSplit() As String

        Dim result As Double = 0

        ' 字串拆解
        numberSplit = Expression.Split(re)
        SignsSplit = Expression.Split(re2)

        ' 設定數字陣列大小
        opNumber = New Integer(numberSplit.Length) {}

        ' 判定是否為空輸入
        If (Expression <> "") Then
            For index As Integer = 0 To numberSplit.Length - 1
                If numberSplit(index) <> "" Then
                    If IsNumeric(numberSplit(index).Replace(" ", "")) Then ' 確認數字
                        opNumber(index) = CInt(numberSplit(index).Replace(" ", "")) ' 字串數字轉為int數字(各字串中空白去除)
                    Else ' 含有不法字元跳出Sub
                        MessageBox.Show("您的運算式含有不法字元")
                        Exit Function
                    End If
                End If
                'MessageBox.Show(opNumber(index))
            Next
        End If

        ' 判斷運算子,進行該動作
        For index As Integer = 0 To numberSplit.Length - 1
            Select Case SignsSplit(index)
                Case "+"
                    result += opNumber(index)
                Case "-"
                    result -= opNumber(index)
                Case "*"
                    result *= opNumber(index)
                Case "/"
                    result /= opNumber(index)
                Case ""
                    result += opNumber(index)
            End Select
            ' Debug.WriteLine(result)
            ' Label1.Text = result
        Next

        ' 輸出結果
        Return result

    End Function

    Public Function Calc2(Expression As String) As String ' 參考
        Dim i, n As Integer
        Dim TempExpression, Temp As String
        Dim digitals() As String '數字 digitals
        Dim Operators As Integer '運算符的數目 Operators
        Dim preOperators As Integer '上一運算符的位置
        Dim Add, Mul, Div, Lks, Rks As Integer   '運算符的數目 Add(+) Mul(*) Div(/) Lks(左括) Rks(右括)
        Dim Adp(), Mup(), Dip(), Lkp(), Rkp(), Lkn(), Rkn() As Integer '運算符的位置
        Dim Adn(), Mun(), Din() As Integer '運算符的排列次序
        Dim Sig() As Integer '每一個運算符的位置


        ' 無限回圈直到結束function
        Do While True

            ' 設定陣列數
            ReDim Adp(Len(Expression)), Mup(Len(Expression)), Dip(Len(Expression)), Lkp(Len(Expression)), Rkp(Len(Expression))
            ReDim Adn(Len(Expression)), Mun(Len(Expression)), Din(Len(Expression)), Lkn(Len(Expression)), Rkn(Len(Expression)), Sig(Len(Expression))
            ReDim digitals(Len(Expression))

            ' 檢查運算式是否為空
            If Len(Expression) = 0 Then
                Return "公式有錯誤"
                Exit Function
            End If

            ' 檢查字串最後是否有自定義 # 終止符
            If Mid(Expression, Len(Expression), 1) <> "#" Then
                TempExpression = Expression

                ' 將減改為+的方式 例: 1+-2
                For i = 1 To Len(Expression)
                    ' 查找-號及不為第首字
                    If Mid(Expression, i, 1) = "-" And i <> 1 Then
                        ' 判斷前項是否有其他運算符
                        If Mid(Expression, i - 1, 1) <> "+" And Mid(Expression, i - 1, 1) <> "-" _
                                  And Mid(Expression, i - 1, 1) <> "*" And Mid(Expression, i - 1, 1) <> "/" Then
                            TempExpression = Mid(TempExpression, 1, i - 1 + n) + "+" + Mid(Expression, i) ' 進行字串切割及塞入+
                            n = n + 1
                        End If
                    End If
                Next i

                ' 暫存處理字串給回原來
                Expression = TempExpression

                n = 0
                '處理負負得正
                For i = 1 To Len(Expression)
                    If Mid(Expression, i, 1) = "-" Then
                        ' 找尋該 - 字元後一位字元是否為 -
                        If Mid(Expression, i + 1, 1) = "-" Then
                            TempExpression = Mid(TempExpression, 1, i - 1 - n) + Mid(Expression, i + 2) ' 進行字串切割，原字串(+--)處理為(+)
                            n = n + 2 ' 每一負負得正被處理為(+--)，而上將後面兩個負號切割掉，故要扣除兩個負號字長
                        End If
                    End If
                Next i
                ' 暫存處理字串給回原來
                Expression = TempExpression
                Expression = Expression + "#" ' 給與自訂截止符
                'Debug.WriteLine(Expression)
            End If


            Operators = 1
            Add = 0 : Mul = 0 : Div = 0 : Lks = 0 : Rks = 0
            ' 運算符的順序、位置取出
            For i = 1 To Len(Expression)
                If (Not IsNumeric(Mid(Expression, i, 1))) Then
                    Select Case Mid(Expression, i, 1)
                        Case "+"
                            Add += 1
                            Adp(Add) = i
                            Adn(Add) = Operators ' 用來控制運算符順序
                        Case "*"
                            Mul += 1
                            Mup(Mul) = i
                            Mun(Mul) = Operators
                        Case "/"
                            Div += 1
                            Dip(Div) = i
                            Din(Div) = Operators
                        Case "("
                            Lks = Lks + 1
                            Lkp(Lks) = i

                        Case ")"
                            Rks = Rks + 1
                            Rkp(Rks) = i
                    End Select
                End If

                If Mid(Expression, i, 1) = "+" Or Mid(Expression, i, 1) = "*" Or
                Mid(Expression, i, 1) = "/" Or Mid(Expression, i, 1) = "#" Then

                    If preOperators + 1 = i And Mid(Expression, i + 1, 1) <> "#" Then ' 運算符不當連續或以運算符開頭(不包含-號)
                        Return "公式有錯誤"
                        Exit Function
                    Else
                        preOperators = i
                    End If

                    If Not IsNumeric(digitals(Operators)) And Mid(Expression, i + 1, 1) <> "#" Then ' 數字不是數字
                        Return "公式有錯誤"
                        Exit Function
                    End If
                    Sig(Operators) = i ' 用來控制運算符位置
                    Operators += 1

                Else
                    If Mid(Expression, i, 1) <> "(" And Mid(Expression, i, 1) <> ")" Then
                        digitals(Operators) += Mid(Expression, i, 1) ' 製作數字
                    Else
                        If i <> 1 Then
                            ' 判定括號前後的合法性
                            If ((Mid(Expression, i - 1, 1) = "(" And Mid(Expression, i, 1) = ")") Or (Mid(Expression, i - 1, 1) = ")" And Mid(Expression, i, 1) = "(")) Then
                                Return "公式有錯誤"
                                Exit Function
                            End If
                        End If
                    End If
                End If

            Next i

            If Lks <> Rks Then ' 左右括號數量是否匹配
                Return "公式有錯誤"
                Exit Function
            End If

            For i = 1 To Lks
                If Lkp(i) > Rkp(i) Then ' 左右括號順序錯誤
                    Return "公式有錯誤"
                    Exit Function
                End If
            Next i

            If Lks <> 0 Then ' 括號處理
                Do While True ' 無限迴圈直到跳出迴圈
                    ' 括號的搜尋為左刮號由內到外，右括號則是由外到內漸縮
                    For i = Lks To 1 Step -1
                        For n = Rks To 1 Step -1
                            Temp = Calc2(Mid(Expression, Lkp(i) + 1, Rkp(n) - Lkp(i) - 1)) ' 使用遞迴處理括號內的運算，並return至Temp
                            ' 括號漸縮過程可能產生不法的運算式，將期濾掉。 例: (3+2)*5) = 錯誤(括號數不相等)  => (3+2) = 5
                            If Temp <> "公式有錯誤" Then
                                Expression = Mid(Expression, 1, Lkp(i) - 1) + Temp + Mid(Expression, Rkp(n) + 1) ' 字串切割，重新整理字串內容，結束右括號內迴圈
                                Exit Do
                            End If
                        Next n
                    Next i
                    If Temp = "公式有錯誤" Then ' 括號中有錯誤退出
                        Return "公式有錯誤"
                        Exit Function
                    End If
                Loop
            Else
                ' 由於減號接被我們化為加號，故這邊不需減號的判斷運算
                If Mul <> 0 Then '乘法處理
                    ' 字串切割，計算乘法內容後再將值轉為字串塞回
                    Expression = Mid(Expression, 1, Sig(Mun(1) - 1)) + Str(Val(digitals(Mun(1))) * Val(digitals(Mun(1) + 1))) + Mid(Expression, Val(Mup(1)) + Len(digitals(Mun(1) + 1)) + 1)
                Else
                    If Div <> 0 Then '除法處理
                        ' 字串切割，計算除法內容後再將值轉為字串塞回
                        Expression = Mid(Expression, 1, Sig(Din(1) - 1)) + Str(Val(digitals(Din(1))) / Val(digitals(Din(1) + 1))) + Mid(Expression, Val(Dip(1)) + Len(digitals(Din(1) + 1)) + 1)
                    Else
                        If Add <> 0 Then '加法處理
                            ' 字串切割，計算加法內容後再將值轉為字串塞回
                            Expression = Str(Val(digitals(1)) + Val(digitals(2))) + Mid(Expression, Val(Adp(1)) + Len(digitals(2)) + 1)
                        Else
                            ' 最後完成運算值會得到如: 123# ， 所已將自訂截止符#去掉及可輸出返回
                            Return Mid(Expression, 1, Len(Expression) - 1)
                            Exit Function
                        End If
                    End If
                End If
            End If
        Loop

    End Function

    Public Function Calc3(Expression As String) As String

        Dim Obj As Object = CreateObject("ScriptControl")
        Obj.Language = "VBScript"

        Return Obj.Eval(Expression)

    End Function

    ' clear button
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' 清除輸入框及輸出顯示
        TextBox1.Text = ""
        Label1.Text = ""
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 清除輸入框及輸出顯示
        TextBox1.Text = ""
        Label1.Text = ""
    End Sub
End Class
