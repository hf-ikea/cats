using CATS;

Identification id = new Identification("NO6H", 250, 0);
byte[] idWhisker = id.Encode();

Identification newId = new Identification();

newId.Decode(idWhisker);

Console.WriteLine(BitConverter.ToString(idWhisker));
Console.WriteLine("Callsign: " + newId._call + ", SSID: " + newId._ssid + ", Icon: " + newId._icon);

Console.WriteLine(1 << 40);
ulong unixTime = 1099511627775;
byte[] unixBytes = BitConverter.GetBytes(unixTime);
Console.WriteLine(BitConverter.ToString(unixBytes));

Timestamp ts = new Timestamp(1000);
Console.WriteLine(BitConverter.ToString(ts._timeBytes));