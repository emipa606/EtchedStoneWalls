using HarmonyLib;
using Verse;
using Verse.AI;

namespace RSSW_Code;

[HarmonyPatch(typeof(TouchPathEndModeUtility), nameof(TouchPathEndModeUtility.IsCornerTouchAllowed_NewTemp))]
public static class TouchPathEndModeUtility_IsCornerTouchAllowed
{
    public static bool Prefix(IntVec3 adjCardinalX, IntVec3 adjCardinalZ, PathingContext pc, Thing target,
        ref bool __result)
    {
        if (target != null && hasCornerWorkDesignation(pc, target.Position))
        {
            __result = true;
            return false;
        }

        var impliedCornerA = new IntVec3(adjCardinalX.x, 0, adjCardinalZ.z);
        if (hasCornerWorkDesignation(pc, impliedCornerA))
        {
            __result = true;
            return false;
        }

        var impliedCornerB = new IntVec3(adjCardinalZ.x, 0, adjCardinalX.z);
        if (!hasCornerWorkDesignation(pc, impliedCornerB))
        {
            return true;
        }

        __result = true;
        return false;
    }

    private static bool hasCornerWorkDesignation(PathingContext pc, IntVec3 cell)
    {
        var designationManager = pc.map.designationManager;
        return designationManager.DesignationAt(cell, DesignationDefOf.SmoothWall) != null
               || designationManager.DesignationAt(cell, DesignationDefOf.EtchWall) != null
               || designationManager.DesignationAt(cell, DesignationDefOf.EtchWallDecorative) != null;
    }
}