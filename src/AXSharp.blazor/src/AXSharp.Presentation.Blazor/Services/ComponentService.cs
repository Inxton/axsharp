// AXSharp.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AXSharp.Abstractions.Presentation;
using AXSharp.Presentation.Blazor.Attributes;
using AXSharp.Presentation.Blazor.Interfaces;

namespace AXSharp.Presentation.Blazor.Services
{
    /// <summary>
    ///  Component service class, which contains methods to load, find, and access blazor components dynamically within framework.
    /// </summary>
    public sealed class ComponentService : IComponentService
    {
        /// <summary>
        /// Creates new instance of <see cref="ComponentService"/>.
        /// </summary>
        public ComponentService()
        {
            PresentationProvider.Create(new BlazorLayoutProvider(), new BlazorGroupLayoutProvider());
            LoadComponents();
        }

        public IDictionary<string, Type> Components { get; private set; } = new Dictionary<string, Type>();
        private bool _isEntryAssemblyPresent { get; set; }
        /// <summary>
        ///  Method to get dynamically instance of blazor component.
        /// <param name="fullName">Full name of the component.</param>
        /// </summary>
        public IRenderableComponent GetComponent(string fullName)
        {
            Type foundedType;
            var isFound = Components.TryGetValue(fullName.ToLower(), out foundedType);
            if (isFound)
            {
                return (IRenderableComponent)Activator.CreateInstance(foundedType);
            }
            else return null;
        }
        /// <summary>
        ///  Method to get dynamically generic instance of blazor component.
        /// <param name="fullName">Full name of the component.</param>
        /// <param name="typeArg">Generic type.</param>
        /// </summary>
        public IRenderableComponent GetGenericComponent(string fullName, Type typeArg)
        {
            Type foundedType;
            var isFound = Components.TryGetValue(fullName.ToLower(), out foundedType);
            if (isFound)
            {
                Type genericType = foundedType.MakeGenericType(typeArg);
                return (IRenderableComponent)Activator.CreateInstance(genericType);
            }
            else return null;
        }
        /// <summary>
        ///  Method to Load all implemented components types from assembly.
        /// </summary>
        public void LoadComponents()
        {
            var loadedAssemblies = LoadAssemblies();
            // get assemblies with renderable attribute
            var filteredAssemblies = loadedAssemblies.Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(RenderableBlazorAssemblyAttribute)));
            var entryAssembly = Assembly.GetEntryAssembly();
            foreach (var assembly in filteredAssemblies)
            {
                //if entry assembly contains custom components, skip them and add them at the end
                if (assembly.FullName == entryAssembly.FullName)
                {
                    _isEntryAssemblyPresent = true;
                    continue;
                }
                //get assembly types which contains renderable interface
                AddTypesToDictionary(assembly);
            }

            if (_isEntryAssemblyPresent)
            {
                AddTypesToDictionary(entryAssembly);
            }
        }

        private void AddTypesToDictionary(Assembly assembly)
        {
            var types = GetTypesWithInterface(assembly);
            foreach (var type in types)
            {
                //calculate id
                var id = $"{type.FullName.ToLower()}";
                try
                {
                    Components.Add(id, type);
                }
                catch (ArgumentException)
                {
                    //overwrite view if is already present
                    Components[id] = type;
                }
            }
        }
        private List<Assembly> LoadAssemblies()
        {
            var loadedAssemblies = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Prepend(Assembly.GetExecutingAssembly())
                .ToList();
            var loadedPaths = loadedAssemblies.Where(p => !p.IsDynamic).Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path =>
            {
                try
                {
                    loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path)));
                }
                catch (System.BadImageFormatException)
                {
                    // Ignore
                }
                catch (System.IO.FileLoadException)
                {
                    // Ignore
                }
            });
            return loadedAssemblies;
        }

        private IEnumerable<Type> GetTypesWithInterface(Assembly asm)
        {
            return GetLoadableTypes(asm).Where(typeof(IRenderableComponent).IsAssignableFrom).ToList();
        }

        private IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
