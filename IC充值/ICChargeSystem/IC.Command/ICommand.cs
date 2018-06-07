using System;
namespace IC.Command
{
    interface ICommand
    {
        System.Collections.Generic.List<byte> btRead { get; set; }
        System.Collections.Generic.List<byte> btWrite { get; set; }
        bool ReadData(int ReadLength);
        bool WriteData();
    }
}
