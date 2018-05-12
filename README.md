# ProtocolGenerater

protobuf批量管理自动生成方案（2018/04/20）
================================================
简介
-------------------------------------------
这是一个批量管理生成协议文件的方式。

如子目录protoforclient项目，是一个协议管理项目的例子

将协议模型内容写入项目中的.code文件内，可以写入多个，然后，通过一键生成项目来生成相应语言环境下的protobuf相关代码

目前
-----------------------------------------
手游主流客户端开发，多为Unity3D，要用到c# 

服务端开发多用c++、java，同样也有用c#的，

所以，这里主要实现c#，java，c++这3种语言（下文中各个语言）的protobuf协议的自动批量生成。

谷歌protobuf分两个版本，proto2和proto3 

其中，版本syntax="proto3"，Google/protobuf原生全面支持各个语言 https://github.com/google/protobuf 
所以，我用官方protoc.exe来生成代码（这里用的是protoc-3.5.1-win32）

而，默认版本syntax="proto2" ，Google/protobuf并没有提供c#版，所以proto2需要通过第三方的protobuf-net https://github.com/mgravell/protobuf-net 来支持c#，所以，我用protogen.exe来生成代码


具体需求和实现思路
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

可以看出，这个模型是在标准proto结构的msgName后面加了一个msgId ，形成列一个新的协议描述模型，

我这里做的就是:
---------------------------------------------
	1 将这个模型，拆分成msgId和标准的proto协议

	2 通过标准的proto协议，生成对应语言的protobuf协议代码。

	3 生成相应语言的msgId与msgName之间的映射

实现批量完成上述操作。

大致使用方式：
----------------------------------------

将如上 msgId和msgValue描述模型的协议写入ClientProtocolBuilder项目中的.code文件。

一键生成ClientProtocolBuilder项目

就会在protocol文件夹下找到生成好的相应语言的文件。

目前只实现了c# 和 java 的生成。
