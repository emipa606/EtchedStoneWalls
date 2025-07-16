using System;
using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using Verse;
using Verse.AI;

namespace RSSW_Code;

public class JobDriver_EtchWallDecorative : JobDriver
{
    private float workLeft = -1000f;
    private int BaseWorkAmount => 1200;
    private DesignationDef DesDef => DesignationDefOf.EtchWallDecorative;
    private StatDef SpeedStat => StatDefOf.WorkSpeedGlobal;

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        var pawn1 = pawn;
        var target = job.targetA;
        var job1 = job;
        bool arg620;
        if (pawn1.Reserve(target, job1, 1, -1, null, errorOnFailed))
        {
            pawn1 = pawn;
            target = job.targetA.Cell;
            job1 = job;
            arg620 = pawn1.Reserve(target, job1, 1, -1, null, errorOnFailed);
        }
        else
        {
            arg620 = false;
        }

        return arg620;
    }

    [DebuggerHidden]
    public override IEnumerable<Toil> MakeNewToils()
    {
        this.FailOn(() =>
            !job.ignoreDesignations && Map.designationManager.DesignationAt(TargetLocA, DesDef) == null);
        this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
        yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.Touch);
        var doWork = new Toil
        {
            initAction = delegate { workLeft = BaseWorkAmount; }
        };
        doWork.tickAction = delegate
        {
            var num = SpeedStat == null ? 1f : doWork.actor.GetStatValue(SpeedStat);
            workLeft -= num;
            doWork.actor.skills?.Learn(SkillDefOf.Artistic, 0.1f);

            if (!(workLeft <= 0f))
            {
                return;
            }

            doEffect();
            var designation = Map.designationManager.DesignationAt(TargetLocA, DesDef);
            designation?.Delete();

            ReadyForNextToil();
        };
        doWork.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
        doWork.WithProgressBar(TargetIndex.A, () => 1f - (workLeft / BaseWorkAmount));
        doWork.defaultCompleteMode = ToilCompleteMode.Never;
        doWork.activeSkill = () => SkillDefOf.Artistic;
        yield return doWork;
    }

    private void doEffect()
    {
        var wall = TargetA.Thing.def.building.smoothedThing.defName;
        var rnd = new Random();
        var pictureChance = rnd.Next(20);
        if (pawn.skills != null)
        {
            pictureChance += pawn.skills.GetSkill(SkillDefOf.Artistic).Level;
        }

        if (pictureChance < 20)
        {
        }
        else
        {
            var pictureQuality = 1;
            var pictureQualityMod = Rand.Value;
            if (pawn.skills != null)
            {
                if (pawn.skills.GetSkill(SkillDefOf.Artistic).Level < 7)
                {
                    if (pictureQualityMod > 0.67)
                    {
                        pictureQuality = 2;
                    }
                }
                else if (pawn.skills.GetSkill(SkillDefOf.Artistic).Level < 14)
                {
                    if (pictureQualityMod > 0.67)
                    {
                        pictureQuality = 3;
                    }
                    else if (pictureQualityMod > 0.33)
                    {
                        pictureQuality = 2;
                    }
                }
                else
                {
                    pictureQuality = pictureQualityMod > 0.33 ? 3 : 2;
                }
            }

            var pictureChoice = rnd.Next(5);
            float xpGain;
            switch (pictureQuality)
            {
                case 1:
                    xpGain = 100f;
                    switch (pictureChoice)
                    {
                        case 0:
                            wall += "XXAtom";
                            break;
                        case 1:
                            wall += "XXBeer";
                            break;
                        case 2:
                            wall += "XXCheese";
                            break;
                        case 3:
                            wall += "XXDoor";
                            break;
                        default:
                            wall += "XXShovel";
                            break;
                    }

                    break;
                case 2:
                    xpGain = 200f;
                    switch (pictureChoice)
                    {
                        case 0:
                            wall += "XXBoomalope";
                            break;
                        case 1:
                            wall += "XXForest";
                            break;
                        case 2:
                            wall += "XXHouse";
                            break;
                        case 3:
                            wall += "XXPlanet";
                            break;
                        default:
                            wall += "XXRocket";
                            break;
                    }

                    break;
                default:
                    xpGain = 400f;
                    switch (pictureChoice)
                    {
                        case 0:
                            wall += "XXGerbils";
                            break;
                        case 1:
                            wall += "XXIsland";
                            break;
                        case 2:
                            wall += "XXMan";
                            break;
                        case 3:
                            wall += "XXRose";
                            break;
                        default:
                            wall += "XXWoman";
                            break;
                    }

                    break;
            }

            pawn.skills?.Learn(SkillDefOf.Artistic, xpGain);
        }

        SmoothableWallUtility.Notify_SmoothedByPawn(decorateWall(TargetA.Thing, wall, pawn), pawn);
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref workLeft, "workLeft");
    }

    private static Thing decorateWall(Thing target, string newWall, Pawn smoother)
    {
        var map = target.Map;
        target.Destroy(DestroyMode.WillReplace);
        var thing = ThingMaker.MakeThing(ThingDef.Named(newWall), target.Stuff);
        thing.SetFaction(smoother.Faction);
        GenSpawn.Spawn(thing, target.Position, map, target.Rotation);
        map.designationManager.TryRemoveDesignation(target.Position, DesignationDefOf.SmoothWall);
        return thing;
    }
}