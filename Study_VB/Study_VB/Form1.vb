'快速註解 ctrl + k -> ctrl + c
'快速取消註解 ctrl + k -> ctrl + u

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim obj1 As New Car
        Dim obj2 As New Motorcycle
        Dim eq As Boolean = obj1 Is obj2

        If (eq) Then
            MessageBox.Show(eq.ToString())
        Else
            MessageBox.Show(eq.ToString())
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim MyChar As Char = "t"c
        ' Dim MyChar As Char = "t" 'This is no different with the above declare. But book talk me this must care
        Dim MyInt As Integer = 123

        MessageBox.Show(MyInt) 'do not change type
        ' MessageBox.Show(MyChar)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For index As Integer = 1 To 5
            For index2 As Integer = 5 To 1 Step -1
                ' Debug.WriteLine(index.ToString())
                Debug.WriteLine(index)
            Next ' Next index2
        Next ' Next index2

        Dim chk As Boolean = True
        Dim chkInt As Integer = 1

        While chk 'chkInt
            Debug.WriteLine(chkInt)
            If chkInt = 3 Then
                chk = False
                Exit While
            End If
            chkInt += 1
        End While
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' VB's Select = C#'s switch
        Select Case TextBox1.Text
            Case 1 To 5
                Debug.WriteLine(TextBox1.Text)
            Case Else
                Debug.WriteLine("undefine")
        End Select

    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ComboBox1.Items.Add("Test")
        ComboBox1.Items.Add("Test2")
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If IsNumeric(TextBox2.Text) Then
            MessageBox.Show("test")
        End If
    End Sub
End Class

Friend Class Car
End Class

Class Motorcycle
End Class
