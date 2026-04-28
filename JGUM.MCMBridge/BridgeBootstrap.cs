using JGUM.Config;
using JGUM.MCMBridge.Config;

namespace JGUM.MCMBridge
{
    public static class BridgeBootstrap
    {
        public static void TryRegister()
        {
            // Force MCM settings type initialization so the settings page is discovered by MCM.
            _ = JgumMcmSettings.Instance;

            // If this assembly can load, MCM chain is present enough for typed settings access.
            JgumSettingsManager.RegisterExternalSettingsProvider(BuildModelFromMcm);
        }

        private static JgumJsonModel BuildModelFromMcm()
        {
            JgumMcmSettings mcm = JgumMcmSettings.Instance ?? new JgumMcmSettings();
            JgumJsonModel json = new JgumJsonModel
            {
                SurrenderTendencyMultiplier = mcm.SurrenderTendencyMultiplier,
                BaseSurrenderThreshold = mcm.BaseSurrenderThreshold,
                SurrenderRandomnessMode = mcm.SurrenderRandomnessMode.SelectedIndex,
                GuaranteedSurrenderThreshold = mcm.GuaranteedSurrenderThreshold,
                PlayerMercyMultiplier = mcm.PlayerMercyMultiplier,
                RequiredSurrenderCount = mcm.RequiredSurrenderCount,
                EnableSiegeSurrender = mcm.EnableSiegeSurrender,
                EnableSiegeStarvationSallyOut = mcm.EnableSiegeStarvationSallyOut,
                NearbyEnemyLordStrengthPercentage = mcm.NearbyEnemyLordStrengthPercentage,
                NearbyEnemyLordDetectionRange = mcm.NearbyEnemyLordDetectionRange,
                SiegeTowerEnginePressureMultiplier = mcm.SiegeTowerEnginePressureMultiplier,
                SiegeRamEnginePressureMultiplier = mcm.SiegeRamEnginePressureMultiplier,
                SiegeCatapultEnginePressureMultiplier = mcm.SiegeCatapultEnginePressureMultiplier,
                SiegeTrebuchetEnginePressureMultiplier = mcm.SiegeTrebuchetEnginePressureMultiplier,
                SiegeEnginePowerRatioMultiplier = mcm.SiegeEnginePowerRatioMultiplier,
                SiegeWallDamagePressureMultiplier = mcm.SiegeWallDamagePressureMultiplier,
                SiegeWallDestroyedBonus = mcm.SiegeWallDestroyedBonus,
                SiegeWallDamageCurveExponent = mcm.SiegeWallDamageCurveExponent,
                SiegeNegotiationEasyThreshold = mcm.SiegeNegotiationEasyThreshold,
                SiegeNegotiationNormalThreshold = mcm.SiegeNegotiationNormalThreshold,
                SiegeNegotiationHardThreshold = mcm.SiegeNegotiationHardThreshold,
                EnableLordSurrender = mcm.EnableLordSurrender,
                LordDialogPriority = mcm.LordDialogPriority,
                EnablePatrolSurrender = mcm.EnablePatrolSurrender,
                PatrolDialogPriority = mcm.PatrolDialogPriority,
                LordCalculatingMultiplier = mcm.LordCalculatingMultiplier,
                LordValorMultiplier = mcm.LordValorMultiplier,
                LordMercyMultiplier = mcm.LordMercyMultiplier,
                LordHonorMultiplier = mcm.LordHonorMultiplier
            };
#if !DEBUG
            json.SiegeNegotiationDifficultyPreset = mcm.SiegeNegotiationDifficultyPreset.SelectedIndex;
#endif
            return json;
        }
    }
}


