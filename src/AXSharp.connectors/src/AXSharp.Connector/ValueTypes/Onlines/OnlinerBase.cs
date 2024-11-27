// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using AXSharp.Connector.Localizations;

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Base non-generic class for onliner types
/// </summary>
[DebuggerDisplay("[{Symbol}]")]
public abstract class OnlinerBase : ITwinPrimitive
{
    /// <summary>
    ///     Provides delegate for detection of a change of tag value.
    /// </summary>
    /// <param name="twinPrimitive">Tag on which the value change occurred.</param>
    /// <param name="original">Original value.</param>
    /// <param name="newValue">Changed value.</param>
    public delegate void ValueChangeDelegate(ITwinPrimitive twinPrimitive, dynamic original, dynamic newValue);

    private string _attributeName;

    internal string _humanReadable;
    
    internal ConcurrentDictionary<object, byte> PollingHolders = new ConcurrentDictionary<object, byte>();
    
    public int PollingInterval { get; internal set; }


    /// <summary>
    ///     Creates new instance of <see cref="OnlinerBase" />
    /// </summary>
    protected OnlinerBase()
    {
        SymbolTail = string.Empty;
#pragma warning disable CS0618 // Type or member is obsolete
        Parent = new RootTwinObject();
#pragma warning restore CS0618 // Type or member is obsolete
        Symbol = CreateSymbol(Parent.Symbol, string.Empty);
        _humanReadable = CreateSymbol(Parent.HumanReadable, string.Empty);
        Parent.AddValueTag(this);
    }

    /// <summary>
    ///     Creates an instance of <see cref="OnlinerBase{T}" />.
    /// </summary>
    /// <param name="parent">Parent object that creates this instance.</param>
    /// <param name="readableTail"><see cref="HumanReadable" /> tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    protected OnlinerBase(ITwinObject parent, string readableTail, string symbolTail)
    {
        if (parent == null) throw new ArgumentNullException(nameof(parent));
        if (string.IsNullOrWhiteSpace(symbolTail)) throw new ArgumentNullException(nameof(symbolTail));
        SymbolTail = symbolTail;
        Parent = parent;
        Symbol = CreateSymbol(parent.Symbol, symbolTail);
        _humanReadable = CreateSymbol(parent.HumanReadable, readableTail);
        parent.AddValueTag(this);
        parent.AddKid(this);
    }

    /// <summary>
    ///     Gets or sets string format for the display of the value.
    /// </summary>
    /// <remarks>
    ///     If you would like to transform the value using String.Format use <b>FormatString</b> attribute in  your PLC code.
    ///     Notice the usage of <b>[[</b> and <b>]]</b>. During runtime "[[" will be replaced with "{" and "[[" will be
    ///     replaced with "}"
    ///     so you can use
    ///     <a href="https://docs.microsoft.com/en-us/dotnet/api/system.string.format?view=netframework-4.8">String.Format</a>
    ///     class as you wish.
    /// </remarks>
    /// <example>
    ///     <code>
    ///      {attribute addProperty Name "REAL"}
    ///      {attribute addProperty FormatString "[[0:F2]]"}
    ///      REAL_val : REAL;
    ///      </code>
    /// </example>
    public string AttributeFormatString { get; set; }

    /// <summary>
    ///     Gets or sets the tail of the symbol of this tag.
    /// </summary>
    protected string SymbolTail { get; set; }

    /// <summary>
    ///     Gets the parent object of this instance. Parent object is the object that created this instance.
    /// </summary>
    protected ITwinObject Parent { get; set; }

    /// <inheritdoc />
    public void SubscribeForPeriodicReading()
    {
        this.Parent?.GetConnector()?.Subscribe(this);
    }


    /// <summary>
    ///     Delegate invoked when the 'Edit' value changes.
    /// </summary>
    public virtual ValueChangeDelegate EditValueChange { get; set; }

    /// <summary>
    ///     Delegate invoked when the 'Shadow' value changes.
    /// </summary>
    public virtual ValueChangeDelegate ShadowValueChange { get; set; }

    /// <summary>
    ///     Gets the read write access mode for this tag.
    /// </summary>
    public ReadWriteAccess ReadWriteAccess { get; private set; } = ReadWriteAccess.ReadWrite;

    /// <summary>
    ///     Makes this tag readonly for the current application.
    /// </summary>
    public void MakeReadOnly()
    {
        ReadWriteAccess = ReadWriteAccess.Read;
    }

    /// <inheritdoc />
    public bool ReadOnce { get; private set; }

    /// <inheritdoc />
    public void MakeReadOnce()
    {
        ReadOnce = true;
    }

    /// <summary>
    ///     Gets tail of the this tag symbol.
    /// </summary>
    /// <returns>Tail of this tag's symbol.</returns>
    public string GetSymbolTail()
    {
        return SymbolTail;
    }

