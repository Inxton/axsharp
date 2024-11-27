// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using AXSharp.Connector.Localizations;
using AXSharp.Connector.ValueTypes.Online;
using AXSharp.Connector.ValueTypes.Shadows;
using AXSharp.Connector.ValueValidation;
using Newtonsoft.Json.Linq;

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Base generic class for onliner types. Instance of <see cref="OnlinerBase{T}" /> is commonly referred to as 'tag' or
///     'PLC tag'.
/// </summary>
[DebuggerDisplay("[{Symbol}]")]
public abstract class OnlinerBase<T> : OnlinerBase, IOnline<T>, IShadow<T>, INotifyPropertyChanged, IValueBoundaries<T>
{
    /// <summary>
    ///     Field contains last cyclically read value.
    /// </summary>
    private T _cyclic;

    private T _shadow;

    private T attributeMax;

    private T attributeMin;


    private string attributeToolTip;


    private T raw;

    /// <summary>
    ///     Holder field for validation rule.
    /// </summary>
    protected OnlinerValidationRule<T> validator;

    /// <summary>
    ///     Allows for parameter-less constructor in derived class.
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
    /// <param name="readableTail">HumanReadable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    protected OnlinerBase(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
    }

    /// <summary>
    ///     Gets or sets delegate that executes when <see cref="Edit" /> property is changed.
    /// </summary>
    public override ValueChangeDelegate EditValueChange
    {
        get => base.EditValueChange;
        set => base.EditValueChange = value;
    }

    /// <summary>
    ///     Gets or sets delegate that executed when <see cref="Shadow" /> property is changed.
    /// </summary>
    public override ValueChangeDelegate ShadowValueChange
    {
        get => base.ShadowValueChange;
        set => base.ShadowValueChange = value;
    }

    /// <summary>
    ///     Gets the validation rule for this type. Validation occurs only when property <see cref="Edit" /> is changed.
    /// </summary>
    public OnlinerValidationRule<T> Validator => validator;

    /// <summary>
    ///     Sets <see cref="LastValue" /> property value. This property should be used only for testing purposes.
    /// </summary>
    [Obsolete("Use cyclic instead", false)]
    public T SetLastValue
    {
        set { LastValue = value; }

        internal get { return LastValue; }
    }

    private long CwCycle { get; set; }

    internal void SetValueToWrite(T val)
    {
        CyclicToWrite = val;
    }

    /// <summary>
    ///     Gets the value that will be written in the next cycle.
    /// </summary>
    protected T CyclicToWrite { get; set; }

    private void AddForCyclicAccess()
    {
        if (Parent != null && Parent.GetConnector() != null
            && Parent.GetConnector().SubscriptionMode == ReadSubscriptionMode.AutoSubscribeUsedVariables)
        {
            Parent.GetConnector()?.AddToNextPeriodicReadSet(this);
        }
    }

    /// <summary>
    ///     Gets the value that was read in the last cycle or sets the value to be written in the next cycle.
    /// </summary>
    public virtual T Cyclic
    {
        get
        {
            AddForCyclicAccess();

            return _cyclic;
        }

        set
        {
            if (HasWriteAccess())
            {
                CyclicToWrite = value;
                _cyclic = value;
                Parent.GetConnector()?.AddToPeriodicWriteSet(this);
            }
        }
    }


    /// <summary>
    /// Writes the value to be written in the next call of bulk write to the PLC.
    /// </summary>
    /// <param name="val">Value to be written.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Not to be used in your code my friend... use only if you know what you're doing")]
    public T LethargicWrite(T val)
    {
        if (HasWriteAccess())
        {
            CyclicToWrite = val;
        }

        return val;
    }


    /// <summary>
    /// Gets <see cref="Cyclic"/> translated with provided <see cref="CultureInfo"/>
    /// </summary>
    /// <param name="culture">Desired culture.</param>
    /// <returns>Translated value</returns>
    public virtual T GetCyclic(CultureInfo culture = default)
    {
        return Cyclic;
    }

    /// <summary>
    ///     Gets the cyclically read value. Setter set the value that will be written in the next cycle. The value is validated
    ///     prior to performing write operation.
    ///     The value must fall between <see cref="InstanceMinValue" /> and <see cref="InstanceMaxValue" />, any value outside
    ///     permissible range will not be written and original value will remain unaltered.
    ///     Editing of the value invokes <see cref="EditValue(T, T)" /> which allows for value change logging via
    ///     <see cref="EditValueChange" /> delegate.
    /// </summary>
    public T Edit
    {
        get => _cyclic;

        set
        {

            if (Validator.Validate(value, CultureInfo.InvariantCulture).IsValid)
            {
                EditValue(this.Cyclic, value);
                Cyclic = value;
            }
        }
    }

    /// <summary>
    ///     Gets or sets shadow placeholder for the value of this instance. <see cref="Shadow" /> is off line placeholder that
    ///     does not affect the PLC controller's value of this tag.
    ///     Shadow values can be written to PLC controller via 'FlushShadowToOnline' method of a respective structure.
    ///     Online values can be written to its Shadow representation via 'FlushOnlineToShadow' method of the respective
    ///     structure.
    ///     The value must fall between <see cref="InstanceMinValue" /> and <see cref="InstanceMaxValue" />, any value outside
    ///     permissible range will not be set and original value will remain unaltered.
    ///     Change of the shadow value can be observed via <see cref="ShadowValueChange" /> delegate.
    /// </summary>
    public T Shadow
    {
        get
        {
            if (_shadow == null) _shadow = InitializeDefaults();
            return _shadow;
        }
        set
        {
            if (value == null) value = InitializeDefaults();

            var validationResult = Validator == null || Validator.Validate(value, CultureInfo.InvariantCulture).IsValid;

            if (validationResult)
            {
                ChangeShadowValue(_shadow, value);
                _shadow = value;
                OnShadowValueChangeEvent(_shadow);
            }
        }
    }

    /// <summary>
    ///     Gets the raw value read from the controller.
    ///     The value is not affected by <see cref="TranslatorBase" /> or <see cref="StringInterpolator" />.
    /// </summary>
    public virtual T Raw
    {
        get
        {
            ReadFromPlcIsRequested = true;
            return raw;
        }
        private set => raw = value;
    }


    /// <summary>
    ///     Gets or sets the attribute units.
    /// </summary>
    public string AttributeUnits { get; set; }

    /// <summary>
    ///     Gets or sets tool tip message for this tag.
    /// </summary>
    public string AttributeToolTip
    {
        get => attributeToolTip.Interpolate(this);
        set => attributeToolTip = value;
    }

    /// <summary>
    /// Gets translated tooltip for given <see cref="CultureInfo"/>
    /// </summary>
    /// <param name="culture">Culture used to translate this tooltip</param>
    /// <returns>Translated tooltip</returns>
    public string GetAttributeToolTip(CultureInfo culture)
    {
        return this.Translate(AttributeToolTip, culture);
        
    }

    /// <summary>
    ///     Gets information about this tag's online variable info.
    /// </summary>
    protected ITwinPrimitiveInfo VariableInfo { get; set; }


    /// <summary>
    ///     Gets or sets a value indicating whether read from plc is requested.
    /// </summary>
    public bool ReadFromPlcIsRequested { get; protected set; }

    internal bool AttributeMaxSet { get; private set; }

    /// <summary>
    ///     Gets or sets max. value allowed for this tag.
    ///     If the value is not set the max. value will be the maximum allowed for this type.
    ///     <para>
    ///         <note>The value is typically set by attribute in the PLC code</note>
    ///     </para>
    ///     <example>
    ///         This example demonstrates how to set value boundaries for a numerical value. <see cref="AttributeMinimum" />
    ///         and <see cref="AttributeMaximum" />.
    ///         Notice that the name of the 'added property' is prefixed with 'Attribute' when
    ///         trans-piled into .net class.
    ///         <code>
    ///                 {attribute addProperty Minimum 10}
    ///                 {attribute addProperty Maximum 50}
    ///                 _integerVar : INT;
    ///             </code>
    ///     </example>
    /// </summary>
    public T AttributeMaximum
    {
        get => attributeMax;
        set
        {
            attributeMax = value;
            AttributeMaxSet = true;
        }
    }

    internal bool AttributeMinSet { get; private set; }

    /// <summary>
    ///     Gets or sets min. value allowed for this tag.
    ///     If the value is not set the min. value will be the maximum allowed for this type.
    ///     <para>
    ///         <note>The value is typically set by attribute in the PLC code</note>
    ///     </para>
    ///     <example>
    ///         This example demonstrates how to set value boundaries for a numerical value. <see cref="AttributeMinimum" />
    ///         and <see cref="AttributeMaximum" />.
    ///         Notice that the name of the 'added property' is prefixed with 'Attribute' when
    ///         trans-piled into .net class.
    ///         <code>
    ///                 {attribute addProperty Minimum 10}
    ///                 {attribute addProperty Maximum 50}
    ///                 _integerVar : INT;
    ///             </code>
    ///     </example>
    /// </summary>
    public T AttributeMinimum
    {
        get => attributeMin;
        set
        {
            attributeMin = value;
            AttributeMinSet = true;
        }
    }


    /// <inheritdoc />
    public virtual T LastValue { get; internal set; }

    event ValueChangedEventHandlerDelegate IOnline<T>.ValueChanged
    {
        add => ValueChangeEvent += value;

        remove => ValueChangeEvent -= value;
    }


    string IOnline<T>.Symbol => Symbol;

    string IOnline<T>.AttributeName
    {
        get => AttributeName;
        set => AttributeName = value;
    }

    string IOnline<T>.AttributeUnits
    {
        get => AttributeUnits;
        set => AttributeUnits = value;
    }

    T IOnline<T>.Cyclic
    {
        get => Cyclic;
        set => Cyclic = value;
    }

    T IOnline<T>.Edit
    {
        get => Edit;
        set => Edit = value;
    }

    T IOnline<T>.Value
    {
        get => Cyclic;
        set => Cyclic = value;
    }

    event ValueChangedEventHandlerDelegate IShadow<T>.ValueChanged
    {
        add => ShadowValueChangeEvent += value;

        remove => ShadowValueChangeEvent -= value;
    }

    T IShadow<T>.Value
    {
        get => Shadow;
        set => Shadow = value;
    }

    string IShadow<T>.Symbol => Symbol;

    string IShadow<T>.AttributeName
    {
        get => AttributeName;
        set => AttributeName = value;
    }

    string IShadow<T>.AttributeUnits
    {
        get => AttributeUnits;
        set => AttributeUnits = value;
    }

    T IShadow<T>.Shadow
    {
        get => Shadow;
        set => Shadow = value;
    }


    /// <summary>
    ///     Gets the minimal value allowed for this tag.
    ///     The default min. value is minimum of this type if <see cref="AttributeMinimum" /> is not set.
    ///     If the <see cref="AttributeMinimum" /> property is set then <see cref="InstanceMinValue" /> will return its value.
    /// </summary>
    public abstract T InstanceMinValue { get; }

    /// <summary>
    ///     Gets the maximum value allowed for this tag.
    ///     The default max. value is maximum of this type if <see cref="AttributeMaximum" /> is not set.
    ///     If the <see cref="AttributeMaximum" /> property is set then <see cref="InstanceMaxValue" /> will return its value.
    /// </summary>
    public abstract T InstanceMaxValue { get; }

    /// <summary>
    /// Get whether this primitive is subscribed for periodic reading.
    /// </summary>
    public bool? IsSubscribed => this.Parent?.GetConnector()?.Subscribed?.ContainsKey(this.Symbol);

    /// <summary>
    ///   Updates cyclically read value and performs notifications.
    /// </summary>
    /// <param name="val">Updated value.</param>
    protected void UpdateRead(T val)
    {
        if (Parent != null && Parent.GetConnector() != null)
        {
            CwCycle = Parent.GetConnector().RwCycleCount;
        }

        if (_cyclic == null)
        {
            _cyclic = val;
            Raw = val;
            OnValueChangeEvent(val);
            NotifyPropertyChanged(nameof(Cyclic));
            NotifyPropertyChanged(nameof(Raw));
            LastValue = _cyclic;
            return;
        }


        _cyclic = val;
        Raw = val;

        if (LastValue == null)
        {
            LastValue = val;
            OnValueChangeEvent(val);
            NotifyPropertyChanged(nameof(Cyclic));
            NotifyPropertyChanged(nameof(Raw));
        }

        if (!LastValue.Equals(val))
        {
            LastValue = val;
            OnValueChangeEvent(val);
            NotifyPropertyChanged(nameof(Cyclic));
            NotifyPropertyChanged(nameof(Raw));
        }

        LastValue = val;
    }

    /// <summary>
    ///     Gets tha most recent value either <see cref="LastValue" /> or <see cref="Cyclic" /> if that is more recent.
    /// </summary>
    /// <returns></returns>
    public virtual T GetLastAvailableValue()
    {
        if (CwCycle >= Parent.GetConnector().RwCycleCount)
            return Cyclic;
        return LastValue;
    }

    /// <summary>
    ///     Initializes default value of this generic <see cref="OnlinerBase{T}" />.
    /// </summary>
    /// <returns></returns>
    protected static T InitializeDefaults()
    {
        return typeof(T) == typeof(string) ? (dynamic)string.Empty : default(T);
    }

    /// <summary>
    ///     Gets the value of this variable from the controller asynchronously.
    /// </summary>
    /// <remarks>
    ///     This method accesses the item in a single request over communication layer;
    ///     accessing multiple items may result in performance degradation
    ///     over the communication interface with the target system.
    ///     This method is to be used only when you need the item to be read from the PLC
    ///     and the return the control of the program.
    ///     Consider using <see cref="ITwinObject" />s <see cref="TwinObjectExtensions.ReadAsync" />
    ///     or <see cref="TwinObjectExtensions.WriteAsync" /> methods for bulk access to entire structures.
    /// </remarks>
    public virtual async Task<T> GetAsync()
    {
        return await Task.Run(() => Cyclic);
    }

    /// <summary>
    /// Gets value translated in give <see cref="CultureInfo"/>
    /// </summary>
    /// <param name="culture">Culture into which the value should be translated.</param>
    /// <returns>Translated value.</returns>
    public virtual Task<T> GetAsync(CultureInfo culture = default)
    {
        return Task.Run(() => GetCyclic(culture));
    }

    /// <summary>
    ///     Sets the value of this variable to the controller asynchronously.
    /// </summary>
    /// <remarks>
    ///     This method accesses the item in a single request over communication layer;
    ///     accessing multiple items may result in performance degradation
    ///     over the communication interface with the target system.
    ///     This method is to be used only when you need the item to be read from the PLC
    ///     and the return the control of the program.
    ///     Consider using <see cref="ITwinObject" />s <see cref="TwinObjectExtensions.ReadAsync" />
    ///     or <see cref="TwinObjectExtensions.WriteAsync" /> methods for bulk access to entire structures.
    /// </remarks>
    public virtual async Task<T> SetAsync(T value)
    {
        return await Task.Run(() => Cyclic = value);
    }


    /// <summary>
    ///     <see cref="Shadow" /> value changed event.
    /// </summary>
    public event ValueChangedEventHandlerDelegate ShadowValueChangeEvent;

    /// <summary>
    ///     Get true when this tag has write access.
    /// </summary>
    /// <returns>
    ///     The <see cref="bool" />.
    /// </returns>
    public bool HasWriteAccess()
    {
        var retVal = ReadWriteAccess >= ReadWriteAccess.ReadWrite
                     || Parent.GetConnector().WriteProtectionSuspended;
        return retVal;
    }


    private void OnShadowValueChangeEvent(T newValue)
    {
        NotifyPropertyChanged(nameof(Shadow));

        if (ShadowValueChangeEvent != null) ShadowValueChangeEvent(this, new ValueChangedEventArgs(newValue));
    }

    /// <summary>
    ///     NVI should be implemented in derived class. When implemented in derived class provide reading of the
    ///     <see cref="Cyclic" />value for this tag from a stream.
    /// </summary>
    protected virtual void ReadFromItem()
    {
        // DO NOT ADD ANYTHING HERE
        throw new NotImplementedException($"NVI. Call of virtual member not allowed {GetType()}");
    }

    /// <summary>
    ///     NVI should be implemented in derived class. When implemented in derived class provide writing of the
    ///     <see cref="Cyclic" />value for this tag from a stream.
    /// </summary>
    protected virtual void WriteItem()
    {
        // DO NOT ADD ANYTHING HERE
        throw new NotImplementedException($"NVI. Call of virtual member not allowed {GetType()}");
    }


    private void EditValue(T origin, T newValue)
    {
        if (origin == null || !origin.Equals(newValue))
        {
            Parent.GetConnector()?.Logger.Verbose($"Value change; online; {Symbol};{origin};{newValue}'.");
            EditValueChange?.Invoke(this, origin, newValue);
            NotifyPropertyChanged(nameof(Edit));
        }
    }

    private void ChangeShadowValue(T origin, T newValue)
    {
        if (origin == null || !origin.Equals(newValue))
        {
            Parent?.GetConnector()?.Logger.Verbose($"Value change; shadow; {Symbol};{origin};{newValue}'.");

            var isComplex = newValue as string;

            if (isComplex == null || !string.IsNullOrEmpty(isComplex))
                ShadowValueChange?.Invoke(this, origin, newValue);
        }
    }

    /// <summary>
    ///     Gets the assembly that declares the instance of this tag.
    /// </summary>
    /// <returns></returns>
    public Assembly GetDeclaringAssembly()
    {
        return Assembly.GetAssembly(Parent.GetType());
    }


    /// <inheritdoc />
    public override void FromOnlineToShadow()
    {
        this.Shadow = this.LastValue;
    }

    /// <inheritdoc />
    public override void FromShadowToOnline()
    {
        this.Cyclic = this.Shadow;
    }
}