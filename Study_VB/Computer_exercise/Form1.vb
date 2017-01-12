Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim re() As Char = {"+"c, "-"c, "*"c, "/"c}
        Dim re2() As Char = {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c}

        ' 計算的順序
        Dim countSort() As Char = {"*"c, "/"c, "+"c, "-"c}

        Dim numberSplit() As String
        Dim opNumber() As Integer
        Dim SignsSplit() As String

        Dim result As Double = 0

        ' 字串拆解
        numberSplit = TextBox1.Text.Split(re)
        SignsSplit = TextBox1.Text.Split(re2)

        ' 設定數字陣列大小
        opNumber = New Integer(numberSplit.Length) {}

        ' 判定是否為空輸入
        If (TextBox1.Text <> "") Then
            For index As Integer = 0 To numberSplit.Length - 1
                If numberSplit(index) <> "" Then
                    If IsNumeric(numberSplit(index).Replace(" ", "")) Then ' 確認數字
                        opNumber(index) = CInt(numberSplit(index).Replace(" ", "")) ' 字串數字轉為int數字(各字串中空白去除)
                    Else ' 含有不法字元跳出Sub
                        MessageBox.Show("您的運算式含有不法字元")
                        Exit Sub
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
        Label1.Text = result

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = ""
    End Sub
End Class
