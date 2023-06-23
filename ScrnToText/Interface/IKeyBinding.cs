using System.Windows.Input;

namespace ScrnToText.Interface
{
    public interface IKeyBinding
    {
        public Key FirstKey { get; set; }
        public Key? SecondKey { get; set; }
        public void SetKeys(Key firstKey, Key? secondKey);
        public (Key, Key?) GetKey();
        public (Key, Key?) LoadKeys();
    }
}
