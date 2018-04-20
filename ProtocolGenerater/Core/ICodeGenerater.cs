using System.Collections.Generic;
using System.Text;

namespace ProtocolGenerater
{
    public interface ICodeGenerater
    {
        /// <summary>
        /// 变量框架
        /// </summary>
        /// <param name="attrType"></param>
        /// <param name="attrName"></param>
        /// <param name="spaceCount"></param>
        /// <returns></returns>
        StringBuilder AttrFrame(string attrType, string attrName, int spaceCount = 2);
        /// <summary>
        /// 类框架
        /// </summary>
        /// <param name="className"></param>
        /// <param name="attrs"></param>
        /// <param name="methods"></param>
        /// <param name="spaceCount"></param>
        /// <returns></returns>
        StringBuilder ClassFrame(string className, List<StringBuilder> attrs = null, List<StringBuilder> methods = null, int spaceCount = 1);
        /// <summary>
        /// 文件开头说明文字框架
        /// </summary>
        /// <returns></returns>
        StringBuilder ClassCommentsFrame();
        /// <summary>
        /// 包含头文件框架
        /// </summary>
        /// <returns></returns>
        StringBuilder IncludeHeadFrame();
        /// <summary>
        /// 函数框架
        /// </summary>
        /// <param name="methodType"></param>
        /// <param name="methodName"></param>
        /// <param name="methodValue"></param>
        /// <param name="spaceCount"></param>
        /// <returns></returns>
        StringBuilder MethodFrame(string methodType, string methodName, List<StringBuilder> methodValue, int spaceCount = 2);
        /// <summary>
        /// namespace框架
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="classBody"></param>
        /// <returns></returns>
        StringBuilder NameSpaceFrame(string nameSpace, StringBuilder classBody);
    }
}