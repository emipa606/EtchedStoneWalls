﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RSSW_Code
{
    [StaticConstructorOnStartup]
    internal static class RSSW_Initializer
    {
        static RSSW_Initializer()
        {
            var harmony = new Harmony("net.rainbeau.rimworld.mod.smoothstone");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            var usingNPS = ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("Nature's Pretty Sweet"));

            var allThings = DefDatabase<ThingDef>.AllDefsListForReading;
            var defsToAdd = new List<ThingDef>();
            foreach (var originalStructure in allThings
                .Where(originalStructure => originalStructure.defName != "SmoothedLavaRock" || !usingNPS.Equals(false))
                .Where(originalStructure => originalStructure.defName.Contains("Smoothed") &&
                                            !originalStructure.defName.Contains("Etched") &&
                                            originalStructure.category == ThingCategory.Building &&
                                            originalStructure.thingClass == typeof(Mineable)))
            {
                if (originalStructure.defName == "SmoothedLavaRock")
                {
                    originalStructure.building.mineableThing = ThingDef.Named("TKKN_ChunkLava");
                    originalStructure.building.mineableDropChance = 0.25f;
                }

                //
                var newEtchedStone = GenerateBaseStructure(originalStructure);
                newEtchedStone.defName = "Etched" + originalStructure.defName;
                newEtchedStone.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label;
                newEtchedStone.description = originalStructure.description + "RSSW.EtchedWall".Translate();
                newEtchedStone.graphicData.texPath = "Walls/Wall_Atlas_Basic";
                defsToAdd.Add(newEtchedStone);
                //
                var newEtchedStoneAtom = GenerateBaseStructure(originalStructure);
                newEtchedStoneAtom.defName = "Etched" + originalStructure.defName + "XXAtom";
                newEtchedStoneAtom.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                           "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneAtom.description = "RSSW.EtchedWallAtom".Translate();
                newEtchedStoneAtom.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Atom";
                defsToAdd.Add(newEtchedStoneAtom);
                //
                var newEtchedStoneBeer = GenerateBaseStructure(originalStructure);
                newEtchedStoneBeer.defName = "Etched" + originalStructure.defName + "XXBeer";
                newEtchedStoneBeer.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                           "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneBeer.description = "RSSW.EtchedWallBeer".Translate();
                newEtchedStoneBeer.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Beer";
                defsToAdd.Add(newEtchedStoneBeer);
                //
                var newEtchedStoneCheese = GenerateBaseStructure(originalStructure);
                newEtchedStoneCheese.defName = "Etched" + originalStructure.defName + "XXCheese";
                newEtchedStoneCheese.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                             "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneCheese.description = "RSSW.EtchedWallCheese".Translate();
                newEtchedStoneCheese.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Cheese";
                defsToAdd.Add(newEtchedStoneCheese);
                //
                var newEtchedStoneDoor = GenerateBaseStructure(originalStructure);
                newEtchedStoneDoor.defName = "Etched" + originalStructure.defName + "XXDoor";
                newEtchedStoneDoor.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                           "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneDoor.description = "RSSW.EtchedWallDoor".Translate();
                newEtchedStoneDoor.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Door";
                defsToAdd.Add(newEtchedStoneDoor);
                //
                var newEtchedStoneShovel = GenerateBaseStructure(originalStructure);
                newEtchedStoneShovel.defName = "Etched" + originalStructure.defName + "XXShovel";
                newEtchedStoneShovel.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                             "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneShovel.description = "RSSW.EtchedWallShovel".Translate();
                newEtchedStoneShovel.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Shovel";
                defsToAdd.Add(newEtchedStoneShovel);
                //
                var newEtchedStoneBoomalope = GenerateBaseStructure(originalStructure);
                newEtchedStoneBoomalope.defName = "Etched" + originalStructure.defName + "XXBoomalope";
                newEtchedStoneBoomalope.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                                "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneBoomalope.description = "RSSW.EtchedWallBoomalope".Translate();
                newEtchedStoneBoomalope.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Boomalope";
                defsToAdd.Add(newEtchedStoneBoomalope);
                //
                var newEtchedStoneForest = GenerateBaseStructure(originalStructure);
                newEtchedStoneForest.defName = "Etched" + originalStructure.defName + "XXForest";
                newEtchedStoneForest.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                             "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneForest.description = "RSSW.EtchedWallForest".Translate();
                newEtchedStoneForest.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Forest";
                defsToAdd.Add(newEtchedStoneForest);
                //
                var newEtchedStoneHouse = GenerateBaseStructure(originalStructure);
                newEtchedStoneHouse.defName = "Etched" + originalStructure.defName + "XXHouse";
                newEtchedStoneHouse.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                            "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneHouse.description = "RSSW.EtchedWallHouse".Translate();
                newEtchedStoneHouse.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_House";
                defsToAdd.Add(newEtchedStoneHouse);
                //
                var newEtchedStonePlanet = GenerateBaseStructure(originalStructure);
                newEtchedStonePlanet.defName = "Etched" + originalStructure.defName + "XXPlanet";
                newEtchedStonePlanet.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                             "RSSW.EtchedWall.label2".Translate();
                newEtchedStonePlanet.description = "RSSW.EtchedWallPlanet".Translate();
                newEtchedStonePlanet.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Planet";
                defsToAdd.Add(newEtchedStonePlanet);
                //
                var newEtchedStoneRocket = GenerateBaseStructure(originalStructure);
                newEtchedStoneRocket.defName = "Etched" + originalStructure.defName + "XXRocket";
                newEtchedStoneRocket.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                             "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneRocket.description = "RSSW.EtchedWallRocket".Translate();
                newEtchedStoneRocket.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Rocket";
                defsToAdd.Add(newEtchedStoneRocket);
                //
                var newEtchedStoneGerbils = GenerateBaseStructure(originalStructure);
                newEtchedStoneGerbils.defName = "Etched" + originalStructure.defName + "XXGerbils";
                newEtchedStoneGerbils.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                              "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneGerbils.description = "RSSW.EtchedWallGerbils".Translate();
                newEtchedStoneGerbils.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Gerbils";
                defsToAdd.Add(newEtchedStoneGerbils);
                //
                var newEtchedStoneIsland = GenerateBaseStructure(originalStructure);
                newEtchedStoneIsland.defName = "Etched" + originalStructure.defName + "XXIsland";
                newEtchedStoneIsland.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                             "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneIsland.description = "RSSW.EtchedWallIsland".Translate();
                newEtchedStoneIsland.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Island";
                defsToAdd.Add(newEtchedStoneIsland);
                //
                var newEtchedStoneMan = GenerateBaseStructure(originalStructure);
                newEtchedStoneMan.defName = "Etched" + originalStructure.defName + "XXMan";
                newEtchedStoneMan.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                          "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneMan.description = "RSSW.EtchedWallMan".Translate();
                newEtchedStoneMan.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Man";
                defsToAdd.Add(newEtchedStoneMan);
                //
                var newEtchedStoneRose = GenerateBaseStructure(originalStructure);
                newEtchedStoneRose.defName = "Etched" + originalStructure.defName + "XXRose";
                newEtchedStoneRose.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                           "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneRose.description = "RSSW.EtchedWallRose".Translate();
                newEtchedStoneRose.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Rose";
                defsToAdd.Add(newEtchedStoneRose);
                //
                var newEtchedStoneWoman = GenerateBaseStructure(originalStructure);
                newEtchedStoneWoman.defName = "Etched" + originalStructure.defName + "XXWoman";
                newEtchedStoneWoman.label = "RSSW.EtchedWall.label1".Translate() + originalStructure.label +
                                            "RSSW.EtchedWall.label2".Translate();
                newEtchedStoneWoman.description = "RSSW.EtchedWallWoman".Translate();
                newEtchedStoneWoman.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Woman";
                defsToAdd.Add(newEtchedStoneWoman);
                //
                originalStructure.building.smoothedThing = newEtchedStone;
            }

            foreach (var thingDef in defsToAdd)
            {
                DefGenerator.AddImpliedDef(thingDef);
                AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash")
                    .Invoke(null, new object[] {thingDef, typeof(ThingDef)});
            }
        }

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
            newStructure.statBases.Add(new StatModifier {stat = StatDefOf.MeditationFocusStrength, value = 0.22f});
            return newStructure;
        }
    }
}