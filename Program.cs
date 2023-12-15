using CATS;

Route r = new Route(5);
Hop h1 = new Hop("K1ABC", 1, -64, HopType.Past);
Hop h2 = new Hop("K2DEF", 2, -13, HopType.Past);
Hop h3 = new Hop("K3XYZ", 3, HopType.Future);
r.AddHop(h1);
r.AddInternetHop();
r.AddHop(h2);
r.AddHop(h3);

r.Encode();
Route newR = new Route();
newR.Decode(r.encoded);
IEnumerable<Hop> iter = newR.GetHopList();

foreach(Hop h in iter)
{
    Console.WriteLine("HopType: " + h.hopType.ToString() + " Call: " + h.call + "~" + h.ssid + " RSSI: " + h.GetRSSI());
}