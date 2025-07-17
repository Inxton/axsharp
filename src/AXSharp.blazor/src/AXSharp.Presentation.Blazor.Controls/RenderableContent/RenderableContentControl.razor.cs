// AXSharp.Presentation.Blazor.Controls
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using AXSharp.Connector;
using AXSharp.Abstractions.Presentation;
using AXSharp.Presentation.Attributes;
using AXSharp.Presentation.Blazor;
using AXSharp.Presentation.Blazor.Interfaces;
using System.Runtime.CompilerServices;
using AXSharp.Presentation.Blazor.Services;
using System.Collections.Generic;
using AXSharp.Presentation.Blazor.Exceptions;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;
using AXSharp.Connector.ValueTypes;
using System.Xml.Linq;
using AXSharp.Connector.Localizations;

namespace AXSharp.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  This class implements main logic behind auto-generated UI. 
    /// </summary>
    public partial class RenderableContentControl : ComponentBase, IDisposable
    {
        private ITwinElement _context;

        /// <summary>
        /// Gets or sets polling interval for this control and nested controls.
        /// [!NOTE] Nested element can have different polling setting that will override this property.
        /// </summary>
        [Parameter] public int PollingInterval { get; set; } = 250;

        /// <summary>
        /// Gets or sets the presentation template used for displaying data.
        /// </summary>
        /// <remarks>
        /// The presentation template specifies the way the data is formatted and presented to the user.
        /// It is a string value that can be assigned using markup syntax or plain text.
        /// By default, the presentation template is an empty string. If no presentation template is given
        /// the renderer will use the locator pattern to provide the presentation type.
        /// </remarks>
        /// <value>
        /// The presentation template string.
        /// </value>
        [Parameter]
        public string PresentationTemplate
        {
            get => presentationTemplate;

            set
            {
                if (presentationTemplate != value)
                {
                    presentationTemplate = value;
                    OnPresentationChanged();
                    this.ForceRender();
                }
            }
        } 

        /// <summary>
        /// Parameter Context accept ITwinElement instance, which is used as base model for UI generation.
        /// </summary>
        [Parameter]
        public ITwinElement Context
        {
            get => _context;
            set
            {
                if (_context != value)
                {
                    _context = value as ITwinElement;
                    this.OnContextChanged();
                    this.ForceRender();
                }
            }
        }

        /// <summary>
        /// Method called when the context is changed.
        /// </summary>
        public virtual void OnContextChanged()
        {

        }

        /// <summary>
        /// Parameter Presentation specify mode, in which view UI is generated. Type PresentationType is used.
        /// </summary>
        [Parameter]
        public string Presentation
        {
            get => _presentation;
            set
            {
                if (_presentation != value)
                {
                    _presentation = value;
                    OnPresentationChanged();
                    this.ForceRender();
                }
            }
        }

        public virtual void OnPresentationChanged()
        {

        }

        /// <summary>
        /// Parameter Class, in which RenderableContentenControl will be wrapped.
        /// </summary>
        [Parameter]
        public string Class { get; set; }
        /// <summary>
        /// Parameter LayoutClass, in which layouts will be wrapped.
        /// </summary>
        [Parameter]
        public string LayoutClass { get; set; }

        /// <summary>
        /// Gets or sets parent container containing this renderable content control
        /// [!NOTE] This method is used in advanced scenarios it must be set explicitly.
        /// </summary>
        [Parameter]
        public ComponentBase ParentContainer { get; set; }
        
        /// <summary>
        /// Parameter LayoutChildrenClass, in which children of layouts will be wrapped.
        /// </summary>
        [Parameter]
        public string LayoutChildrenClass { get; set; }

        /// <summary>
        /// Parameter HideLabel, if set to true, will hide label of this control.
        /// </summary>
        [Parameter]
        public bool HideLabel { get; set; } = false;

        [Inject]
        public ComponentService ComponentService { get; set; }
        [Inject]
        public AttributesHandler AttributesHandler { get; set; }
        [Inject]
        private ViewModelCacheService _viewModelCache { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        private Type _groupContainer { get; set; }
        public Type MainLayoutType { get; set; }

        /// <summary>
        /// Forces re-rendering of this rcc.
        /// [!IMPORTANT]
        /// > Forced re-rendering may impact client-side performance.
        /// > The method is automatically called when <see cref="Presentation"/> or <see cref="Context"/> property change.
        /// </summary>
        public void ForceRender()
        {
            shouldRender = true;
            this.StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            Type layoutType = TryLoadLayoutTypeFromProperty(Context);
            if (layoutType == null)
            {
                layoutType = TryLoadLayoutType(Context);
            }
            if (layoutType != null) MainLayoutType = layoutType;

            _groupContainer = TryLoadGroupTypeFromProperty(Context);
            if (_groupContainer == null) _groupContainer = TryLoadGroupType(Context);

            if (String.IsNullOrEmpty(Presentation)) Presentation = "";
            _viewModelCache.ResetCounter();
        }

        private IList<IRenderableComponent> PolledComponents { get; } = new List<IRenderableComponent>();

        private bool _pollingStarted = false;

        

        /// <summary>
        /// Method to build component name from passed parameters, which instance will be found in assembly.
        /// <param name="twinType">Type of passed object.</param>
        /// <param name="presentationType">Type of presentation.</param>
        /// </summary>

        internal IRenderableComponent ViewLocatorBuilder(Type twinType, ITwinElement twin, string presentationType, string presentationTemplate = null)
        {
            if (!string.IsNullOrWhiteSpace(presentationTemplate))
            {
                return ComponentService.GetComponent(presentationTemplate);
            }

            var namespc = twinType.Namespace;
            //set default namespace if is namespace of primitive types or empty
            if (string.IsNullOrEmpty(namespc) || namespc == "AXSharp.Connector.ValueTypes")
                namespc = "AXSharp.Presentation.Blazor.Controls.Templates";

            //end recursion if we are at object type
            if (twinType.FullName == "System.Object") return null;

            var pipeline = presentationType.Split('-');

            foreach (var item in pipeline)
            {
                var presentationName = item;
                if (presentationName.ToLower() == "base") presentationName = "";

                IRenderableComponent component = null;

                var overrideAttribute = AttributesHandler.GetRenderTemplateOverrideAttribute(twin);

                if (overrideAttribute != null && !string.IsNullOrWhiteSpace(overrideAttribute.TemplateOverrideName))
                {
                    // try to find override template component view
                    var buildedComponentName = $"{overrideAttribute.TemplateOverrideName}{presentationName}View";
                    component = ComponentService.GetComponent(buildedComponentName);
                }
                if(component == null) // if not set and foun override template
                {
                    // try to find component view
                    component = GetComponent(twinType, twin, presentationName, namespc);
                    if (component == null)
                    {
                        //if not found, look at predecessor
                        component = ViewLocatorBuilder(twinType.BaseType, twin, presentationName, PresentationTemplate);
                    }
                }

                if (component != null)
                {
                    if (component is RenderableComponentBase)
                    {
                        ((RenderableComponentBase)component).RccContainer = this;
                    }

                    return component;
                }
            }
            return null;
        }

        /// <summary>
        /// Method to build Generic component name from passed parameters, which instance will be found in assembly.
        /// <param name="twinType">Generic type.</param>
        /// <param name="presentationType">Type of presentation.</param>
        /// </summary>
        internal IRenderableComponent ViewEnumLocatorBuilder(Type twinType, string presentationType)
        {
            var namespc = twinType.Namespace;
            //set default namespace if is primitive type or unknown namespace
            if (string.IsNullOrEmpty(namespc) || namespc == "AXSharp.Connector.ValueTypes")
                namespc = "AXSharp.Presentation.Blazor.Controls.Templates";

            var genericTypeArg = GetGenericInfo(twinType).Item2;
            var pipeline = presentationType.Split('-');
            foreach (var item in pipeline)
            {
                var presentationName = item;
                string buildedComponentName;
                buildedComponentName = $"{namespc}.EnumeratorContainer{presentationName}View`1";
                var component = ComponentService.GetGenericComponent(buildedComponentName, genericTypeArg);
                if (component != null) return component;
            }
            return null;
        }
        private RenderFragment CreatePrimitiveComponent(ITwinPrimitive twinPrimitive, IRenderableComponent primitiveComponent) => __builder =>
        {
            if (primitiveComponent == null) return;
            __builder.OpenComponent(1, primitiveComponent.GetType());
            __builder.AddAttribute(2, "Onliner", twinPrimitive);
            __builder.AddAttribute(3, "IsReadOnly", HasReadAccess(twinPrimitive));
            __builder.AddAttribute(1, "RccContainer", this);
            __builder.AddAttribute(4, "PollingInterval", this.PollingInterval);
            __builder.AddAttribute(5, "HideLabel", HideLabel);
            __builder.CloseComponent();
        };

        private RenderFragment CreateComplexComponent(ITwinObject twin, IRenderableComponent component) => __builder =>
        {
            if (component == null) return;
            __builder.OpenComponent(0, component.GetType());
            if (component is IRenderableComplexComponentBase)
            {
                __builder.AddAttribute(1, "Component", twin);
                __builder.AddAttribute(1, "RccContainer", this);
            }
            else if (component is IRenderableViewModelBase)
            {
                __builder.AddAttribute(1, "TwinContainer", new TwinContainerObject(twin, 
                                                _viewModelCache.CreateCacheId(_navigationManager.Uri, twin.Symbol, 
                                                    Presentation.ToLower())));
            }
            else
            {
                __builder.AddAttribute(1, "Twin", twin);
            }

           

            __builder.CloseComponent();
        };

        private RenderFragment CreateEnumComponent(EnumeratorDiscriminatorAttribute enumDiscriminatorAttribute, ITwinPrimitive kid, IRenderableComponent component) => __builder =>
        {
            if (component == null) return;
            __builder.OpenComponent(0, component.GetType());
            __builder.AddAttribute(1, "Onliner", kid);
            __builder.AddAttribute(2, "EnumDiscriminatorAttribute", enumDiscriminatorAttribute);
            __builder.AddAttribute(3, "IsReadOnly", HasReadAccess(kid));
            __builder.AddAttribute(4, "HideLabel", HideLabel);
            __builder.CloseComponent();
        };

        internal Type TryLoadLayoutType(ITwinElement obj)
        {
            if (obj == null) return null;
            var typeAttribute = obj.GetType()
                  .GetCustomAttributes(true)
                  .ToList()
                  .Find(p => p is PresentationContainerAttribute) as PresentationContainerAttribute;

            if (typeAttribute != null)
                return Type.GetType(typeAttribute.FullTypeName);
            else
                return null;
        }
        internal Type TryLoadLayoutTypeFromProperty(ITwinElement obj)
        {
            if (obj == null) return null;
            var propertyInfo = AttributesHandler.GetPropertyViaSymbol(obj);
            if (propertyInfo != null)
            {
                var typeAttribute = propertyInfo.GetCustomAttributes(true)
                    .ToList()
                    .Where(p => p is PresentationContainerAttribute)
                    .FirstOrDefault()
                    as PresentationContainerAttribute;
                if (typeAttribute != null)
                    return Type.GetType(typeAttribute.FullTypeName);
            }
            return null;
        }


        internal Type TryLoadGroupTypeFromProperty(ITwinElement obj)
        {
            if (obj == null) return null;
            var propertyInfo = AttributesHandler.GetPropertyViaSymbol(obj);
            if (propertyInfo != null)
            {
                var typeAttribute = propertyInfo.GetCustomAttributes(true)
                    .ToList()
                    .Where(p => p is PresentationGroupAttribute)
                    .FirstOrDefault()
                    as PresentationGroupAttribute;
                if (typeAttribute != null)
                    return Type.GetType(typeAttribute.FullTypeName);
            }
            return null;
        }

        internal Type TryLoadGroupType(ITwinElement obj)
        {
            if (obj == null) return null;
            var typeAttribute = obj.GetType()
                  .GetCustomAttributes(true)
                  .ToList()
                  .Find(p => p is PresentationGroupAttribute) as PresentationGroupAttribute;

            if (typeAttribute != null)
                return Type.GetType(typeAttribute.FullTypeName);
            else
                return null;
        }


        internal Group CreateGroup(IEnumerator<ITwinElement> enumerator, ITwinElement child, Type groupLayout, Type parentLayout, bool canEnumerate)
        {
            var name = String.IsNullOrEmpty(child.AttributeName) ? child.GetSymbolTail() : child.AttributeName;
            var group = new Group(parentLayout, groupLayout, name);
            while (canEnumerate)
            {
                if (!HasRenderIgnoreAttribute(Presentation.ToString(), child))
                {
                    group.GroupElements.Add(child);
                }
                canEnumerate = enumerator.MoveNext();
                if (canEnumerate == false) break;
                child = enumerator.Current;
                var isEndOfGroup = TryLoadLayoutTypeFromProperty(child) != null;
                if (isEndOfGroup) break;
            }
            return group;
        }

        private bool HasRenderIgnoreAttribute(string presentationType, ITwinElement element)
        {
            var renderIngoreAttribute = AttributesHandler.GetIgnoreRenderingAttribute(element);
            if (renderIngoreAttribute != null)
            {
                return renderIngoreAttribute.HasIgnore(presentationType);
            }
            return false;
        }

        private bool IsEnumerator(ITwinElement obj)
        {
            var enumeratorDiscriminatorAttribute = AttributesHandler.GetEnumeratorDiscriminatorAttribute(obj);
            if (enumeratorDiscriminatorAttribute != null)
            {
                return true;
            }
            return false;
        }
        
        private (string, Type) GetGenericInfo(Type primitiveKidType)
        {
            if (primitiveKidType == null) return (null, null);

            var baseName = primitiveKidType?.BaseType?.Name;

            if (baseName == "OnlinerBase")
            {
                Type genericTypeArg = primitiveKidType.GenericTypeArguments[0];
                return (baseName, genericTypeArg);
            }
            else
            {
                return GetGenericInfo(primitiveKidType?.BaseType);
            }
        }
        private IRenderableComponent GetComponent(Type twinType, ITwinElement twin, string presentationName, string namespc)
        {
            switch (twin)
            {
                case OnlinerBase onliner:
                    if (twinType.IsGenericType)
                    {
                        var (baseName, genericTypeArg) = GetGenericInfo(twinType);
                        var onlinerName = $"{namespc}.{baseName}";
                        var onlinerBuildedComponentName = $"{onlinerName}{presentationName}View`1";
                        var genericComponent = ComponentService.GetGenericComponent(onlinerBuildedComponentName, genericTypeArg);
                        return genericComponent;
                    }
                    else
                    {
                        var onlinerName = $"{namespc}.{twinType.Name}";
                        var onlinerBuildedComponentName = $"{onlinerName}{presentationName}View";
                        var primitiveComponent = ComponentService.GetComponent(onlinerBuildedComponentName);
                        return primitiveComponent;
                    }
                default:
                    var name = $"{namespc}.{FilterOutGeneric(twinType.Name)}";
                    var buildedComponentName = $"{name}{presentationName}View";
                    var defaultComponent = ComponentService.GetComponent(buildedComponentName);
                    return defaultComponent;
            }
        }

        private string FilterOutGeneric(string twinTypeName)
        {
            int indexOfBacktick = twinTypeName.IndexOf('`');
            if (indexOfBacktick >= 0)
            {
                return twinTypeName.Substring(0, indexOfBacktick);
            }
            return twinTypeName;
        }

        private bool HasReadAccess(ITwinPrimitive kid) => kid.ReadWriteAccess == ReadWriteAccess.Read;

        private bool CheckForArray(ITwinObject twinObject)
        {
            var tail = twinObject.GetSymbolTail();
            if (tail.Last() == ']')
            {
                return true;
            }
            return false;
        }

        private string GetDisplayPresentationIfEmpty()
        {
            if (string.IsNullOrEmpty(Presentation))
            {
                return "Display";
            }
            return Presentation;

        }

        private bool shouldRender = false;
        private string _presentation;
        private string presentationTemplate = null;

        protected override bool ShouldRender()
        {
            if (shouldRender)
            {
                shouldRender = false;
                return true;
            }

            return false;
        }


        public virtual void Dispose()
        {
            _viewModelCache.ResetCounter();
        }
    }
}