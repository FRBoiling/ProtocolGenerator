# ProtocolGenerater

protobuf 自动批量生成方案（2018/04/20）

这是一个批量管理生成协议文件的方式。

目前，手游主流客户端开发，多为Unity3D，要用到c# 

服务端开发多用c++、java，同样也有用c#的，

所以，这里主要实现c#，java，c++这3种语言（下文中各个语言）的protobuf协议的自动批量生成。

谷歌protobuf分两个版本，proto2和proto3 

其中，版本syntax="proto3"，Google/protobuf原生全面支持各个语言 https://github.com/google/protobuf 
所以，我用官方protoc.exe来生成代码（这里用的是protoc-3.5.1-win32）

而，默认版本syntax="proto2" ，Google/protobuf并没有提供c#版，所以proto2需要通过第三方的protobuf-net https://github.com/mgravell/protobuf-net 来支持c#，所以，我用protogen.exe来生成代码


以下为具体需求和实现思路：

我自定义的协议格式为，一个int32的协议编号，加protobuf数据。大致通过如下格式来描述：

message A2BTest 0xff00001

{

	  required int32 testint = 1;
	  required string teststring = 2;

}

其中， A2BTest为协议名， 0xff00001为协议编号，testint为int32类型字段，teststring为string类型字段

可以看出，这个描述比标准proto结构只多了一个协议编号（0xff00001）这样的一个数据。

我这里做的就是，将这个描述，拆分，并生成标准的proto协议，和相应语言的协议名称、协议编号的键值对。

最终，实现通过协议编号来识别对应的proto协议的目的。




