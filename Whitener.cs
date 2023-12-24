namespace CATS
{
    public class Whitener
    {
        public static byte[] Whiten(byte[] data)
        {
            ushort start_state = 59855; // start state of 0xE9CF
            ushort state = start_state;

            for(int i = 0; i < data.Length; i++)        // iterate through all the input data bytes
            {
                byte output = 0;
                for(int j = 8; j--> 0;)                 // repeat 8 times, counting j down from 7 to 0
                {
                    output |= (byte)((state & 1) << j); // set each bit of the output to the LSB of the output state
                    state = AdvanceLFSR(state);         // advance the LFSR 1 time
                }
                data[i] ^= output;                      // xor the data with the output byte
            }
            
            return data;
        }

        // https://en.wikipedia.org/wiki/Linear-feedback_shift_register#Galois_LFSRs
        public static ushort AdvanceLFSR(ushort state)
        {
            ushort lsb = (ushort)(state & 1);  // get least significant bit of state
            state >>= 1;                       // shift the register one time to the right
            if(lsb > 0) { state ^= 0xB400; }   // toggle taps at 16, 14, 13, 11 if the LSB is one
            return state;
        }
    }
}
