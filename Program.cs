using CATS;

GPS gps = new GPS(34.4, -108.9, 100, 0, 20, 100);
gps.Encode();
Console.WriteLine(BitConverter.ToString(gps.encoded));
GPS newGPS = new GPS();
newGPS.Decode(gps.encoded);
Console.WriteLine("Lat: " + newGPS.latitude + " Long: " + newGPS.longitude + " Alt: " + newGPS.altitude + " LocErr: " + newGPS.locationError + " Heading: " + newGPS.heading + " Speed: " + newGPS.speed);

Comment c = new Comment("sawyer");
c.Encode();
Console.WriteLine(BitConverter.ToString(c.encoded));
Comment newC = new Comment();
newC.Decode(c.encoded);
Console.WriteLine(newC.comment);

Destination d = new Destination("NO6H", 1, true, 1);
d.Encode();
Console.WriteLine(BitConverter.ToString(d.encoded));
Destination newD = new Destination();
newD.Decode(d.encoded);
Console.WriteLine("Dest: " + newD.call + "~" + newD.ssid + " isAck? " + newD.isAck + " ackID: " + newD.ackID);