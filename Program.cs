using CATS;

NodeInfo i = new NodeInfo();
List<Variable> list = new();


list.Add(new Variable(VariableType.TrancieverTemp, -4));
list.Add(new Variable(VariableType.Uptime, 2));
list.Add(new Variable(VariableType.AntennaGain, 4));
list.Add(new Variable(VariableType.SWID, 1));
list.Add(new Variable(VariableType.HWID, 0));
list.Add(new Variable(VariableType.AntennaHeight, 3));

Console.WriteLine(Convert.ToString(i.GetBitmap(list), 2));