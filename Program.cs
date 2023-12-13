using CATS;

byte[] data = new byte[128];
byte[] interleaved = new byte[128];
byte[] deinterleaved = new byte[128];
for(int i = 0; i < data.Length; i++)
{
    data[i] = (byte)i;
}

Console.WriteLine("Original:      " + BitConverter.ToString(data));
Interleaver inter = new Interleaver();
interleaved = inter.Interleave(data);
Console.WriteLine("Interleaved:   " + BitConverter.ToString(interleaved));
deinterleaved = inter.Deinterleave(interleaved);
Console.WriteLine("Deinterleaved: " + BitConverter.ToString(deinterleaved));