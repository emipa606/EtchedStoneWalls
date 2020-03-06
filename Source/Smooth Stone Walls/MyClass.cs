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

namespace RSSW_Code {

	[StaticConstructorOnStartup]
	internal static class RSSW_Initializer {
		static RSSW_Initializer() {
			var harmony = new Harmony("net.rainbeau.rimworld.mod.smoothstone");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
			bool usingNPS = false;
			if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("Nature's Pretty Sweet"))) {
				usingNPS = true;
			}
			List<ThingDef> allThings = DefDatabase<ThingDef>.AllDefsListForReading;
			for (int i = 0; i < allThings.Count; i++) {
				if (allThings[i].defName == "SmoothedLavaRock" && usingNPS.Equals(false)) { }
				else if (allThings[i].defName.Contains("Smoothed") && !allThings[i].defName.Contains("Etched") && allThings[i].category == ThingCategory.Building && allThings[i].thingClass == typeof(Mineable)) {
					if (allThings[i].defName == "SmoothedLavaRock") {
						allThings[i].building.mineableThing = ThingDef.Named("TKKN_ChunkLava");
						allThings[i].building.mineableDropChance = 0.25f;
					}
					//
					ThingDef newEtchedStone = new ThingDef();
					newEtchedStone.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStone.blockLight = allThings[i].blockLight;
					newEtchedStone.blockWind = allThings[i].blockWind;
					newEtchedStone.building = allThings[i].building;
					newEtchedStone.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStone.category = allThings[i].category;
					newEtchedStone.coversFloor = allThings[i].coversFloor;
					newEtchedStone.drawerType = allThings[i].drawerType;
					newEtchedStone.fillPercent = allThings[i].fillPercent;
					newEtchedStone.filthLeaving = allThings[i].filthLeaving;
					newEtchedStone.graphicData = new GraphicData();
					newEtchedStone.graphicData.color = allThings[i].graphicData.color;
					newEtchedStone.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStone.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStone.holdsRoof = allThings[i].holdsRoof;
					newEtchedStone.mineable = allThings[i].mineable;
					newEtchedStone.modContentPack = allThings[i].modContentPack;
					newEtchedStone.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStone.passability = allThings[i].passability;
					newEtchedStone.repairEffect = allThings[i].repairEffect;
					newEtchedStone.rotatable = allThings[i].rotatable;
					newEtchedStone.saveCompressible = allThings[i].saveCompressible;
					newEtchedStone.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStone.selectable = allThings[i].selectable;
					newEtchedStone.statBases = allThings[i].statBases;
					newEtchedStone.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStone.thingClass = allThings[i].thingClass;
					newEtchedStone.defName = "Etched" + allThings[i].defName;
					newEtchedStone.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label;
					newEtchedStone.description = allThings[i].description + "RSSW.EtchedWall".Translate();
					newEtchedStone.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStone.graphicData.texPath = "Walls/Wall_Atlas_Basic";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStone);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStone, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneAtom = new ThingDef();
					newEtchedStoneAtom.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneAtom.blockLight = allThings[i].blockLight;
					newEtchedStoneAtom.blockWind = allThings[i].blockWind;
					newEtchedStoneAtom.building = allThings[i].building;
					newEtchedStoneAtom.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneAtom.category = allThings[i].category;
					newEtchedStoneAtom.coversFloor = allThings[i].coversFloor;
					newEtchedStoneAtom.drawerType = allThings[i].drawerType;
					newEtchedStoneAtom.fillPercent = allThings[i].fillPercent;
					newEtchedStoneAtom.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneAtom.graphicData = new GraphicData();
					newEtchedStoneAtom.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneAtom.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneAtom.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneAtom.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneAtom.mineable = allThings[i].mineable;
					newEtchedStoneAtom.modContentPack = allThings[i].modContentPack;
					newEtchedStoneAtom.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneAtom.passability = allThings[i].passability;
					newEtchedStoneAtom.repairEffect = allThings[i].repairEffect;
					newEtchedStoneAtom.rotatable = allThings[i].rotatable;
					newEtchedStoneAtom.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneAtom.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneAtom.selectable = allThings[i].selectable;
					newEtchedStoneAtom.statBases = allThings[i].statBases;
					newEtchedStoneAtom.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneAtom.thingClass = allThings[i].thingClass;
					newEtchedStoneAtom.defName = "Etched" + allThings[i].defName + "XXAtom";
					newEtchedStoneAtom.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneAtom.description = "RSSW.EtchedWallAtom".Translate();
					newEtchedStoneAtom.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneAtom.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Atom";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneAtom);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneAtom, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneBeer = new ThingDef();
					newEtchedStoneBeer.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneBeer.blockLight = allThings[i].blockLight;
					newEtchedStoneBeer.blockWind = allThings[i].blockWind;
					newEtchedStoneBeer.building = allThings[i].building;
					newEtchedStoneBeer.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneBeer.category = allThings[i].category;
					newEtchedStoneBeer.coversFloor = allThings[i].coversFloor;
					newEtchedStoneBeer.drawerType = allThings[i].drawerType;
					newEtchedStoneBeer.fillPercent = allThings[i].fillPercent;
					newEtchedStoneBeer.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneBeer.graphicData = new GraphicData();
					newEtchedStoneBeer.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneBeer.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneBeer.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneBeer.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneBeer.mineable = allThings[i].mineable;
					newEtchedStoneBeer.modContentPack = allThings[i].modContentPack;
					newEtchedStoneBeer.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneBeer.passability = allThings[i].passability;
					newEtchedStoneBeer.repairEffect = allThings[i].repairEffect;
					newEtchedStoneBeer.rotatable = allThings[i].rotatable;
					newEtchedStoneBeer.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneBeer.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneBeer.selectable = allThings[i].selectable;
					newEtchedStoneBeer.statBases = allThings[i].statBases;
					newEtchedStoneBeer.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneBeer.thingClass = allThings[i].thingClass;
					newEtchedStoneBeer.defName = "Etched" + allThings[i].defName + "XXBeer";
					newEtchedStoneBeer.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneBeer.description = "RSSW.EtchedWallBeer".Translate();
					newEtchedStoneBeer.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneBeer.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Beer";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneBeer);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneBeer, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneCheese = new ThingDef();
					newEtchedStoneCheese.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneCheese.blockLight = allThings[i].blockLight;
					newEtchedStoneCheese.blockWind = allThings[i].blockWind;
					newEtchedStoneCheese.building = allThings[i].building;
					newEtchedStoneCheese.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneCheese.category = allThings[i].category;
					newEtchedStoneCheese.coversFloor = allThings[i].coversFloor;
					newEtchedStoneCheese.drawerType = allThings[i].drawerType;
					newEtchedStoneCheese.fillPercent = allThings[i].fillPercent;
					newEtchedStoneCheese.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneCheese.graphicData = new GraphicData();
					newEtchedStoneCheese.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneCheese.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneCheese.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneCheese.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneCheese.mineable = allThings[i].mineable;
					newEtchedStoneCheese.modContentPack = allThings[i].modContentPack;
					newEtchedStoneCheese.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneCheese.passability = allThings[i].passability;
					newEtchedStoneCheese.repairEffect = allThings[i].repairEffect;
					newEtchedStoneCheese.rotatable = allThings[i].rotatable;
					newEtchedStoneCheese.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneCheese.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneCheese.selectable = allThings[i].selectable;
					newEtchedStoneCheese.statBases = allThings[i].statBases;
					newEtchedStoneCheese.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneCheese.thingClass = allThings[i].thingClass;
					newEtchedStoneCheese.defName = "Etched" + allThings[i].defName + "XXCheese";
					newEtchedStoneCheese.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneCheese.description = "RSSW.EtchedWallCheese".Translate();
					newEtchedStoneCheese.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneCheese.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Cheese";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneCheese);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneCheese, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneDoor = new ThingDef();
					newEtchedStoneDoor.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneDoor.blockLight = allThings[i].blockLight;
					newEtchedStoneDoor.blockWind = allThings[i].blockWind;
					newEtchedStoneDoor.building = allThings[i].building;
					newEtchedStoneDoor.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneDoor.category = allThings[i].category;
					newEtchedStoneDoor.coversFloor = allThings[i].coversFloor;
					newEtchedStoneDoor.drawerType = allThings[i].drawerType;
					newEtchedStoneDoor.fillPercent = allThings[i].fillPercent;
					newEtchedStoneDoor.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneDoor.graphicData = new GraphicData();
					newEtchedStoneDoor.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneDoor.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneDoor.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneDoor.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneDoor.mineable = allThings[i].mineable;
					newEtchedStoneDoor.modContentPack = allThings[i].modContentPack;
					newEtchedStoneDoor.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneDoor.passability = allThings[i].passability;
					newEtchedStoneDoor.repairEffect = allThings[i].repairEffect;
					newEtchedStoneDoor.rotatable = allThings[i].rotatable;
					newEtchedStoneDoor.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneDoor.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneDoor.selectable = allThings[i].selectable;
					newEtchedStoneDoor.statBases = allThings[i].statBases;
					newEtchedStoneDoor.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneDoor.thingClass = allThings[i].thingClass;
					newEtchedStoneDoor.defName = "Etched" + allThings[i].defName + "XXDoor";
					newEtchedStoneDoor.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneDoor.description = "RSSW.EtchedWallDoor".Translate();
					newEtchedStoneDoor.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneDoor.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Door";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneDoor);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneDoor, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneShovel = new ThingDef();
					newEtchedStoneShovel.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneShovel.blockLight = allThings[i].blockLight;
					newEtchedStoneShovel.blockWind = allThings[i].blockWind;
					newEtchedStoneShovel.building = allThings[i].building;
					newEtchedStoneShovel.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneShovel.category = allThings[i].category;
					newEtchedStoneShovel.coversFloor = allThings[i].coversFloor;
					newEtchedStoneShovel.drawerType = allThings[i].drawerType;
					newEtchedStoneShovel.fillPercent = allThings[i].fillPercent;
					newEtchedStoneShovel.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneShovel.graphicData = new GraphicData();
					newEtchedStoneShovel.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneShovel.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneShovel.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneShovel.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneShovel.mineable = allThings[i].mineable;
					newEtchedStoneShovel.modContentPack = allThings[i].modContentPack;
					newEtchedStoneShovel.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneShovel.passability = allThings[i].passability;
					newEtchedStoneShovel.repairEffect = allThings[i].repairEffect;
					newEtchedStoneShovel.rotatable = allThings[i].rotatable;
					newEtchedStoneShovel.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneShovel.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneShovel.selectable = allThings[i].selectable;
					newEtchedStoneShovel.statBases = allThings[i].statBases;
					newEtchedStoneShovel.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneShovel.thingClass = allThings[i].thingClass;
					newEtchedStoneShovel.defName = "Etched" + allThings[i].defName + "XXShovel";
					newEtchedStoneShovel.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneShovel.description = "RSSW.EtchedWallShovel".Translate();
					newEtchedStoneShovel.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneShovel.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Shovel";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneShovel);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneShovel, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneBoomalope = new ThingDef();
					newEtchedStoneBoomalope.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneBoomalope.blockLight = allThings[i].blockLight;
					newEtchedStoneBoomalope.blockWind = allThings[i].blockWind;
					newEtchedStoneBoomalope.building = allThings[i].building;
					newEtchedStoneBoomalope.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneBoomalope.category = allThings[i].category;
					newEtchedStoneBoomalope.coversFloor = allThings[i].coversFloor;
					newEtchedStoneBoomalope.drawerType = allThings[i].drawerType;
					newEtchedStoneBoomalope.fillPercent = allThings[i].fillPercent;
					newEtchedStoneBoomalope.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneBoomalope.graphicData = new GraphicData();
					newEtchedStoneBoomalope.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneBoomalope.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneBoomalope.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneBoomalope.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneBoomalope.mineable = allThings[i].mineable;
					newEtchedStoneBoomalope.modContentPack = allThings[i].modContentPack;
					newEtchedStoneBoomalope.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneBoomalope.passability = allThings[i].passability;
					newEtchedStoneBoomalope.repairEffect = allThings[i].repairEffect;
					newEtchedStoneBoomalope.rotatable = allThings[i].rotatable;
					newEtchedStoneBoomalope.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneBoomalope.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneBoomalope.selectable = allThings[i].selectable;
					newEtchedStoneBoomalope.statBases = allThings[i].statBases;
					newEtchedStoneBoomalope.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneBoomalope.thingClass = allThings[i].thingClass;
					newEtchedStoneBoomalope.defName = "Etched" + allThings[i].defName + "XXBoomalope";
					newEtchedStoneBoomalope.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneBoomalope.description = "RSSW.EtchedWallBoomalope".Translate();
					newEtchedStoneBoomalope.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneBoomalope.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Boomalope";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneBoomalope);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneBoomalope, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneForest = new ThingDef();
					newEtchedStoneForest.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneForest.blockLight = allThings[i].blockLight;
					newEtchedStoneForest.blockWind = allThings[i].blockWind;
					newEtchedStoneForest.building = allThings[i].building;
					newEtchedStoneForest.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneForest.category = allThings[i].category;
					newEtchedStoneForest.coversFloor = allThings[i].coversFloor;
					newEtchedStoneForest.drawerType = allThings[i].drawerType;
					newEtchedStoneForest.fillPercent = allThings[i].fillPercent;
					newEtchedStoneForest.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneForest.graphicData = new GraphicData();
					newEtchedStoneForest.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneForest.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneForest.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneForest.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneForest.mineable = allThings[i].mineable;
					newEtchedStoneForest.modContentPack = allThings[i].modContentPack;
					newEtchedStoneForest.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneForest.passability = allThings[i].passability;
					newEtchedStoneForest.repairEffect = allThings[i].repairEffect;
					newEtchedStoneForest.rotatable = allThings[i].rotatable;
					newEtchedStoneForest.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneForest.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneForest.selectable = allThings[i].selectable;
					newEtchedStoneForest.statBases = allThings[i].statBases;
					newEtchedStoneForest.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneForest.thingClass = allThings[i].thingClass;
					newEtchedStoneForest.defName = "Etched" + allThings[i].defName + "XXForest";
					newEtchedStoneForest.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneForest.description = "RSSW.EtchedWallForest".Translate();
					newEtchedStoneForest.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneForest.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Forest";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneForest);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneForest, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneHouse = new ThingDef();
					newEtchedStoneHouse.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneHouse.blockLight = allThings[i].blockLight;
					newEtchedStoneHouse.blockWind = allThings[i].blockWind;
					newEtchedStoneHouse.building = allThings[i].building;
					newEtchedStoneHouse.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneHouse.category = allThings[i].category;
					newEtchedStoneHouse.coversFloor = allThings[i].coversFloor;
					newEtchedStoneHouse.drawerType = allThings[i].drawerType;
					newEtchedStoneHouse.fillPercent = allThings[i].fillPercent;
					newEtchedStoneHouse.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneHouse.graphicData = new GraphicData();
					newEtchedStoneHouse.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneHouse.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneHouse.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneHouse.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneHouse.mineable = allThings[i].mineable;
					newEtchedStoneHouse.modContentPack = allThings[i].modContentPack;
					newEtchedStoneHouse.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneHouse.passability = allThings[i].passability;
					newEtchedStoneHouse.repairEffect = allThings[i].repairEffect;
					newEtchedStoneHouse.rotatable = allThings[i].rotatable;
					newEtchedStoneHouse.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneHouse.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneHouse.selectable = allThings[i].selectable;
					newEtchedStoneHouse.statBases = allThings[i].statBases;
					newEtchedStoneHouse.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneHouse.thingClass = allThings[i].thingClass;
					newEtchedStoneHouse.defName = "Etched" + allThings[i].defName + "XXHouse";
					newEtchedStoneHouse.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneHouse.description = "RSSW.EtchedWallHouse".Translate();
					newEtchedStoneHouse.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneHouse.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_House";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneHouse);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneHouse, typeof(ThingDef) });
					//
					ThingDef newEtchedStonePlanet = new ThingDef();
					newEtchedStonePlanet.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStonePlanet.blockLight = allThings[i].blockLight;
					newEtchedStonePlanet.blockWind = allThings[i].blockWind;
					newEtchedStonePlanet.building = allThings[i].building;
					newEtchedStonePlanet.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStonePlanet.category = allThings[i].category;
					newEtchedStonePlanet.coversFloor = allThings[i].coversFloor;
					newEtchedStonePlanet.drawerType = allThings[i].drawerType;
					newEtchedStonePlanet.fillPercent = allThings[i].fillPercent;
					newEtchedStonePlanet.filthLeaving = allThings[i].filthLeaving;
					newEtchedStonePlanet.graphicData = new GraphicData();
					newEtchedStonePlanet.graphicData.color = allThings[i].graphicData.color;
					newEtchedStonePlanet.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStonePlanet.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStonePlanet.holdsRoof = allThings[i].holdsRoof;
					newEtchedStonePlanet.mineable = allThings[i].mineable;
					newEtchedStonePlanet.modContentPack = allThings[i].modContentPack;
					newEtchedStonePlanet.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStonePlanet.passability = allThings[i].passability;
					newEtchedStonePlanet.repairEffect = allThings[i].repairEffect;
					newEtchedStonePlanet.rotatable = allThings[i].rotatable;
					newEtchedStonePlanet.saveCompressible = allThings[i].saveCompressible;
					newEtchedStonePlanet.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStonePlanet.selectable = allThings[i].selectable;
					newEtchedStonePlanet.statBases = allThings[i].statBases;
					newEtchedStonePlanet.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStonePlanet.thingClass = allThings[i].thingClass;
					newEtchedStonePlanet.defName = "Etched" + allThings[i].defName + "XXPlanet";
					newEtchedStonePlanet.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStonePlanet.description = "RSSW.EtchedWallPlanet".Translate();
					newEtchedStonePlanet.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStonePlanet.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Planet";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStonePlanet);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStonePlanet, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneRocket = new ThingDef();
					newEtchedStoneRocket.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneRocket.blockLight = allThings[i].blockLight;
					newEtchedStoneRocket.blockWind = allThings[i].blockWind;
					newEtchedStoneRocket.building = allThings[i].building;
					newEtchedStoneRocket.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneRocket.category = allThings[i].category;
					newEtchedStoneRocket.coversFloor = allThings[i].coversFloor;
					newEtchedStoneRocket.drawerType = allThings[i].drawerType;
					newEtchedStoneRocket.fillPercent = allThings[i].fillPercent;
					newEtchedStoneRocket.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneRocket.graphicData = new GraphicData();
					newEtchedStoneRocket.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneRocket.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneRocket.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneRocket.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneRocket.mineable = allThings[i].mineable;
					newEtchedStoneRocket.modContentPack = allThings[i].modContentPack;
					newEtchedStoneRocket.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneRocket.passability = allThings[i].passability;
					newEtchedStoneRocket.repairEffect = allThings[i].repairEffect;
					newEtchedStoneRocket.rotatable = allThings[i].rotatable;
					newEtchedStoneRocket.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneRocket.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneRocket.selectable = allThings[i].selectable;
					newEtchedStoneRocket.statBases = allThings[i].statBases;
					newEtchedStoneRocket.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneRocket.thingClass = allThings[i].thingClass;
					newEtchedStoneRocket.defName = "Etched" + allThings[i].defName + "XXRocket";
					newEtchedStoneRocket.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneRocket.description = "RSSW.EtchedWallRocket".Translate();
					newEtchedStoneRocket.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneRocket.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Rocket";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneRocket);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneRocket, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneGerbils = new ThingDef();
					newEtchedStoneGerbils.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneGerbils.blockLight = allThings[i].blockLight;
					newEtchedStoneGerbils.blockWind = allThings[i].blockWind;
					newEtchedStoneGerbils.building = allThings[i].building;
					newEtchedStoneGerbils.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneGerbils.category = allThings[i].category;
					newEtchedStoneGerbils.coversFloor = allThings[i].coversFloor;
					newEtchedStoneGerbils.drawerType = allThings[i].drawerType;
					newEtchedStoneGerbils.fillPercent = allThings[i].fillPercent;
					newEtchedStoneGerbils.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneGerbils.graphicData = new GraphicData();
					newEtchedStoneGerbils.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneGerbils.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneGerbils.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneGerbils.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneGerbils.mineable = allThings[i].mineable;
					newEtchedStoneGerbils.modContentPack = allThings[i].modContentPack;
					newEtchedStoneGerbils.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneGerbils.passability = allThings[i].passability;
					newEtchedStoneGerbils.repairEffect = allThings[i].repairEffect;
					newEtchedStoneGerbils.rotatable = allThings[i].rotatable;
					newEtchedStoneGerbils.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneGerbils.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneGerbils.selectable = allThings[i].selectable;
					newEtchedStoneGerbils.statBases = allThings[i].statBases;
					newEtchedStoneGerbils.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneGerbils.thingClass = allThings[i].thingClass;
					newEtchedStoneGerbils.defName = "Etched" + allThings[i].defName + "XXGerbils";
					newEtchedStoneGerbils.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneGerbils.description = "RSSW.EtchedWallGerbils".Translate();
					newEtchedStoneGerbils.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneGerbils.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Gerbils";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneGerbils);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneGerbils, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneIsland = new ThingDef();
					newEtchedStoneIsland.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneIsland.blockLight = allThings[i].blockLight;
					newEtchedStoneIsland.blockWind = allThings[i].blockWind;
					newEtchedStoneIsland.building = allThings[i].building;
					newEtchedStoneIsland.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneIsland.category = allThings[i].category;
					newEtchedStoneIsland.coversFloor = allThings[i].coversFloor;
					newEtchedStoneIsland.drawerType = allThings[i].drawerType;
					newEtchedStoneIsland.fillPercent = allThings[i].fillPercent;
					newEtchedStoneIsland.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneIsland.graphicData = new GraphicData();
					newEtchedStoneIsland.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneIsland.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneIsland.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneIsland.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneIsland.mineable = allThings[i].mineable;
					newEtchedStoneIsland.modContentPack = allThings[i].modContentPack;
					newEtchedStoneIsland.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneIsland.passability = allThings[i].passability;
					newEtchedStoneIsland.repairEffect = allThings[i].repairEffect;
					newEtchedStoneIsland.rotatable = allThings[i].rotatable;
					newEtchedStoneIsland.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneIsland.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneIsland.selectable = allThings[i].selectable;
					newEtchedStoneIsland.statBases = allThings[i].statBases;
					newEtchedStoneIsland.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneIsland.thingClass = allThings[i].thingClass;
					newEtchedStoneIsland.defName = "Etched" + allThings[i].defName + "XXIsland";
					newEtchedStoneIsland.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneIsland.description = "RSSW.EtchedWallIsland".Translate();
					newEtchedStoneIsland.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneIsland.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Island";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneIsland);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneIsland, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneMan = new ThingDef();
					newEtchedStoneMan.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneMan.blockLight = allThings[i].blockLight;
					newEtchedStoneMan.blockWind = allThings[i].blockWind;
					newEtchedStoneMan.building = allThings[i].building;
					newEtchedStoneMan.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneMan.category = allThings[i].category;
					newEtchedStoneMan.coversFloor = allThings[i].coversFloor;
					newEtchedStoneMan.drawerType = allThings[i].drawerType;
					newEtchedStoneMan.fillPercent = allThings[i].fillPercent;
					newEtchedStoneMan.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneMan.graphicData = new GraphicData();
					newEtchedStoneMan.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneMan.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneMan.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneMan.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneMan.mineable = allThings[i].mineable;
					newEtchedStoneMan.modContentPack = allThings[i].modContentPack;
					newEtchedStoneMan.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneMan.passability = allThings[i].passability;
					newEtchedStoneMan.repairEffect = allThings[i].repairEffect;
					newEtchedStoneMan.rotatable = allThings[i].rotatable;
					newEtchedStoneMan.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneMan.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneMan.selectable = allThings[i].selectable;
					newEtchedStoneMan.statBases = allThings[i].statBases;
					newEtchedStoneMan.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneMan.thingClass = allThings[i].thingClass;
					newEtchedStoneMan.defName = "Etched" + allThings[i].defName + "XXMan";
					newEtchedStoneMan.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneMan.description = "RSSW.EtchedWallMan".Translate();
					newEtchedStoneMan.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneMan.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Man";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneMan);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneMan, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneRose = new ThingDef();
					newEtchedStoneRose.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneRose.blockLight = allThings[i].blockLight;
					newEtchedStoneRose.blockWind = allThings[i].blockWind;
					newEtchedStoneRose.building = allThings[i].building;
					newEtchedStoneRose.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneRose.category = allThings[i].category;
					newEtchedStoneRose.coversFloor = allThings[i].coversFloor;
					newEtchedStoneRose.drawerType = allThings[i].drawerType;
					newEtchedStoneRose.fillPercent = allThings[i].fillPercent;
					newEtchedStoneRose.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneRose.graphicData = new GraphicData();
					newEtchedStoneRose.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneRose.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneRose.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneRose.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneRose.mineable = allThings[i].mineable;
					newEtchedStoneRose.modContentPack = allThings[i].modContentPack;
					newEtchedStoneRose.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneRose.passability = allThings[i].passability;
					newEtchedStoneRose.repairEffect = allThings[i].repairEffect;
					newEtchedStoneRose.rotatable = allThings[i].rotatable;
					newEtchedStoneRose.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneRose.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneRose.selectable = allThings[i].selectable;
					newEtchedStoneRose.statBases = allThings[i].statBases;
					newEtchedStoneRose.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneRose.thingClass = allThings[i].thingClass;
					newEtchedStoneRose.defName = "Etched" + allThings[i].defName + "XXRose";
					newEtchedStoneRose.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneRose.description = "RSSW.EtchedWallRose".Translate();
					newEtchedStoneRose.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneRose.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Rose";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneRose);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneRose, typeof(ThingDef) });
					//
					ThingDef newEtchedStoneWoman = new ThingDef();
					newEtchedStoneWoman.altitudeLayer = allThings[i].altitudeLayer;
					newEtchedStoneWoman.blockLight = allThings[i].blockLight;
					newEtchedStoneWoman.blockWind = allThings[i].blockWind;
					newEtchedStoneWoman.building = allThings[i].building;
					newEtchedStoneWoman.castEdgeShadows = allThings[i].castEdgeShadows;
					newEtchedStoneWoman.category = allThings[i].category;
					newEtchedStoneWoman.coversFloor = allThings[i].coversFloor;
					newEtchedStoneWoman.drawerType = allThings[i].drawerType;
					newEtchedStoneWoman.fillPercent = allThings[i].fillPercent;
					newEtchedStoneWoman.filthLeaving = allThings[i].filthLeaving;
					newEtchedStoneWoman.graphicData = new GraphicData();
					newEtchedStoneWoman.graphicData.color = allThings[i].graphicData.color;
					newEtchedStoneWoman.graphicData.linkFlags = allThings[i].graphicData.linkFlags;
					newEtchedStoneWoman.graphicData.linkType = allThings[i].graphicData.linkType;
					newEtchedStoneWoman.holdsRoof = allThings[i].holdsRoof;
					newEtchedStoneWoman.mineable = allThings[i].mineable;
					newEtchedStoneWoman.modContentPack = allThings[i].modContentPack;
					newEtchedStoneWoman.neverMultiSelect = allThings[i].neverMultiSelect;
					newEtchedStoneWoman.passability = allThings[i].passability;
					newEtchedStoneWoman.repairEffect = allThings[i].repairEffect;
					newEtchedStoneWoman.rotatable = allThings[i].rotatable;
					newEtchedStoneWoman.saveCompressible = allThings[i].saveCompressible;
					newEtchedStoneWoman.scatterableOnMapGen = allThings[i].scatterableOnMapGen;
					newEtchedStoneWoman.selectable = allThings[i].selectable;
					newEtchedStoneWoman.statBases = allThings[i].statBases;
					newEtchedStoneWoman.staticSunShadowHeight = allThings[i].staticSunShadowHeight;
					newEtchedStoneWoman.thingClass = allThings[i].thingClass;
					newEtchedStoneWoman.defName = "Etched" + allThings[i].defName + "XXWoman";
					newEtchedStoneWoman.label = "RSSW.EtchedWall.label1".Translate() + allThings[i].label + "RSSW.EtchedWall.label2".Translate();
					newEtchedStoneWoman.description = "RSSW.EtchedWallWoman".Translate();
					newEtchedStoneWoman.graphicData.graphicClass = typeof(Graphic_Single);
					newEtchedStoneWoman.graphicData.texPath = "WallsDecorated/Wall_Atlas_Bricks_Woman";
					DefGenerator.AddImpliedDef<ThingDef>(newEtchedStoneWoman);
					AccessTools.Method(typeof(ShortHashGiver), "GiveShortHash", null, null).Invoke(null, new object[] { newEtchedStoneWoman, typeof(ThingDef) });
					//
					allThings[i].building.smoothedThing = newEtchedStone;
				}
			}
		}
	}

	[DefOf]
	public static class DesignationDefOf {
		public static DesignationDef EtchWall;		
		public static DesignationDef EtchWallDecorative;
		public static DesignationDef Mine;
		public static DesignationDef SmoothFloor;
		public static DesignationDef SmoothWall;
	}

	[DefOf]
	public static class JobDefOf {
		public static JobDef EtchWall;		
		public static JobDef EtchWallDecorative;		
	}

	[HarmonyPatch(typeof(Designator_SmoothSurface), "CanDesignateCell", null)]
	public static class Designator_SmoothSurface_CanDesignateCell {
		public static bool Prefix(IntVec3 c, ref AcceptanceReport __result, ref Designator_SmoothSurface __instance) {
			Building edifice = c.GetEdifice(__instance.Map);
			if (edifice != null && edifice.def.IsSmoothable) {
				if (edifice.def.building != null && edifice.def.building.smoothedThing.defName.Contains("Etched")) {
					__result = false;
					return false;
				}
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(ReverseDesignatorDatabase), "InitDesignators", null)]
	public static class ReverseDesignatorDatabase_InitDesignators {
		public static bool Prefix(ref List<Designator> ___desList) {
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
	public static class SmoothableWallUtility_Notify_BuildingDestroying {
		public static bool Prefix() {
			return false;
		}
	}
		
	[HarmonyPatch(typeof(SmoothableWallUtility), "Notify_SmoothedByPawn", null)]
	public static class SmoothableWallUtility_Notify_SmoothedByPawn {
		public static bool Prefix() {
			return false;
		}
	}

	[HarmonyPatch(typeof(TouchPathEndModeUtility), "IsCornerTouchAllowed", null)]
	public static class TouchPathEndModeUtility_IsCornerTouchAllowed {
		public static bool Prefix(int cornerX, int cornerZ, Map map, ref bool __result) {
			IntVec3 cell = new IntVec3(cornerX, 0, cornerZ);
			if (map.designationManager.DesignationAt(cell, DesignationDefOf.SmoothWall) != null) {
				__result = true;
				return false;
			}
			return true;
		}
	}
	
	public class Designator_EtchWall : Designator {
		public override int DraggableDimensions {
			get { return 2; }
		}
		public override bool DragDrawMeasurements {
			get { return true; }
		}
		public Designator_EtchWall() {
			this.defaultLabel = "RSSW.DesignatorEtchWall".Translate();
			this.defaultDesc = "RSSW.DesignatorEtchWallDesc".Translate();
			this.icon = ContentFinder<Texture2D>.Get("EtchWall", true);
			this.useMouseIcon = true;
			this.soundDragSustain = SoundDefOf.Designate_DragStandard;
			this.soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
			this.soundSucceeded = SoundDefOf.Designate_SmoothSurface;
			this.hotKey = KeyBindingDefOf.Misc1;
		}
		public override AcceptanceReport CanDesignateThing(Thing t) {
			if (t != null && t.def.IsSmoothable && this.CanDesignateCell(t.Position).Accepted) {
				return AcceptanceReport.WasAccepted;
			}
			return false;
		}
		public override void DesignateThing(Thing t) {
			this.DesignateSingleCell(t.Position);
		}
		public override AcceptanceReport CanDesignateCell(IntVec3 c) {
			if (!c.InBounds(base.Map)) {
				return false;
			}
			if (c.Fogged(base.Map)) {
				return false;
			}
			if (base.Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothFloor) != null || base.Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothWall) != null) {
				return "SurfaceBeingSmoothed".Translate();
			}
			if (base.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWall) != null || base.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWallDecorative) != null) {
				return "RSSW.TerrainBeingEtched".Translate();
			}
			if (c.InNoBuildEdgeArea(base.Map)) {
				return "TooCloseToMapEdge".Translate();
			}
			Building edifice = c.GetEdifice(base.Map);
			if (edifice != null && edifice.def.IsSmoothable) {
				if (edifice.def.building.smoothedThing.defName.Contains("Etched") && (!edifice.def.defName.Contains("Etched") || edifice.def.defName.Contains("XX"))) {
					return AcceptanceReport.WasAccepted;
				}
			}
			return "RSSW.MessageMustDesignateSmooth".Translate();
		}
		public override void DesignateSingleCell(IntVec3 c) {
			base.Map.designationManager.AddDesignation(new Designation(c, DesignationDefOf.EtchWall));
			base.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.Mine);
		}
		public override void SelectedUpdate() {
			GenUI.RenderMouseoverBracket();
		}
		public override void RenderHighlight(List<IntVec3> dragCells) {
			DesignatorUtility.RenderHighlightOverSelectableCells(this, dragCells);
		}
	}

	public class Designator_EtchWallDecorative : Designator {
		public override int DraggableDimensions {
			get { return 2; }
		}
		public override bool DragDrawMeasurements {
			get { return true; }
		}
		public Designator_EtchWallDecorative() {
			this.defaultLabel = "RSSW.DesignatorEtchWallDecorative".Translate();
			this.defaultDesc = "RSSW.DesignatorEtchWallDecorativeDesc".Translate();
			this.icon = ContentFinder<Texture2D>.Get("EtchWall", true);
			this.useMouseIcon = true;
			this.soundDragSustain = SoundDefOf.Designate_DragStandard;
			this.soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
			this.soundSucceeded = SoundDefOf.Designate_SmoothSurface;
			this.hotKey = KeyBindingDefOf.Misc1;
		}
		public override AcceptanceReport CanDesignateThing(Thing t) {
			if (t != null && t.def.IsSmoothable && this.CanDesignateCell(t.Position).Accepted) {
				return AcceptanceReport.WasAccepted;
			}
			return false;
		}
		public override void DesignateThing(Thing t) {
			this.DesignateSingleCell(t.Position);
		}
		public override AcceptanceReport CanDesignateCell(IntVec3 c) {
			if (!c.InBounds(base.Map)) {
				return false;
			}
			if (c.Fogged(base.Map)) {
				return false;
			}
			if (base.Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothFloor) != null || base.Map.designationManager.DesignationAt(c, DesignationDefOf.SmoothWall) != null) {
				return "SurfaceBeingSmoothed".Translate();
			}
			if (base.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWall) != null || base.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWallDecorative) != null) {
				return "RSSW.TerrainBeingEtched".Translate();
			}
			if (c.InNoBuildEdgeArea(base.Map)) {
				return "TooCloseToMapEdge".Translate();
			}
			Building edifice = c.GetEdifice(base.Map);
			if (edifice != null && edifice.def.IsSmoothable) {
				if (edifice.def.building.smoothedThing.defName.Contains("Etched") && !edifice.def.defName.Contains("XX")) {
					return AcceptanceReport.WasAccepted;
				}
			}
			return "RSSW.MessageMustDesignateDecoratable".Translate();
		}
		public override void DesignateSingleCell(IntVec3 c) {
			base.Map.designationManager.AddDesignation(new Designation(c, DesignationDefOf.EtchWallDecorative));
			base.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.Mine);
		}
		public override void SelectedUpdate() {
			GenUI.RenderMouseoverBracket();
		}
		public override void RenderHighlight(List<IntVec3> dragCells) {
			DesignatorUtility.RenderHighlightOverSelectableCells(this, dragCells);
		}
	}

	public class JobDriver_EtchWall : JobDriver {
		private float workLeft = -1000f;
		protected int BaseWorkAmount {
			get { return 600; }
		}
		protected DesignationDef DesDef {
			get { return DesignationDefOf.EtchWall; }
		}
		protected StatDef SpeedStat {
			get { return StatDefOf.SmoothingSpeed; }
		}
		public override bool TryMakePreToilReservations(bool errorOnFailed) {
			Pawn pawn = this.pawn;
			LocalTargetInfo target = this.job.targetA;
			Job job = this.job;
			bool arg_62_0;
			if (pawn.Reserve(target, job, 1, -1, null, errorOnFailed)) {
				pawn = this.pawn;
				target = this.job.targetA.Cell;
				job = this.job;
				arg_62_0 = pawn.Reserve(target, job, 1, -1, null, errorOnFailed);
			}
			else {
				arg_62_0 = false;
			}
			return arg_62_0;
		}
		[DebuggerHidden]
		protected override IEnumerable<Toil> MakeNewToils() {
			this.FailOn(() => !this.job.ignoreDesignations && this.Map.designationManager.DesignationAt(this.TargetLocA, this.DesDef) == null);
			this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
			yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.Touch);
			Toil doWork = new Toil();
			doWork.initAction = delegate {
				this.workLeft = (float)this.BaseWorkAmount;
			};
			doWork.tickAction = delegate {
				float num = (this.SpeedStat == null) ? 1f : doWork.actor.GetStatValue(this.SpeedStat, true);
				this.workLeft -= num;
				if (doWork.actor.skills != null) {
					doWork.actor.skills.Learn(SkillDefOf.Crafting, 0.1f, false);
				}
				if (this.workLeft <= 0f) {
					this.DoEffect();
					Designation designation = this.Map.designationManager.DesignationAt(this.TargetLocA, this.DesDef);
					if (designation != null) {
						designation.Delete();
					}
					this.ReadyForNextToil();
					return;
				}
			};
			doWork.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
			doWork.WithProgressBar(TargetIndex.A, () => 1f - this.workLeft / (float)this.BaseWorkAmount, false, -0.5f);
			doWork.defaultCompleteMode = ToilCompleteMode.Never;
			doWork.activeSkill = (() => SkillDefOf.Crafting);
			yield return doWork;
		}
		protected void DoEffect() {
			SmoothableWallUtility.Notify_SmoothedByPawn(SmoothableWallUtility.SmoothWall(base.TargetA.Thing, this.pawn), this.pawn);
		}
		public override void ExposeData() {
			base.ExposeData();
			Scribe_Values.Look<float>(ref this.workLeft, "workLeft", 0f, false);
		}
	}
	
	public class JobDriver_EtchWallDecorative : JobDriver {
		private float workLeft = -1000f;
		protected int BaseWorkAmount {
			get { return 1200; }
		}
		protected DesignationDef DesDef {
			get { return DesignationDefOf.EtchWallDecorative; }
		}
		protected StatDef SpeedStat {
			get { return StatDefOf.WorkSpeedGlobal; }
		}
		public override bool TryMakePreToilReservations(bool errorOnFailed) {
			Pawn pawn = this.pawn;
			LocalTargetInfo target = this.job.targetA;
			Job job = this.job;
			bool arg_62_0;
			if (pawn.Reserve(target, job, 1, -1, null, errorOnFailed)) {
				pawn = this.pawn;
				target = this.job.targetA.Cell;
				job = this.job;
				arg_62_0 = pawn.Reserve(target, job, 1, -1, null, errorOnFailed);
			}
			else {
				arg_62_0 = false;
			}
			return arg_62_0;
		}		
		[DebuggerHidden]
		protected override IEnumerable<Toil> MakeNewToils() {
			this.FailOn(() => !this.job.ignoreDesignations && this.Map.designationManager.DesignationAt(this.TargetLocA, this.DesDef) == null);
			this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
			yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.Touch);
			Toil doWork = new Toil();
			doWork.initAction = delegate {
				this.workLeft = (float)this.BaseWorkAmount;
			};
			doWork.tickAction = delegate {
				float num = (this.SpeedStat == null) ? 1f : doWork.actor.GetStatValue(this.SpeedStat, true);
				this.workLeft -= num;
				if (doWork.actor.skills != null) {
					doWork.actor.skills.Learn(SkillDefOf.Artistic, 0.1f, false);
				}
				if (this.workLeft <= 0f) {
					this.DoEffect();
					Designation designation = this.Map.designationManager.DesignationAt(this.TargetLocA, this.DesDef);
					if (designation != null) {
						designation.Delete();
					}
					this.ReadyForNextToil();
					return;
				}
			};
			doWork.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
			doWork.WithProgressBar(TargetIndex.A, () => 1f - this.workLeft / (float)this.BaseWorkAmount, false, -0.5f);
			doWork.defaultCompleteMode = ToilCompleteMode.Never;
			doWork.activeSkill = (() => SkillDefOf.Artistic);
			yield return doWork;
		}		
		protected void DoEffect() {
			string wall = base.TargetA.Thing.def.building.smoothedThing.defName;
			System.Random rnd = new System.Random();
			int pictureChance = rnd.Next(20);
			float xpGain = 100f;
			if (pawn.skills != null) {
				pictureChance += (pawn.skills.GetSkill(SkillDefOf.Artistic).Level);
			}
			if (pictureChance < 20) { }
			else {
				int pictureQuality = 1;
				float pictureQualityMod = Rand.Value;
				if (pawn.skills != null) {
					if (pawn.skills.GetSkill(SkillDefOf.Artistic).Level < 7) {
						if (pictureQualityMod > 0.67) { pictureQuality = 2; }
					}
					else if (pawn.skills.GetSkill(SkillDefOf.Artistic).Level < 14) {
						if (pictureQualityMod > 0.67) { pictureQuality = 3; }
						else if (pictureQualityMod > 0.33) {pictureQuality = 2; }
					}
					else {
						if (pictureQualityMod > 0.33) {pictureQuality = 3; }
						else { pictureQuality = 2; }
					}
				}
				int pictureChoice = rnd.Next(5);
				if (pictureQuality == 1) {
					xpGain = 100f;
					if (pictureChoice == 0) { wall = wall+"XXAtom"; }
					else if (pictureChoice == 1) { wall = wall+"XXBeer"; }
					else if (pictureChoice == 2) { wall = wall+"XXCheese"; }
					else if (pictureChoice == 3) { wall = wall+"XXDoor"; }
					else { wall = wall+"XXShovel"; }
				}
				else if (pictureQuality == 2) {
					xpGain = 200f;
					if (pictureChoice == 0) { wall = wall+"XXBoomalope"; }
					else if (pictureChoice == 1) { wall = wall+"XXForest"; }
					else if (pictureChoice == 2) { wall = wall+"XXHouse"; }
					else if (pictureChoice == 3) { wall = wall+"XXPlanet"; }
					else { wall = wall+"XXRocket"; }
				}
				else {
					xpGain = 400f;
					if (pictureChoice == 0) { wall = wall+"XXGerbils"; }
					else if (pictureChoice == 1) { wall = wall+"XXIsland"; }
					else if (pictureChoice == 2) { wall = wall+"XXMan"; }
					else if (pictureChoice == 3) { wall = wall+"XXRose"; }
					else { wall = wall+"XXWoman"; }
				}
				pawn.skills.Learn(SkillDefOf.Artistic, xpGain, false);
			}			
			SmoothableWallUtility.Notify_SmoothedByPawn(DecorateWall(base.TargetA.Thing, wall, this.pawn), this.pawn);
		}
		public override void ExposeData() {
			base.ExposeData();
			Scribe_Values.Look<float>(ref this.workLeft, "workLeft", 0f, false);
		}
		public static Thing DecorateWall(Thing target, string newWall, Pawn smoother) {
			Map map = target.Map;
			target.Destroy(DestroyMode.WillReplace);
			Thing thing = ThingMaker.MakeThing(ThingDef.Named(newWall), target.Stuff);
			thing.SetFaction(smoother.Faction, null);
			GenSpawn.Spawn(thing, target.Position, map, target.Rotation, WipeMode.Vanish, false);
			map.designationManager.TryRemoveDesignation(target.Position, DesignationDefOf.SmoothWall);
			return thing;
		}
	}

	public class WorkGiver_EtchWall : WorkGiver_Scanner {
		public override PathEndMode PathEndMode {
			get { return PathEndMode.Touch; }
		}
		[DebuggerHidden]
		public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn) {
			if (pawn.Faction == Faction.OfPlayer) {
				foreach (Designation des in pawn.Map.designationManager.SpawnedDesignationsOfDef(DesignationDefOf.EtchWall)) {
					yield return des.target.Cell;
				}
			}
		}
		public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false) {
			if (c.IsForbidden(pawn) || pawn.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWall) == null) {
				return false;
			}
			Building edifice = c.GetEdifice(pawn.Map);
			if (edifice == null || !edifice.def.IsSmoothable) {
				Log.ErrorOnce("Failed to find valid edifice when trying to smooth a wall", 58988176, false);
				pawn.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.EtchWall);
				return false;
			}
			LocalTargetInfo target = edifice;
			if (pawn.CanReserve(target, 1, -1, null, forced)) {
				target = c;
				if (pawn.CanReserve(target, 1, -1, null, forced)) {
					return true;
				}
			}
			return false;
		}
		public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false) {
			return new Job(JobDefOf.EtchWall, c.GetEdifice(pawn.Map));
		}
	}

	public class WorkGiver_EtchWallDecorative : WorkGiver_Scanner {
		public override PathEndMode PathEndMode {
			get { return PathEndMode.Touch; }
		}
		[DebuggerHidden]
		public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn) {
			if (pawn.Faction == Faction.OfPlayer) {
				foreach (Designation des in pawn.Map.designationManager.SpawnedDesignationsOfDef(DesignationDefOf.EtchWallDecorative)) {
					yield return des.target.Cell;
				}
			}
		}
		public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false) {
			if (c.IsForbidden(pawn) || pawn.Map.designationManager.DesignationAt(c, DesignationDefOf.EtchWallDecorative) == null) {
				return false;
			}
			Building edifice = c.GetEdifice(pawn.Map);
			if (edifice == null || !edifice.def.IsSmoothable) {
				Log.ErrorOnce("Failed to find valid edifice when trying to smooth a wall", 58988176, false);
				pawn.Map.designationManager.TryRemoveDesignation(c, DesignationDefOf.EtchWallDecorative);
				return false;
			}
			LocalTargetInfo target = edifice;
			if (pawn.CanReserve(target, 1, -1, null, forced)) {
				target = c;
				if (pawn.CanReserve(target, 1, -1, null, forced)) {
					return true;
				}
			}
			return false;
		}
		public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false) {
			return new Job(JobDefOf.EtchWallDecorative, c.GetEdifice(pawn.Map));
		}
	}
	
}
