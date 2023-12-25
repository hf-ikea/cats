using CATS;

List<Whisker> list = new()
{
    new Whisker(WhiskerType.Identification, new Identification("N0CALL", 1, 1).Encode()),
    new Whisker(WhiskerType.Timestamp, new Timestamp(1703470519).Encode()),
    new Whisker(WhiskerType.Comment, new Comment("sawyer").Encode())
};



byte[] bytes = Packet.SemiEncode(list);

byte[] packet = Packet.AddHeader(bytes);

foreach(Whisker w in Packet.SemiDecode(bytes))
{
    Console.WriteLine(w.type.ToString());
}

Console.WriteLine(BitConverter.ToString(packet));