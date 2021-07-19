using BepInEx;
using HarmonyLib;
using UnityEngine;
using AuthoritativeConfig;

namespace Adjust_Breeding_Limit
{
    [BepInPlugin("zukane2.AdjustBreedingLimit", "Adjust Breeding Limit", "2.0.0")]
    public class AdjustBreedingLimit : BaseUnityPlugin
    {
        public new AuthoritativeConfig.Config Config
        {
            get { return AuthoritativeConfig.Config.Instance; }
            set { }
        }

        private readonly Harmony harmony = new Harmony("zukane2.AdjustBreedingLimit");
        private static ConfigEntry<int> maxCreatures;

        void Awake()
        {
            Config.init(this, true);
            maxCreatures = Config.Bind<int>("General", "MaxCreatures", 5, "Max Creatures");

            harmony.PatchAll();
        }

        void OnDestroy()
        {

            harmony.UnpatchSelf();
        }

        [HarmonyPatch(typeof(Procreation), "Awake")]
        static class Breed_Awake_Patch
        {
            static void Postfix(ref int ___m_maxCreatures)
            {
                ___m_maxCreatures = maxCreatures.Value;

            }
        }

    }
}