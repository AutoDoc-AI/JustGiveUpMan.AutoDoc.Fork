namespace JGUM.Config
{
    public class JgumJsonModel
    {
        // Common
        public float SurrenderTendencyMultiplier { get; set; } = 1f;
        public float BaseSurrenderThreshold { get; set; } = 3.5f;
        public int SurrenderRandomnessMode { get; set; } = 1;
        public float GuaranteedSurrenderThreshold { get; set; } = 5.0f;
        public float PlayerMercyMultiplier { get; set; } = 100f;
        public int RequiredSurrenderCount { get; set; } = 3;

        // Siege
        public bool EnableSiegeSurrender { get; set; } = true;
        public bool EnableSiegeStarvationSallyOut { get; set; } = true;
        public float NearbyEnemyLordStrengthPercentage { get; set; } = 50f;
        public float NearbyEnemyLordDetectionRange { get; set; } = 7f;
        public float SiegeTowerEnginePressureMultiplier { get; set; } = 70f;
        public float SiegeRamEnginePressureMultiplier { get; set; } = 70f;
        public float SiegeCatapultEnginePressureMultiplier { get; set; } = 65f;
        public float SiegeTrebuchetEnginePressureMultiplier { get; set; } = 65f;
        public float SiegeEnginePowerRatioMultiplier { get; set; } = 35f;
        public float SiegeWallDamagePressureMultiplier { get; set; } = 75f;
        public float SiegeWallDestroyedBonus { get; set; } = 0.2f;
        public float SiegeWallDamageCurveExponent { get; set; } = 1.5f;
        public float SiegeNegotiationEasyThreshold { get; set; } = 3.0f;
        public float SiegeNegotiationNormalThreshold { get; set; } = 2.2f;
        public float SiegeNegotiationHardThreshold { get; set; } = 1.6f;
        public int SiegeNegotiationDifficultyPreset { get; set; } = 1;

        // Lord
        public bool EnableLordSurrender { get; set; } = true;
        public int LordDialogPriority { get; set; } = 10000;

        // Patrol
        public bool EnablePatrolSurrender { get; set; } = true;
        public int PatrolDialogPriority { get; set; } = 10000;

        // Trait tuning
        public float LordCalculatingMultiplier { get; set; } = 100f;
        public float LordValorMultiplier { get; set; } = 100f;
        public float LordMercyMultiplier { get; set; } = 100f;
        public float LordHonorMultiplier { get; set; } = 100f;
    }
}
