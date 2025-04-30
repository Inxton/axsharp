// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Provides delegate for notification of tags value change.
/// </summary>
/// <param name="sender">Value tag where the change occurred.</param>
/// <param name="args">Value change arguments.</param>
public delegate void ValueChangedEventHandlerDelegate(ITwinPrimitive sender, ValueChangedEventArgs args);