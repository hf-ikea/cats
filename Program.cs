using CATS;

NodeInfo i = new();
List<Variable> list = new()
{
    new Variable(VariableType.TrancieverTemp, -8),
    new Variable(VariableType.Uptime, uint.MaxValue),
    new Variable(VariableType.AntennaGain, 4),
    new Variable(VariableType.SWID, 68),
    new Variable(VariableType.HWID, 7888),
    new Variable(VariableType.AntennaHeight, 8),
    new Variable(VariableType.Voltage, 4)
};

i.GetBitmap(list);
i.PushVariableList(list);

foreach(Variable v in i.GetVariableList())
{
    Console.WriteLine(v.type.ToString());
    Console.WriteLine(v.value);
}