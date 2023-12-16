using CATS;

NodeInfo i = new NodeInfo();
i.PushVariable(new Variable(VariableType.TrancieverTemp, -4));

List<Variable> list = new();

list.Add(new Variable(VariableType.Uptime, 2));
list.Add(new Variable(VariableType.AntennaGain, 4));
list.Add(new Variable(VariableType.SWID, 1));
list.Add(new Variable(VariableType.HWID, 0));
list.Add(new Variable(VariableType.AntennaHeight, 3));



foreach(Variable v in list)
{
    Console.WriteLine(v.type.ToString());
}


list.Sort((a, b) => a.type.CompareTo(b.type));
foreach(Variable v in list)
{
    Console.WriteLine(v.type.ToString());
}