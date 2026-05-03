using JGUM.Calculators;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Common;

namespace JGUM.MCMBridge.Config
{
    public class JgumMcmSettings : AttributeGlobalSettings<JgumMcmSettings>
    {
        public enum SiegeNegotiationDifficultyPresetOption
        {
            Easy,
            Normal,
            Hard
        }

        public enum SurrenderRandomnessModeOption
        {
            Off,
            Thresholded,
            Unbound
        }

        public override string Id => "JGUMSettings";
        public override string DisplayName => StringCalculator.GetString("JGUM.Settings.DisplayName", "Just Give Up Man!");
        public override string FolderName => "JGUM";
        public override string FormatType => "json";

        // ──────────────────────────────────────────────
        // Basic settings (always visible in MCM)
        // ──────────────────────────────────────────────

        [SettingPropertyFloatingInteger("{=JGUM.Settings.SurrenderTendencyMultiplier.Name}General Surrender Tendency", 0f, 2f, Order = 0, RequireRestart = false, HintText = "{=JGUM.Settings.SurrenderTendencyMultiplier.Hint}Controls how willing enemies are to surrender overall.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Common}Common")]
        public float SurrenderTendencyMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=JGUM.Settings.BaseSurrenderThreshold.Name}Base Surrender Threshold", 0f, 10f, Order = 1, RequireRestart = false, HintText = "{=JGUM.Settings.BaseSurrenderThreshold.Hint}The minimum power advantage you need before enemies consider surrendering.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Common}Common")]
        public float BaseSurrenderThreshold { get; set; } = 3.5f;

        [SettingPropertyDropdown("{=JGUM.Settings.SurrenderRandomnessMode.Name}Surrender Randomness Mode", Order = 2, RequireRestart = false, HintText = "{=JGUM.Settings.SurrenderRandomnessMode.Hint}Determines how RNG is applied to surrender calculations.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Common}Common")]
        public Dropdown<SurrenderRandomnessModeOption> SurrenderRandomnessMode { get; set; } =
            new Dropdown<SurrenderRandomnessModeOption>(new[]
            {
                SurrenderRandomnessModeOption.Off,
                SurrenderRandomnessModeOption.Thresholded,
                SurrenderRandomnessModeOption.Unbound
            }, 1);

        [SettingPropertyFloatingInteger("{=JGUM.Settings.GuaranteedSurrenderThreshold.Name}Guaranteed Surrender Threshold", 0f, 15f, Order = 2, RequireRestart = false, HintText = "{=JGUM.Settings.GuaranteedSurrenderThreshold.Hint}Power advantage required for guaranteed surrender without RNG.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Common}Common")]
        public float GuaranteedSurrenderThreshold { get; set; } = 5.0f;

#if DEBUG
        [SettingPropertyInteger("{=JGUM.Settings.RequiredSurrenderCount.Name}Required Surrender Count", 1, 10, Order = 3, RequireRestart = false, HintText = "{=JGUM.Settings.RequiredSurrenderCount.Hint}How many surrenders you must accept before gaining a Mercy trait point.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Common}Common")]
#endif
        public int RequiredSurrenderCount { get; set; } = 3;

        [SettingPropertyBool("{=JGUM.Settings.EnableSiegeSurrender.Name}Enable Siege Surrender", Order = 0, RequireRestart = false, HintText = "{=JGUM.Settings.EnableSiegeSurrender.Hint}When enabled, besieged settlements can offer to surrender.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Siege}Siege")]
        public bool EnableSiegeSurrender { get; set; } = true;

        [SettingPropertyBool("{=JGUM.Settings.EnableSiegeStarvationSallyOut.Name}Enable Starvation Sally Out", Order = 1, RequireRestart = false, HintText = "{=JGUM.Settings.EnableSiegeStarvationSallyOut.Hint}Starving defenders may charge out in a desperate attack.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Siege}Siege")]
        public bool EnableSiegeStarvationSallyOut { get; set; } = true;

