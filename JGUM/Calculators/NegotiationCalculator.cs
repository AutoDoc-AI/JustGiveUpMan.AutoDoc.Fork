using System;
using System.Collections.Generic;
using System.Linq;
using JGUM.Config;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace JGUM.Calculators
{
    public static class NegotiationCalculator
    {
        public static float GetPowerRatio(Settlement settlement)
        {
            var siegeEvent = settlement.SiegeEvent;
            if (siegeEvent == null)
                return 0f;

            var besiegerParties = siegeEvent.BesiegerCamp.GetInvolvedPartiesForEventType(MapEvent.BattleTypes.Siege);
            float attackerStrength = besiegerParties.Sum(p => p.GetCustomStrength(BattleSideEnum.Attacker, MapEvent.PowerCalculationContext.PlainBattle));

            var defenderParties = settlement.GetInvolvedPartiesForEventType(MapEvent.BattleTypes.Siege);
            float defenderStrength = defenderParties.Sum(p => p.GetCustomStrength(BattleSideEnum.Defender, MapEvent.PowerCalculationContext.PlainBattle));

            if (defenderStrength <= 0f)
                return 99f;

            return attackerStrength / defenderStrength;
        }

        public static PersuasionArgumentStrength GetBaseStrengthFromPowerRatio(float powerRatio)
        {
#if !DEBUG
            int preset = JgumSettingsManager.SiegeNegotiationDifficultyPreset;
            float easyThreshold = preset == 0 ? 2.0f : preset == 1 ? 3.0f : 4.0f;
            float normalThreshold = preset == 0 ? 1.5f : preset == 1 ? 2.2f : 3.0f;
            float hardThreshold = preset == 0 ? 1.0f : preset == 1 ? 1.6f : 2.2f;
#else
            float easyThreshold = Math.Max(0f, JgumSettingsManager.SiegeNegotiationEasyThreshold);
            float normalThreshold = Math.Min(easyThreshold, Math.Max(0f, JgumSettingsManager.SiegeNegotiationNormalThreshold));
            float hardThreshold = Math.Min(normalThreshold, Math.Max(0f, JgumSettingsManager.SiegeNegotiationHardThreshold));
#endif

            if (powerRatio >= easyThreshold)
                return PersuasionArgumentStrength.Easy;
            if (powerRatio >= normalThreshold)
                return PersuasionArgumentStrength.Normal;
            if (powerRatio >= hardThreshold)
                return PersuasionArgumentStrength.Hard;
            return PersuasionArgumentStrength.ExtremelyHard;
        }

        public static int GetRequiredSuccessScore(Settlement? settlement)
        {
            return settlement?.IsTown == true ? 4 : 3;
        }

        public static PersuasionArgumentStrength ShiftStrength(PersuasionArgumentStrength value, int delta)
        {
            int shifted = (int)value + delta;
            if (shifted > 3)
                shifted = 3;
            if (shifted < -3)
                shifted = -3;
            return (PersuasionArgumentStrength)shifted;
        }

        public static List<int> BuildRoundRandomBiases(int optionCount)
        {
            List<int> biases = new List<int>();
            for (int i = 0; i < optionCount; i++)
                biases.Add(0);
            return biases;
        }

        public static int GetSlotStrengthBias(int optionIndex, int optionCount)
        {
            if (optionCount < 3)
                return 0;

            if (optionIndex == 0)
                return 1;
            if (optionIndex == optionCount - 1)
                return -1;

            return 0;
        }
    }
}

