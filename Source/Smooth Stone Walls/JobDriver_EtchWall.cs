using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using Verse;
using Verse.AI;

namespace RSSW_Code
{
    public class JobDriver_EtchWall : JobDriver
    {
        private float workLeft = -1000f;
        protected int BaseWorkAmount => 600;
        protected DesignationDef DesDef => DesignationDefOf.EtchWall;
        protected StatDef SpeedStat => StatDefOf.SmoothingSpeed;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            var pawn1 = pawn;
            var target = job.targetA;
            var job1 = job;
            bool arg_62_0;
            if (pawn1.Reserve(target, job1, 1, -1, null, errorOnFailed))
            {
                pawn1 = pawn;
                target = job.targetA.Cell;
                job1 = job;
                arg_62_0 = pawn1.Reserve(target, job1, 1, -1, null, errorOnFailed);
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
                doWork.actor.skills?.Learn(SkillDefOf.Crafting, 0.1f);

                if (!(workLeft <= 0f))
                {
                    return;
                }

                DoEffect();
                var designation = Map.designationManager.DesignationAt(TargetLocA, DesDef);
                designation?.Delete();

                ReadyForNextToil();
            };
            doWork.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            doWork.WithProgressBar(TargetIndex.A, () => 1f - (workLeft / (float) BaseWorkAmount));
            doWork.defaultCompleteMode = ToilCompleteMode.Never;
            doWork.activeSkill = () => SkillDefOf.Crafting;
            yield return doWork;
        }

        protected void DoEffect()
        {
            SmoothableWallUtility.Notify_SmoothedByPawn(SmoothableWallUtility.SmoothWall(TargetA.Thing, pawn), pawn);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref workLeft, "workLeft");
        }
    }
}