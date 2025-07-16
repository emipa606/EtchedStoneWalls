﻿using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace RSSW_Code;

[HarmonyPatch(typeof(ReverseDesignatorDatabase), nameof(ReverseDesignatorDatabase.InitDesignators), null)]
public static class ReverseDesignatorDatabase_InitDesignators
{
    public static void Postfix(ref List<Designator> ___desList)
    {
        ___desList.Add(new Designator_EtchWall());
        ___desList.Add(new Designator_EtchWallDecorative());
    }
}