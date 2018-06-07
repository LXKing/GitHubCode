Basic（基本的）
     Data Source=filename;Version=3;
Using UTF16（使用UTF16编码）
    Data Source=filename;Version=3;UseUTF16Encoding=True;
With password（带密码的）
    Data Source=filename;Version=3;Password=myPassword;
Using the pre 3.3x database format（使用3.3x前数据库格式）
     Data Source=filename;Version=3;Legacy Format=True;
Read only connection（只读连接）
    Data Source=filename;Version=3;Read Only=True;
With connection pooling（设置连接池）
    Data Source=filename;Version=3;Pooling=False;Max Pool Size=100;
Using DateTime.Ticks as datetime format（）
     Data Source=filename;Version=3;DateTimeFormat=Ticks;
     
Store GUID as text（把Guid作为文本存储，默认是Binary）
    Data Source=filename;Version=3;BinaryGUID=False;
      如果把Guid作为文本存储需要更多的存储空间 
Specify cache size（指定Cache大小）
     Data Source=filename;Version=3;Cache Size=2000;
      Cache Size 单位是字节
Specify page size（指定页大小）
     Data Source=filename;Version=3;Page Size=1024;
      Page Size 单位是字节