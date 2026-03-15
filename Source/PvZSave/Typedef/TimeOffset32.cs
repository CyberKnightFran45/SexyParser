using System;
using System.IO;

namespace SexyParsers.PvZSave
{
/// <summary> Helper for handling DateTimes prefixed by an Offset </summary>

public static class TimeOffset32
{
// Time offset

private static readonly DateTime offset = new(2000, 1, 1);

// Read

public static DateTime? Read(Stream reader)
{
uint days = reader.ReadUInt32();

if(days == 0)
return null;

return offset.AddDays(days);
}

// Write

public static void Write(Stream writer, DateTime? date)
{
uint days;

if(date is null)
days = 0;

else
{
var dt = (DateTime)date;

days = (uint)dt.Subtract(offset).TotalDays;
}

writer.WriteUInt32(days);
}

}

}