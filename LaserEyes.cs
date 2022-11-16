using HarmonyLib;
using Verse;

namespace UFO.CastBeam
{
    public class LaserEyes : Mod
    {
        public LaserEyes(ModContentPack content) : base(content)
        {
            Harmony.DEBUG = true;
            new Harmony("UFO.LaserEyes").PatchAll();
        }
    }
}
