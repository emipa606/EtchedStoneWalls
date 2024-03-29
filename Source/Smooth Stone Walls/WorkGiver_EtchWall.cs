﻿using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using Verse;
using Verse.AI;

namespace RSSW_Code;

public class WorkGiver_EtchWall : WorkGiver_Scanner
{
    public override PathEndMode PathEndMode => PathEndMode.Touch;

    [DebuggerHidden]
    public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
    {
        if (pawn.Faction != Faction.OfPlayer)
        {
            yield break;
        }

        foreach (var des in pawn.Map.designationManager.SpawnedDesignationsOfDef(DesignationDefOf.EtchWall))
        {
            yield return des.target.Cell;
        }
    }

    public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
    {
        if (c.IsForbidden(pawn) || pawn.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWall) == null)
        {
            return false;
        }

        var edifice = c.GetEdifice(pawn.Map);
        if (edifice == null || !edifice.def.IsSmoothable)
        {
            Log.ErrorOnce("Failed to find valid edifice when trying to smooth a wall", 58988176);
            pawn.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.EtchWall);
            return false;
        }

        LocalTargetInfo target = edifice;
        if (!pawn.CanReserve(target, 1, -1, null, forced))
        {
            return false;
        }

        target = c;
        return pawn.CanReserve(target, 1, -1, null, forced);
    }

    public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
    {
        return new Job(JobDefOf.EtchWall, c.GetEdifice(pawn.Map));
    }
}