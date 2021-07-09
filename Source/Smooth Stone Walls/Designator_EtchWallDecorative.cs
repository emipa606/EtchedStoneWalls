using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace RSSW_Code
{
    public class Designator_EtchWallDecorative : Designator
    {
        public Designator_EtchWallDecorative()
        {
            defaultLabel = "RSSW.DesignatorEtchWallDecorative".Translate();
            defaultDesc = "RSSW.DesignatorEtchWallDecorativeDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("EtchWall");
            useMouseIcon = true;
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            soundSucceeded = SoundDefOf.Designate_SmoothSurface;
            hotKey = KeyBindingDefOf.Misc1;
        }

        public override int DraggableDimensions => 2;
        public override bool DragDrawMeasurements => true;

        public override AcceptanceReport CanDesignateThing(Thing t)
        {
            if (t != null && t.def.IsSmoothable && CanDesignateCell(t.Position).Accepted)
            {
                return AcceptanceReport.WasAccepted;
            }

            return false;
        }

        public override void DesignateThing(Thing t)
        {
            DesignateSingleCell(t.Position);
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(Map))
            {
                return false;
            }

            if (c.Fogged(Map))
            {
                return false;
            }

            if (Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothFloor) != null ||
                Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothWall) != null)
            {
                return "SurfaceBeingSmoothed".Translate();
            }

            if (Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWall) != null ||
                Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWallDecorative) != null)
            {
                return "RSSW.TerrainBeingEtched".Translate();
            }

            if (c.InNoBuildEdgeArea(Map))
            {
                return "TooCloseToMapEdge".Translate();
            }

            var edifice = c.GetEdifice(Map);
            if (edifice == null || !edifice.def.IsSmoothable)
            {
                return "RSSW.MessageMustDesignateDecoratable".Translate();
            }

            if (edifice.def.building.smoothedThing.defName.Contains("Etched") &&
                !edifice.def.defName.Contains("XX"))
            {
                return AcceptanceReport.WasAccepted;
            }

            return "RSSW.MessageMustDesignateDecoratable".Translate();
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            Map.designationManager.AddDesignation(new Designation(c, DesignationDefOf.EtchWallDecorative));
            Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.Mine);
        }

        public override void SelectedUpdate()
        {
            GenUI.RenderMouseoverBracket();
        }

        public override void RenderHighlight(List<IntVec3> dragCells)
        {
            DesignatorUtility.RenderHighlightOverSelectableCells(this, dragCells);
        }
    }
}