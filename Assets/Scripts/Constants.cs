namespace Signal
{
    public class Constants
    {
        public enum SoundType
        {
            SignalCollision
        }

        public enum WallType
        {
            Default,
            Speeder,
            Slowner,
            Splitter,
            SignalBroker,
            SignalPlusser
        }

        public enum LevelState
        {
            Load,
            Retry,
            Next
        }
    }
}