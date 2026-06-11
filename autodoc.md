# Just Give Up Man! (JGUM) - Documentation

This document provides an overview of the "Just Give Up Man!" (JGUM) module, detailing its features, API, and recent changes. JGUM enhances the surrender mechanics in Mount & Blade II: Bannerlord, allowing for more dynamic and varied surrender scenarios in various encounters.

## Version History

### v2.1.1.26

*   **New Feature:** Introduced a `JgumSurrenderOutcome` enum and integrated it into `JgumSurrenderRecord` to explicitly track whether a surrender attempt was `Accepted` or `Rejected`.
*   **Internal Improvements:** All relevant surrender behaviors (`AILordEncounterSurrenderBehavior`, `AISiegeSurrenderBehavior`, `FiefPurchaseOfferBehavior`, `LordEncounterSurrenderBehavior`, `PatrolEncounterSurrenderBehavior`, `SiegeNegotiationBehavior.Persuasion`, `SiegeSurrenderBehavior`, `VoluntarySurrenderBehavior`) now correctly set the `Outcome` field when recording surrender events.

## Interop API (`JGUM/Interop/JgumInteropEvents.cs`)

The `JGUM/Interop/JgumInteropEvents.cs` file defines the public API for other modules to interact with and query surrender events handled by JGUM.

### `JgumSurrenderKind` Enum

This enum specifies the type of surrender event that occurred.

```csharp
public enum JgumSurrenderKind
{
    LordEncounterSurrender,
    PatrolEncounterSurrender,
    SiegeSurrender,
    AISiegeSurrender,
    AILordEncounterSurrender,
    FiefPurchaseOfferSurrender,
    VoluntarySurrender,
    SiegeNegotiatedSurrender
}
```

### `JgumSurrenderOutcome` Enum

**New in v2.1.1.26**
This enum specifies the outcome of a surrender attempt.

```csharp
public enum JgumSurrenderOutcome
{
    Accepted,
    Rejected
}
```

### `JgumSurrenderRecord` Class

This class holds the details of a specific surrender event. Instances of this class are passed when surrender events occur.

```csharp
public sealed class JgumSurrenderRecord
{
    public JgumSurrenderKind Kind { get; set; }
    public string? WinnerHeroId { get; set; }
    public string? WinnerClanId { get; set; }
    public string? LoserHeroId { get; set; }
    public string? LoserFactionId { get; set; }
    public float CampaignTimeDays { get; set; }
    public bool AcceptedByPlayer { get; set; }
    public JgumSurrenderOutcome Outcome { get; set; } // New in v2.1.1.26
}
```

*   `Kind`: The type of surrender event.
*   `WinnerHeroId`: The `StringId` of the hero who emerged victorious (or accepted the surrender).
*   `WinnerClanId`: The `StringId` of the clan associated with the winner.
*   `LoserHeroId`: The `StringId` of the hero who surrendered (if applicable).
*   `LoserFactionId`: The `StringId` of the faction that surrendered.
*   `CampaignTimeDays`: The in-game time (in days) when the event occurred.
*   `AcceptedByPlayer`: A boolean indicating if the surrender was accepted by the player.
*   `Outcome`: The explicit outcome of the surrender attempt (`Accepted` or `Rejected`).

### `JgumInteropEvents` Static Class

This class provides static events that can be subscribed to by other modules.

```csharp
public static class JgumInteropEvents
{
    public static event Action<JgumSurrenderRecord>? OnSurrenderEvent;
}
```

*   `OnSurrenderEvent`: This event is triggered whenever a surrender event is processed by JGUM. Subscribers will receive a `JgumSurrenderRecord` instance detailing the event.

## Core Behaviors

JGUM implements various behaviors to handle different surrender scenarios:

*   **AI Lord Encounter Surrender:** AI lords deciding to surrender or not during encounters.
*   **AI Siege Surrender:** AI factions surrendering settlements during sieges.
*   **Fief Purchase Offer Behavior:** Handling offers to purchase fiefs, which can result in a 'surrender' of the fief.
*   **Lord Encounter Surrender:** Player encounters with enemy lords where surrender is an option.
*   **Patrol Encounter Surrender:** Player encounters with enemy patrols where surrender is an option.
*   **Siege Negotiation Behavior:** Player-initiated negotiations during sieges.
*   **Siege Surrender Behavior:** Handling surrender events during player-led sieges.
*   **Voluntary Surrender Behavior:** When a settlement voluntarily surrenders to a besieger.

In all these behaviors, when a surrender event is concluded (either accepted or rejected), a `JgumSurrenderRecord` is created and the `OnSurrenderEvent` is invoked, now including the explicit `Outcome`.