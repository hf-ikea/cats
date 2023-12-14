using CATS;

GPS gps = new GPS(34.4, -108.9, 100, 0, 20, 100);
gps.Encode();
Console.WriteLine(BitConverter.ToString(gps.encoded));
GPS newGPS = new GPS();
newGPS.Decode(gps.encoded);
Console.WriteLine("Lat: " + newGPS.latitude + " Long: " + newGPS.longitude + " Alt: " + newGPS.altitude + " LocErr: " + newGPS.locationError + " Heading: " + newGPS.heading + " Speed: " + newGPS.speed);