    /// <summary>
    ///     Gets the parent object of this instance.
    ///     Parent object is the object that created this instance.
    /// </summary>
    /// <returns>Parent object.</returns>
    public ITwinObject GetParent()
    {
        return Parent;
    }

    
    /// <summary>
    /// Add this primitive to next periodic read queue.
    /// </summary>
    public void Poll()
    {
        this.Parent.GetConnector().AddToNextPeriodicReadSet(this);
    }

    public Translator Interpreter => this.Parent?.Interpreter;


    /// <summary>
    ///     Subscribes this tag for cyclical reading and invokes <see cref="ValueChangedEventHandlerDelegate" /> when the value
    ///     changes.
    /// </summary>
    /// <param name="handler">Handles the value change event.</param>
    public void Subscribe(ValueChangedEventHandlerDelegate handler)
    {
        SubscribeForPeriodicReading();
        ValueChangeEvent += handler;
    }

    /// <summary>
    /// Subscribes for periodic reading of this variable.
    /// </summary>
    public void Subscribe()
    {
        SubscribeForPeriodicReading();
    }


    /// <summary>
    ///     Un-subscribes this tag from handling the value change.
    ///     The cyclical reading of this tag will not be cancelled.
    /// </summary>
    /// <param name="handler"></param>
    public void UnSubscribe(ValueChangedEventHandlerDelegate handler)
    {
        ValueChangeEvent -= handler;
    }

    /// <summary>
    /// Default date and time, init value to indicate not assigned.
    /// </summary>
    internal static readonly DateTime DefaultDateTime = DateTime.MinValue;

    /// <inheritdoc />
    public PrimitiveAccessStatus AccessStatus { get; } = new PrimitiveAccessStatus()
    { Cycle = 0, Failure = false, FailureReason = string.Empty, LastAccess = DefaultDateTime };

    /// <summary>
    ///     Gets the symbol of this on line variable.
    /// </summary>
    public string Symbol { get; protected set; }

    /// <summary>
    ///     Gets or sets the attribute name.
    /// </summary>
    public string AttributeName
    {
        get => string.IsNullOrEmpty(_attributeName)
            ? SymbolTail
            : _attributeName.Interpolate(this).CleanUpLocalizationTokens();

        set => _attributeName = value;
    }

    public string GetAttributeName(CultureInfo culture)
    {
        return string.IsNullOrEmpty(_attributeName)
            ? SymbolTail
            : this.Translate(_attributeName, culture).Interpolate(this);
    }

    /// <summary>
    ///     Provides a string combined from <see cref="AttributeName" /> of ancestors (<see cref="OnlinerBase.Parent" />) of
    ///     this instance and the tail of this instance.
    /// </summary>
    public string HumanReadable
    {
        get => this._humanReadable.Interpolate(this).CleanUpLocalizationTokens();
        protected set => _humanReadable = value;
    }

    public string GetHumanReadable(CultureInfo culture) 
    {
        return this.Translate(_humanReadable, culture).Interpolate(this);
    }

    /// <summary>
    ///     Creates symbol path concatenating parent symbol with child symbol
    /// </summary>
    /// <param name="rootSymbol">Parent symbol.</param>
    /// <param name="symbol">Symbol tail.</param>
    /// <returns></returns>
    protected static string CreateSymbol(string rootSymbol, string symbol)
    {
        return !string.IsNullOrEmpty(rootSymbol) ? $"{rootSymbol}.{symbol}" : symbol;
    }

    /// <summary>
    ///     Onliner cyclic property changed event.
    /// </summary>
    public event ValueChangedEventHandlerDelegate ValueChangeEvent;

    /// <summary>
    ///     Gets delegates associated with ValueChangeEvent
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Delegate> GetValueChangeEventSubscribers()
    {
        if (ValueChangeEvent != null) return ValueChangeEvent.GetInvocationList();

        return new Delegate[] { };
    }

    /// <summary>
    ///     Notifies the change of cyclic value.
    /// </summary>
    /// <param name="newValue"></param>
    protected void OnValueChangeEvent(object newValue)
    {
        NotifyPropertyChanged("Cyclic");

        var handler = ValueChangeEvent;
        if (handler != null) handler(this, new ValueChangedEventArgs(newValue));
    }

    /// <summary>
    ///     Implementation of <see cref="INotifyPropertyChanged" /> interface.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     Implementation of <see cref="INotifyPropertyChanged" />.
    /// </summary>
    /// <param name="propertyName">
    ///     Property name.
    /// </param>
    protected void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    public abstract void FromOnlineToShadow();
    public abstract void FromShadowToOnline();

    public void AddToPeriodicQueue()
    {
        this.Parent?.GetConnector()?.AddToNextPeriodicReadSet(this);
    }
}