/************************************************************************ 
 * 项目名称 :  ProtocolGenerater   
 * 项目描述 :      
 * 类 名 称 :  DataManager 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  Boiling 
 * 创建时间 :  2018/4/20 星期五 16:46:38 
 * 更新时间 :  2018/4/20 星期五 16:46:38 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using ProtocolBuffersGenerater.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolGenerater
{
    public class DataManager
    {

        private static DataManager _inst;

        // 定义私有构造函数，使外界不能创建该类实例
        private DataManager()
        {
        }
        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static DataManager GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (_inst == null)
            {
                _inst = new DataManager();
            }
            return _inst;
        }

        /// <summary>
        /// proto datas
        /// </summary>
        Dictionary<string, Data> _dataDic = new Dictionary<string, Data>();

        public Dictionary<string, Data> DataDic { get => _dataDic; set => _dataDic = value; }

        /// <summary>
        /// load proto.code file 
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
            Data data = DataParser.ParseCodeFile(fileName);
            if (_dataDic.ContainsKey(data.ProtoFileKey))
            {
                Console.WriteLine("Load an repeated file {0}",data.ProtoFileKey);
            }
            else
            {
                _dataDic.Add(data.ProtoFileKey, data);
            }
        }

        /// <summary>
        /// 检查文件是否失效
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public bool CheckFilePast(string fileFullName)
        {
            GeneraterCache cache = new GeneraterCache();
            return cache.CheckCache(fileFullName);
        }
    }
}
