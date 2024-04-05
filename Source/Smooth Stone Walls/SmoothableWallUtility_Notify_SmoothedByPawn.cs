using HarmonyLib;
using RimWorld;

namespace RSSW_Code;

[HarmonyPatch(typeof(SmoothableWallUtility), nameof(SmoothableWallUtility.Notify_SmoothedByPawn), null)]
public static class SmoothableWallUtility_Notify_SmoothedByPawn
{
    public static bool Prefix()
    {
        return false;
    }
}