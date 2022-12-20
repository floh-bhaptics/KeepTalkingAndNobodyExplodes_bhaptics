using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MelonLoader;
using HarmonyLib;
using MyBhapticsTactsuit;

namespace KeepTalkingAndNobodyExplodes_bhaptics
{
    public class KeepTalkingAndNobodyExplodes_bhaptics : MelonMod
    {
        public static TactsuitVR tactsuitVr;

        public override void OnInitializeMelon()
        {
            tactsuitVr = new TactsuitVR();
            tactsuitVr.PlaybackHaptics("HeartBeat");
        }

        [HarmonyPatch(typeof(Bomb), "Detonate", new Type[] { })]
        public class bhaptics_BombExploded
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopThreads();
                tactsuitVr.PlaybackHaptics("ExplosionFace");
            }
        }

        [HarmonyPatch(typeof(Bomb), "BombSolved", new Type[] { })]
        public class bhaptics_BombSolved
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopThreads();
                tactsuitVr.PlaybackHaptics("BombSolved");
            }
        }

        [HarmonyPatch(typeof(Bomb), "OnStrike", new Type[] { typeof(BombComponent) })]
        public class bhaptics_Mistake
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("ErrorArm_L");
                tactsuitVr.PlaybackHaptics("ErrorArm_R");
                tactsuitVr.PlaybackHaptics("ErrorVest_L");
                tactsuitVr.PlaybackHaptics("ErrorVest_R");
            }
        }

        [HarmonyPatch(typeof(Bomb), "OnPass", new Type[] { typeof(BombComponent) })]
        public class bhaptics_CorrectEntry
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("Correct");
            }
        }

    }
}
