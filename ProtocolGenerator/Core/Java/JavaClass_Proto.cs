/************************************************************************ 
 * 项目名称 :  ProtocolGenerator.Core.Java       
 * 类 名 称 :  JavaClass_Proto 
 * 类 描 述 : 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/21 星期六 12:32:44 
 * 更新时间 :  2018/4/21 星期六 12:32:44 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System.Diagnostics;
using System.Windows.Forms;

namespace ProtocolGenerator.Core.Java
{
    public class JavaClass_Proto
    {
        private string m_InputfilePath;
        private string m_OutputfilePath;
        private string m_InputfileName;
        private string m_InpufileSuffix;
        private string m_InputfileSimpleName;

        Data _data = null;

        public JavaClass_Proto(Data data)
        {
            _data = data;
            string tempPath = _data.ProtoPackageName.Replace('.', '\\');
            m_OutputfilePath = Program.OutputPath + @"\Java\";
            m_InputfileName = data.ProtoFileKey;
            m_InpufileSuffix = ".proto";
            m_InputfileSimpleName = m_InputfileName + m_InpufileSuffix;

            #region 指定路径
            //m_InputfilePath = Program.OutputPath + @"Proto\" + tempPath + @"\";
            #endregion
            #region 当前路径与code同路径
            string tempfilename = m_InputfileName + ".code";
            m_InputfilePath = Program.Filename.Replace(tempfilename, "");
            #endregion

        }

        public void GenerateProto()
        {
            //调用外部程序protogen.exe
            Process p = new Process();
            //p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.FileName = Application.StartupPath + @"\google_protoc\protoc.exe";
            //p.StartInfo.WorkingDirectory = Application.StartupPath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = false;
            //protoc D:\a\userinfo.proto -ID:\a --java_out = D:\a 
            p.StartInfo.Arguments = m_InputfileSimpleName + @" -I" + m_InputfilePath + @" --java_out=" + m_OutputfilePath;
            //Console.WriteLine("{0} {1}", p.StartInfo.FileName, p.StartInfo.Arguments);

            p.Start();
            p.StandardInput.WriteLine("exit");
        }
    }
}
