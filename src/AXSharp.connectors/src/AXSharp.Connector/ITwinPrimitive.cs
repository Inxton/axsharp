// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector;

/// <summary>
///     Provides basic contract for plc tag.
/// </summary>
public interface ITwinPrimitive : ITwinElement
{
    /// <summary>
    ///     Delegate invoked when the 'Edit' value changes.
    /// </summary>
    OnlinerBase.ValueChangeDelegate EditValueChange { get; set; }

    /// <summary>
    ///     Delegate invoked when the 'Shadow' value changes.
    /// </summary>
    OnlinerBase.ValueChangeDelegate ShadowValueChange { get; set; }

    /// <summary>
    ///     Get access level from the application.
    /// </summary>
    /// <remarks>This limits only this applications access.</remarks>
    ReadWriteAccess ReadWriteAccess { get; }

    /// <summary>
    ///     Makes this tag readonly for the current application.
    /// </summary>
    void MakeReadOnly();

    /// <summary>
    /// Indicates that the member should be accessed only once during the lifetime of the application
    /// when accessing using polling or cyclic access. Other types of access won't be affected (Batch/Direct).
    /// </summary>
    bool ReadOnce { get; }

    /// <summary>
    ///   Sets the member to be accessed only once during the lifetime of the application.
    ///   This applies only on periodic (polling or cyclic reading)  
    /// </summary>
    void MakeReadOnce();

    /// <summary>
    ///     Subscribes this tag for cyclical reading and invokes <see cref="ValueChangedEventHandlerDelegate" /> when the value
    ///     changes.
    ///     <note type="note">Tag will be set for cyclical reading.</note>
    /// </summary>
    /// <param name="handler">Handles the value change event.</param>
    void Subscribe(ValueChangedEventHandlerDelegate handler);

    /// <summary>
    ///     Un-subscribes this tag from handling the value change.
    ///     <note type="note">The cyclical reading of this tag will not be cancelled.</note>
    /// </summary>
    /// <param name="handler"></param>
    void UnSubscribe(ValueChangedEventHandlerDelegate handler);

    /// <summary>
    /// Holds information about the data exchange between target system and the application.
    /// </summary>
    PrimitiveAccessStatus AccessStatus { get; }

    /// <summary>
    /// Gets current polling interval.
    /// </summary>
    int PollingInterval { get;  }


    /// <summary>
    ///     Subscribes this item for periodic reading.
    /// </summary>
    void SubscribeForPeriodicReading();

    /// <summary>
    /// Copies the value from the plc to the shadow value holder.
    /// </summary>
    void FromOnlineToShadow();

    /// <summary>
    /// Copies the value from the shadow value holder to the respective plc variable.
    /// </summary>
    void FromShadowToOnline();

  
}