using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;
using Verse.AI;

namespace RSSW_Code
{

    [StaticConstructorOnStartup]
    internal static class RSSW_Initializer
    {

        private static ThingDef GenerateBaseStructure(ThingDef originalStructure)
        {
            var newStructure = new ThingDef
            {
                altitudeLayer = originalStructure.altitudeLayer,
                blockLight = originalStructure.blockLight,
                blockWind = originalStructure.blockWind,
                building = originalStructure.building,
                castEdgeShadows = originalStructure.castEdgeShadows,
                category = originalStructure.category,
                coversFloor = originalStructure.coversFloor,
                drawerType = originalStructure.drawerType,
                fillPercent = originalStructure.fillPercent,
                filthLeaving = originalStructure.filthLeaving,
                graphicData = new GraphicData()
            };
            newStructure.graphicData.color = originalStructure.graphicData.color;
            newStructure.graphicData.linkFlags = originalStructure.graphicData.linkFlags;
            newStructure.graphicData.linkType = originalStructure.graphicData.linkType;
            newStructure.graphicData.graphicClass = typeof(Graphic_Single);
            newStructure.holdsRoof = originalStructure.holdsRoof;
            newStructure.mineable = originalStructure.mineable;
            newStructure.modContentPack = originalStructure.modContentPack;
            newStructure.neverMultiSelect = originalStructure.neverMultiSelect;
            newStructure.passability = originalStructure.passability;
            newStructure.repairEffect = originalStructure.repairEffect;
            newStructure.rotatable = originalStructure.rotatable;
            newStructure.saveCompressible = originalStructure.saveCompressible;
            newStructure.scatterableOnMapGen = originalStructure.scatterableOnMapGen;
            newStructure.selectable = originalStructure.selectable;
            newStructure.statBases = originalStructure.statBases;
            newStructure.staticSunShadowHeight = originalStructure.staticSunShadowHeight;
            newStructure.thingClass = originalStructure.thingClass;
            newStructure.comps = DefDatabase<ThingDef>.GetNamedSilentFail("Wall").comps;
            newStructure.statBases.Add(new StatModifier { stat = StatDefOf.MeditationFocusStrength, value = 0.22f });
            return newStructure;
        }

