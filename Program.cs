using CATS;

Route r = new Route(5);
Hop h1 = new Hop("K1ABC", 1, -64, HopType.Past);
Hop h2 = new Hop("K2DEF", 2, -13, HopType.Past);
Hop h3 = new Hop("K3XYZ", 3, HopType.Future);
Hop h4 = new Hop("K3XYZ", 3, -8, HopType.Past);
r.IntellegentAddHop(h1);
r.IntellegentAddHop(h2);
r.IntellegentAddHop(h3);

foreach(Hop h in r.GetHopList())
{
    Console.WriteLine("HopType: " + h.hopType.ToString() + " Call: " + h.call + "~" + h.ssid + " RSSI: " + h.GetRSSI());
}
Console.WriteLine("----------------------");
r.IntellegentAddHop(h4);
foreach(Hop h in r.GetHopList())
{
    Console.WriteLine("HopType: " + h.hopType.ToString() + " Call: " + h.call + "~" + h.ssid + " RSSI: " + h.GetRSSI());
}