using CATS;

byte[] test = "123456789"u8.ToArray();

Console.WriteLine(CRC.CRCRemainder(test));