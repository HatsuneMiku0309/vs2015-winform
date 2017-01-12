using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMIMEType
{
    public partial class MIMEType
    {
        /*
        public static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            #region Big freaking list of mime types
            // combination of values from Windows 7 Registry and 
            // from C:\Windows\System32\inetsrv\config\applicationHost.config
            // some added, including .7z and .dat
            {".323", "text/h323"},
            {".3g2", "video/3gpp2"},
            {".3gp", "video/3gpp"},
            {".3gp2", "video/3gpp2"},
            {".3gpp", "video/3gpp"},
            {".7z", "application/x-7z-compressed"},
            {".aa", "audio/audible"},
            {".AAC", "audio/aac"},
            {".aaf", "application/octet-stream"},
            {".aax", "audio/vnd.audible.aax"},
            {".ac3", "audio/ac3"},
            {".aca", "application/octet-stream"},
            {".accda", "application/msaccess.addin"},
            {".accdb", "application/msaccess"},
            {".accdc", "application/msaccess.cab"},
            {".accde", "application/msaccess"},
            {".accdr", "application/msaccess.runtime"},
            {".accdt", "application/msaccess"},
            {".accdw", "application/msaccess.webapplication"},
            {".accft", "application/msaccess.ftemplate"},
            {".acx", "application/internet-property-stream"},
            {".AddIn", "text/xml"},
            {".ade", "application/msaccess"},
            {".adobebridge", "application/x-bridge-url"},
            {".adp", "application/msaccess"},
            {".ADT", "audio/vnd.dlna.adts"},
            {".ADTS", "audio/aac"},
            {".afm", "application/octet-stream"},
            {".ai", "application/postscript"},
            {".aif", "audio/x-aiff"},
            {".aifc", "audio/aiff"},
            {".aiff", "audio/aiff"},
            {".air", "application/vnd.adobe.air-application-installer-package+zip"},
            {".amc", "application/x-mpeg"},
            {".application", "application/x-ms-application"},
            {".art", "image/x-jg"},
            {".asa", "application/xml"},
            {".asax", "application/xml"},
            {".ascx", "application/xml"},
            {".asd", "application/octet-stream"},
            {".asf", "video/x-ms-asf"},
            {".ashx", "application/xml"},
            {".asi", "application/octet-stream"},
            {".asm", "text/plain"},
            {".asmx", "application/xml"},
            {".aspx", "application/xml"},
            {".asr", "video/x-ms-asf"},
            {".asx", "video/x-ms-asf"},
            {".atom", "application/atom+xml"},
            {".au", "audio/basic"},
            {".avi", "video/x-msvideo"},
            {".axs", "application/olescript"},
            {".bas", "text/plain"},
            {".bcpio", "application/x-bcpio"},
            {".bin", "application/octet-stream"},
            {".bmp", "image/bmp"},
            {".c", "text/plain"},
            {".cab", "application/octet-stream"},
            {".caf", "audio/x-caf"},
            {".calx", "application/vnd.ms-office.calx"},
            {".cat", "application/vnd.ms-pki.seccat"},
            {".cc", "text/plain"},
            {".cd", "text/plain"},
            {".cdda", "audio/aiff"},
            {".cdf", "application/x-cdf"},
            {".cer", "application/x-x509-ca-cert"},
            {".chm", "application/octet-stream"},
            {".class", "application/x-java-applet"},
            {".clp", "application/x-msclip"},
            {".cmx", "image/x-cmx"},
            {".cnf", "text/plain"},
            {".cod", "image/cis-cod"},
            {".config", "application/xml"},
            {".contact", "text/x-ms-contact"},
            {".coverage", "application/xml"},
            {".cpio", "application/x-cpio"},
            {".cpp", "text/plain"},
            {".crd", "application/x-mscardfile"},
            {".crl", "application/pkix-crl"},
            {".crt", "application/x-x509-ca-cert"},
            {".cs", "text/plain"},
            {".csdproj", "text/plain"},
            {".csh", "application/x-csh"},
            {".csproj", "text/plain"},
            {".css", "text/css"},
            {".csv", "text/csv"},
            {".cur", "application/octet-stream"},
            {".cxx", "text/plain"},
            {".dat", "application/octet-stream"},
            {".datasource", "application/xml"},
            {".dbproj", "text/plain"},
            {".dcr", "application/x-director"},
            {".def", "text/plain"},
            {".deploy", "application/octet-stream"},
            {".der", "application/x-x509-ca-cert"},
            {".dgml", "application/xml"},
            {".dib", "image/bmp"},
            {".dif", "video/x-dv"},
            {".dir", "application/x-director"},
            {".disco", "text/xml"},
            {".dll", "application/x-msdownload"},
            {".dll.config", "text/xml"},
            {".dlm", "text/dlm"},
            {".doc", "application/msword"},
            {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".dot", "application/msword"},
            {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
            {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
            {".dsp", "application/octet-stream"},
            {".dsw", "text/plain"},
            {".dtd", "text/xml"},
            {".dtsConfig", "text/xml"},
            {".dv", "video/x-dv"},
            {".dvi", "application/x-dvi"},
            {".dwf", "drawing/x-dwf"},
            {".dwp", "application/octet-stream"},
            {".dxr", "application/x-director"},
            {".eml", "message/rfc822"},
            {".emz", "application/octet-stream"},
            {".eot", "application/octet-stream"},
            {".eps", "application/postscript"},
            {".etl", "application/etl"},
            {".etx", "text/x-setext"},
            {".evy", "application/envoy"},
            {".exe", "application/octet-stream"},
            {".exe.config", "text/xml"},
            {".fdf", "application/vnd.fdf"},
            {".fif", "application/fractals"},
            {".filters", "Application/xml"},
            {".fla", "application/octet-stream"},
            {".flr", "x-world/x-vrml"},
            {".flv", "video/x-flv"},
            {".fsscript", "application/fsharp-script"},
            {".fsx", "application/fsharp-script"},
            {".generictest", "application/xml"},
            {".gif", "image/gif"},
            {".group", "text/x-ms-group"},
            {".gsm", "audio/x-gsm"},
            {".gtar", "application/x-gtar"},
            {".gz", "application/x-gzip"},
            {".h", "text/plain"},
            {".hdf", "application/x-hdf"},
            {".hdml", "text/x-hdml"},
            {".hhc", "application/x-oleobject"},
            {".hhk", "application/octet-stream"},
            {".hhp", "application/octet-stream"},
            {".hlp", "application/winhlp"},
            {".hpp", "text/plain"},
            {".hqx", "application/mac-binhex40"},
            {".hta", "application/hta"},
            {".htc", "text/x-component"},
            {".htm", "text/html"},
            {".html", "text/html"},
            {".htt", "text/webviewhtml"},
            {".hxa", "application/xml"},
            {".hxc", "application/xml"},
            {".hxd", "application/octet-stream"},
            {".hxe", "application/xml"},
            {".hxf", "application/xml"},
            {".hxh", "application/octet-stream"},
            {".hxi", "application/octet-stream"},
            {".hxk", "application/xml"},
            {".hxq", "application/octet-stream"},
            {".hxr", "application/octet-stream"},
            {".hxs", "application/octet-stream"},
            {".hxt", "text/html"},
            {".hxv", "application/xml"},
            {".hxw", "application/octet-stream"},
            {".hxx", "text/plain"},
            {".i", "text/plain"},
            {".ico", "image/x-icon"},
            {".ics", "application/octet-stream"},
            {".idl", "text/plain"},
            {".ief", "image/ief"},
            {".iii", "application/x-iphone"},
            {".inc", "text/plain"},
            {".inf", "application/octet-stream"},
            {".inl", "text/plain"},
            {".ins", "application/x-internet-signup"},
            {".ipa", "application/x-itunes-ipa"},
            {".ipg", "application/x-itunes-ipg"},
            {".ipproj", "text/plain"},
            {".ipsw", "application/x-itunes-ipsw"},
            {".iqy", "text/x-ms-iqy"},
            {".isp", "application/x-internet-signup"},
            {".ite", "application/x-itunes-ite"},
            {".itlp", "application/x-itunes-itlp"},
            {".itms", "application/x-itunes-itms"},
            {".itpc", "application/x-itunes-itpc"},
            {".IVF", "video/x-ivf"},
            {".jar", "application/java-archive"},
            {".java", "application/octet-stream"},
            {".jck", "application/liquidmotion"},
            {".jcz", "application/liquidmotion"},
            {".jfif", "image/pjpeg"},
            {".jnlp", "application/x-java-jnlp-file"},
            {".jpb", "application/octet-stream"},
            {".jpe", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".js", "application/x-javascript"},
            {".json", "application/json"},
            {".jsx", "text/jscript"},
            {".jsxbin", "text/plain"},
            {".latex", "application/x-latex"},
            {".library-ms", "application/windows-library+xml"},
            {".lit", "application/x-ms-reader"},
            {".loadtest", "application/xml"},
            {".lpk", "application/octet-stream"},
            {".lsf", "video/x-la-asf"},
            {".lst", "text/plain"},
            {".lsx", "video/x-la-asf"},
            {".lzh", "application/octet-stream"},
            {".m13", "application/x-msmediaview"},
            {".m14", "application/x-msmediaview"},
            {".m1v", "video/mpeg"},
            {".m2t", "video/vnd.dlna.mpeg-tts"},
            {".m2ts", "video/vnd.dlna.mpeg-tts"},
            {".m2v", "video/mpeg"},
            {".m3u", "audio/x-mpegurl"},
            {".m3u8", "audio/x-mpegurl"},
            {".m4a", "audio/m4a"},
            {".m4b", "audio/m4b"},
            {".m4p", "audio/m4p"},
            {".m4r", "audio/x-m4r"},
            {".m4v", "video/x-m4v"},
            {".mac", "image/x-macpaint"},
            {".mak", "text/plain"},
            {".man", "application/x-troff-man"},
            {".manifest", "application/x-ms-manifest"},
            {".map", "text/plain"},
            {".master", "application/xml"},
            {".mda", "application/msaccess"},
            {".mdb", "application/x-msaccess"},
            {".mde", "application/msaccess"},
            {".mdp", "application/octet-stream"},
            {".me", "application/x-troff-me"},
            {".mfp", "application/x-shockwave-flash"},
            {".mht", "message/rfc822"},
            {".mhtml", "message/rfc822"},
            {".mid", "audio/mid"},
            {".midi", "audio/mid"},
            {".mix", "application/octet-stream"},
            {".mk", "text/plain"},
            {".mmf", "application/x-smaf"},
            {".mno", "text/xml"},
            {".mny", "application/x-msmoney"},
            {".mod", "video/mpeg"},
            {".mov", "video/quicktime"},
            {".movie", "video/x-sgi-movie"},
            {".mp2", "video/mpeg"},
            {".mp2v", "video/mpeg"},
            {".mp3", "audio/mpeg"},
            {".mp4", "video/mp4"},
            {".mp4v", "video/mp4"},
            {".mpa", "video/mpeg"},
            {".mpe", "video/mpeg"},
            {".mpeg", "video/mpeg"},
            {".mpf", "application/vnd.ms-mediapackage"},
            {".mpg", "video/mpeg"},
            {".mpp", "application/vnd.ms-project"},
            {".mpv2", "video/mpeg"},
            {".mqv", "video/quicktime"},
            {".ms", "application/x-troff-ms"},
            {".msi", "application/octet-stream"},
            {".mso", "application/octet-stream"},
            {".mts", "video/vnd.dlna.mpeg-tts"},
            {".mtx", "application/xml"},
            {".mvb", "application/x-msmediaview"},
            {".mvc", "application/x-miva-compiled"},
            {".mxp", "application/x-mmxp"},
            {".nc", "application/x-netcdf"},
            {".nsc", "video/x-ms-asf"},
            {".nws", "message/rfc822"},
            {".ocx", "application/octet-stream"},
            {".oda", "application/oda"},
            {".odc", "text/x-ms-odc"},
            {".odh", "text/plain"},
            {".odl", "text/plain"},
            {".odp", "application/vnd.oasis.opendocument.presentation"},
            {".ods", "application/oleobject"},
            {".odt", "application/vnd.oasis.opendocument.text"},
            {".one", "application/onenote"},
            {".onea", "application/onenote"},
            {".onepkg", "application/onenote"},
            {".onetmp", "application/onenote"},
            {".onetoc", "application/onenote"},
            {".onetoc2", "application/onenote"},
            {".orderedtest", "application/xml"},
            {".osdx", "application/opensearchdescription+xml"},
            {".p10", "application/pkcs10"},
            {".p12", "application/x-pkcs12"},
            {".p7b", "application/x-pkcs7-certificates"},
            {".p7c", "application/pkcs7-mime"},
            {".p7m", "application/pkcs7-mime"},
            {".p7r", "application/x-pkcs7-certreqresp"},
            {".p7s", "application/pkcs7-signature"},
            {".pbm", "image/x-portable-bitmap"},
            {".pcast", "application/x-podcast"},
            {".pct", "image/pict"},
            {".pcx", "application/octet-stream"},
            {".pcz", "application/octet-stream"},
            {".pdf", "application/pdf"},
            {".pfb", "application/octet-stream"},
            {".pfm", "application/octet-stream"},
            {".pfx", "application/x-pkcs12"},
            {".pgm", "image/x-portable-graymap"},
            {".pic", "image/pict"},
            {".pict", "image/pict"},
            {".pkgdef", "text/plain"},
            {".pkgundef", "text/plain"},
            {".pko", "application/vnd.ms-pki.pko"},
            {".pls", "audio/scpls"},
            {".pma", "application/x-perfmon"},
            {".pmc", "application/x-perfmon"},
            {".pml", "application/x-perfmon"},
            {".pmr", "application/x-perfmon"},
            {".pmw", "application/x-perfmon"},
            {".png", "image/png"},
            {".pnm", "image/x-portable-anymap"},
            {".pnt", "image/x-macpaint"},
            {".pntg", "image/x-macpaint"},
            {".pnz", "image/png"},
            {".pot", "application/vnd.ms-powerpoint"},
            {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
            {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
            {".ppa", "application/vnd.ms-powerpoint"},
            {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
            {".ppm", "image/x-portable-pixmap"},
            {".pps", "application/vnd.ms-powerpoint"},
            {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
            {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {".ppt", "application/vnd.ms-powerpoint"},
            {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".prf", "application/pics-rules"},
            {".prm", "application/octet-stream"},
            {".prx", "application/octet-stream"},
            {".ps", "application/postscript"},
            {".psc1", "application/PowerShell"},
            {".psd", "application/octet-stream"},
            {".psess", "application/xml"},
            {".psm", "application/octet-stream"},
            {".psp", "application/octet-stream"},
            {".pub", "application/x-mspublisher"},
            {".pwz", "application/vnd.ms-powerpoint"},
            {".qht", "text/x-html-insertion"},
            {".qhtm", "text/x-html-insertion"},
            {".qt", "video/quicktime"},
            {".qti", "image/x-quicktime"},
            {".qtif", "image/x-quicktime"},
            {".qtl", "application/x-quicktimeplayer"},
            {".qxd", "application/octet-stream"},
            {".ra", "audio/x-pn-realaudio"},
            {".ram", "audio/x-pn-realaudio"},
            {".rar", "application/octet-stream"},
            {".ras", "image/x-cmu-raster"},
            {".rat", "application/rat-file"},
            {".rc", "text/plain"},
            {".rc2", "text/plain"},
            {".rct", "text/plain"},
            {".rdlc", "application/xml"},
            {".resx", "application/xml"},
            {".rf", "image/vnd.rn-realflash"},
            {".rgb", "image/x-rgb"},
            {".rgs", "text/plain"},
            {".rm", "application/vnd.rn-realmedia"},
            {".rmi", "audio/mid"},
            {".rmp", "application/vnd.rn-rn_music_package"},
            {".roff", "application/x-troff"},
            {".rpm", "audio/x-pn-realaudio-plugin"},
            {".rqy", "text/x-ms-rqy"},
            {".rtf", "application/rtf"},
            {".rtx", "text/richtext"},
            {".ruleset", "application/xml"},
            {".s", "text/plain"},
            {".safariextz", "application/x-safari-safariextz"},
            {".scd", "application/x-msschedule"},
            {".sct", "text/scriptlet"},
            {".sd2", "audio/x-sd2"},
            {".sdp", "application/sdp"},
            {".sea", "application/octet-stream"},
            {".searchConnector-ms", "application/windows-search-connector+xml"},
            {".setpay", "application/set-payment-initiation"},
            {".setreg", "application/set-registration-initiation"},
            {".settings", "application/xml"},
            {".sgimb", "application/x-sgimb"},
            {".sgml", "text/sgml"},
            {".sh", "application/x-sh"},
            {".shar", "application/x-shar"},
            {".shtml", "text/html"},
            {".sit", "application/x-stuffit"},
            {".sitemap", "application/xml"},
            {".skin", "application/xml"},
            {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
            {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
            {".slk", "application/vnd.ms-excel"},
            {".sln", "text/plain"},
            {".slupkg-ms", "application/x-ms-license"},
            {".smd", "audio/x-smd"},
            {".smi", "application/octet-stream"},
            {".smx", "audio/x-smd"},
            {".smz", "audio/x-smd"},
            {".snd", "audio/basic"},
            {".snippet", "application/xml"},
            {".snp", "application/octet-stream"},
            {".sol", "text/plain"},
            {".sor", "text/plain"},
            {".spc", "application/x-pkcs7-certificates"},
            {".spl", "application/futuresplash"},
            {".src", "application/x-wais-source"},
            {".srf", "text/plain"},
            {".SSISDeploymentManifest", "text/xml"},
            {".ssm", "application/streamingmedia"},
            {".sst", "application/vnd.ms-pki.certstore"},
            {".stl", "application/vnd.ms-pki.stl"},
            {".sv4cpio", "application/x-sv4cpio"},
            {".sv4crc", "application/x-sv4crc"},
            {".svc", "application/xml"},
            {".swf", "application/x-shockwave-flash"},
            {".t", "application/x-troff"},
            {".tar", "application/x-tar"},
            {".tcl", "application/x-tcl"},
            {".testrunconfig", "application/xml"},
            {".testsettings", "application/xml"},
            {".tex", "application/x-tex"},
            {".texi", "application/x-texinfo"},
            {".texinfo", "application/x-texinfo"},
            {".tgz", "application/x-compressed"},
            {".thmx", "application/vnd.ms-officetheme"},
            {".thn", "application/octet-stream"},
            {".tif", "image/tiff"},
            {".tiff", "image/tiff"},
            {".tlh", "text/plain"},
            {".tli", "text/plain"},
            {".toc", "application/octet-stream"},
            {".tr", "application/x-troff"},
            {".trm", "application/x-msterminal"},
            {".trx", "application/xml"},
            {".ts", "video/vnd.dlna.mpeg-tts"},
            {".tsv", "text/tab-separated-values"},
            {".ttf", "application/octet-stream"},
            {".tts", "video/vnd.dlna.mpeg-tts"},
            {".txt", "text/plain"},
            {".u32", "application/octet-stream"},
            {".uls", "text/iuls"},
            {".user", "text/plain"},
            {".ustar", "application/x-ustar"},
            {".vb", "text/plain"},
            {".vbdproj", "text/plain"},
            {".vbk", "video/mpeg"},
            {".vbproj", "text/plain"},
            {".vbs", "text/vbscript"},
            {".vcf", "text/x-vcard"},
            {".vcproj", "Application/xml"},
            {".vcs", "text/plain"},
            {".vcxproj", "Application/xml"},
            {".vddproj", "text/plain"},
            {".vdp", "text/plain"},
            {".vdproj", "text/plain"},
            {".vdx", "application/vnd.ms-visio.viewer"},
            {".vml", "text/xml"},
            {".vscontent", "application/xml"},
            {".vsct", "text/xml"},
            {".vsd", "application/vnd.visio"},
            {".vsi", "application/ms-vsi"},
            {".vsix", "application/vsix"},
            {".vsixlangpack", "text/xml"},
            {".vsixmanifest", "text/xml"},
            {".vsmdi", "application/xml"},
            {".vspscc", "text/plain"},
            {".vss", "application/vnd.visio"},
            {".vsscc", "text/plain"},
            {".vssettings", "text/xml"},
            {".vssscc", "text/plain"},
            {".vst", "application/vnd.visio"},
            {".vstemplate", "text/xml"},
            {".vsto", "application/x-ms-vsto"},
            {".vsw", "application/vnd.visio"},
            {".vsx", "application/vnd.visio"},
            {".vtx", "application/vnd.visio"},
            {".wav", "audio/wav"},
            {".wave", "audio/wav"},
            {".wax", "audio/x-ms-wax"},
            {".wbk", "application/msword"},
            {".wbmp", "image/vnd.wap.wbmp"},
            {".wcm", "application/vnd.ms-works"},
            {".wdb", "application/vnd.ms-works"},
            {".wdp", "image/vnd.ms-photo"},
            {".webarchive", "application/x-safari-webarchive"},
            {".webtest", "application/xml"},
            {".wiq", "application/xml"},
            {".wiz", "application/msword"},
            {".wks", "application/vnd.ms-works"},
            {".WLMP", "application/wlmoviemaker"},
            {".wlpginstall", "application/x-wlpg-detect"},
            {".wlpginstall3", "application/x-wlpg3-detect"},
            {".wm", "video/x-ms-wm"},
            {".wma", "audio/x-ms-wma"},
            {".wmd", "application/x-ms-wmd"},
            {".wmf", "application/x-msmetafile"},
            {".wml", "text/vnd.wap.wml"},
            {".wmlc", "application/vnd.wap.wmlc"},
            {".wmls", "text/vnd.wap.wmlscript"},
            {".wmlsc", "application/vnd.wap.wmlscriptc"},
            {".wmp", "video/x-ms-wmp"},
            {".wmv", "video/x-ms-wmv"},
            {".wmx", "video/x-ms-wmx"},
            {".wmz", "application/x-ms-wmz"},
            {".wpl", "application/vnd.ms-wpl"},
            {".wps", "application/vnd.ms-works"},
            {".wri", "application/x-mswrite"},
            {".wrl", "x-world/x-vrml"},
            {".wrz", "x-world/x-vrml"},
            {".wsc", "text/scriptlet"},
            {".wsdl", "text/xml"},
            {".wvx", "video/x-ms-wvx"},
            {".x", "application/directx"},
            {".xaf", "x-world/x-vrml"},
            {".xaml", "application/xaml+xml"},
            {".xap", "application/x-silverlight-app"},
            {".xbap", "application/x-ms-xbap"},
            {".xbm", "image/x-xbitmap"},
            {".xdr", "text/plain"},
            {".xht", "application/xhtml+xml"},
            {".xhtml", "application/xhtml+xml"},
            {".xla", "application/vnd.ms-excel"},
            {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
            {".xlc", "application/vnd.ms-excel"},
            {".xld", "application/vnd.ms-excel"},
            {".xlk", "application/vnd.ms-excel"},
            {".xll", "application/vnd.ms-excel"},
            {".xlm", "application/vnd.ms-excel"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
            {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".xlt", "application/vnd.ms-excel"},
            {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
            {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
            {".xlw", "application/vnd.ms-excel"},
            {".xml", "text/xml"},
            {".xmta", "application/xml"},
            {".xof", "x-world/x-vrml"},
            {".XOML", "text/plain"},
            {".xpm", "image/x-xpixmap"},
            {".xps", "application/vnd.ms-xpsdocument"},
            {".xrm-ms", "text/xml"},
            {".xsc", "application/xml"},
            {".xsd", "text/xml"},
            {".xsf", "text/xml"},
            {".xsl", "text/xml"},
            {".xslt", "text/xml"},
            {".xsn", "application/octet-stream"},
            {".xss", "application/xml"},
            {".xtp", "application/octet-stream"},
            {".xwd", "image/x-xwindowdump"},
            {".z", "application/x-compress"},
            {".zip", "application/x-zip-compressed"},
            #endregion
        };
        */
       
        public static IDictionary<string, string> _mappings2 = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            #region Big freaking list of mime types
            // combination of values from Windows 7 Registry and 
            // from C:\Windows\System32\inetsrv\config\applicationHost.config
            // some added, including .7z and .dat
            {"text/h323", ".323"},
            {"video/3gpp2", ".3g2"},
            {"video/3gpp", ".3gp"},
            //{"video/3gpp2", ".3gp2"}, // repeat
            //{"video/3gpp", ".3gpp"}, // repeat
            {"application/x-7z-compressed", ".7z"},
            {"audio/audible", ".aa"},
            {"audio/aac", ".AAC"},
            {"application/octet-stream", ".aaf"},
            {"audio/vnd.audible.aax", ".aax"},
            {"audio/ac3", ".ac3"},
            //{"application/octet-stream", ".aca"}, // repeat
            {"application/msaccess.addin", ".accda"},
            {"application/msaccess", ".accdb"},
            {"application/msaccess.cab", ".accdc"},
            //{"application/msaccess", ".accde"}, // repeat
            {"application/msaccess.runtime", ".accdr"},
            //{"application/msaccess", ".accdt"}, // repeat
            {"application/msaccess.webapplication", ".accdw"},
            {"application/msaccess.ftemplate", ".accft"},
            {"application/internet-property-stream", ".acx"},
            {"text/xml", ".AddIn"},
            //{"application/msaccess", ".ade"}, // repeat
            {"application/x-bridge-url", ".adobebridge"},
            //{"application/msaccess", ".adp"}, // repeat
            {"audio/vnd.dlna.adts", ".ADT"},
            //{"audio/aac", ".ADTS"}, // repeat
            //{"application/octet-stream", ".afm"}, // repeat
            {"application/postscript", ".ai"},
            {"audio/x-aiff", ".aif"},
            {"audio/aiff", ".aifc"},
            //{"audio/aiff", ".aiff"}, // repeat
            {"application/vnd.adobe.air-application-installer-package+zip", ".air"},
            {"application/x-mpeg", ".amc"},
            {"application/x-ms-application", ".application"},
            {"image/x-jg", ".art"},
            {"application/xml", ".asa"},
            //{"application/xml", ".asax"}, // repeat
            //{"application/xml", ".ascx"}, // repeat
            //{"application/octet-stream", ".asd"}, // repeat
            {"video/x-ms-asf", ".asf"},
            //{"application/xml", ".ashx"}, // repeat
            //{"application/octet-stream", ".asi"}, // repeat
            {"text/plain", ".asm"},
            //{"application/xml", ".asmx"}, // repeat
            //{"application/xml", ".aspx"}, // repeat
            //{"video/x-ms-asf", ".asr"}, // repeat
            //{"video/x-ms-asf", ".asx"}, // repeat
            {"application/atom+xml", ".atom"},
            {"audio/basic", ".au"},
            {"video/x-msvideo", ".avi"},
            {"application/olescript", ".axs"},
            //{"text/plain", ".bas"}, // repeat
            {"application/x-bcpio", ".bcpio"},
            //{"application/octet-stream", ".bin"}, // repeat
            {"image/bmp", ".bmp"},
            //{"text/plain", ".c"}, // repeat
            //{"application/octet-stream", ".cab"}, // repeat
            {"audio/x-caf", ".caf"},
            {"application/vnd.ms-office.calx", ".calx"},
            {"application/vnd.ms-pki.seccat", ".cat"},
            //{"text/plain", ".cc"}, // repeat
            //{"text/plain", ".cd"}, // repeat
            //{"audio/aiff", ".cdda"}, // repeat
            {"application/x-cdf", ".cdf"},
            {"application/x-x509-ca-cert", ".cer"},
            //{"application/octet-stream", ".chm"}, // repeat
            {"application/x-java-applet", ".class"},
            {"application/x-msclip", ".clp"},
            {"image/x-cmx", ".cmx"},
            //{"text/plain", ".cnf"}, // repeat
            {"image/cis-cod", ".cod"},
            //{"application/xml", ".config"}, // repeat
            {"text/x-ms-contact", ".contact"},
            //{"application/xml", ".coverage"}, // repeat
            {"application/x-cpio", ".cpio"},
            //{"text/plain", ".cpp"}, // repeat
            {"application/x-mscardfile", ".crd"},
            {"application/pkix-crl", ".crl"},
            //{"application/x-x509-ca-cert", ".crt"}, // repeat
            //{"text/plain", ".cs"}, // repeat
            //{"text/plain", ".csdproj"}, // repeat
            {"application/x-csh", ".csh"},
            //{"text/plain", ".csproj"}, // repeat
            {"text/css", ".css"},
            {"text/csv", ".csv"},
            //{"application/octet-stream", ".cur"}, // repeat
            //{"text/plain", ".cxx"}, // repeat
            //{"application/octet-stream", ".dat"}, // repeat
            //{"application/xml", ".datasource"}, // repeat
            //{"text/plain", ".dbproj"}, // repeat
            {"application/x-director", ".dcr"},
            //{"text/plain", ".def"}, // repeat
            //{"application/octet-stream", ".deploy"}, // repeat
            //{"application/x-x509-ca-cert", ".der"}, // repeat
            //{"application/xml", ".dgml"}, // repeat
            //{"image/bmp", ".dib"}, // repeat
            {"video/x-dv", ".dif"},
            //{"application/x-director", ".dir"}, // repeat
            //{"text/xml", ".disco"}, // repeat
            {"application/x-msdownload", ".dll"},
            //{"text/xml", ".dll.config"}, // repeat
            {"text/dlm", ".dlm"},
            {"application/msword", ".doc"},
            {"application/vnd.ms-word.document.macroEnabled.12", ".docm"},
            {"application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx"},
            //{"application/msword", ".dot"}, // repeat
            {"application/vnd.ms-word.template.macroEnabled.12", ".dotm"},
            {"application/vnd.openxmlformats-officedocument.wordprocessingml.template", ".dotx"},
            //{"application/octet-stream", ".dsp"}, // repeat
            //{"text/plain", ".dsw"}, // repeat
            //{"text/xml", ".dtd"}, // repeat
            //{"text/xml", ".dtsConfig"}, // repeat
            //{"video/x-dv", ".dv"}, // repeat
            {"application/x-dvi", ".dvi"},
            {"drawing/x-dwf", ".dwf"},
            //{"application/octet-stream", ".dwp"}, // repeat
            //{"application/x-director", ".dxr"}, // repeat
            {"message/rfc822", ".eml"},
            //{"application/octet-stream", ".emz"}, // repeat
            //{"application/octet-stream", ".eot"}, // repeat
            //{"application/postscript", ".eps"}, // repeat
            {"application/etl", ".etl"},
            {"text/x-setext", ".etx"},
            {"application/envoy", ".evy"},
            //{"application/octet-stream", ".exe"}, // repeat
            //{"text/xml", ".exe.config"}, // repeat
            {"application/vnd.fdf", ".fdf"},
            {"application/fractals", ".fif"},
            //{"Application/xml", ".filters"}, // repeat
            //{"application/octet-stream", ".fla"}, // repeat
            {"x-world/x-vrml", ".flr"},
            {"video/x-flv", ".flv"},
            {"application/fsharp-script", ".fsscript"},
            //{"application/fsharp-script", ".fsx"}, // repeat
            //{"application/xml", ".generictest"}, // repeat
            {"image/gif", ".gif"},
            {"text/x-ms-group", ".group"},
            {"audio/x-gsm", ".gsm"},
            {"application/x-gtar", ".gtar"},
            {"application/x-gzip", ".gz"},
            //{"text/plain", ".h"}, // repeat
            {"application/x-hdf", ".hdf"},
            {"text/x-hdml", ".hdml"},
            {"application/x-oleobject", ".hhc"},
            //{"application/octet-stream", ".hhk"}, // repeat
            //{"application/octet-stream", ".hhp"}, // repeat
            {"application/winhlp", ".hlp"},
            //{"text/plain", ".hpp"}, // repeat
            {"application/mac-binhex40", ".hqx"},
            {"application/hta", ".hta"},
            {"text/x-component", ".htc"},
            //{"text/html", ".htm"},
            {"text/html", ".html"}, // repeat
            {"text/webviewhtml", ".htt"},
            //{"application/xml", ".hxa"}, // repeat
            //{"application/xml", ".hxc"}, // repeat
            //{"application/octet-stream", ".hxd"}, // repeat
            //{"application/xml", ".hxe"}, // repeat
            //{"application/xml", ".hxf"}, // repeat
            //{"application/octet-stream", ".hxh"}, // repeat
            //{"application/octet-stream", ".hxi"}, // repeat
            //{"application/xml", ".hxk"}, // repeat
            //{"application/octet-stream", ".hxq"}, // repeat
            //{"application/octet-stream", ".hxr"}, // repeat
            //{"application/octet-stream", ".hxs"}, // repeat
            //{"text/html", ".hxt"}, // repeat
            //{"application/xml", ".hxv"}, // repeat
            //{"application/octet-stream", ".hxw"}, // repeat
            //{"text/plain", ".hxx"}, // repeat
            //{"text/plain", ".i"}, // repeat
            {"image/x-icon", ".ico"},
            //{"application/octet-stream", ".ics"}, // repeat
            //{"text/plain", ".idl"}, // repeat
            {"image/ief", ".ief"},
            {"application/x-iphone", ".iii"},
            //{"text/plain", ".inc"}, // repeat
            //{"application/octet-stream", ".inf"}, // repeat
            //{"text/plain", ".inl"}, // repeat
            {"application/x-internet-signup", ".ins"},
            {"application/x-itunes-ipa", ".ipa"},
            {"application/x-itunes-ipg", ".ipg"},
            //{"text/plain", ".ipproj"}, // repeat
            {"application/x-itunes-ipsw", ".ipsw"},
            {"text/x-ms-iqy", ".iqy"},
            //{"application/x-internet-signup", ".isp"}, // repeat
            {"application/x-itunes-ite", ".ite"},
            {"application/x-itunes-itlp", ".itlp"},
            {"application/x-itunes-itms", ".itms"},
            {"application/x-itunes-itpc", ".itpc"},
            {"video/x-ivf", ".IVF"},
            {"application/java-archive", ".jar"},
            //{"application/octet-stream", ".java"}, // repeat
            {"application/liquidmotion", ".jck"},
            //{"application/liquidmotion", ".jcz"}, // repeat
            {"image/pjpeg", ".jfif"},
            {"application/x-java-jnlp-file", ".jnlp"},
            //{"application/octet-stream", ".jpb"}, // repeat
            {"image/jpeg", ".jpe"},
            //{"image/jpeg", ".jpeg"}, // repeat
            //{"image/jpeg", ".jpg"}, // repeat
            {"application/x-javascript", ".js"},
            {"application/json", ".json"},
            {"text/jscript", ".jsx"},
            //{"text/plain", ".jsxbin"}, // repeat
            {"application/x-latex", ".latex"},
            {"application/windows-library+xml", ".library-ms"},
            {"application/x-ms-reader", ".lit"},
            //{"application/xml", ".loadtest"}, // repeat
            //{"application/octet-stream", ".lpk"}, // repeat
            {"video/x-la-asf", ".lsf"},
            //{"text/plain", ".lst"}, // repeat
            //{"video/x-la-asf", ".lsx"}, // repeat
            //{"application/octet-stream", ".lzh"}, // repeat
            {"application/x-msmediaview", ".m13"},
            //{"application/x-msmediaview", ".m14"}, // repeat
            {"video/mpeg", ".m1v"},
            {"video/vnd.dlna.mpeg-tts", ".m2t"},
            //{"video/vnd.dlna.mpeg-tts", ".m2ts"}, // repeat
            //{"video/mpeg", ".m2v"}, // repeat
            {"audio/x-mpegurl", ".m3u"},
            //{"audio/x-mpegurl", ".m3u8"}, // repeat
            {"audio/m4a", ".m4a"},
            {"audio/m4b", ".m4b"},
            {"audio/m4p", ".m4p"},
            {"audio/x-m4r", ".m4r"},
            {"video/x-m4v", ".m4v"},
            {"image/x-macpaint", ".mac"},
            //{"text/plain", ".mak"}, // repeat
            {"application/x-troff-man", ".man"},
            {"application/x-ms-manifest", ".manifest"},
            //{"text/plain", ".map"}, // repeat
            //{"application/xml", ".master"}, // repeat
            //{"application/msaccess", ".mda"}, // repeat
            {"application/x-msaccess", ".mdb"},
            //{"application/msaccess", ".mde"}, // repeat
            //{"application/octet-stream", ".mdp"}, // repeat
            {"application/x-troff-me", ".me"},
            {"application/x-shockwave-flash", ".mfp"},
            //{"message/rfc822", ".mht"}, // repeat
            //{"message/rfc822", ".mhtml"}, // repeat
            {"audio/mid", ".mid"},
            //{"audio/mid", ".midi"}, // repeat
            //{"application/octet-stream", ".mix"}, // repeat
            //{"text/plain", ".mk"}, // repeat
            {"application/x-smaf", ".mmf"},
            //{"text/xml", ".mno"}, // repeat
            {"application/x-msmoney", ".mny"},
            //{"video/mpeg", ".mod"}, // repeat
            {"video/quicktime", ".mov"},
            {"video/x-sgi-movie", ".movie"},
            //{"video/mpeg", ".mp2"}, // repeat
            //{"video/mpeg", ".mp2v"}, // repeat
            {"audio/mpeg", ".mp3"},
            {"video/mp4", ".mp4"},
            //{"video/mp4", ".mp4v"}, // repeat
            //{"video/mpeg", ".mpa"}, // repeat
            //{"video/mpeg", ".mpe"}, // repeat
            //{"video/mpeg", ".mpeg"}, // repeat
            {"application/vnd.ms-mediapackage", ".mpf"},
            //{"video/mpeg", ".mpg"}, // repeat
            {"application/vnd.ms-project", ".mpp"},
            //{"video/mpeg", ".mpv2"}, // repeat
            //{"video/quicktime", ".mqv"}, // repeat
            {"application/x-troff-ms", ".ms"},
            //{"application/octet-stream", ".msi"}, // repeat
            //{"application/octet-stream", ".mso"}, // repeat
            //{"video/vnd.dlna.mpeg-tts", ".mts"}, // repeat
            //{"application/xml", ".mtx"}, // repeat
            //{"application/x-msmediaview", ".mvb"}, // repeat
            {"application/x-miva-compiled", ".mvc"},
            {"application/x-mmxp", ".mxp"},
            {"application/x-netcdf", ".nc"},
            //{"video/x-ms-asf", ".nsc"}, // repeat
            //{"message/rfc822", ".nws"}, // repeat
            //{"application/octet-stream", ".ocx"}, // repeat
            {"application/oda", ".oda"},
            {"text/x-ms-odc", ".odc"},
            //{"text/plain", ".odh"}, // repeat
            //{"text/plain", ".odl"}, // repeat
            {"application/vnd.oasis.opendocument.presentation", ".odp"},
            {"application/oleobject", ".ods"},
            {"application/vnd.oasis.opendocument.text", ".odt"},
            {"application/onenote", ".one"},
            //{"application/onenote", ".onea"}, // repeat
            //{"application/onenote", ".onepkg"}, // repeat
            //{"application/onenote", ".onetmp"}, // repeat
            //{"application/onenote", ".onetoc"}, // repeat
            //{"application/onenote", ".onetoc2"}, // repeat
            //{"application/xml", ".orderedtest"}, // repeat
            {"application/opensearchdescription+xml", ".osdx"},
            {"application/pkcs10", ".p10"},
            {"application/x-pkcs12", ".p12"},
            {"application/x-pkcs7-certificates", ".p7b"},
            {"application/pkcs7-mime", ".p7c"},
            //{"application/pkcs7-mime", ".p7m"}, // repeat
            {"application/x-pkcs7-certreqresp", ".p7r"},
            {"application/pkcs7-signature", ".p7s"},
            {"image/x-portable-bitmap", ".pbm"},
            {"application/x-podcast", ".pcast"},
            {"image/pict", ".pct"},
            //{"application/octet-stream", ".pcx"}, // repeat
            //{"application/octet-stream", ".pcz"}, // repeat
            {"application/pdf", ".pdf"},
            //{"application/octet-stream", ".pfb"}, // repeat
            //{"application/octet-stream", ".pfm"}, // repeat
            //{"application/x-pkcs12", ".pfx"}, // repeat
            {"image/x-portable-graymap", ".pgm"},
            //{"image/pict", ".pic"}, // repeat
            //{"image/pict", ".pict"}, // repeat
            //{"text/plain", ".pkgdef"}, // repeat
            //{"text/plain", ".pkgundef"}, // repeat
            {"application/vnd.ms-pki.pko", ".pko"},
            {"audio/scpls", ".pls"},
            {"application/x-perfmon", ".pma"},
            //{"application/x-perfmon", ".pmc"}, // repeat
            //{"application/x-perfmon", ".pml"}, // repeat
            //{"application/x-perfmon", ".pmr"}, // repeat
            //{"application/x-perfmon", ".pmw"}, // repeat
            {"image/png", ".png"},
            {"image/x-portable-anymap", ".pnm"},
            //{"image/x-macpaint", ".pnt"}, // repeat
            //{"image/x-macpaint", ".pntg"}, // repeat
            //{"image/png", ".pnz"}, // repeat
            //{"application/vnd.ms-powerpoint", ".pot"}, // repeat
            {"application/vnd.ms-powerpoint.template.macroEnabled.12", ".potm"},
            {"application/vnd.openxmlformats-officedocument.presentationml.template", ".potx"},
            //{"application/vnd.ms-powerpoint", ".ppa"}, // repeat
            {"application/vnd.ms-powerpoint.addin.macroEnabled.12", ".ppam"},
            {"image/x-portable-pixmap", ".ppm"},
            //{"application/vnd.ms-powerpoint", ".pps"}, // repeat
            {"application/vnd.ms-powerpoint.slideshow.macroEnabled.12", ".ppsm"},
            {"application/vnd.openxmlformats-officedocument.presentationml.slideshow", ".ppsx"},
            {"application/vnd.ms-powerpoint", ".ppt"},
            {"application/vnd.ms-powerpoint.presentation.macroEnabled.12", ".pptm"},
            {"application/vnd.openxmlformats-officedocument.presentationml.presentation", ".pptx"},
            {"application/pics-rules", ".prf"},
            //{"application/octet-stream", ".prm"}, // repeat
            //{"application/octet-stream", ".prx"}, // repeat
            //{"application/postscript", ".ps"}, // repeat
            {"application/PowerShell", ".psc1"},
            //{"application/octet-stream", ".psd"}, // repeat
            //{"application/xml", ".psess"}, // repeat
            //{"application/octet-stream", ".psm"}, // repeat
            //{"application/octet-stream", ".psp"}, // repeat
            {"application/x-mspublisher", ".pub"},
            //{"application/vnd.ms-powerpoint", ".pwz"}, // repeat
            {"text/x-html-insertion", ".qht"},
            //{"text/x-html-insertion", ".qhtm"}, // repeat
            //{"video/quicktime", ".qt"}, // repeat
            {"image/x-quicktime", ".qti"},
            //{"image/x-quicktime", ".qtif"}, // repeat
            {"application/x-quicktimeplayer", ".qtl"},
            //{"application/octet-stream", ".qxd"}, // repeat
            {"audio/x-pn-realaudio", ".ra"},
            //{"audio/x-pn-realaudio", ".ram"}, // repeat
            //{"application/octet-stream", ".rar"}, // repeat
            {"image/x-cmu-raster", ".ras"},
            {"application/rat-file", ".rat"},
            //{"text/plain", ".rc"}, // repeat
            //{"text/plain", ".rc2"}, // repeat
            //{"text/plain", ".rct"}, // repeat
            //{"application/xml", ".rdlc"}, // repeat
            //{"application/xml", ".resx"}, // repeat
            {"image/vnd.rn-realflash", ".rf"},
            {"image/x-rgb", ".rgb"},
            //{"text/plain", ".rgs"}, // repeat
            {"application/vnd.rn-realmedia", ".rm"},
            //{"audio/mid", ".rmi"}, // repeat
            {"application/vnd.rn-rn_music_package", ".rmp"},
            {"application/x-troff", ".roff"},
            {"audio/x-pn-realaudio-plugin", ".rpm"},
            {"text/x-ms-rqy", ".rqy"},
            {"application/rtf", ".rtf"},
            {"text/richtext", ".rtx"},
            //{"application/xml", ".ruleset"}, // repeat
            //{"text/plain", ".s"}, // repeat
            {"application/x-safari-safariextz", ".safariextz"},
            {"application/x-msschedule", ".scd"},
            {"text/scriptlet", ".sct"},
            {"audio/x-sd2", ".sd2"},
            {"application/sdp", ".sdp"},
            //{"application/octet-stream", ".sea"}, // repeat
            {"application/windows-search-connector+xml", ".searchConnector-ms"},
            {"application/set-payment-initiation", ".setpay"},
            {"application/set-registration-initiation", ".setreg"},
            //{"application/xml", ".settings"}, // repeat
            {"application/x-sgimb", ".sgimb"},
            {"text/sgml", ".sgml"},
            {"application/x-sh", ".sh"},
            {"application/x-shar", ".shar"},
            //{"text/html", ".shtml"}, // repeat
            {"application/x-stuffit", ".sit"},
            //{"application/xml", ".sitemap"}, // repeat
            //{"application/xml", ".skin"}, // repeat
            {"application/vnd.ms-powerpoint.slide.macroEnabled.12", ".sldm"},
            {"application/vnd.openxmlformats-officedocument.presentationml.slide", ".sldx"},
            {"application/vnd.ms-excel", ".slk"},
            //{"text/plain", ".sln"}, // repeat
            {"application/x-ms-license", ".slupkg-ms"},
            {"audio/x-smd", ".smd"},
            //{"application/octet-stream", ".smi"}, // repeat
            //{"audio/x-smd", ".smx"}, // repeat
            //{"audio/x-smd", ".smz"}, // repeat
            //{"audio/basic", ".snd"}, // repeat
            //{"application/xml", ".snippet"}, // repeat
            //{"application/octet-stream", ".snp"}, // repeat
            //{"text/plain", ".sol"}, // repeat
            //{"text/plain", ".sor"}, // repeat
            //{"application/x-pkcs7-certificates", ".spc"}, // repeat
            {"application/futuresplash", ".spl"},
            {"application/x-wais-source", ".src"},
            //{"text/plain", ".srf"}, // repeat
            //{"text/xml", ".SSISDeploymentManifest"}, // repeat
            {"application/streamingmedia", ".ssm"},
            {"application/vnd.ms-pki.certstore", ".sst"},
            {"application/vnd.ms-pki.stl", ".stl"},
            {"application/x-sv4cpio", ".sv4cpio"},
            {"application/x-sv4crc", ".sv4crc"},
            //{"application/xml", ".svc"}, // repeat
            //{"application/x-shockwave-flash", ".swf"}, // repeat
            //{"application/x-troff", ".t"}, // repeat
            {"application/x-tar", ".tar"},
            {"application/x-tcl", ".tcl"},
            //{"application/xml", ".testrunconfig"}, // repeat
            //{"application/xml", ".testsettings"}, // repeat
            {"application/x-tex", ".tex"},
            {"application/x-texinfo", ".texi"},
            //{"application/x-texinfo", ".texinfo"}, // repeat
            {"application/x-compressed", ".tgz"},
            {"application/vnd.ms-officetheme", ".thmx"},
            //{"application/octet-stream", ".thn"}, // repeat
            {"image/tiff", ".tif"},
            //{"image/tiff", ".tiff"}, // repeat
            //{"text/plain", ".tlh"}, // repeat
            //{"text/plain", ".tli"}, // repeat
            //{"application/octet-stream", ".toc"}, // repeat
            //{"application/x-troff", ".tr"}, // repeat
            {"application/x-msterminal", ".trm"},
            //{"application/xml", ".trx"}, // repeat
            //{"video/vnd.dlna.mpeg-tts", ".ts"}, // repeat
            {"text/tab-separated-values", ".tsv"},
            //{"application/octet-stream", ".ttf"}, // repeat
            //{"video/vnd.dlna.mpeg-tts", ".tts"}, // repeat
            //{"text/plain", ".txt"}, // repeat
            //{"application/octet-stream", ".u32"}, // repeat
            {"text/iuls", ".uls"},
            //{"text/plain", ".user"}, // repeat
            {"application/x-ustar", ".ustar"},
            //{"text/plain", ".vb"}, // repeat
            //{"text/plain", ".vbdproj"}, // repeat
            //{"video/mpeg", ".vbk"}, // repeat
            //{"text/plain", ".vbproj"}, // repeat
            {"text/vbscript", ".vbs"},
            {"text/x-vcard", ".vcf"},
            //{"Application/xml", ".vcproj"}, // repeat
            //{"text/plain", ".vcs"}, // repeat
            //{"Application/xml", ".vcxproj"}, // repeat
            //{"text/plain", ".vddproj"}, // repeat
            //{"text/plain", ".vdp"}, // repeat
            //{"text/plain", ".vdproj"}, // repeat
            {"application/vnd.ms-visio.viewer", ".vdx"},
            //{"text/xml", ".vml"}, // repeat
            //{"application/xml", ".vscontent"}, // repeat
            //{"text/xml", ".vsct"}, // repeat
            {"application/vnd.visio", ".vsd"},
            {"application/ms-vsi", ".vsi"},
            {"application/vsix", ".vsix"},
            //{"text/xml", ".vsixlangpack"}, // repeat
            //{"text/xml", ".vsixmanifest"}, // repeat
            //{"application/xml", ".vsmdi"}, // repeat
            //{"text/plain", ".vspscc"}, // repeat
            //{"application/vnd.visio", ".vss"}, // repeat
            //{"text/plain", ".vsscc"}, // repeat
            //{"text/xml", ".vssettings"}, // repeat
            //{"text/plain", ".vssscc"}, // repeat
            //{"application/vnd.visio", ".vst"}, // repeat
            //{"text/xml", ".vstemplate"}, // repeat
            {"application/x-ms-vsto", ".vsto"},
            //{"application/vnd.visio", ".vsw"}, // repeat
            //{"application/vnd.visio", ".vsx"}, // repeat
            //{"application/vnd.visio", ".vtx"}, // repeat
            {"audio/wav", ".wav"},
            //{"audio/wav", ".wave"}, // repeat
            {"audio/x-ms-wax", ".wax"},
            //{"application/msword", ".wbk"}, // repeat
            {"image/vnd.wap.wbmp", ".wbmp"},
            {"application/vnd.ms-works", ".wcm"},
            //{"application/vnd.ms-works", ".wdb"}, // repeat
            {"image/vnd.ms-photo", ".wdp"},
            {"application/x-safari-webarchive", ".webarchive"},
            //{"application/xml", ".webtest"}, // repeat
            //{"application/xml", ".wiq"}, // repeat
            //{"application/msword", ".wiz"}, // repeat
            //{"application/vnd.ms-works", ".wks"}, // repeat
            {"application/wlmoviemaker", ".WLMP"},
            {"application/x-wlpg-detect", ".wlpginstall"},
            {"application/x-wlpg3-detect", ".wlpginstall3"},
            {"video/x-ms-wm", ".wm"},
            {"audio/x-ms-wma", ".wma"},
            {"application/x-ms-wmd", ".wmd"},
            {"application/x-msmetafile", ".wmf"},
            {"text/vnd.wap.wml", ".wml"},
            {"application/vnd.wap.wmlc", ".wmlc"},
            {"text/vnd.wap.wmlscript", ".wmls"},
            {"application/vnd.wap.wmlscriptc", ".wmlsc"},
            {"video/x-ms-wmp", ".wmp"},
            {"video/x-ms-wmv", ".wmv"},
            {"video/x-ms-wmx", ".wmx"},
            {"application/x-ms-wmz", ".wmz"},
            {"application/vnd.ms-wpl", ".wpl"},
            //{"application/vnd.ms-works", ".wps"}, // repeat
            {"application/x-mswrite", ".wri"},
            //{"x-world/x-vrml", ".wrl"}, // repeat
            //{"x-world/x-vrml", ".wrz"}, // repeat
            //{"text/scriptlet", ".wsc"}, // repeat
            //{"text/xml", ".wsdl"}, // repeat
            {"video/x-ms-wvx", ".wvx"},
            {"application/directx", ".x"},
            //{"x-world/x-vrml", ".xaf"}, // repeat
            {"application/xaml+xml", ".xaml"},
            {"application/x-silverlight-app", ".xap"},
            {"application/x-ms-xbap", ".xbap"},
            {"image/x-xbitmap", ".xbm"},
            //{"text/plain", ".xdr"}, // repeat
            {"application/xhtml+xml", ".xht"},
            //{"application/xhtml+xml", ".xhtml"}, // repeat
            //{"application/vnd.ms-excel", ".xla"}, // repeat
            {"application/vnd.ms-excel.addin.macroEnabled.12", ".xlam"},
            //{"application/vnd.ms-excel", ".xlc"}, // repeat
            //{"application/vnd.ms-excel", ".xld"}, // repeat
            //{"application/vnd.ms-excel", ".xlk"}, // repeat
            //{"application/vnd.ms-excel", ".xll"}, // repeat
            //{"application/vnd.ms-excel", ".xlm"}, // repeat
            //{"application/vnd.ms-excel", ".xls"}, // repeat
            {"application/vnd.ms-excel.sheet.binary.macroEnabled.12", ".xlsb"},
            {"application/vnd.ms-excel.sheet.macroEnabled.12", ".xlsm"},
            {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ".xlsx"},
            //{"application/vnd.ms-excel", ".xlt"}, // repeat
            {"application/vnd.ms-excel.template.macroEnabled.12", ".xltm"},
            {"application/vnd.openxmlformats-officedocument.spreadsheetml.template", ".xltx"},
            //{"application/vnd.ms-excel", ".xlw"}, // repeat
            //{"text/xml", ".xml"}, // repeat
            //{"application/xml", ".xmta"}, // repeat
            //{"x-world/x-vrml", ".xof"}, // repeat
            //{"text/plain", ".XOML"}, // repeat
            {"image/x-xpixmap", ".xpm"},
            {"application/vnd.ms-xpsdocument", ".xps"},
            //{"text/xml", ".xrm-ms"}, // repeat
            //{"application/xml", ".xsc"}, // repeat
            //{"text/xml", ".xsd"}, // repeat
            //{"text/xml", ".xsf"}, // repeat
            //{"text/xml", ".xsl"}, // repeat
            //{"text/xml", ".xslt"}, // repeat
            //{"application/octet-stream", ".xsn"}, // repeat
            //{"application/xml", ".xss"}, // repeat
            //{"application/octet-stream", ".xtp"}, // repeat
            {"image/x-xwindowdump", ".xwd"},
            {"application/x-compress", ".z"},
            {"application/x-zip-compressed", ".zip"},
            #endregion
        };
    }
}