#if !DEBUG
        [SettingPropertyDropdown("{=JGUM.Settings.SiegeNegotiationDifficultyPreset.Name}Persuasion Difficulty", Order = 15, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeNegotiationDifficultyPreset.Hint}Controls how hard it is to persuade defenders to surrender.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Siege}Siege")]
#endif
        public Dropdown<SiegeNegotiationDifficultyPresetOption> SiegeNegotiationDifficultyPreset { get; set; } =
            new Dropdown<SiegeNegotiationDifficultyPresetOption>(new[]
            {
                SiegeNegotiationDifficultyPresetOption.Easy,
                SiegeNegotiationDifficultyPresetOption.Normal,
                SiegeNegotiationDifficultyPresetOption.Hard
            }, 1);

        [SettingPropertyBool("{=JGUM.Settings.EnableLordSurrender.Name}Enable Lord Surrender", Order = 0, RequireRestart = false, HintText = "{=JGUM.Settings.EnableLordSurrender.Hint}Enemy lords may surrender when heavily outmatched.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Lord}Lord")]
        public bool EnableLordSurrender { get; set; } = true;

        [SettingPropertyBool("{=JGUM.Settings.EnablePatrolSurrender.Name}Enable Patrol Surrender", Order = 0, RequireRestart = false, HintText = "{=JGUM.Settings.EnablePatrolSurrender.Hint}Non-lord enemy parties may surrender when heavily outmatched.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Patrol}Patrol")]
        public bool EnablePatrolSurrender { get; set; } = true;

        // ──────────────────────────────────────────────
        // Advanced settings (only visible in MCM in Debug builds)
        // Properties always exist for BridgeBootstrap compatibility.
        // ──────────────────────────────────────────────

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.PlayerMercyMultiplier.Name}Player Mercy Multiplier", 0f, 200f, Order = 2, RequireRestart = false, HintText = "{=JGUM.Settings.PlayerMercyMultiplier.Hint}How much your Mercy trait influences surrender chances.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float PlayerMercyMultiplier { get; set; } = 100f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.NearbyEnemyLordStrengthPercentage.Name}Nearby Enemy Lord Strength (%)", 0f, 100f, Order = 2, RequireRestart = false, HintText = "{=JGUM.Settings.NearbyEnemyLordStrengthPercentage.Hint}Percentage of nearby hostile lord strength that boosts defenders.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float NearbyEnemyLordStrengthPercentage { get; set; } = 50f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.NearbyEnemyLordDetectionRange.Name}Nearby Lord Detection Range", 0f, 20f, Order = 3, RequireRestart = false, HintText = "{=JGUM.Settings.NearbyEnemyLordDetectionRange.Hint}How far the settlement looks for nearby lords.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float NearbyEnemyLordDetectionRange { get; set; } = 7f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeTowerEnginePressureMultiplier.Name}Siege Tower Pressure Multiplier", 0f, 200f, Order = 4, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeTowerEnginePressureMultiplier.Hint}How much siege towers pressure defenders.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeTowerEnginePressureMultiplier { get; set; } = 70f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeRamEnginePressureMultiplier.Name}Ram Pressure Multiplier", 0f, 200f, Order = 5, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeRamEnginePressureMultiplier.Hint}How much battering rams pressure defenders.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeRamEnginePressureMultiplier { get; set; } = 70f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeCatapultEnginePressureMultiplier.Name}Catapult Pressure Multiplier", 0f, 200f, Order = 6, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeCatapultEnginePressureMultiplier.Hint}How much catapults pressure defenders.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeCatapultEnginePressureMultiplier { get; set; } = 65f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeTrebuchetEnginePressureMultiplier.Name}Trebuchet Pressure Multiplier", 0f, 200f, Order = 7, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeTrebuchetEnginePressureMultiplier.Hint}How much trebuchets pressure defenders.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeTrebuchetEnginePressureMultiplier { get; set; } = 65f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeEnginePowerRatioMultiplier.Name}Siege Engine Power Ratio Multiplier", 0f, 200f, Order = 8, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeEnginePowerRatioMultiplier.Hint}How much engine superiority affects surrender pressure.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeEnginePowerRatioMultiplier { get; set; } = 35f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeWallDamagePressureMultiplier.Name}Wall Damage Pressure Multiplier", 0f, 200f, Order = 9, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeWallDamagePressureMultiplier.Hint}How much damaged walls increase surrender pressure.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeWallDamagePressureMultiplier { get; set; } = 75f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeWallDestroyedBonus.Name}Destroyed Wall Bonus", 0f, 2f, Order = 10, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeWallDestroyedBonus.Hint}Extra pressure for each fully breached wall.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeWallDestroyedBonus { get; set; } = 0.2f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeWallDamageCurveExponent.Name}Wall Damage Curve Exponent", 1f, 4f, Order = 11, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeWallDamageCurveExponent.Hint}Controls how wall damage scales with severity.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeWallDamageCurveExponent { get; set; } = 1.5f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeNegotiationEasyThreshold.Name}Negotiation Easy Threshold", 0f, 10f, Order = 12, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeNegotiationEasyThreshold.Hint}Power ratio for easy persuasion.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeNegotiationEasyThreshold { get; set; } = 3.0f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeNegotiationNormalThreshold.Name}Negotiation Normal Threshold", 0f, 10f, Order = 13, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeNegotiationNormalThreshold.Hint}Power ratio for normal persuasion.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeNegotiationNormalThreshold { get; set; } = 2.2f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.SiegeNegotiationHardThreshold.Name}Negotiation Hard Threshold", 0f, 10f, Order = 14, RequireRestart = false, HintText = "{=JGUM.Settings.SiegeNegotiationHardThreshold.Hint}Power ratio for hard persuasion.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float SiegeNegotiationHardThreshold { get; set; } = 1.6f;

