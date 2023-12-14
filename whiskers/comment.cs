namespace CATS
{
    public class Comment
    {
        public string comment;
        public byte[] encoded = new byte[1] { 0 };
        public Comment(string _comment)
        {
            if(_comment.Length > 255)
            {
                comment = _comment.Substring(0, 255);
            } else
            {
                comment = _comment;
            }
        }

        public Comment()
        {
            comment = "";
        }

        public byte[] Encode()
        {
            encoded = new byte[2 + comment.Length];
            encoded[0] = 3;
            encoded[1] = (byte)comment.Length;
            Array.Copy(System.Text.Encoding.UTF8.GetBytes(comment), 0, encoded, 2, comment.Length);
            return encoded;
        }

        public Comment Decode(byte[] data)
        {
            byte[] temp = new byte[data[1]];
            Array.Copy(data, 2, temp, 0, data[1]);
            comment = System.Text.Encoding.UTF8.GetString(temp);
            return this;
        }
    }
}