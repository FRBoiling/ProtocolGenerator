/************************************************************************ 
 * 项目名称 :  ProtocolGenerater.Core.CSharp       
 * 类 名 称 :  CSharpClass_Proto 
 * 类 描 述 : 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 21:15:58 
 * 更新时间 :  2018/4/20 星期五 21:15:58 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProtocolGenerater.Core.CSharp
{
    public class CSharpClass_Proto
    {
        private string m_InputfilePath;
        private string m_OutputfilePath;
        private string m_InputfileName;
        private string m_InpufileSuffix;
        private string m_InputfileSimpleName;

        Data _data = null;

        public CSharpClass_Proto(Data data)
        {
            _data = data;
            string tempPath = _data.ProtoPackageName.Replace('.', '\\');
            m_OutputfilePath = Program.OutputPath + @"CSharp\" + tempPath + @"\";
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

        public void GenerateProto_Net400()
        {
            //调用外部程序protogen.exe
            Process p = new Process();
            //p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.FileName = Application.StartupPath + @"\net462\protogen.exe";
            //p.StartInfo.WorkingDirectory = Application.StartupPath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = false;
            //p.StartInfo.Arguments = @" -I" + m_filePathIn+ " *.proto" + @" --csharp_out=" + m_filePathOut;// -ns:" + Application.StartupPath;
            p.StartInfo.Arguments = m_InputfileSimpleName+ @" -I" + m_InputfilePath + @" --csharp_out=" + m_OutputfilePath;
           // Console.WriteLine("{0} {1}", p.StartInfo.FileName, p.StartInfo.Arguments);

            p.Start();
            p.StandardInput.WriteLine("exit");

            //p.WaitForExit();
            //string command = Application.StartupPath + @"\protogen.exe -i:" + fileName + @" -o:descriptor.cs -ns:" + Application.StartupPath;
            //p.StandardInput.WriteLine(command);
            //p.StandardInput.WriteLine("exit"); //需要有这句，不然程序会挂机
            // 向cmd.exe输入command
            //string output = p.StandardOutput.ReadToEnd(); 这句可以用来获取执行命令的输出结果
        }

        public void GenerateProto_Old()
        {
            //调用外部程序protogen.exe
            Process p = new Process();
            //p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.FileName = Application.StartupPath + @"\net_old\ProtoGen\protogen.exe";
            //Console.WriteLine("---->{0}", p.StartInfo.FileName);
            //p.StartInfo.WorkingDirectory = Application.StartupPath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = false;
            //protogen -i:input.proto -o:output.cs
            p.StartInfo.Arguments = @" -i:" + m_InputfilePath+ m_InputfileSimpleName + " -o:" + m_OutputfilePath + m_InputfileName+@".cs";
            //Console.WriteLine("---->{0} {1}", p.StartInfo.FileName, p.StartInfo.Arguments);

            p.Start();
            //p.WaitForExit();
            p.StandardInput.WriteLine("exit");

            //string command = Application.StartupPath + @"\protogen.exe -i:" + fileName + @" -o:descriptor.cs -ns:" + Application.StartupPath;
            //p.StandardInput.WriteLine(command);
            //p.StandardInput.WriteLine("exit"); //需要有这句，不然程序会挂机
            // 向cmd.exe输入command
            //string output = p.StandardOutput.ReadToEnd(); //这句可以用来获取执行命令的输出结果
            //Console.WriteLine("--->{0}",output);
        }

    }
}