#if DEBUG
        [SettingPropertyInteger("{=JGUM.Settings.LordDialogPriority.Name}Lord Dialog Priority", 0, 20000, Order = 1, RequireRestart = false, HintText = "{=JGUM.Settings.LordDialogPriority.Hint}Technical priority for lord surrender dialog lines.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public int LordDialogPriority { get; set; } = 10000;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.LordCalculatingMultiplier.Name}Calculating Trait Multiplier", 0f, 200f, "0", Order = 2, RequireRestart = false, HintText = "{=JGUM.Settings.LordCalculatingMultiplier.Hint}How much the Calculating trait affects surrender.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float LordCalculatingMultiplier { get; set; } = 100f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.LordValorMultiplier.Name}Valor Trait Multiplier", 0f, 200f, "0", Order = 3, RequireRestart = false, HintText = "{=JGUM.Settings.LordValorMultiplier.Hint}How much the Valor trait affects surrender.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float LordValorMultiplier { get; set; } = 100f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.LordMercyMultiplier.Name}Mercy Trait Multiplier", 0f, 200f, "0", Order = 4, RequireRestart = false, HintText = "{=JGUM.Settings.LordMercyMultiplier.Hint}How much the Mercy trait affects surrender.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float LordMercyMultiplier { get; set; } = 100f;

#if DEBUG
        [SettingPropertyFloatingInteger("{=JGUM.Settings.LordHonorMultiplier.Name}Honor Trait Multiplier", 0f, 200f, "0", Order = 5, RequireRestart = false, HintText = "{=JGUM.Settings.LordHonorMultiplier.Hint}How much the Honor trait affects surrender.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public float LordHonorMultiplier { get; set; } = 100f;

#if DEBUG
        [SettingPropertyInteger("{=JGUM.Settings.PatrolDialogPriority.Name}Patrol Dialog Priority", 0, 20000, Order = 1, RequireRestart = false, HintText = "{=JGUM.Settings.PatrolDialogPriority.Hint}Technical priority for patrol surrender dialog lines.")]
        [SettingPropertyGroup("{=JGUM.Settings.Group.Advanced}Advanced")]
#endif
        public int PatrolDialogPriority { get; set; } = 10000;
    }
}