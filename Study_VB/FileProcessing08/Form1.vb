Imports System.IO

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' open
        OpenFileDialog1.AddExtension = True
        OpenFileDialog1.CheckFileExists = True
        'OpenFileDialog1.Filter = "所有檔案|*.*"

        'OpenFileDialog1.FilterIndex = 4
        OpenFileDialog1.FileName = "FileName.txt"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.RestoreDirectory = True
        'OpenFileDialog1.ShowReadOnly = True
        OpenFileDialog1.Title = "開啟舊檔"

        ' save
        SaveFileDialog1.Title = "另存新檔"
        SaveFileDialog1.InitialDirectory = "C:\"
        SaveFileDialog1.FileName = "FileName.txt"
        SaveFileDialog1.Filter = "所有檔案|*.*"

        ' move
        SaveFileDialog2.Title = "搬移/更名"
        SaveFileDialog2.InitialDirectory = "C:\"
        SaveFileDialog2.FileName = "FileName.txt"
        SaveFileDialog2.Filter = "所有檔案|*.*"
    End Sub

    ' 瀏覽檔案
    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Dim PathString As String = ""

        ' 如有開啟檔案，以該檔案路徑為基礎路徑
        Dim TempPath() As String = TextBox1.Text.Split("\")

        For Each index As String In TempPath
            If (index <> "") Then
                PathString &= index & "\"
            End If
        Next

        OpenFileDialog1.InitialDirectory = PathString
        OpenFileDialog1.ShowDialog()
    End Sub
    ' 選擇開啟檔案確認
    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim files As String = OpenFileDialog1.FileName

        TextBox1.Text = files

    End Sub
    ' 複製檔案
    Private Sub CopyButton_Click(sender As Object, e As EventArgs) Handles CopyButton.Click

        If (TextBox1.Text <> "") Then
            ' 以該檔案路徑為基礎路徑
            Dim PathString As String = ""

            Dim TempPath() As String = TextBox1.Text.Split("\")

            For Each index As String In TempPath
                If (index <> "") Then
                    PathString &= index & "\"
                End If
            Next

            SaveFileDialog1.InitialDirectory = PathString

            Dim FilenameExtension() As String = TextBox1.Text.Split("."c)
            ' 取得瀏覽檔案的副檔名做為copy的副檔名
            SaveFileDialog1.FileName = "FileName." & FilenameExtension(1)


            SaveFileDialog1.ShowDialog()
        Else
            MessageBox.Show("請先開啟檔案！")
        End If


    End Sub
    ' 存檔卻認
    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim SavePath As String = SaveFileDialog1.FileName
        Dim msg As String = ""

        If (File.Exists(TextBox1.Text)) Then
            msg &= "直行檔案拷貝動作" & vbCrLf
            msg &= "拷貝來源:" & TextBox1.Text & vbCrLf
            msg &= "拷貝目的檔:" & SavePath & vbCrLf
            msg &= "檔案複製成功!!" & vbCrLf

            File.Copy(TextBox1.Text, SavePath, True)
            MessageBox.Show(msg)
        Else
            MessageBox.Show("找不到:" & TextBox1.Text)
        End If
        'Debug.Write(SavePath)
    End Sub
    ' 刪除檔案
    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If (TextBox1.Text <> "") Then
            Dim DeletePath As String = TextBox1.Text

            If (File.Exists(DeletePath)) Then
                Dim result As DialogResult
                result = MessageBox.Show("確定要刪除此檔案嗎?", "檔案刪除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

                If (result = 1) Then
                    File.Delete(DeletePath)
                    TextBox1.Text = ""
                End If
            Else
                MessageBox.Show("找不到:" & DeletePath)
            End If
        Else
            MessageBox.Show("請先開啟檔案！")
        End If

    End Sub
    ' 移動/更名檔案
    Private Sub MoveButton_Click(sender As Object, e As EventArgs) Handles MoveButton.Click
        If (TextBox1.Text <> "") Then
            Dim PathString As String = ""
            Dim TempPath() As String = TextBox1.Text.Split("\")

            For Each index As String In TempPath
                If (index <> "") Then
                    PathString &= index & "\"
                End If
            Next

            SaveFileDialog1.InitialDirectory = PathString

            Dim FilenameExtension() As String = TextBox1.Text.Split("."c)
            ' 取得瀏覽檔案的副檔名做為copy的副檔名
            SaveFileDialog2.FileName = "FileName." & FilenameExtension(1)


            SaveFileDialog2.ShowDialog()
        Else
            MessageBox.Show("請先開啟檔案！")
        End If
    End Sub
    ' 移動/更名檔案確認
    Private Sub SaveFileDialog2_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog2.FileOk
        Dim OldFileName As String = TextBox1.Text
        Dim NewFileName As String = SaveFileDialog2.FileName
        Dim msg As String = ""

        If (File.Exists(OldFileName)) Then
            msg &= "檔案:[" & OldFileName & "]有找到!" & vbCrLf
            msg &= "檔名已被更改為:[" & NewFileName & "]!"

            File.Move(OldFileName, NewFileName)
            TextBox1.Text = NewFileName
            MessageBox.Show(msg)
        Else
            msg &= "檔案:[" & OldFileName & "]沒有找到!"
            MessageBox.Show(msg)
        End If
    End Sub
    ' 檔案info
    Private Sub ViewInfoButton_Click(sender As Object, e As EventArgs) Handles ViewInfoButton.Click
        If (TextBox1.Text <> "") Then
            Dim FilePath As String = TextBox1.Text
            Dim msg As String = ""

            If (File.Exists(FilePath)) Then
                msg &= "檔案:[" & FilePath & "]有找到!" & vbCrLf
                msg &= "建立時間:[" & File.GetCreationTime(FilePath) & "]!" & vbCrLf
                msg &= "上次存取時間:[" & File.GetLastAccessTime(FilePath) & "]!" & vbCrLf
                msg &= "上次寫入時間:[" & File.GetLastWriteTime(FilePath) & "]!" & vbCrLf

                MessageBox.Show(msg)
            Else
                MessageBox.Show("找不到:" & FilePath)
            End If
        Else
            MessageBox.Show("請先開啟檔案！")
        End If
    End Sub
End Class
