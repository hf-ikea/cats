namespace CATS
{
    public class Interleaver
    {
        public byte[] Interleave(byte[] data)
        {
            byte[] interleaved = new byte[data.Length];

            int k = 0;
            
            for(int i = 0; i < 32; i++)
            {
                for(int j = 0; j < data.Length; j += 32)
                {
                    if(i + j < data.Length)
                    {
                        interleaved[k] = data[i + j];
                    }

                    k++;
                }
            }

            return interleaved;
        }

        public byte[] Deinterleave(byte[] data)
        {
            byte[] deinterleaved = new byte[data.Length];

            int k = 0;
            
            for(int i = 0; i < 32; i++)
            {
                for(int j = 0; j < data.Length; j += 32)
                {
                    if(i + j < data.Length)
                    {
                        deinterleaved[i + j] = data[k];
                    }

                    k++;
                }
            }

            return deinterleaved;
        }
    }
}