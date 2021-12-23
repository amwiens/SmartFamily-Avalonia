using System.Diagnostics;

namespace SmartFamily.Model
{
    public static class SmartFamilySounds
    {
        private static bool _canPlaySounds;

        public static bool CanPlaySounds
        {
            get
            {
                return _canPlaySounds;
            }

            set
            {
                _canPlaySounds = value;
                Debug.WriteLine($"SmartFamilySounds.CanPlaySounds: value set to '{_canPlaySounds}'.");
            }
        }

        public static void PlayNewOrder()
        {
            if (!CanPlaySounds) return;
            //SystemSounds.Hand.Play();
        }
    }
}