using System;

namespace JGUM.Interop
{
    public enum JgumSurrenderKind
    {
        LordEncounter,
        PatrolEncounter,
        SiegeAutoSurrender,
        SiegeNegotiatedSurrender
    }

    public sealed class JgumSurrenderRecord
    {
        public JgumSurrenderKind Kind { get; set; }
        public string? SettlementId { get; set; }
        public string? SurrenderingHeroId { get; set; }
        public string? SurrenderingPartyName { get; set; }
        public string? WinnerHeroId { get; set; }
        public string? WinnerClanId { get; set; }
        public string? LoserFactionId { get; set; }
        public float CampaignTimeDays { get; set; }
        public bool AcceptedByPlayer { get; set; }
    }

    public static class JgumInteropEvents
    {
        public static event Action<JgumSurrenderRecord>? SurrenderResolved;

        internal static void RaiseSurrenderResolved(JgumSurrenderRecord record)
        {
            SurrenderResolved?.Invoke(record);
        }
    }
}
