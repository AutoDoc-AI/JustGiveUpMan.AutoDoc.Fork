# Just Give Up Man! (JGUM) Documentation

## Version `v2.1.1.26`

This update primarily focuses on enhancing the `JgumSurrenderRecord` for improved data tracking and interoperability, and includes a module version bump.

### Key Changes:

#### 1. Enhanced Surrender Event Tracking with `JgumSurrenderOutcome`

To provide more granular detail about surrender events, a new enum and a corresponding field have been introduced to the `JgumSurrenderRecord`.

##### `JgumSurrenderOutcome` Enum

A new enum, `JgumSurrenderOutcome`, has been added to explicitly define the result of a surrender attempt. This enum is defined in `JGUM/Interop/JgumInteropEvents.cs` and has two possible values:

*   `Accepted`: Indicates that the surrender offer was accepted.
*   `Rejected`: Indicates that the surrender offer was rejected.

##### `Outcome` Property in `JgumSurrenderRecord`

The `JgumSurrenderRecord` class, used for logging and external consumption of surrender event data, now includes a new property:

```csharp
public JgumSurrenderOutcome Outcome { get; set; }
```

This property captures the result of the surrender event using the `JgumSurrenderOutcome` enum. This provides a more semantically clear description of the outcome compared to relying solely on `AcceptedByPlayer`.

##### Impact on Behaviors

Numerous AI and player-facing surrender behaviors across the module have been updated to correctly set the `Outcome` property when a `JgumSurrenderRecord` is created. This ensures that all recorded surrender events accurately reflect whether the surrender was accepted or rejected, regardless of whether the player was involved in the decision.

Affected behaviors include:
*   `AILordEncounterSurrenderBehavior.cs`
*   `AISiegeSurrenderBehavior.cs`
*   `FiefPurchaseOfferBehavior.cs`
*   `LordEncounterSurrenderBehavior.cs`
*   `PatrolEncounterSurrenderBehavior.cs`
*   `SiegeNegotiationBehavior.Persuasion.cs`
*   `SiegeSurrenderBehavior.cs`
*   `VoluntarySurrenderBehavior.cs`

#### 2. Module Version Update

The module version has been updated in `JGUM/SubModule.xml` from `v2.1.0.24` to `v2.1.1.26`.