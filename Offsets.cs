namespace AotForms
{
    internal static class Offsets
    {

        internal static uint Il2Cpp;
        // ═══ InitBase list — har APK ka offset yahan add karo ═══
        // Code automatically detect karega kaunsa kaam karta hai
        internal static uint[] InitBases = { 0xA9870BC, 0xABFF3C0, 0xA98D0CC, 0xA988FDC, 0xA997484, 0xA986B7C, 0xA986E9C };

        // Auto-detected working InitBase (0 = not yet detected)
        internal static volatile uint ActiveInitBase = 0;
        internal static uint InitBase => ActiveInitBase != 0 ? ActiveInitBase : InitBases[0];

        /// <summary>
        /// Tries each InitBase, validates full chain, caches the working one.
        /// Returns true + baseGameFacade if found. On failure resets ActiveInitBase for re-detect.
        /// </summary>
        internal static bool TryResolveInitBase(out uint baseGameFacade)
        {
            baseGameFacade = 0;

            if (Il2Cpp == 0) return false;

            // If we already have a working one, try it first
            if (ActiveInitBase != 0)
            {
                if (InternalMemory.Read<uint>(Il2Cpp + ActiveInitBase, out baseGameFacade) && baseGameFacade != 0)
                {
                    if (InternalMemory.Read<uint>(baseGameFacade, out uint gf) && gf != 0)
                        return true;
                }

                // Cached one failed — reset and re-scan
                ActiveInitBase = 0;
            }

            // Try each InitBase and validate full chain
            foreach (var initBase in InitBases)
            {
                if (!InternalMemory.Read<uint>(Il2Cpp + initBase, out uint testBase) || testBase == 0)
                    continue;

                // Validate: baseGameFacade → gameFacade → staticClass → currentGame
                if (!InternalMemory.Read<uint>(testBase, out uint gf) || gf == 0)
                    continue;
                if (!InternalMemory.Read<uint>(gf + StaticClass, out uint sgf) || sgf == 0)
                    continue;
                if (!InternalMemory.Read<uint>(sgf, out uint cg) || cg == 0)
                    continue;

                // Full chain valid! Cache this InitBase
                ActiveInitBase = initBase;
                baseGameFacade = testBase;
                try { System.Console.WriteLine($"[AUTO-DETECT] InitBase=0x{initBase:X} WORKS! baseGameFacade=0x{testBase:X}"); } catch { }
                return true;
            }

            return false;
        }
        internal static uint InSnowSlideWayDashing = 0x1480;   // Player -> block gravity/falling (used by ClimbUp/Fly)
        internal static uint UpdateBehavior = 0x61B0E04;
        internal static uint LocalPlayer_RVA = 0x6507824;
        internal static uint StaticClass = 0x5C;
        internal static uint CurrentMatch = 0x50;
        internal static uint MatchStatus = 0x8C;
        internal static uint LocalPlayer = 0x94;
        internal static uint DictionaryEntities = 0x68;
        internal static uint Player_IsDead = 0x50;
        internal static uint Player_Name = 0x2DC;
        internal static uint Player_Data = 0x48;
        internal static uint Player_ShadowBase = 0x18B8;
        internal static uint XPose = 0x78;
        internal static uint AvatarManager = 0x4C0;
        internal static uint Avatar = 0xA8;
        internal static uint Avatar_IsVisible = 0x95;
        internal static uint Avatar_Data = 0x14;
        internal static uint Avatar_Data_IsTeam = 0x59;
        internal static uint FollowCamera = 0x450;
        internal static uint Camera = 0x18;
        internal static uint AimRotation = 0x400;
        internal static uint MainCameraTransform = 0x24C;
        internal static uint Weapon = 0x3F4;
        internal static uint WeaponData = 0x58;
        internal static uint WeaponRecoil = 0x0C;
        internal static uint ViewMatrix = 0xE8;

        internal static uint sAim1 = 0x540;
        internal static uint isFiring = 0x540;

        internal static uint sAim2 = 0x978;
        internal static uint pomba = 0x540;
        internal static uint weaponinfo = 0x978;

        internal static uint sAim3 = 0x38;//
        internal static uint sAim4 = 0x2c;//
        internal static uint lund = 0x38;//
        internal static uint dick = 0x2c;//
        internal static uint bullet_hit = 0x2C; //
        internal static uint guntipposition = 0x38;//

        internal static uint FireRate = 0x184;//

        internal static uint m_LocalObserver = 0xB4;//
        internal static uint m_TargetPlayer = 0x28;//
        internal static uint CurrentObserver = 0xB4;//
        internal static uint ObserverPlayer = 0x28;//
        internal static uint NoReload = 0x99;
        internal static uint LocalPlayerAttributes = 0x4BC;//
        internal static uint m_FireIntervalScale = 0x184; // RapidFire (verify for current version)
        internal static uint AimbotVisible = 0x4A4;
        internal static uint GameTimer = 0x10;//
        internal static uint FixedDeltaTime = 0x24;//
        internal static uint FireDelay = 0x10;

        // Added features (from "C# - SILENT AIM" project)
        internal static uint Player_BaseProfileInfo = 0x16D0; // Player -> BaseProfileInfo (Level / Rank)
        internal static uint Nogravityfly = 0x124C;           // Player -> MovementComponent (NoGravity Fly)

    }
}
