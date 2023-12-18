using CATS;

NodeInfo i = new();
List<Variable> list = new()
{
    new Variable(VariableType.TrancieverTemp, -4),
    new Variable(VariableType.Uptime, 2),
    new Variable(VariableType.AntennaGain, 4),
    new Variable(VariableType.SWID, 1),
    new Variable(VariableType.HWID, 0),
    new Variable(VariableType.AntennaHeight, 3),
    new Variable(VariableType.Voltage, 3)
};
i.GetBitmap(list);

foreach(Variable v in i.GetVariableList())
{
    Console.WriteLine(v.type.ToString());
}