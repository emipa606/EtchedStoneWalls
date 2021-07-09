using HarmonyLib;
using RimWorld;
using Verse;

namespace RSSW_Code
{
    [HarmonyPatch(typeof(Designator_SmoothSurface), "CanDesignateCell", null)]
    public static class Designator_SmoothSurface_CanDesignateCell
    {
        public static bool Prefix(IntVec3 c, ref AcceptanceReport __result, ref Designator_SmoothSurface __instance)
        {
            var edifice = c.GetEdifice(__instance.Map);
            if (edifice == null || !edifice.def.IsSmoothable)
            {
                return true;
            }

            if (edifice.def.building == null || !edifice.def.building.smoothedThing.defName.Contains("Etched"))
            {
                return true;
            }

            __result = false;
            return false;
        }
    }
}