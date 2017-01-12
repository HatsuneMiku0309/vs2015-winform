using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Web;

using System.Reflection; // memberinfo
using System.ComponentModel;

namespace MyWebRequestAndResponse
{
    public partial class WebRequestAndResponse
    {
        public static void fWebRequestCreate(string uri)
        {
            Uri uri1 = new Uri(uri);
            string result = "";
            WebRequest MyWebRequest = null;

            // 以WebRequest抽像類別的Create方法建立WebRequest物件 (不了解)
            MyWebRequest = WebRequest.Create(uri);

            // 連結群組名稱
            if (MyWebRequest.ConnectionGroupName != null) {
                result += "ConnectionGroupName: " + MyWebRequest.ConnectionGroupName.ToString() + "\r\n";
            }

            // 用戶端所傳送資料內容大小 (byte)
            if (MyWebRequest.ContentLength != -1) {
                result += "ConnectionLenght: " + MyWebRequest.ContentLength.ToString() + "\r\n";
            }

            // 用戶端所傳送資料內容的MIME格式
            if (MyWebRequest.ContentType != null) {
                result += "ConnectionType: " + MyWebRequest.ContentType.ToString() + "\r\n";
            }

            // 用戶端網路認證
            if (MyWebRequest.Credentials != null) {
                result += "Credentials: " + MyWebRequest.Credentials.ToString() + "\r\n";
            }

            // 用戶端所傳送資料內容的標題資訊
            result += "Headers: " + MyWebRequest.Headers.ToString() + "\r\n";

            // 用戶端所使用的通訊協定
            result += "Method: " + MyWebRequest.Method.ToString() + "\r\n";

            // 是否要求預先驗正
            result += "PreAuthenticate: " + MyWebRequest.PreAuthenticate.ToString() + "\r\n";

            // 用戶端所傳送的URI
            result += "RequestUri: " + MyWebRequest.RequestUri.ToString() + "\r\n";

            MessageBox.Show(result);
        }


        /*
           非同步需新增一個"線程(Thread)" 去處理。
         */
        public static System.Threading.ManualResetEvent allDone = null;

        /// <summary>
        ///     async WebRequest and Synchronize WebResponse
        /// </summary>
        /// <param name="uri">http://114.35.57.190:889/sweet/ibon_apn.php</param>
        public static void fAnsycWebRequest(string uri)
        {

            try
            {
                allDone = new System.Threading.ManualResetEvent(false);

                // 建立WebRequest抽像類別物件
                WebRequest MyWebRequest = WebRequest.Create(uri);

                // 建立State物件
                RequestState State = new RequestState();

                State.Request = MyWebRequest;
                State.Request.Method = "POST";

                // start ansyc process client request
                // define Callback function is RequestCallback
                IAsyncResult result = MyWebRequest.BeginGetRequestStream(new AsyncCallback(RequestCallback), State);

                // Wait until asynchronous call completes.
                allDone.WaitOne();

                // using WEbRequest class's GetResponse function create WebResponse object
                WebResponse MyWebResponse = MyWebRequest.GetResponse();

                string responseResulte = "";
                
                responseResulte += "ContentLength: " + MyWebResponse.ContentLength.ToString() + "\r\n"; // client catch data length
                responseResulte += "ContentType: " + MyWebResponse.ContentType.ToString() + "\r\n"; // client catch data MIME type
                responseResulte += "ResponseUri: " + MyWebResponse.ResponseUri.ToString() + "\r\n"; // client catch from uri
                
                MessageBox.Show(responseResulte);

                // get server response stream
                Stream respStream = MyWebResponse.GetResponseStream();
                
                // setting encode is utf-8 and get server response stream context
                StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);

                string strBuff = "";
                char[] cbuffer = new char[256];

                int byteRead = respStreamReader.Read(cbuffer, 0, 256);

                while (byteRead != 0)
                {
                    string strResp = new string(cbuffer, 0, byteRead);
                    strBuff += strResp;
                    byteRead = respStreamReader.Read(cbuffer, 0, 256);
                }

                MessageBox.Show(strBuff);

                // close response stream
                respStream.Close();
                // close WebResponse object
                MyWebResponse.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void RequestCallback(IAsyncResult result)
        {
            RequestState State = (RequestState)result.AsyncState;

            WebRequest MyWebRequest = State.Request;

            // 結束非同步處理用戶端請求
            Stream respStream = MyWebRequest.EndGetRequestStream(result);

            /// <中文傳送處理>
            ///     HttpUtility 需引用 System.Web 組件
            ///     不同格式之處理GET , POST , JSON
            ///     GET , POST 方法至 php 並非是陣列, 而是純文字...
            ///     
            ///     結論:
            ///         使用json格式傳送為優
            /// </中文傳送處理>
            Encoding myEncoding = Encoding.GetEncoding("UTF-8");
            //string param = HttpUtility.UrlEncode("參數一", myEncoding) + "=" + HttpUtility.UrlEncode("值一", myEncoding) + "&" + HttpUtility.UrlEncode("參數二", myEncoding) + "=" + HttpUtility.UrlEncode("值二", myEncoding); // 多項
            //string param = HttpUtility.UrlEncode("test", myEncoding) + "=" + HttpUtility.UrlEncode("123", myEncoding);
            string param = "{\"" + HttpUtility.UrlEncode("request", myEncoding) + "\":\"" + HttpUtility.UrlEncode("From C# ansyc request 中文測試", myEncoding) + "\"}"; // JSON type

            /// <POST>
            ///     GET , POST , JSON 格式字串處理
            ///     取得邊碼格式長度並存於byte陣列
            ///     Write所有byte
            /// </POST>
            string postData = "{\"request\": \"From C# ansyc request testing\"}"; // JSON type
            //string postData = "test=123"; // GET , POST type
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            respStream.Write(byteArray, 0, param.Length);
            respStream.Close();
            allDone.Set();
            allDone.Reset();
        }
    }

