using System.Data;

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
        public bool hasFuture;
        public byte[] encoded = new byte[1] { 0 };
        public Route(byte _maxHops)
        {
            maxHops = _maxHops;
            hopList = new List<byte>(254);
        }

        public Route()
        {
            hopList = new List<byte>(254);
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
            if(hopList.Capacity - hopList.Count < pastHop.call.Length + 3) { return; }
            if(hasFuture)
            {
                return; // you cant have something happen before something else is supposed to happen
            }
            hopList.AddRange(System.Text.Encoding.UTF8.GetBytes(pastHop.call));
            hopList.Add(0xFF);
            hopList.Add(pastHop.ssid);
            hopList.Add(pastHop.rssi);
        }

        public void AddFutureHop(Hop futureHop)
        {
            if(hopList.Capacity - hopList.Count < futureHop.call.Length + 2) { return; }
            hasFuture = true;
            hopList.AddRange(System.Text.Encoding.UTF8.GetBytes(futureHop.call));
            hopList.Add(0xFD);
            hopList.Add(futureHop.ssid);
        }

        public void AddInternetHop()
        {
            if(hopList.Capacity - hopList.Count < 1) { return; }
            hopList.Add(0xFE);
        }

        public void IntellegentAddHop(Hop newHop)
        {
            Route newRoute = new(maxHops);
            bool inserted = false;
            foreach(Hop h in GetHopList())
            {
                switch(h.hopType)
                {
                    case HopType.Internet:
                    {
                        newRoute.AddInternetHop();
                        break;
                    }
                    case HopType.Past:
                    {
                        if(h.call == newHop.call && h.ssid == newHop.ssid)
                        {
                            throw new Exception("New hop is a duplicate.");
                        }
                        else
                        {
                            newRoute.AddPastHop(h);
                        }
                        break;
                    }
                    case HopType.Future:
                    {
                        if(newHop.call == h.call && newHop.ssid == h.ssid)
                        {
                            if(inserted)
                            {
                                throw new Exception("New hop is a duplicate.");
                            }
                            else
                            {
                                newRoute.AddPastHop(h);
                                inserted = true;
                            }
                        } else if(inserted)
                        {
                            newRoute.AddFutureHop(h);
                        }
                        else
                        {
                            throw new Exception("Cannot place hops (that have happened) after ones that have not already happened.");
                        }
                        break;
                    }
                }
            }
            if(!inserted)
            {
                newRoute.AddHop(newHop);
            }

            if(newRoute.GetHopList().Count() > newRoute.maxHops)
            {
                throw new Exception("Reached hop limit.");
            }

            hopList = newRoute.hopList;
            hasFuture = newRoute.hasFuture;
        }

        public byte[] Encode()
        {
            encoded = new byte[hopList.Count + 3];
            encoded[0] = 4;
            encoded[1] = (byte)(hopList.Count + 1);
            encoded[2] = maxHops;
            Array.Copy(hopList.ToArray(), 0, encoded, 3, hopList.Count);
            return encoded;
        }

        public Route Decode(byte[] data)
        {
            byte[] temp = new byte[data.Length - 3];
            Array.Copy(data, 3, temp, 0, data[1] - 1);
            hopList = temp.ToList();
            maxHops = data[2];
            return this;
        }

        public IEnumerable<Hop> GetHopList()
        {
            byte currentByte;
            bool callFound = false;
            List<byte> callBytes = new();
            for(int i = 0; i < hopList.Count;)
            {
                currentByte = hopList[i];
                if(currentByte == 0xFE)
                {
                    Hop internetHop = new(true)
                    {
                        hopType = HopType.Internet
                    };
                    yield return internetHop;
                }
                else if(currentByte != 0xFD && currentByte != 0xFF)
                {
                    if(!callFound)
                    {
                        callFound = true;
                        callBytes.Clear();
                    }
                    callBytes.Add(currentByte);
                }
                else if(currentByte == 0xFD)
                {
                    callFound = false;
                    i++;
                    yield return new(System.Text.Encoding.UTF8.GetString(callBytes.ToArray()), hopList[i], HopType.Future);
                }
                else if(currentByte == 0xFF)
                {
                    callFound = false;
                    i += 2;
                    yield return new(System.Text.Encoding.UTF8.GetString(callBytes.ToArray()), hopList[i - 1], hopList[i], HopType.Past, false);
                }

                i++;
            }
        }
    }

    public class Hop
    {
        public HopType hopType;
        public string call;
        public byte ssid;
        public byte rssi;
        public bool internet = false;
        public Hop(string _call, byte _ssid, byte _rssi, HopType _hopType, bool _convertRSSI = true)
        {
            call = _call;
            ssid = _ssid;
            if(_convertRSSI)
            {
                rssi = (byte)Math.Max(1.5 * _rssi + 240, 1);
            }
            else 
            {
                rssi = _rssi;
            }
            hopType = _hopType;
        }

        public Hop(string _call, byte _ssid, float _rssi, HopType _hopType)
        {
            call = _call;
            ssid = _ssid;
            rssi = (byte)Math.Max(1.5 * _rssi + 240, 1);
            hopType = _hopType;
        }

        public Hop(string _call, byte _ssid, float _rssi)
        {
            call = _call;
            ssid = _ssid;
            rssi = (byte)Math.Max(1.5 * _rssi + 240, 1);
        }

        public Hop(string _call, byte _ssid, HopType _hopType)
        {
            call = _call;
            ssid = _ssid;
            hopType = _hopType;
        }

        public Hop(string _call, byte _ssid)
        {
            call = _call;
            ssid = _ssid;
        }
        
        public Hop(bool _internet)
        {
            call = "";
            internet = _internet;
        }

        public Hop()
        {
            call = "";
        }

        public float GetRSSI()
        {
            if(hopType == HopType.Internet || hopType == HopType.Future)
            {
                return 0;
            }
            else
            {
                return (rssi - 240.0f) / 1.5f;
            }
        }
    }
}