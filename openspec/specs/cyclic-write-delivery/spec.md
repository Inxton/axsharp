# cyclic-write-delivery

## Purpose

Guarantee that values assigned to primitives' `Cyclic` setters reach the PLC — never silently lost, retried across transient failures, coalesced per symbol, and converging on the newest assigned value — with re-authentication as the single audited exception.

## Requirements

### Requirement: Cyclic writes are never silently lost
Every assignment to a primitive's `Cyclic` setter (which stores the value in `CyclicToWrite` and marks the primitive dirty) SHALL result in at least one subsequent successful PLC write of that primitive carrying that value or a newer one — with a single, audited exception: pending writes are dropped (with a logged warning) when the connector enters re-authentication (see "Re-login clears pending writes"). The connector SHALL NOT discard dirty primitives by clearing the pending-write set while a batch is in flight: the cyclic write cycle SHALL atomically swap the pending-write set for a fresh one (draining an exact snapshot) so that assignments made during an in-flight batch land in the new set and are written in the next cycle.

#### Scenario: Value set during an in-flight batch is written next cycle
- **WHEN** an application assigns `Cyclic` on a primitive while a cyclic write batch is in flight
- **THEN** the primitive is written in the next cyclic write batch with its latest `CyclicToWrite` value

#### Scenario: Newly dirtied primitive not in the current snapshot survives
- **WHEN** a primitive becomes dirty after the batch snapshot was taken but before the batch completes
- **THEN** the primitive remains pending and is included in the next batch (it is not wiped by any clear operation)

### Requirement: Failed batch writes remain pending across transient failures
When a cyclic write batch fails with a **transient** failure (communication failure, API error that does not trigger re-authentication), the affected primitives SHALL be returned to the pending-write set (newest-value-wins: re-adding does not overwrite a primitive re-dirtied in the meantime, whose `CyclicToWrite` already holds the latest value). A dirty primitive SHALL remain pending until a successful write completes after its most recent assignment — except across re-login (see below).

#### Scenario: Comm failure retries on next cycle
- **WHEN** a cyclic write batch fails with a communication error
- **THEN** the snapshot's primitives are pending again and are retried in the next cyclic write cycle

#### Scenario: Failure plus concurrent re-assignment converges to newest value
- **WHEN** a batch containing primitive P fails and the application meanwhile assigned a newer value to P
- **THEN** P is pending exactly once and the next successful write carries the newer value

### Requirement: Re-login clears pending writes
On entering re-authentication (`ReLoginToConnectorApi`), the connector SHALL clear the pending-write set and SHALL log a warning listing the dropped symbols. Rationale: re-login implies session loss or PLC state change (STOP, program download) — resurrecting old write values into possibly changed PLC state is a hazard; the no-missed-writes guarantee covers transient failures, not session loss.

#### Scenario: Pending writes dropped with audit trail at re-login
- **WHEN** re-authentication starts while primitives are pending in the write set
- **THEN** the pending-write set is cleared and a warning lists each dropped symbol

#### Scenario: Writes after re-login behave normally
- **WHEN** the application assigns `Cyclic` values after re-login completed
- **THEN** they are written in the next cyclic write cycle under the normal guarantee

### Requirement: Dispatched requests carry the value current at send time
Write request payloads SHALL be materialized at dispatch time on the worker (from the onliner's current `CyclicToWrite`), not at enqueue time. A write that waited in the dispatcher queue SHALL go on the wire with the latest assigned value. Pending accesses SHALL coalesce per symbol — at most one pending entry per variable in the cyclic write and read sets; repeated assignments or read subscriptions MUST NOT queue duplicate entries.

#### Scenario: Queued write sends the newest value
- **WHEN** a write chunk waits in the queue (busy workers or paused dispatcher) and the application assigns a newer value to one of its primitives before dispatch
- **THEN** the request sent to the PLC carries the newer value

#### Scenario: Repeated assignments do not queue duplicates
- **WHEN** the application assigns `Cyclic` on the same primitive several times within one cycle
- **THEN** the pending-write set holds one entry for that symbol and one write request is dispatched with the last value

### Requirement: Last-writer-wins convergence
Because the pending value lives on the primitive (`CyclicToWrite`), a re-dirtied primitive SHALL always be written with its most recent value; an older value MAY reach the PLC first from an in-flight batch, but the PLC SHALL subsequently receive the newest value.

#### Scenario: Overlapping assignments end at newest value
- **WHEN** a primitive is assigned value A, the batch carrying A is in flight, and the application assigns value B
- **THEN** the PLC ultimately holds B after the following cyclic write cycle
