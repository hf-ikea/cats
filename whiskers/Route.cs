namespace CATS
{
    public enum HopType
    {
        Internet,
        Past,
        Future
    }
    public class Route
    {
        public List<byte> hopList;
        public byte maxHops;
        public Route()
        {
            hopList = new List<byte>();
        }

        public List<byte> AddHop(Hop hop)
        {
            switch(hop.hopType)
            {
                case HopType.Internet:
                    AddInternetHop();
                    break;
                case HopType.Past:
                    AddPastHop(hop);
                    break;
                case HopType.Future:
                    AddFutureHop(hop);
                    break;
            }
            return hopList;
        }

        public void AddPastHop(Hop pastHop)
        {
            hopList.AddRange(System.Text.Encoding.UTF8.GetBytes(pastHop.call));
            hopList.Add(0xFF);
            hopList.Add(pastHop.ssid);
            hopList.Add(pastHop.rssi);
        }

        public void AddFutureHop(Hop futureHop)
        {
            hopList.AddRange(System.Text.Encoding.UTF8.GetBytes(futureHop.call));
            hopList.Add(0xFD);
            hopList.Add(futureHop.ssid);
        }

        public void AddInternetHop()
        {
            hopList.Add(0xFE);
        }
    }

    public class Hop
    {
        public HopType hopType;
        public string call;
        public byte ssid;
        public byte rssi;
        public Hop(string _call, byte _ssid, float _rssi)
        {
            call = _call;
            ssid = _ssid;
            rssi = (byte)Math.Max(_rssi * 1.5 + 240, 1);
        }

        public Hop(string _call, byte _ssid)
        {
            call = _call;
            ssid = _ssid;
        }

        public Hop()
        {
            call = "";
        }

        public float GetRSSI()
        {
            return (rssi - 240.0f) / 1.5f;
        }
    }
}