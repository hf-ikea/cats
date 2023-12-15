using CATS;

Route r = new Route(5);
Hop h1 = new Hop("K1ABC", 1, -64, HopType.Past);
Hop h2 = new Hop("K2DEF", 2, -13, HopType.Past);
r.AddHop(h1);
r.AddHop(h2);
IEnumerable<Hop> iter = r.GetHopList();

foreach(Hop h in iter)
{
    Console.WriteLine("HopType: " + h.hopType.ToString() + " Call: " + h.call + "~" + h.ssid + " RSSI: " + h.GetRSSI());
}