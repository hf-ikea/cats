namespace CATS
{
    public class NodeInfo
    {
        public byte[] bitmap = new byte[3];
        public List<byte> variableBytes = new List<byte>(254);
        public byte[] encoded = new byte[1];
        public NodeInfo()
        {

        }

        public uint GetBitmap(List<Variable> list)
        {
            uint bits = 0;
            foreach(Variable v in list)
            {
                if((uint)v.type > 23) throw new Exception("Type is not valid");
                bits |= (uint)(1 << (int)v.type);
            }
            bitmap = BitConverter.GetBytes(bits);
            return bits; 
        }

        public uint GetBitmap(Variable v)
        {
            if((uint)v.type > 23) throw new Exception("Type is not valid");
            uint bits = BitConverter.ToUInt32(bitmap);
            bits |= (uint)(1 << (int)v.type);
            bitmap = BitConverter.GetBytes(bits);
            return bits; 
        }

        private void PushUVariable(Variable v)
        {
            uint x = (uint)v.value;
            switch(v.type)
            {
                case VariableType.HWID:
                    if(x > UInt16.MaxValue) throw new Exception("Value too large");
                    variableBytes.AddRange(BitConverter.GetBytes(x));
                    break;
                case VariableType.SWID:
                    if(x > byte.MaxValue) throw new Exception("Value too large");
                    variableBytes.Add((byte)x);
                    break;
                case VariableType.Uptime:
                    if(x > UInt32.MaxValue) throw new Exception("Value too large");
                    variableBytes.AddRange(BitConverter.GetBytes(x));
                    break;
                case VariableType.AntennaHeight:
                    if(x > byte.MaxValue) throw new Exception("Value too large");
                    variableBytes.Add((byte)x);
                    break;
                case VariableType.AntennaGain:
                    x *= 4;
                    if(x > byte.MaxValue) throw new Exception("Value too large");
                    variableBytes.Add((byte)x);
                    break;
                case VariableType.TXPower:
                    x *= 4;
                    if(x > byte.MaxValue) throw new Exception("Value too large");
                    variableBytes.Add((byte)x);
                    break;
                case VariableType.Voltage:
                    x *= 10;
                    if(x > byte.MaxValue) throw new Exception("Value too large");
                    variableBytes.Add((byte)x);
                    break;
                case VariableType.BatteryCharge:
                    x *= 4;
                    if(x > byte.MaxValue) throw new Exception("Value too large");
                    variableBytes.Add((byte)x);
                    break;
                default:
                    throw new Exception("Variable not implemented");
            }
        }

        // do not use directly to avoid pushing variables in the incorrect order
        // still exists to push one (1) single variable
        public void PushVariable(Variable v)
        {
            if(v.value < 0)
            {
                if(v.type == VariableType.TrancieverTemp)
                {
                    variableBytes.Add((byte)v.value);
                }
                else
                {
                    throw new Exception("Variable selected cannot have a negative value");
                }
            }
            else
            {
                PushUVariable(v);
            }
        }

        public void PushVariableList(List<Variable> list)
        {
            list.Sort((a, b) => a.type.CompareTo(b.type));
            foreach(Variable v in list)
            {
                PushVariable(v);
            }
        }

        public byte[] Encode()
        {
            encoded = new byte[5 + variableBytes.Count];
            encoded[0] = 9;

            return encoded;
        }
    }
}