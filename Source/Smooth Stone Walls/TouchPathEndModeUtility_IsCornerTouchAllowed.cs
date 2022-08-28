using HarmonyLib;
using Verse;
using Verse.AI;

namespace RSSW_Code;

[HarmonyPatch(typeof(TouchPathEndModeUtility), "IsCornerTouchAllowed")]
public static class TouchPathEndModeUtility_IsCornerTouchAllowed
{
    public static bool Prefix(int cornerX, int cornerZ, PathingContext pc, ref bool __result)
    {
        var cell = new IntVec3(cornerX, 0, cornerZ);
        if (pc.map.designationManager.DesignationAt(cell, DesignationDefOf.SmoothWall) == null)
        {
            return true;
        }

        __result = true;
        return false;
    }
}