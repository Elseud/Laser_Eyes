# Laser Eyes

This mod adds a simple Ability to fire a beam from a pawn with an ability.

## Attribution
Portions of the materials used to create this content/mod are trademarks and/or copyrighted works of Ludeon Studios Inc. All rights reserved by Ludeon. This content/mod is not official and is not endorsed by Ludeon.

The main body of this mod is the `Verb_AbilityCastBeam` class. This is almost a direct copy of a decompiled copy of the basegame `Verse.Verb_ShootBeam`. This version however inherits from `Verb_CastAbility` instead of just `Verb` and provides the `Pawn` in place of the `EquipmentSource`.
A good faith attempt was made to create this by composition using a wrapped `Verse.Verb_ShootBeam` but this proved complex due to certain methods not being public or virtual.
This mod will be taken down without complaint if Ludeon requests it.
