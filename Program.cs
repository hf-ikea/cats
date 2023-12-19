using CATS;

byte[] test = "123456789"u8.ToArray();

CRC c = new();
Console.WriteLine(c.CRCRemainder(test));