        static RSSW_Initializer()
        {
            var harmony = new Harmony("net.rainbeau.rimworld.mod.smoothstone");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            var usingNPS = false;
            if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("Nature's Pretty Sweet")))
            {
                usingNPS = true;
            }
            List<ThingDef> allThings = DefDatabase<ThingDef>.AllDefsListForReading;
            for (var i = 0; i < allThings.Count; i++)
            {
                if (allThings[i].defName == "SmoothedLavaRock" && usingNPS.Equals(false)) { }
                else if (allThings[i].defName.Contains("Smoothed") && !allThings[i].defName.Contains("Etched") && allThings[i].category == ThingCategory.Building && allThings[i].thingClass == typeof(Mineable))
                {
                    if (allThings[i].defName == "SmoothedLavaRock")
                    {
                        allThings[i].building.mineableThing = ThingDef.Named("TKKN_ChunkLava");
                        allThings[i].building.mineableDropChance = 0.25f;
                    }
                    //
                    ThingDef newEtchedStone = GenerateBaseStructure(allThings[i]);
                    newEtchedStone.defName = "Etched" + allThings[i].defName;
                    newEtchedStone.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label;
                    newEtchedStone.description = allThings[i].description + "RSSW.EtchedWall".Translate();
                    newEtchedStone.graphicData.texPath = "Walls/Wall_Atlas_Basic";
                    DefGenerator.AddImpliedDef(newEtchedStone);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStone, typeof(ThingDef) });
                    //
                    var newEtchedStoneAtom = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneAtom.defName = "Etched" + allThings[i].defName + "XXAtom";
                    newEtchedStoneAtom.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneAtom.description = "RSSW.EtchedWallAtom".Translate();
                    newEtchedStoneAtom.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Atom";
                    DefGenerator.AddImpliedDef(newEtchedStoneAtom);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneAtom, typeof(ThingDef) });
                    //
                    var newEtchedStoneBeer = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneBeer.defName = "Etched" + allThings[i].defName + "XXBeer";
                    newEtchedStoneBeer.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneBeer.description = "RSSW.EtchedWallBeer".Translate();
                    newEtchedStoneBeer.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Beer";
                    DefGenerator.AddImpliedDef(newEtchedStoneBeer);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneBeer, typeof(ThingDef) });
                    //
                    var newEtchedStoneCheese = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneCheese.defName = "Etched" + allThings[i].defName + "XXCheese";
                    newEtchedStoneCheese.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneCheese.description = "RSSW.EtchedWallCheese".Translate();
                    newEtchedStoneCheese.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Cheese";
                    DefGenerator.AddImpliedDef(newEtchedStoneCheese);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneCheese, typeof(ThingDef) });
                    //
                    var newEtchedStoneDoor = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneDoor.defName = "Etched" + allThings[i].defName + "XXDoor";
                    newEtchedStoneDoor.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneDoor.description = "RSSW.EtchedWallDoor".Translate();
                    newEtchedStoneDoor.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Door";
                    DefGenerator.AddImpliedDef(newEtchedStoneDoor);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneDoor, typeof(ThingDef) });
                    //
                    var newEtchedStoneShovel = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneShovel.defName = "Etched" + allThings[i].defName + "XXShovel";
                    newEtchedStoneShovel.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneShovel.description = "RSSW.EtchedWallShovel".Translate();
                    newEtchedStoneShovel.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Shovel";
                    DefGenerator.AddImpliedDef(newEtchedStoneShovel);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneShovel, typeof(ThingDef) });
                    //
                    var newEtchedStoneBoomalope = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneBoomalope.defName = "Etched" + allThings[i].defName + "XXBoomalope";
                    newEtchedStoneBoomalope.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneBoomalope.description = "RSSW.EtchedWallBoomalope".Translate();
                    newEtchedStoneBoomalope.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Boomalope";
                    DefGenerator.AddImpliedDef(newEtchedStoneBoomalope);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneBoomalope, typeof(ThingDef) });
                    //
                    var newEtchedStoneForest = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneForest.defName = "Etched" + allThings[i].defName + "XXForest";
                    newEtchedStoneForest.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneForest.description = "RSSW.EtchedWallForest".Translate();
                    newEtchedStoneForest.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Forest";
                    DefGenerator.AddImpliedDef(newEtchedStoneForest);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneForest, typeof(ThingDef) });
                    //
                    var newEtchedStoneHouse = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneHouse.defName = "Etched" + allThings[i].defName + "XXHouse";
                    newEtchedStoneHouse.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneHouse.description = "RSSW.EtchedWallHouse".Translate();
                    newEtchedStoneHouse.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_House";
                    DefGenerator.AddImpliedDef(newEtchedStoneHouse);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneHouse, typeof(ThingDef) });
                    //
                    var newEtchedStonePlanet = GenerateBaseStructure(allThings[i]);
                    newEtchedStonePlanet.defName = "Etched" + allThings[i].defName + "XXPlanet";
                    newEtchedStonePlanet.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStonePlanet.description = "RSSW.EtchedWallPlanet".Translate();
                    newEtchedStonePlanet.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Planet";
                    DefGenerator.AddImpliedDef(newEtchedStonePlanet);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStonePlanet, typeof(ThingDef) });
                    //
                    var newEtchedStoneRocket = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneRocket.defName = "Etched" + allThings[i].defName + "XXRocket";
                    newEtchedStoneRocket.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneRocket.description = "RSSW.EtchedWallRocket".Translate();
                    newEtchedStoneRocket.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Rocket";
                    DefGenerator.AddImpliedDef(newEtchedStoneRocket);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneRocket, typeof(ThingDef) });
                    //
                    var newEtchedStoneGerbils = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneGerbils.defName = "Etched" + allThings[i].defName + "XXGerbils";
                    newEtchedStoneGerbils.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneGerbils.description = "RSSW.EtchedWallGerbils".Translate();
                    newEtchedStoneGerbils.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Gerbils";
                    DefGenerator.AddImpliedDef(newEtchedStoneGerbils);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneGerbils, typeof(ThingDef) });
                    //
                    var newEtchedStoneIsland = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneIsland.defName = "Etched" + allThings[i].defName + "XXIsland";
                    newEtchedStoneIsland.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneIsland.description = "RSSW.EtchedWallIsland".Translate();
                    newEtchedStoneIsland.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Island";
                    DefGenerator.AddImpliedDef(newEtchedStoneIsland);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneIsland, typeof(ThingDef) });
                    //
                    var newEtchedStoneMan = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneMan.defName = "Etched" + allThings[i].defName + "XXMan";
                    newEtchedStoneMan.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneMan.description = "RSSW.EtchedWallMan".Translate();
                    newEtchedStoneMan.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Man";
                    DefGenerator.AddImpliedDef(newEtchedStoneMan);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneMan, typeof(ThingDef) });
                    //
                    var newEtchedStoneRose = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneRose.defName = "Etched" + allThings[i].defName + "XXRose";
                    newEtchedStoneRose.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneRose.description = "RSSW.EtchedWallRose".Translate();
                    newEtchedStoneRose.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Rose";
                    DefGenerator.AddImpliedDef(newEtchedStoneRose);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneRose, typeof(ThingDef) });
                    //
                    var newEtchedStoneWoman = GenerateBaseStructure(allThings[i]);
                    newEtchedStoneWoman.defName = "Etched" + allThings[i].defName + "XXWoman";
                    newEtchedStoneWoman.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
                    newEtchedStoneWoman.description = "RSSW.EtchedWallWoman".Translate();
                    newEtchedStoneWoman.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Woman";
                    DefGenerator.AddImpliedDef(newEtchedStoneWoman);
                    AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneWoman, typeof(ThingDef) });
                    //
                    allThings[i].building.smoothedThing = newEtchedStone;
                }
            }
        }
    }

    [DefOf]
    public static class DesignationDefOf
    {
        public static DesignationDef EtchWall;
        public static DesignationDef EtchWallDecorative;
        public static DesignationDef Mine;
        public static DesignationDef SmoothFloor;
        public static DesignationDef SmoothWall;
    }

    [DefOf]
    public static class JobDefOf
    {
        public static JobDef EtchWall;
        public static JobDef EtchWallDecorative;
    }

    [HarmonyPatch(typeof(Designator_SmoothSurface), "CanDesignateCell", null)]
    public static class Designator_SmoothSurface_CanDesignateCell
    {
        public static bool Prefix(IntVec3 c, ref AcceptanceReport __result, ref Designator_SmoothSurface __instance)
        {
            Building edifice = c.GetEdifice(__instance.Map);
            if (edifice != null && edifice.def.IsSmoothable)
            {
                if (edifice.def.building != null && edifice.def.building.smoothedThing.defName.Contains("Etched"))
                {
                    __result = false;
                    return false;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(ReverseDesignatorDatabase), "InitDesignators", null)]
    public static class ReverseDesignatorDatabase_InitDesignators
    {
        public static bool Prefix(ref List<Designator> ___desList)
        {
            ___desList = new List<Designator>() {
                new Designator_Cancel(),
                new Designator_Claim(),
                new Designator_Deconstruct(),
                new Designator_Uninstall(),
                new Designator_Haul(),
                new Designator_Hunt(),
                new Designator_Slaughter(),
                new Designator_Tame(),
                new Designator_PlantsCut(),
                new Designator_PlantsHarvest(),
                new Designator_PlantsHarvestWood(),
                new Designator_Mine(),
                new Designator_Strip(),
                new Designator_Open(),
                new Designator_SmoothSurface(),
                new Designator_EtchWall(),
                new Designator_EtchWallDecorative()
            };
            ___desList.RemoveAll((Designator des) => !Current.Game.Rules.DesignatorAllowed(des));
            return false;
        }
    }

    [HarmonyPatch(typeof(SmoothableWallUtility), "Notify_BuildingDestroying", null)]
    public static class SmoothableWallUtility_Notify_BuildingDestroying
    {
        public static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(SmoothableWallUtility), "Notify_SmoothedByPawn", null)]
    public static class SmoothableWallUtility_Notify_SmoothedByPawn
    {
        public static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(TouchPathEndModeUtility), "IsCornerTouchAllowed", null)]
    public static class TouchPathEndModeUtility_IsCornerTouchAllowed
    {
        public static bool Prefix(int cornerX, int cornerZ, Map map, ref bool __result)
        {
            var cell = new IntVec3(cornerX, 0, cornerZ);
            if (map.designationManager.DesignationAt(cell, DesignationDefOf.SmoothWall) != null)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }

    public class Designator_EtchWall : Designator
    {
        public override int DraggableDimensions => 2;
        public override bool DragDrawMeasurements => true;
        public Designator_EtchWall()
        {
            defaultLabel = "RSSW.DesignatorEtchWall".Translate();
            defaultDesc = "RSSW.DesignatorEtchWallDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("EtchWall", true);
            useMouseIcon = true;
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            soundSucceeded = SoundDefOf.Designate_SmoothSurface;
            hotKey = KeyBindingDefOf.Misc1;
        }
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
            if (!c.InBounds(base.Map))
            {
                return false;
            }
            if (c.Fogged(base.Map))
            {
                return false;
            }
            if (base.Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothFloor) != null || base.Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothWall) != null)
            {
                return "SurfaceBeingSmoothed".Translate();
            }
            if (base.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWall) != null || base.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWallDecorative) != null)
            {
                return "RSSW.TerrainBeingEtched".Translate();
            }
            if (c.InNoBuildEdgeArea(base.Map))
            {
                return "TooCloseToMapEdge".Translate();
            }
            Building edifice = c.GetEdifice(base.Map);
            if (edifice != null && edifice.def.IsSmoothable)
            {
                if (edifice.def.building.smoothedThing.defName.Contains("Etched") && (!edifice.def.defName.Contains("Etched") || edifice.def.defName.Contains("XX")))
                {
                    return AcceptanceReport.WasAccepted;
                }
            }
            return "RSSW.MessageMustDesignateSmooth".Translate();
        }
        public override void DesignateSingleCell(IntVec3 c)
        {
            base.Map.designationManager.AddDesignation(new Designation(c, DesignationDefOf.EtchWall));
            base.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.Mine);
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

    public class Designator_EtchWallDecorative : Designator
    {
        public override int DraggableDimensions => 2;
        public override bool DragDrawMeasurements => true;
        public Designator_EtchWallDecorative()
        {
            defaultLabel = "RSSW.DesignatorEtchWallDecorative".Translate();
            defaultDesc = "RSSW.DesignatorEtchWallDecorativeDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("EtchWall", true);
            useMouseIcon = true;
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            soundSucceeded = SoundDefOf.Designate_SmoothSurface;
            hotKey = KeyBindingDefOf.Misc1;
        }
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
            if (!c.InBounds(base.Map))
            {
                return false;
            }
            if (c.Fogged(base.Map))
            {
                return false;
            }
            if (base.Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothFloor) != null || base.Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothWall) != null)
            {
                return "SurfaceBeingSmoothed".Translate();
            }
            if (base.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWall) != null || base.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWallDecorative) != null)
            {
                return "RSSW.TerrainBeingEtched".Translate();
            }
            if (c.InNoBuildEdgeArea(base.Map))
            {
                return "TooCloseToMapEdge".Translate();
            }
            Building edifice = c.GetEdifice(base.Map);
            if (edifice != null && edifice.def.IsSmoothable)
            {
                if (edifice.def.building.smoothedThing.defName.Contains("Etched") && !edifice.def.defName.Contains("XX"))
                {
                    return AcceptanceReport.WasAccepted;
                }
            }
            return "RSSW.MessageMustDesignateDecoratable".Translate();
        }
        public override void DesignateSingleCell(IntVec3 c)
        {
            base.Map.designationManager.AddDesignation(new Designation(c, DesignationDefOf.EtchWallDecorative));
            base.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.Mine);
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

    public class JobDriver_EtchWall : JobDriver
    {
        private float workLeft = -1000f;
        protected int BaseWorkAmount => 600;
        protected DesignationDef DesDef => DesignationDefOf.EtchWall;
        protected StatDef SpeedStat => StatDefOf.SmoothingSpeed;
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Pawn pawn = this.pawn;
            LocalTargetInfo target = this.job.targetA;
            Job job = this.job;
            bool arg_62_0;
            if (pawn.Reserve(target, job, 1, -1, null, errorOnFailed))
            {
                pawn = this.pawn;
                target = this.job.targetA.Cell;
                job = this.job;
                arg_62_0 = pawn.Reserve(target, job, 1, -1, null, errorOnFailed);
            }
            else
            {
                arg_62_0 = false;
            }
            return arg_62_0;
        }
        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOn(() => !job.ignoreDesignations && Map.designationManager.DesignationAt(TargetLocA, DesDef) == null);
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.Touch);
            var doWork = new Toil
            {
                initAction = delegate
                {
                    workLeft = (float)BaseWorkAmount;
                }
            };
            doWork.tickAction = delegate
            {
                var num = (SpeedStat == null) ? 1f : doWork.actor.GetStatValue(SpeedStat, true);
                workLeft -= num;
                if (doWork.actor.skills != null)
                {
                    doWork.actor.skills.Learn(SkillDefOf.Crafting, 0.1f, false);
                }
                if (workLeft <= 0f)
                {
                    DoEffect();
                    Designation designation = Map.designationManager.DesignationAt(TargetLocA, DesDef);
                    if (designation != null)
                    {
                        designation.Delete();
                    }
                    ReadyForNextToil();
                    return;
                }
            };
            doWork.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            doWork.WithProgressBar(TargetIndex.A, () => 1f - (workLeft / (float)BaseWorkAmount), false, -0.5f);
            doWork.defaultCompleteMode = ToilCompleteMode.Never;
            doWork.activeSkill = () => SkillDefOf.Crafting;
            yield return doWork;
        }
        protected void DoEffect()
        {
            SmoothableWallUtility.Notify_SmoothedByPawn(SmoothableWallUtility.SmoothWall(base.TargetA.Thing, pawn), pawn);
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref workLeft, "workLeft", 0f, false);
        }
    }

    public class JobDriver_EtchWallDecorative : JobDriver
    {
        private float workLeft = -1000f;
        protected int BaseWorkAmount => 1200;
        protected DesignationDef DesDef => DesignationDefOf.EtchWallDecorative;
        protected StatDef SpeedStat => StatDefOf.WorkSpeedGlobal;
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Pawn pawn = this.pawn;
            LocalTargetInfo target = this.job.targetA;
            Job job = this.job;
            bool arg_62_0;
            if (pawn.Reserve(target, job, 1, -1, null, errorOnFailed))
            {
                pawn = this.pawn;
                target = this.job.targetA.Cell;
                job = this.job;
                arg_62_0 = pawn.Reserve(target, job, 1, -1, null, errorOnFailed);
            }
            else
            {
                arg_62_0 = false;
            }
            return arg_62_0;
        }
        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOn(() => !job.ignoreDesignations && Map.designationManager.DesignationAt(TargetLocA, DesDef) == null);
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.Touch);
            var doWork = new Toil
            {
                initAction = delegate
                {
                    workLeft = (float)BaseWorkAmount;
                }
            };
            doWork.tickAction = delegate
            {
                var num = (SpeedStat == null) ? 1f : doWork.actor.GetStatValue(SpeedStat, true);
                workLeft -= num;
                if (doWork.actor.skills != null)
                {
                    doWork.actor.skills.Learn(SkillDefOf.Artistic, 0.1f, false);
                }
                if (workLeft <= 0f)
                {
                    DoEffect();
                    Designation designation = Map.designationManager.DesignationAt(TargetLocA, DesDef);
                    if (designation != null)
                    {
                        designation.Delete();
                    }
                    ReadyForNextToil();
                    return;
                }
            };
            doWork.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            doWork.WithProgressBar(TargetIndex.A, () => 1f - (workLeft / (float)BaseWorkAmount), false, -0.5f);
            doWork.defaultCompleteMode = ToilCompleteMode.Never;
            doWork.activeSkill = () => SkillDefOf.Artistic;
            yield return doWork;
        }
        protected void DoEffect()
        {
            var wall = base.TargetA.Thing.def.building.smoothedThing.defName;
            var rnd = new System.Random();
            var pictureChance = rnd.Next(20);
            var xpGain = 100f;
            if (pawn.skills != null)
            {
                pictureChance += pawn.skills.GetSkill(SkillDefOf.Artistic).Level;
            }
            if (pictureChance < 20) { }
            else
            {
                var pictureQuality = 1;
                var pictureQualityMod = Rand.Value;
                if (pawn.skills != null)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Artistic).Level < 7)
                    {
                        if (pictureQualityMod > 0.67) { pictureQuality = 2; }
                    }
                    else if (pawn.skills.GetSkill(SkillDefOf.Artistic).Level < 14)
                    {
                        if (pictureQualityMod > 0.67) { pictureQuality = 3; }
                        else if (pictureQualityMod > 0.33) { pictureQuality = 2; }
                    }
                    else
                    {
                        if (pictureQualityMod > 0.33) { pictureQuality = 3; }
                        else { pictureQuality = 2; }
                    }
                }
                var pictureChoice = rnd.Next(5);
                if (pictureQuality == 1)
                {
                    xpGain = 100f;
                    if (pictureChoice == 0) { wall += "XXAtom"; }
                    else if (pictureChoice == 1) { wall += "XXBeer"; }
                    else if (pictureChoice == 2) { wall += "XXCheese"; }
                    else if (pictureChoice == 3) { wall += "XXDoor"; }
                    else { wall += "XXShovel"; }
                }
                else if (pictureQuality == 2)
                {
                    xpGain = 200f;
                    if (pictureChoice == 0) { wall += "XXBoomalope"; }
                    else if (pictureChoice == 1) { wall += "XXForest"; }
                    else if (pictureChoice == 2) { wall += "XXHouse"; }
                    else if (pictureChoice == 3) { wall += "XXPlanet"; }
                    else { wall += "XXRocket"; }
                }
                else
                {
                    xpGain = 400f;
                    if (pictureChoice == 0) { wall += "XXGerbils"; }
                    else if (pictureChoice == 1) { wall += "XXIsland"; }
                    else if (pictureChoice == 2) { wall += "XXMan"; }
                    else if (pictureChoice == 3) { wall += "XXRose"; }
                    else { wall += "XXWoman"; }
                }
                pawn.skills.Learn(SkillDefOf.Artistic, xpGain, false);
            }
            SmoothableWallUtility.Notify_SmoothedByPawn(DecorateWall(base.TargetA.Thing, wall, pawn), pawn);
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref workLeft, "workLeft", 0f, false);
        }
        public static Thing DecorateWall(Thing target, string newWall, Pawn smoother)
        {
            Map map = target.Map;
            target.Destroy(DestroyMode.WillReplace);
            Thing thing = ThingMaker.MakeThing(ThingDef.Named(newWall), target.Stuff);
            thing.SetFaction(smoother.Faction, null);
            GenSpawn.Spawn(thing, target.Position, map, target.Rotation, WipeMode.Vanish, false);
            map.designationManager.TryRemoveDesignation(target.Position, DesignationDefOf.SmoothWall);
            return thing;
        }
    }

    public class WorkGiver_EtchWall : WorkGiver_Scanner
    {
        public override PathEndMode PathEndMode => PathEndMode.Touch;
        [DebuggerHidden]
        public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
        {
            if (pawn.Faction == Faction.OfPlayer)
            {
                foreach (Designation des in pawn.Map.designationManager.SpawnedDesignationsOfDef(DesignationDefOf.EtchWall))
                {
                    yield return des.target.Cell;
                }
            }
        }
        public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            if (c.IsForbidden(pawn) || pawn.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWall) == null)
            {
                return false;
            }
            Building edifice = c.GetEdifice(pawn.Map);
            if (edifice == null || !edifice.def.IsSmoothable)
            {
                Log.ErrorOnce("Failed to find valid edifice when trying to smooth a wall", 58988176, false);
                pawn.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.EtchWall);
                return false;
            }
            LocalTargetInfo target = edifice;
            if (pawn.CanReserve(target, 1, -1, null, forced))
            {
                target = c;
                if (pawn.CanReserve(target, 1, -1, null, forced))
                {
                    return true;
                }
            }
            return false;
        }
        public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return new Job(JobDefOf.EtchWall, c.GetEdifice(pawn.Map));
        }
    }

    public class WorkGiver_EtchWallDecorative : WorkGiver_Scanner
    {
        public override PathEndMode PathEndMode => PathEndMode.Touch;
        [DebuggerHidden]
        public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
        {
            if (pawn.Faction == Faction.OfPlayer)
            {
                foreach (Designation des in pawn.Map.designationManager.SpawnedDesignationsOfDef(DesignationDefOf.EtchWallDecorative))
                {
                    yield return des.target.Cell;
                }
            }
        }
        public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            if (c.IsForbidden(pawn) || pawn.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWallDecorative) == null)
            {
                return false;
            }
            Building edifice = c.GetEdifice(pawn.Map);
            if (edifice == null || !edifice.def.IsSmoothable)
            {
                Log.ErrorOnce("Failed to find valid edifice when trying to smooth a wall", 58988176, false);
                pawn.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.EtchWallDecorative);
                return false;
            }
            LocalTargetInfo target = edifice;
            if (pawn.CanReserve(target, 1, -1, null, forced))
            {
                target = c;
                if (pawn.CanReserve(target, 1, -1, null, forced))
                {
                    return true;
                }
            }
            return false;
        }
        public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return new Job(JobDefOf.EtchWallDecorative, c.GetEdifice(pawn.Map));
        }
    }

}
