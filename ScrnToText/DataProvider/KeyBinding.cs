using ScrnToText.Interface;
using System.Windows.Input;

namespace ScrnToText.DataProvider
{
    public class KeyBinding : IKeyBinding
    {
        public KeyBinding() 
        {
            LoadKeys();
        }

        public Key FirstKey { get; set; }
        public Key? SecondKey { get; set; }

        public (Key, Key?) GetKey()
        {
            return (FirstKey, SecondKey);
        }

        public (Key, Key?) LoadKeys()
        {
            // Get data from files
            return default;
        }

        public void SetKeys(Key firstKey, Key? secondKey)
        {
            FirstKey = firstKey;
            SecondKey = secondKey;
        }
    }
}
