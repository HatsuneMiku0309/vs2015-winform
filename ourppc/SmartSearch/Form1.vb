Imports System.Threading

'Imports mshtml
Public Class Form1

    Public GoogleStep As Integer
    Public MaxIPercent As Double
    Public MaxKeyword As String
    Public BP, PB As Integer
    Public LastDoTime As DateTime
    Public ExeCount As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LastDoTime = Now
        Me.WebBrowser1.Navigate("http://www.ourppc.com/tw/account-login")
        BP = 0
        PB = 0
        MaxKeyword = ""
        MaxIPercent = 0
        'GoogleStep = 0
        GoogleStep = 1
        'MsgBox(Me.WebBrowser1.Version.ToString)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'WebBrowser1.Document.GetElementById("yschsp").SetAttribute("value", Me.TextBox2.Text)
        'WebBrowser1.Document.GetElementById("sf").InvokeMember("submit")
        If ExeCount >= 3 Then
            Exit Sub
        End If
        Me.WebBrowser1.Navigate("http://www.ourppc.com/tw/p/ads")
        GoogleStep = 2
        
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GoogleStep = 0
        Me.ComboBox1.SelectedIndex = 1
        LastDoTime = Now
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim i As Integer
        Dim GetData As Boolean
        GetData = False
        Dim element As HtmlElement
        Dim elementList As HtmlElementCollection

        If ExeCount >= 3 Then
            Exit Sub
        End If

        element = Me.WebBrowser1.Document.GetElementById("sb-site").GetElementsByTagName("header").Item(0).GetElementsByTagName("section").Item(1).GetElementsByTagName("div").Item(0).GetElementsByTagName("div").Item(0).GetElementsByTagName("div").Item(1).GetElementsByTagName("li").Item(1).GetElementsByTagName("span").Item(1)
        Dim strBP = element.InnerHtml.Replace("$", "").Replace(",", "").Replace(".00", "")
        BP = CInt(strBP)

        element = Me.WebBrowser1.Document.GetElementById("sb-site").GetElementsByTagName("header").Item(0).GetElementsByTagName("section").Item(1).GetElementsByTagName("div").Item(0).GetElementsByTagName("div").Item(0).GetElementsByTagName("div").Item(1).GetElementsByTagName("li").Item(5).GetElementsByTagName("span").Item(1)
        Dim strPB = element.InnerHtml.Replace("$", "").Replace(",", "").Replace(".00", "")
        PB = CInt(strPB)
        If PB > 0 Then
            BP = PB
        End If
        Me.TextBox9.Text = BP.ToString
        
        If Me.CheckBox1.Checked = True Then
            Me.TextBox8.Text = BP.ToString
        End If
        Me.TextBox6.Text = BP.ToString + vbCrLf + Me.TextBox6.Text
        
        'WebBrowser1.Document.GetElementById("keyword").SetAttribute("Value", "e")
        WebBrowser1.Document.GetElementById("keyword").GetElementsByTagName("option").Item(Me.ComboBox1.SelectedIndex).SetAttribute("selected", "selected")
        'WebBrowser1.Document.GetElementById("keyword").InvokeMember("onchange")
        Dim seltype As String
        Select Case Me.ComboBox1.Text
            Case "標準"
                seltype = "type_s"
            Case "高級"
                seltype = "type_p"
            Case "專屬"
                seltype = "type_e"
            Case "專屬共享"
                seltype = "type_l"
        End Select
       
        Me.WebBrowser1.Document.GetElementById("type_s").Style = "display: none;"
        Me.WebBrowser1.Document.GetElementById("type_p").Style = "display: none;"
        Me.WebBrowser1.Document.GetElementById("type_e").Style = "display: none;"
        Me.WebBrowser1.Document.GetElementById("type_l").Style = "display: none;"
        Me.WebBrowser1.Document.GetElementById("" + seltype.ToString).Style = "display: ;"

        elementList = Me.WebBrowser1.Document.GetElementById(seltype).GetElementsByTagName("div")
        For Each element In elementList
            i = i + 1
            Debug.Print(i.ToString + ":::" + element.GetAttribute("classname").ToString)
            'Debug.Print(element.InnerHtml)
            If element.TagName() = "DIV" And element.GetAttribute("classname").ToString = "data-container" Then

                If InStr(element.GetElementsByTagName("header").Item(0).InnerHtml, Me.ComboBox1.Text) > 0 Then
                    For j = 1 To element.GetElementsByTagName("table").Item(0).GetElementsByTagName("tbody").Item(0).GetElementsByTagName("tr").Count
                        With element.GetElementsByTagName("table").Item(0).GetElementsByTagName("tbody").Item(0).GetElementsByTagName("tr").Item(j - 1).GetElementsByTagName("td")
                            If .Item(0).GetElementsByTagName("a").Item(0).GetAttribute("classname").ToString = "btn btn-sm btn-success make_invest" Then
                                ' Me.TextBox6.Text = Me.TextBox6.Text + .Item(0).InnerText + "   ,  "
                                'Me.TextBox6.Text = Me.TextBox6.Text + .Item(1).InnerText + "   ,  "
                                'Me.TextBox6.Text = Me.TextBox6.Text + .Item(2).InnerText + "   ,  "
                                'Me.TextBox6.Text = Me.TextBox6.Text + .Item(3).InnerText + "   ,  "


                                Dim strseday = .Item(4).GetElementsByTagName("span").Item(0).InnerText.Replace(" ", "")
                                strseday = strseday.Replace("年", "/")
                                strseday = strseday.Replace("月", "/")
                                strseday = strseday.Replace("日", "")
                                Dim arrayseday = strseday.Split("-")
                                Dim seday = DateTime.Parse(arrayseday(1)) - DateTime.Parse(arrayseday(0))
                                Dim iday = .Item(5).InnerText.Replace("天", "")
                                iday = iday.Replace(" ", "")
                                Dim stripercent = .Item(6).InnerText.Replace("%", "")
                                stripercent = stripercent.Replace(" ", "")
                                Dim arrayipercent = stripercent.Split("-")
                                Dim ipercent = ((arrayipercent(1) + 0) + (arrayipercent(0) + 0)) / 2
                                If (ipercent / (seday.Days + iday)) > MaxIPercent Then
                                    MaxIPercent = (ipercent / (seday.Days + iday))
                                    MaxKeyword = .Item(1).InnerText
                                End If
                                ' Me.TextBox6.Text = (ipercent / (seday.Days + iday)).ToString + "   ,  " + Me.TextBox6.Text

                                'Me.TextBox6.Text = Me.TextBox6.Text + vbCrLf
                            End If
                        End With
                    Next
                    GoogleStep = 3
                    Button4_Click(Me, e)
                    Me.TextBox1.Text = "Button4_Click(Me, e)" + Me.TextBox1.Text + vbCrLf
                End If
                Me.TextBox6.Text = MaxKeyword + "::::" + MaxIPercent.ToString + vbCrLf + Me.TextBox6.Text
                'Me.TextBox6.Text = Me.TextBox6.Text + ":::::" + elementList(i - 1).InnerHtml
                'For Each element2 As HtmlElement In elementList
                'Debug.Print(element2.GetAttribute("classname").ToString)
                'Next
            End If
        Next
        ''If GetData = False Then
        ''    'Call Button6_Click(Me, e)
        ''    GoogleStep = 1
        ''End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim element, element2 As HtmlElement
        Dim elementList, elementList2 As HtmlElementCollection

        If ExeCount >= 3 Then
            Exit Sub
        End If
        ExeCount = ExeCount + 1
        If MaxIPercent < CDbl(Me.TextBox7.Text) Then
            GoogleStep = 1
            Me.TextBox1.Text = "MaxIPercent < Me.TextBox7.Text" + ";" + Me.TextBox1.Text
            Button2_Click(Me, e)
            Exit Sub
        End If
        If BP <= 0 Then
            GoogleStep = 1
            Me.TextBox1.Text = GoogleStep.ToString + ";" + Me.TextBox1.Text
            Exit Sub
        End If
        Dim seltype
        Select Case Me.ComboBox1.Text
            Case "標準"
                seltype = "type_s"
            Case "高級"
                seltype = "type_p"
            Case "專屬"
                seltype = "type_e"
            Case "專屬共享"
                seltype = "type_l"
        End Select
        elementList = Me.WebBrowser1.Document.GetElementById(seltype.ToString).GetElementsByTagName("div")
        For Each element In elementList
            'Debug.Print(element.InnerHtml)
            If element.TagName() = "DIV" And element.GetAttribute("classname").ToString = "data-container" Then
                If InStr(element.GetElementsByTagName("header").Item(0).InnerHtml, Me.ComboBox1.Text) > 0 Then
                    For j = 1 To element.GetElementsByTagName("table").Item(0).GetElementsByTagName("tbody").Item(0).GetElementsByTagName("tr").Count
                        With element.GetElementsByTagName("table").Item(0).GetElementsByTagName("tbody").Item(0).GetElementsByTagName("tr").Item(j - 1).GetElementsByTagName("td")
                            If .Item(0).GetElementsByTagName("a").Item(0).GetAttribute("classname").ToString = "btn btn-sm btn-success make_invest" Then
                                If MaxKeyword = .Item(1).InnerText Then
                                    .Item(0).GetElementsByTagName("a").Item(0).Id = "gggggggg"
                                    WebBrowser1.Document.GetElementById("gggggggg").InvokeMember("click")
                                    System.Threading.Thread.Sleep(0.3)
                                    WebBrowser1.Document.GetElementById("amount").SetAttribute("value", Me.TextBox8.Text)
                                    elementList2 = Me.WebBrowser1.Document.GetElementsByTagName("form")
                                    For Each element2 In elementList2
                                        If element2.GetAttribute("name").ToString = "invest_ads_form" Then
                                            element2.InvokeMember("submit")
                                            MaxIPercent = 0
                                            MaxKeyword = ""
                                            BP = 0
                                            GoogleStep = 1
                                        End If
                                    Next
                                    '.Item(0).GetElementsByTagName("a").Item(0).Focus()
                                    'SendKeys.Send("{ENTER}")
                                End If
                            End If
                        End With
                    Next
                End If
            End If
        Next
        'WebBrowser1.Document.GetElementById("pg-next").Focus()
        'WebBrowser1.Document.GetElementById("pg-next").InvokeMember("click")
        'Me.TextBox4.Text = Me.TextBox4.Text + 1
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Me.WebBrowser1.Navigate("https://www.google.com.tw/")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        WebBrowser1.Document.GetElementById("lst-ib").SetAttribute("value", Me.TextBox2.Text)
        WebBrowser1.Document.GetElementById("tsf").InvokeMember("submit")

        Me.Timer1.Enabled = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)

        WebBrowser1.Document.GetElementById("pnnext").Focus()
        WebBrowser1.Document.GetElementById("pnnext").InvokeMember("click")
        Me.TextBox4.Text = Me.TextBox4.Text + 1
        GoogleStep = 2
        ' Dim rndNum As New Random()
        ' Dim ss = rndNum.Next(1, 5)
        'Thread.Sleep(ss * 1000)
        'Me.TextBox1.Text = Me.TextBox1.Text + vbCrLf + ss.ToString
        'Call Button7_Click(Me, e)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)

        Dim i As Integer
        Dim GetData As Boolean
        GetData = False
        For Each element As HtmlElement In Me.WebBrowser1.Document.All
            i = i + 1
            If element.TagName() = "A" And element.GetAttribute("onmousedown").ToString <> "" Then
                If element.GetAttribute("href").ToString = Me.TextBox3.Text Then
                    Dim startpos = InStr(1, element.OuterHtml.ToString(), "return rwt(this,'','','','")
                    If startpos > 0 Then
                        startpos = startpos + 26
                        Dim endpos = InStr(startpos, element.OuterHtml.ToString(), "'")
                        Me.TextBox5.Text = element.OuterHtml.Substring(startpos - 1, endpos - startpos)
                        'Me.TextBox1.Text = Me.TextBox1.Text + element.OuterHtml.ToString()
                        'onmousedown="return rwt(this,'','','','
                        Me.TextBox1.Text = Me.TextBox1.Text + "第" + Me.TextBox4.Text + "頁,第" + Me.TextBox5.Text + "筆" + ":::" + element.GetAttribute("href").ToString + vbCrLf
                        GetData = True
                        GoogleStep = 99
                    End If
                End If
            End If
        Next
        If GetData = False Then
            'Call Button6_Click(Me, e)
            GoogleStep = 1
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If DateDiff("s", LastDoTime, Now) > 180 Then
            Button1_Click(Me, e)
        End If
        ''Select Case GoogleStep
        ''    Case 0
        ''        Call Button7_Click(Me, e)
        ''    Case 1
        ''        Call Button6_Click(Me, e)
        ''    Case 2
        ''        Call Button7_Click(Me, e)
        ''End Select
        ''If Me.TextBox4.Text > 10 Then
        ''    Me.Timer1.Enabled = False
        ''End If
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted       
        LastDoTime = Now
        Select Case GoogleStep
            Case 0
                ' ''If e.Url.ToString = "http://www.ourppc.com/tw/p/" Then
                ' ''    GoogleStep = 1
                ' ''    Button2_Click(Me, e)
                ' ''    Exit Sub
                ' ''End If
                ' ''If InStr(e.Url.ToString, "http://www.ourppc.com/tw/account-login") > 0 Then
                ' ''    Me.TextBox1.Text = e.Url.ToString + vbCrLf + Me.TextBox1.Text
                ' ''    Dim captcha As HtmlElement = WebBrowser1.Document.GetElementById("captcha")
                ' ''    '判斷captcha是否存在
                ' ''    If captcha IsNot Nothing Then
                ' ''        '調整顯示的位置
                ' ''        captcha.ScrollIntoView(False)
                ' ''        Try
                ' ''            'WebBrowser1.Document.DomDocument.GetType().GetProperty("designMode").SetValue(WebBrowser1.Document.DomDocument, "On", Nothing)
                ' ''            'Me.WebBrowser1.Document.ExecCommand.ExecCommand("EditMode", True, Nothing)
                ' ''            Dim doc As IHTMLDocument2 = DirectCast(WebBrowser1.Document.DomDocument, IHTMLDocument2)
                ' ''            Dim body As IHTMLElement2 = DirectCast(doc.body, IHTMLElement2)
                ' ''            Dim rang As IHTMLControlRange = DirectCast(body.createControlRange(), IHTMLControlRange)
                ' ''            Dim Img As IHTMLControlElement = DirectCast(DirectCast(doc, IHTMLDocument3).getElementById("captcha"), IHTMLControlElement)
                ' ''            ''imgRange.add(image)
                ' ''            ''imgRange.execCommand("Copy", False, Nothing)


                ' ''            'Dim rang = WebBrowser1.Document.DomDocument.Body.createControlRange()
                ' ''            Me.TextBox1.Text = rang.ToString + vbCrLf + Me.TextBox1.Text
                ' ''            ''Dim Img = captcha.DomElement
                ' ''            Me.TextBox1.Text = "000000" + vbCrLf + Me.TextBox1.Text
                ' ''            rang.add(Img)
                ' ''            Me.TextBox1.Text = "11111111" + vbCrLf + Me.TextBox1.Text
                ' ''            rang.execCommand("Copy", False, Nothing)
                ' ''            Dim RegImg As Image = Clipboard.GetImage()
                ' ''            Clipboard.Clear()
                ' ''            Me.BackgroundImageLayout = ImageLayout.None
                ' ''            Me.BackgroundImage = RegImg

                ' ''            Me.BackgroundImage.Save(Application.StartupPath.ToString + "\test_save.bmp")

                ' ''            '取得captcha圖片的src
                ' ''            Dim src As String = Application.StartupPath.ToString + "\test_save.bmp"
                ' ''            '從src取得圖片
                ' ''            Dim img2 As New Bitmap(GetImageFromURL(src))
                ' ''            '進行OCR並將結果填到網頁中
                ' ''            Dim element As IHTMLElement
                ' ''            Dim elementList As IHTMLElementCollection

                ' ''            elementList = DirectCast(doc, IHTMLDocument3).getElementsByTagName("input")
                ' ''            For Each element In elementList
                ' ''                'Me.TextBox1.Text = "eee:" + element.getAttribute("name").ToString + vbCrLf + Me.TextBox1.Text
                ' ''                If element.getAttribute("name").ToString = "captcha" Then
                ' ''                    element.setAttribute("value", TesseractOCR(img2))
                ' ''                End If
                ' ''            Next
                ' ''            DirectCast(doc, IHTMLDocument3).getElementById("username").setAttribute("value", Me.TextBox2.Text)
                ' ''            DirectCast(doc, IHTMLDocument3).getElementById("password").setAttribute("value", Me.TextBox3.Text)
                ' ''            DirectCast(doc, IHTMLDocument3).getElementById("login_btn").click()
                ' ''            'WebBrowser1.Document.GetElementById("password").SetAttribute("value", Me.TextBox3.Text)
                ' ''            'WebBrowser1.Document.GetElementById("login_btn").InvokeMember("click")
                ' ''        Catch ex As Exception
                ' ''            MessageBox.Show(ex.Message)
                ' ''        End Try
                ' ''    End If
                ' ''End If
            Case 1
                Button2_Click(Me, e)
                Me.TextBox1.Text = "Button2_Click(Me, e)" + vbCrLf + Me.TextBox1.Text
            Case 2
                Button3_Click(Me, e)
                Me.TextBox1.Text = "Button3_Click(Me, e)" + vbCrLf + Me.TextBox1.Text
            Case 3
                GoogleStep = 1
                Button2_Click(Me, e)
                Me.TextBox1.Text = "Button2_Click(Me, e)" + vbCrLf + Me.TextBox1.Text
        End Select
        
    End Sub

    Private Sub WebBrowser1_StatusTextChanged(sender As Object, e As EventArgs) Handles WebBrowser1.StatusTextChanged
        ''On Error Resume Next
        ''Dim head As HtmlElement = WebBrowser1.Document.GetElementsByTagName("head")(0)
        ''Dim script As HtmlElement
        ''script = WebBrowser1.Document.CreateElement("script")
        ''script.SetAttribute("type", "text/javascript")
        ''Dim alertBlocker As String = "window.alert = function () { }; window.confirm=function () { }; "
        ''script.SetAttribute("text", alertBlocker)
        ''head.AppendChild(script)
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        Me.WebBrowser1.Navigate("http://my-user-agent.com/")

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Me.TextBox1.Text = GoogleStep.ToString + ":" + BP.ToString + ":" + PB.ToString + ";" + Me.TextBox1.Text + vbCrLf

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        GoogleStep = -1
    End Sub

    Function TesseractOCR(img As Bitmap) As String
        Try
            Dim ocr As New tessnet2.Tesseract
            ocr.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
            ocr.Init(".\tessdata", "eng", False)
            Dim result As List(Of tessnet2.Word) = ocr.DoOCR(img, Rectangle.Empty)
            Dim str As String = ""
            For i As Integer = 0 To result.Count - 1
                str &= result(i).Text
            Next
            Return str
        Catch ex As Exception
            Throw ex
        End Try
        Return String.Empty
    End Function

    Function GetImageFromURL(url As String) As Image
        Try

            Dim fs As New System.IO.FileStream(url, IO.FileMode.Open)
            Dim img As Image = Image.FromStream(fs)
            fs.Close()
            'Dim request As Net.WebRequest = Net.WebRequest.Create(url)
            'Dim response As Net.WebResponse = request.GetResponse()
            'Dim stream As IO.Stream = response.GetResponseStream()
            'Dim img As New Bitmap(stream)
            Return img
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        MaxIPercent = 0
        MaxKeyword = ""
        BP = 0
        PB = 0
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        Dim dt As String

        Try
            If InStr(Me.WebBrowser1.Document.Body.InnerHtml, "無法顯示") > 0 Then
                Me.TextBox1.Text = "網頁無法連線重置" + vbCrLf + Me.TextBox1.Text
                Button1_Click(Me, e)
                Exit Sub
            End If
            dt = Me.WebBrowser1.Document.GetElementById("local_time_text").InnerText.Substring(6, 2)
            Me.TextBox2.Text = "wb:" + WebBrowser1.StatusText + ",GS:" + GoogleStep.ToString + ",dt:" + dt.ToString + ",EC:" + ExeCount.ToString
            If Me.WebBrowser1.Url.ToString = "http://www.ourppc.com/tw/p/ads" Then
                If dt = Me.TextBox5.Text Then
                    ExeCount = 1
                    Button2_Click(Me, e)
                End If
            End If
        Catch ex As Exception

        End Try
        '
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        Me.TextBox1.Text = ""
        Me.TextBox6.Text = ""
    End Sub
End Class
