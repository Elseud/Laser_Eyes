using HarmonyLib;
using Verse;

namespace UFO.CastBeam
{
    [HarmonyPatch(typeof(Verb), nameof(Verb_ShootBeam.EquipmentSource), MethodType.Getter)]
    public class Verb_ShootBeamFromPawn
    {
        public static ThingWithComps Postfix(ThingWithComps equipment, VerbProperties ___verbProps,
            Thing ___caster)
        {
            Log.Message("harmony is working");
            Log.Message(___caster?.def?.defName);
            Log.Message(___verbProps?.verbClass.FullName);
            return equipment ?? ___caster as ThingWithComps;
            // return equipment ?? (___verbProps?.verbClass == typeof(Verb_AbilityCastBeam)
            //     ? ___caster as ThingWithComps
            //     : null);
        }
    }
}
