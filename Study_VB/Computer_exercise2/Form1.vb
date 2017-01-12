'reference http://goo.gl/Vrhv3K
'reference http://goo.gl/QCDJns

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Dim returnValue As String = Calc(TextBox1.Text)
        Dim returnValue2 As String = Calc2(TextBox1.Text)
        MessageBox.Show(returnValue2)


        'Dim opNumStr(10) As Char
        'Dim opNumDub(20) As String

        'Dim inputExpression() As Char = TextBox1.Text.ToCharArray()

        'Dim strCount As Integer = 0
        'Dim DubCount As Integer = 0

        'Dim rule() As Char = {"*"c, "/"c, "+"c, "-"c}
        'Dim rule2() As Char = {"+"c, "-"c, "*"c, "/"c}
        'Dim result As Integer = 0

        'For index As Integer = 0 To inputExpression.Length - 1
        '    Debug.WriteLine(inputExpression(index))
        '    If inputExpression(index) <> "+"c And inputExpression(index) <> "-"c And inputExpression(index) <> "*"c And inputExpression(index) <> "/"c Then
        '        opNumStr(strCount) = inputExpression(index)
        '        strCount += 1
        '        If index = inputExpression.Length - 1 Then
        '            opNumDub(DubCount) = CStr(opNumStr)
        '            ' Debug.WriteLine(opNumDub(DubCount))
        '            DubCount += 1
        '            ReDim opNumStr(10)
        '        End If
        '    Else
        '        opNumDub(DubCount) = CStr(opNumStr) ' 運算值
        '        ' Debug.WriteLine(opNumDub(DubCount))
        '        DubCount += 1
        '        opNumDub(DubCount) = inputExpression(index) ' 運算符號
        '        DubCount += 1
        '        strCount = 0
        '        ReDim opNumStr(10)
        '    End If
        'Next

        'For index As Integer = 0 To rule.Length - 1
        '    For index2 As Integer = 0 To opNumDub.Length - 1
        '        If (rule(index) = opNumDub(index2)) Then
        '            Select Case rule(index)
        '                Case "+"c
        '                    result += CInt(opNumDub(index2 - 1)) + CInt(opNumDub(index2 + 1))
        '                Case "-"c
        '                    result += CInt(opNumDub(index2 - 1)) - CInt(opNumDub(index2 + 1))
        '                Case "*"c
        '                    result += CInt(opNumDub(index2 - 1)) * CInt(opNumDub(index2 + 1))
        '                Case "/"c
        '                    result += CInt(opNumDub(index2 - 1)) / CInt(opNumDub(index2 + 1))
        '            End Select

        '        End If
        '    Next
        'Next

        'Label1.Text = result


        'For Each value In opNumDub
        '    If value <> "" Then
        '        MessageBox.Show(value)
        '    End If
        'Next

    End Sub

    Public Function Calc(GS As String) As String
        Dim i, n As Integer
        Dim TempGs, Temp As String
        Dim Vl() As String '操作數
        Dim Vls As Integer '操作數的數目
        Dim Si As Integer '上一操作符的位置
        Dim Ads, Sus, Mus, Bys, Lks, Rks As Integer   '操作符的數目
        Dim Adp(), Mup(), Byp(), Lkp(), Rkp(), Lkn(), Rkn() As Integer '操作符的位置
        Dim Adn(), Mun(), Byn() As Integer '操作符的排列次序
        Dim Sig() As Integer '每一個操作符的位置

        On Error GoTo Err
        Do While True
            ' MessageBox.Show(GS)
            ReDim Adp(Len(GS)), Mup(Len(GS)), Byp(Len(GS)), Lkp(Len(GS)), Rkp(Len(GS))
            ReDim Adn(Len(GS)), Mun(Len(GS)), Byn(Len(GS)), Lkn(Len(GS)), Rkn(Len(GS)), Sig(Len(GS))

            ReDim Vl(Len(GS))

            If Len(GS) = 0 Then GoTo Err
            If Mid(GS, Len(GS), 1) <> "#" Then

                TempGs = GS
                For i = 1 To Len(GS) '將減化加
                    If Mid(GS, i, 1) = "-" And i <> 1 Then
                        If Mid(GS, i - 1, 1) <> "+" And Mid(GS, i - 1, 1) <> "-" _
                                  And Mid(GS, i - 1, 1) <> "*" And Mid(GS, i - 1, 1) <> "/" Then
                            TempGs = Mid(TempGs, 1, i - 1 + n) + "+" + Mid(GS, i)
                            n = n + 1
                        End If

                    End If
                Next i
                GS = TempGs

                n = 0
                For i = 1 To Len(GS) '處理負負得正
                    If Mid(GS, i, 1) = "-" Then
                        If Mid(GS, i + 1, 1) = "-" Then
                            TempGs = Mid(TempGs, 1, i - 1 - n) + Mid(GS, i + 2)
                            n = n + 2
                        End If
                    End If
                Next i
                GS = TempGs
                GS = GS + "#"
                'Debug.WriteLine(GS)
            End If

            Vls = 1
            Ads = 0 : Sus = 0 : Mus = 0 : Bys = 0 : Lks = 0 : Rks = 0

            For i = 1 To Len(GS)

                Select Case Mid(GS, i, 1)
                    Case "+"
                        Ads = Ads + 1
                        Adp(Ads) = i
                        Adn(Ads) = Vls
                    Case "*"
                        Mus = Mus + 1
                        Mup(Mus) = i
                        Mun(Mus) = Vls
                    Case "/"
                        Bys = Bys + 1
                        Byp(Bys) = i
                        Byn(Bys) = Vls
                    Case "("
                        Lks = Lks + 1
                        Lkp(Lks) = i

                    Case ")"
                        Rks = Rks + 1
                        Rkp(Rks) = i

                End Select

                If Mid(GS, i, 1) = "+" Or Mid(GS, i, 1) = "*" Or
                Mid(GS, i, 1) = "/" Or Mid(GS, i, 1) = "#" Then

                    If Si + 1 = i And Mid(GS, i + 1, 1) <> "#" _
                             Then '操作符非法連續或以操作符開頭
                        GoTo Err
                    Else
                        Si = i
                    End If

                    If Not IsNumeric(Vl(Vls)) And Mid(GS, i + 1, 1) <> "#" _
                             Then '操作數不是數字
                        GoTo Err
                    End If
                    Sig(Vls) = i
                    Vls = Vls + 1

                Else
                    If Mid(GS, i, 1) <> "(" And Mid(GS, i, 1) <> ")" Then
                        Vl(Vls) = Vl(Vls) + Mid(GS, i, 1) '制作操作數

                    Else
                        If i <> 1 Then
                            If ((Mid(GS, i - 1, 1) = "(" And Mid(GS, i, 1) = ")") Or
                             (Mid(GS, i - 1, 1) = ")" And Mid(GS, i, 1) = "(")) _
                                       Then '判定括號前後符號的合法性
                                GoTo Err
                            End If
                        End If
                    End If
                End If

            Next i

            If Lks <> Rks Then
                GoTo Err '左右括號數是否匹配
            End If

            For i = 1 To Lks
                If Lkp(i) > Rkp(i) Then GoTo Err '左右括號出現順序錯誤
            Next i

            If Lks <> 0 Then '括號處理
                Do While True
                    For i = Lks To 1 Step -1
                        For n = Rks To 1 Step -1
                            Temp = Calc(Mid(GS, Lkp(i) + 1, Rkp(n) - Lkp(i) - 1))
                            MessageBox.Show(Mid(GS, Lkp(i) + 1, Rkp(n) - Lkp(i) - 1))
                            If Temp <> "公式有錯誤" Then
                                GS = Mid(GS, 1, Lkp(i) - 1) + Temp + Mid(GS, Rkp(n) + 1)
                                Exit Do
                            End If
                        Next n
                    Next i
                    If Temp = "公式有錯誤" Then GoTo Err
                    '括號中有錯誤退出
                Loop
            Else
                If Mus <> 0 Then '乘法處理
                    GS = Mid(GS, 1, Sig(Mun(1) - 1)) + Trim(Str(Val(Vl(Mun(1))) _
                      * Val(Vl(Mun(1) + 1)))) + Mid(GS, Val(Mup(1)) + Len(Vl(Mun(1) _
                      + 1)) + 1)
                Else
                    If Bys <> 0 Then '除法處理
                        GS = Mid(GS, 1, Sig(Byn(1) - 1)) + Trim(Str(Val(Vl(Byn(1))) _
                          / Val(Vl(Byn(1) + 1)))) + Mid(GS, Val(Byp(1)) + Len(Vl(Byn(1) _
                          + 1)) + 1)
                    Else
                        If Ads <> 0 Then '加法處理
                            GS = Trim(Str(Val(Vl(1)) + Val(Vl(2)))) + Mid(GS, Val(Adp(1)) _
                              + Len(Vl(2)) + 1)
                        Else
                            Calc = Mid(GS, 1, Len(GS) - 1)
                            Exit Function
                        End If
                    End If
                End If
            End If
        Loop

