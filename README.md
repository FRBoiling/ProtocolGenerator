# ProtocolGenerater

protobuf 自动生成工具

目的是批量自动生成各个版本的protoBuf

目前，手游主流客户端开发，多为Unity3D，要用到c# 

服务端开发多用c++、java，同样也有用c#的，

所以，这里主要实现c#，java，c++这3种语言（下文中各个语言）的protobuf协议的自动批量生成。

谷歌protobuf分两个版本，proto2和proto3 （2018/04/20）

其中，版本syntax="proto3"，Google/protobuf原生全面支持各个语言 https://github.com/google/protobuf 
所以，我用官方protoc.exe来生成代码（这里用的是protoc-3.5.1-win32）

而，默认版本syntax="proto2" ，Google/protobuf并没有提供c#版，所以proto2需要通过第三方的protobuf-net https://github.com/mgravell/protobuf-net 来支持c#，所以，我用protogen.exe来生成代码