    public partial class WebClientTesting
    {
        public static void fDownloadData(string uri)
        {
            WebClient Client = new WebClient();

            byte[] buffer = Client.DownloadData(uri);

            string data = Encoding.ASCII.GetString(buffer);

            MessageBox.Show(data);
        }

        /// <summary>
        ///     檔案下載(含檔案類型判別)
        /// </summary>
        /// <param name="uri">http://localhost/test/index.php</param>
        /// <param name="uri">http://localhost/test/40123153.xxx</param>
        public void fDownloadFile(string uri)
        {
            //string localfile = "C:\\download.txt";
            string localfile = System.Windows.Forms.Application.StartupPath + "/download";

            // 設定IO的權限，方可寫入檔案於本機 ( 就算不設定也可以寫入... )
            //System.Security.Permissions.FileIOPermission MyFileIOPermission = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.Write, localfile);
            try
            {
                using (WebClient Clinet = new WebClient())
                {
                    // 下載後以字串形式儲存
                    //string data = Clinet.DownloadString(uri);

                    byte[] fileBytes = Clinet.DownloadData(uri);

                    string fileType = Clinet.ResponseHeaders[HttpResponseHeader.ContentType];

                    bool IsDownload = true;

                    if (fileType != null)
                    {
                        /// <summary>
                        ///     由附檔名取得MIME type
                        /// </summary>
                        /// <param name="ExtrationFile" string>xxx.xxx</param>
                        string ExtensionFileName = "";
                        string MIMEType = fileType;
                        ExtensionFileName = MyMIMEType.MIMEType._mappings2.TryGetValue(MIMEType, out ExtensionFileName) ? ExtensionFileName : "Unknow";

                        /*
                        string[] MIME = new string[MyMIMEType.MIMEType._mappings.Count];
                        int i = 0;
                        foreach (KeyValuePair<string, string> obj in MyMIMEType.MIMEType._mappings)
                        {
                            if (i != 0 && !MIME.Contains(obj.Value.ToString().ToLower()))
                            {
                                MIMEType += "{" + string.Format("\"{0}\", \"{1}\"", obj.Value, obj.Key) + "},\r\n";
                            }
                            else if (i == 0)
                            {
                                MIMEType += "{" + string.Format("\"{0}\", \"{1}\"", obj.Value, obj.Key) + "},\r\n";
                            }
                            else {
                                MIMEType += "//{" + string.Format("\"{0}\", \"{1}\"", obj.Value, obj.Key) + "}, // repeat\r\n";
                            }
                            MIME[i] = obj.Value.ToString().ToLower();
                            i++;
                        }

                        File.AppendAllText(localfile, MIMEType);
                        */


                        /// <summary>
                        ///     MIME Types List search
                        ///     http://www.freeformatter.com/mime-types-list.html
                        /// </summary>
                        /*
                        switch (fileType)
                        {
                            case "image/jpeg":
                                localfile += ".jpg";
                                break;
                            case "image/gif":
                                localfile += ".gif";
                                break;
                            case "image/png":
                                localfile += ".png";
                                break;
                            case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                                localfile += ".docx";
                                break;
                            case "application/msword":
                                localfile += ".doc";
                                break;
                            case "application/x-msdownload":
                                localfile += ".exe";
                                break;
                            default:
                                IsDownload = false;
                                break;
                        }
                        */

                        // 此處可以參考uri最後的檔案名稱
                        localfile += ExtensionFileName;

                        byte[] info = new UTF8Encoding(true).GetBytes(fileType + "\r\n");

                        using (FileStream fStream = File.Open("filetype.txt", FileMode.Append))
                        {
                            fStream.Write(info, 0, info.Length);
                            fStream.Close();
                        }

                        if (IsDownload)
                            Clinet.DownloadFile(uri, localfile);
                        //File.WriteAllBytes(localfile, fileBytes);
                        else
                            MessageBox.Show("不符合的檔案格式");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     所有傳至web的參數接以JSON格式傳送為優(方便處理參數)
        ///     以下傳至php的$data = "test=22", 而非陣列$data['test'] = 22
        /// </summary>
        /// <param name="uri"></param>
        public static void fUploadData(string uri)
        {
            try
            {
                WebClient client = new WebClient();

                byte[] data = Encoding.UTF8.GetBytes("test=22");
                
                byte[] response = client.UploadData(uri, WebRequestMethods.Http.Post, data);

                string resulte = "";

                resulte += Encoding.UTF8.GetString(response); // response is ASCII encode, so vonvert to char
                
                MessageBox.Show(resulte);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        /// <summary>
        ///     testing fail
        ///     無法將檔案上傳
        /// </summary>
        /// <param name="uri"></param>
        public static void fUploadFile(string uri)
        {
            try
            {
                WebClient client = new WebClient();

                string fileName = "download.doc";

                byte[] response = client.UploadFile(uri, WebRequestMethods.Http.Post, fileName);

                string resulte = "";

                resulte += Encoding.UTF8.GetString(response); // response is ASCII encode, so vonvert to char

                MessageBox.Show(resulte);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        ///     跨類別From.Controls 控制(將要控制的物件傳入)
        ///     非同步下載檔案
        ///     
        ///     參考網站:
        ///         http://stackoverflow.com/questions/12879181/cant-change-textbox-text-in-form-from-other-classes-c-sharp-vs-2010
        ///         
        /// </summary>
        private static ProgressBar MyProgressBar;
        public static void fAsyncDownloadFile(ProgressBar progressBar, string uri)
        {
            try
            {
                WebClient client = new WebClient();
                MyProgressBar = progressBar;

                // 宣告非同步下載檔案完成時處發事件
                // 定義所呼叫的方法 DownloadFileCompletedCallback
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompletedCallback);

                // 宣告非同步下載作業過程處發事件
                // 定義所呼叫的方法 DownloadFileProcessChanged
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadFileProcessChanged);
                
                Uri uri1 = new Uri(uri);

                string[] ExtensionFileName = uri.Split('.');
                string FileName = "download." + ExtensionFileName[(ExtensionFileName.Length-1)];
                client.DownloadFileAsync(uri1, FileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void DownloadFileProcessChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                MyProgressBar.Maximum = 100;
                MyProgressBar.Value = e.ProgressPercentage;
                // Displays the operation identifier, and the transfer progress.
                Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                    (string)e.UserState,
                    e.BytesReceived,
                    e.TotalBytesToReceive,
                    e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void DownloadFileCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Complete");
                MyProgressBar.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }

    public partial class RequestState
    {
        public WebRequest Request;

        public RequestState()
        {
            Request = null;
        }
    }
}