Err:
        Calc = "公式有錯誤"

    End Function


    Public Function Calc2(GS As String) As String
        Dim i, n As Integer
        Dim TempGs, Temp As String
        Dim digitals() As String '數字 digitals
        Dim Operators As Integer '運算符的數目 Operators
        Dim preOperators As Integer '上一操作符的位置
        Dim Add, Mul, Div, Lks, Rks As Integer   '操作符的數目 Add(+) Mul(*) Div(/) Lks(左括) Rks(右括)
        Dim Adp(), Mup(), Dip(), Lkp(), Rkp(), Lkn(), Rkn() As Integer '操作符的位置
        Dim Adn(), Mun(), Din() As Integer '操作符的排列次序
        Dim Sig() As Integer '每一個操作符的位置


        ' 無限回圈直到結束function
        Do While True

            ' 設定陣列數
            ReDim Adp(Len(GS)), Mup(Len(GS)), Dip(Len(GS)), Lkp(Len(GS)), Rkp(Len(GS))
            ReDim Adn(Len(GS)), Mun(Len(GS)), Din(Len(GS)), Lkn(Len(GS)), Rkn(Len(GS)), Sig(Len(GS))
            ReDim digitals(Len(GS))

            ' 檢查運算式是否為空
            If Len(GS) = 0 Then
                Return "公式有錯誤"
                Exit Function
            End If

            ' 檢查字串最後是否有自定義 # 終止符
            If Mid(GS, Len(GS), 1) <> "#" Then
                TempGs = GS

                ' 將減改為+的方式 例: 1+-2
                For i = 1 To Len(GS)
                    ' 查找-號及不為第首字
                    If Mid(GS, i, 1) = "-" And i <> 1 Then
                        ' 判斷前項是否有其他運算符
                        If Mid(GS, i - 1, 1) <> "+" And Mid(GS, i - 1, 1) <> "-" _
                                  And Mid(GS, i - 1, 1) <> "*" And Mid(GS, i - 1, 1) <> "/" Then
                            TempGs = Mid(TempGs, 1, i - 1 + n) + "+" + Mid(GS, i) ' 進行字串切割及塞入+
                            n = n + 1
                        End If
                    End If
                Next i

                ' 暫存處理字串給回原來
                GS = TempGs

                n = 0
                '處理負負得正
                For i = 1 To Len(GS)
                    If Mid(GS, i, 1) = "-" Then
                        ' 找尋該 - 字元後一位字元是否為 -
                        If Mid(GS, i + 1, 1) = "-" Then
                            TempGs = Mid(TempGs, 1, i - 1 - n) + Mid(GS, i + 2) ' 進行字串切割，原字串(+--)處理為(+)
                            n = n + 2 ' 每一負負得正被處理為(+--)，而上將後面兩個負號切割掉，故要扣除兩個負號字長
                        End If
                    End If
                Next i
                ' 暫存處理字串給回原來
                GS = TempGs
                GS = GS + "#" ' 給與自訂截止符
                'Debug.WriteLine(GS)
            End If


            Operators = 1
            Add = 0 : Mul = 0 : Div = 0 : Lks = 0 : Rks = 0
            ' 操作符的順序、位置取出
            For i = 1 To Len(GS)
                If (Not IsNumeric(Mid(GS, i, 1))) Then
                    Select Case Mid(GS, i, 1)
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

                If Mid(GS, i, 1) = "+" Or Mid(GS, i, 1) = "*" Or
                Mid(GS, i, 1) = "/" Or Mid(GS, i, 1) = "#" Then

                    If preOperators + 1 = i And Mid(GS, i + 1, 1) <> "#" Then '操作符不當連續或以操作符開頭(不包含-號)
                        Return "公式有錯誤"
                        Exit Function
                    Else
                        preOperators = i
                    End If

                    If Not IsNumeric(digitals(Operators)) And Mid(GS, i + 1, 1) <> "#" Then ' 數字不是數字
                        Return "公式有錯誤"
                        Exit Function
                    End If
                    Sig(Operators) = i ' 用來控制運算符位置
                    Operators += 1

                Else
                    If Mid(GS, i, 1) <> "(" And Mid(GS, i, 1) <> ")" Then
                        digitals(Operators) += Mid(GS, i, 1) ' 製作數字
                    Else
                        If i <> 1 Then
                            ' 判定括號前後的合法性
                            If ((Mid(GS, i - 1, 1) = "(" And Mid(GS, i, 1) = ")") Or (Mid(GS, i - 1, 1) = ")" And Mid(GS, i, 1) = "(")) Then
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
                            Temp = Calc2(Mid(GS, Lkp(i) + 1, Rkp(n) - Lkp(i) - 1)) ' 使用遞迴處理括號內的運算，並return至Temp
                            ' 括號漸縮過程可能產生不法的運算式，將期濾掉。 例: (3+2)*5) = 錯誤(括號數不相等)  => (3+2) = 5
                            If Temp <> "公式有錯誤" Then
                                GS = Mid(GS, 1, Lkp(i) - 1) + Temp + Mid(GS, Rkp(n) + 1) ' 字串切割，重新整理字串內容，結束右括號內迴圈
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
                    GS = Mid(GS, 1, Sig(Mun(1) - 1)) + Str(Val(digitals(Mun(1))) * Val(digitals(Mun(1) + 1))) + Mid(GS, Val(Mup(1)) + Len(digitals(Mun(1) + 1)) + 1)
                Else
                    If Div <> 0 Then '除法處理
                        ' 字串切割，計算除法內容後再將值轉為字串塞回
                        GS = Mid(GS, 1, Sig(Din(1) - 1)) + Str(Val(digitals(Din(1))) / Val(digitals(Din(1) + 1))) + Mid(GS, Val(Dip(1)) + Len(digitals(Din(1) + 1)) + 1)
                    Else
                        If Add <> 0 Then '加法處理
                            ' 字串切割，計算加法內容後再將值轉為字串塞回
                            GS = Str(Val(digitals(1)) + Val(digitals(2))) + Mid(GS, Val(Adp(1)) + Len(digitals(2)) + 1)
                        Else
                            ' 最後完成運算值會得到如: 123# ， 所已將自訂截止符#去掉及可輸出返回
                            Return Mid(GS, 1, Len(GS) - 1)
                            Exit Function
                        End If
                    End If
                End If
            End If
        Loop

Err:
        Calc2 = "公式有錯誤"

    End Function

End Class
