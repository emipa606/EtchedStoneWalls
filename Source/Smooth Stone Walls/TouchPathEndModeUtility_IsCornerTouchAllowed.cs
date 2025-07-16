using HarmonyLib;
using Verse;
using Verse.AI;

namespace RSSW_Code;

[HarmonyPatch(typeof(TouchPathEndModeUtility), nameof(TouchPathEndModeUtility.IsCornerTouchAllowed))]
public static class TouchPathEndModeUtility_IsCornerTouchAllowed
{
    public static bool Prefix(IntVec3 corner, PathingContext pc, ref bool __result)
    {
        if (pc.map.designationManager.DesignationAt(corner, DesignationDefOf.SmoothWall) == null)
        {
            return true;
        }

        __result = true;
        return false;
    }
}