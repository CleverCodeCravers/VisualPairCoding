namespace VisualPairCoding.BL.AutoUpdates
{
    public static class AutoUpdateDetector
    {
        private static bool explicitUpdate = false;

        public static void setUpdateIsAvailable()
        {
            explicitUpdate = true;
        }
        public static bool isUpdateAvailable()
        {
            if (explicitUpdate) return true;
            return VersionInformation.Version != "$$VERSION$$";
        }
    }
}
