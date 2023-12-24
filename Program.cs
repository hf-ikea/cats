using CATS;

byte[] data = new byte[16];
for(byte i = 0; i < data.Length; i++)
{
    data[i] = i;
}
byte[] outputData = Whitener.Whiten(data);
Console.WriteLine(BitConverter.ToString(Whitener.Whiten(outputData)));