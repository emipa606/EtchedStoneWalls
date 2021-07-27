using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace RSSW_Code
{
    [HarmonyPatch(typeof(ReverseDesignatorDatabase), "InitDesignators", null)]
    public static class ReverseDesignatorDatabase_InitDesignators
    {
        public static void Postfix(ref List<Designator> ___desList)
        {
            //___desList = new List<Designator>
            //{
            //    new Designator_Cancel(),
            //    new Designator_Claim(),
            //    new Designator_Deconstruct(),
            //    new Designator_Uninstall(),
            //    new Designator_Haul(),
            //    new Designator_Hunt(),
            //    new Designator_Slaughter(),
            //    new Designator_Tame(),
            //    new Designator_PlantsCut(),
            //    new Designator_PlantsHarvest(),
            //    new Designator_PlantsHarvestWood(),
            //    new Designator_Mine(),
            //    new Designator_Strip(),
            //    new Designator_Open(),
            //    new Designator_SmoothSurface(),
            //    new Designator_EtchWall(),
            //    new Designator_EtchWallDecorative()
            //};
            //___desList.RemoveAll(des => !Current.Game.Rules.DesignatorAllowed(des));
            //return false;
            ___desList.Add(new Designator_EtchWall());
            ___desList.Add(new Designator_EtchWallDecorative());
        }
    }
}