namespace CATS
{
    public enum VariableType
    {
        HWID,
        SWID,
        Uptime,
        AntennaHeight,
        AntennaGain,
        TXPower,
        Voltage,
        TrancieverTemp,
        BatteryCharge
    }

    public class Variable
    {
        public VariableType type;
        public long value;

        public Variable(VariableType _v, long _x)
        {
            type = _v;
            value = _x;
        }
    }
}