# ProtocolGenerator

Protobuf批量管理自动生成方案（2019/03/20）
================================================
#简介
-------------------------------------------
这是一个批量管理生成协议文件的方式。

如子目录ProtocolBuilder项目，是一个协议管理项目的例子

将协议模型内容写入项目中的.code文件内，可以写入多个，然后，通过生成项目的方式，一键生成相应语言环境下的protobuf相关代码

#目前
-----------------------------------------
手游主流客户端开发，多为Unity3D，要用到c# 

服务端开发多用c++、java，同样也有用c#的，

所以，这里主要实现c#，java，c++这3种语言（下文中各个语言）的protobuf协议的自动批量生成。

谷歌protobuf分两个版本，proto2和proto3，

其中，版本syntax="proto3"，Google/protobuf原生全面支持各个语言 https://github.com/google/protobuf 
注：我这里不再维护proto2相关内容。

所以，我用官方protoc.exe来生成代码： https://github.com/protocolbuffers/protobuf/releases/download/v3.6.0/protoc-3.6.0-win32.zip

截止目前为止，依然只实现了java和c#代码（c++的后续跟进）

#具体需求和实现思路
--------------------------------------
我定义的协议格式为：
```c
length + msgId + msgData
```
length为short型的数据长度，msgId为int32的协议编号，msgData为标准protobuf数据。

其中，msgId和msgData的描述模型，是以如下格式写在.code后缀的文件中的：
```c
message A2BTest 0xff00001
{
	required int32 testInt = 1;
	required string testString = 2;
}
```
A2BTest为协议名msgName， 0xff00001为协议编号msgId，testInt为int32类型字段，testString为string类型字段

可以看出，这个模型是在标准.proto结构的msgName后面加了一个msgId ，形成列一个新的协议描述模型

我这里做的就是:
---------------------------------------------
	1 将这个模型，拆分成msgId和标准的.proto协议

	2 通过标准的.proto协议，生成对应语言的protobuf协议代码。

	3 生成相应语言的msgId与msgName之间的映射

	4 实现批量完成上述操作。

大致使用方式：
----------------------------------------
1 生成ProtocolGenerator.exe文件,如已经生成过了会在Output目录下

2 将如上msgId和msgValue描述模型的协议写入ProtocolBuilder项目中的.code文件。

3 生成ProtocolBuilder项目

4 会在Cache目录下找到生成好的相应语言的文件。

目前只实现了c# 和 java 的生成。

测试例子
--------------------------------------
项目例子主要用c#实现，用到谷歌的protobuf源码：https://github.com/protocolbuffers/protobuf/releases/download/v3.6.0/protobuf-csharp-3.6.0.zip 
protobuf-csharp-3.6.0官方默认为.net4.x，但是，我这里出于unity客户端版本原因，仅支持到.net3.5。所以我选择自己编译.net35的dll。Google.Protobuf的net35项目，在子目录Google.Protobuf下面
