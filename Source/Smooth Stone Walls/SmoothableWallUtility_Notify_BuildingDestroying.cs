using HarmonyLib;
using RimWorld;

namespace RSSW_Code
{
    [HarmonyPatch(typeof(SmoothableWallUtility), "Notify_BuildingDestroying", null)]
    public static class SmoothableWallUtility_Notify_BuildingDestroying
    {
        public static bool Prefix()
        {
            return false;
        }
    }
}