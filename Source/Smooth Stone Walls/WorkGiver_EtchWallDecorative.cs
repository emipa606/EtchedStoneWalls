using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using Verse;
using Verse.AI;

namespace RSSW_Code
{
    public class WorkGiver_EtchWallDecorative : WorkGiver_Scanner
    {
        public override PathEndMode PathEndMode => PathEndMode.Touch;

        [DebuggerHidden]
        public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
        {
            if (pawn.Faction != Faction.OfPlayer)
            {
                yield break;
            }

            foreach (var des in pawn.Map.designationManager.SpawnedDesignationsOfDef(DesignationDefOf
                .EtchWallDecorative))
            {
                yield return des.target.Cell;
            }
        }

        public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            if (c.IsForbidden(pawn) ||
                pawn.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWallDecorative) == null)
            {
                return false;
            }

            var edifice = c.GetEdifice(pawn.Map);
            if (edifice == null || !edifice.def.IsSmoothable)
            {
                Log.ErrorOnce("Failed to find valid edifice when trying to smooth a wall", 58988176);
                pawn.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.EtchWallDecorative);
                return false;
            }

            LocalTargetInfo target = edifice;
            if (!pawn.CanReserve(target, 1, -1, null, forced))
            {
                return false;
            }

            target = c;
            if (pawn.CanReserve(target, 1, -1, null, forced))
            {
                return true;
            }

            return false;
        }

        public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return new Job(JobDefOf.EtchWallDecorative, c.GetEdifice(pawn.Map));
        }
    }
}