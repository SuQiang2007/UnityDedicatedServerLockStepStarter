namespace Client.ClientUtils
{
    public class FrameId
    {
        private static int _frameId;
        public static int NowId => _frameId;
        public static int NextId => ++_frameId;
    }
}
