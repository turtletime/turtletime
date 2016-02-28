namespace CheloniiUnity
{
    struct GameModuleKey
    {
        public int UnderlyingValue { get; private set; }

        public GameModuleKey(int underLyingValue)
        {
            UnderlyingValue = underLyingValue;
        }
    }
}
