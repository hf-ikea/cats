namespace CATS
{
    public class ToneInfo
    {
        public bool uplinkDCS;
        public bool downlinkDCS;
        public uint uplinkCode;
        public uint downlinkCode;
        public ToneInfo(bool _uplinkDCS, bool _downlinkDCS, uint _uplinkCode, uint _downlinkCode)
        {
            uplinkDCS = _uplinkDCS;
            downlinkDCS = _downlinkDCS;
            uplinkCode = _uplinkCode;
            downlinkCode = _downlinkCode;
        }

        public ToneInfo()
        {
            
        }
    }
}