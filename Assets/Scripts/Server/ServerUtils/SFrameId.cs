namespace Server.ServerUtils
{
    public class SFrameId
    {
        private static int _frameId;
        public static int NowId => _frameId;

        public static void IncreaseId()
        {
            _frameId++;
        }
    }
}