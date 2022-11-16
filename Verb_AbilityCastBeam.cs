using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace UFO.CastBeam
{
    /**
     * Portions of the materials used to create this content/mod are trademarks and/or copyrighted works of Ludeon Studios Inc. All rights reserved by Ludeon. This content/mod is not official and is not endorsed by Ludeon.
     *
     * This is almost a direct copy of a decompiled copy of the basegame `Verse.Verb_ShootBeam`. This version however inherits from `Verb_CastAbility` instead of just `Verb` and provides the `Pawn` in place of the `EquipmentSource`.
     * A good faith attempt was made to create this by composition using a wrapped `Verse.Verb_ShootBeam` but this proved complex due to certain methods not being public or virtual.
     */
    public class Verb_AbilityCastBeam : Verb_CastAbility
    {
        private Verb_ShootBeam _wrappedVerb;
        private Traverse _tryCastShotMethodRef;

        private Verb_ShootBeam WrappedVerb() => _wrappedVerb ?? SetupVerb();

        public Verb_AbilityCastBeam() : base()
        {
            SetupVerb();
        }

        private Verb_ShootBeam SetupVerb()
        {
            if (_wrappedVerb != null || verbProps == null) return _wrappedVerb;
            Log.Message("boop " + (verbProps == null));
            _wrappedVerb = new Verb_ShootBeam()
            {
                caster = caster,
                maneuver = maneuver,
                state = state,
                tool = tool,
                controlGroup = controlGroup,
                verbProps = verbProps,
                verbTracker = verbTracker,
                castCompleteCallback = castCompleteCallback
            };
            _tryCastShotMethodRef = Traverse.Create(_wrappedVerb).Method("TryCastShot");
            return _wrappedVerb;
        }

        protected override int ShotsPerBurst => WrappedVerb().verbProps.burstShotCount;

        public float ShotProgress => WrappedVerb().ShotProgress;

        public Vector3 InterpolatedPosition => WrappedVerb().InterpolatedPosition;

        public override float? AimAngleOverride => WrappedVerb().AimAngleOverride;

        protected override bool TryCastShot()
        {
            return _tryCastShotMethodRef.GetValue<bool>();
        }

        public override bool TryStartCastOn(
            LocalTargetInfo castTarg,
            LocalTargetInfo destTarg,
            bool surpriseAttack = false,
            bool canHitNonTargetPawns = true,
            bool preventFriendlyFire = false,
            bool nonInterruptingSelfCast = false)
        {
            return WrappedVerb().TryStartCastOn(verbProps.beamTargetsGround ? castTarg.Cell : castTarg,
                destTarg, surpriseAttack, canHitNonTargetPawns, preventFriendlyFire, nonInterruptingSelfCast);
        }

        public override void BurstingTick()
        {
            WrappedVerb().BurstingTick();
            this.burstShotsLeft = WrappedVerb().Bursting ? 1 : 0;
        }

        public override void WarmupComplete()
        {
            WrappedVerb().WarmupComplete();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Deep.Look(ref _wrappedVerb, "_wrappedVerb");
            if (Scribe.mode != LoadSaveMode.PostLoadInit || _tryCastShotMethodRef != null)
                return;
            SetupVerb();
        }
    }
}
