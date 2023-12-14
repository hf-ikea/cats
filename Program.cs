using CATS;

Arbitrary a = new Arbitrary(new byte[4] { 0, 1, 2, 3 });
a.Encode();
Console.WriteLine(BitConverter.ToString(a.encoded));
Arbitrary newA = new Arbitrary();
newA.Decode(a.encoded);
Console.WriteLine(BitConverter.ToString(newA.data));