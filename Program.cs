using CATS;

Identification id = new Identification("NO6H", 250, 0);
byte[] idWhisker = id.Encode();

Identification newId = new Identification();

newId.Decode(idWhisker);

Console.WriteLine(BitConverter.ToString(idWhisker));
Console.WriteLine("Callsign: " + newId._call + ", SSID: " + newId._ssid + ", Icon: " + newId._icon);