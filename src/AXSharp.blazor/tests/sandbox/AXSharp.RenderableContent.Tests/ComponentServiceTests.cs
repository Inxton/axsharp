// AXSharp.RenderableContent.Tests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Presentation.Blazor.Controls.RenderableContent;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Components.Rendering;
using Xunit;
using System.Runtime.InteropServices;

namespace AXSharp.RenderableContent.Tests
{
    public class ComponentServiceTests : IClassFixture<RenderableContentTestsFixture>
    {
        private RenderableContentTestsFixture _fixture;
        private string _display = "Display";
        private string _control = "Control";
        private string _shadowDisplay = "ShadowDisplay";
        private string _shadowControl = "ShadowControl";
        private string _templatesNamespace = "AXSharp.Presentation.Blazor.Controls.Templates";
        public ComponentServiceTests(RenderableContentTestsFixture fixture)
        {
            this._fixture = fixture;
        }


        //GenericComponents
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BaseGenericControlComponent_NotNullAreEqual()
        {
            //Arrange
            Type typeArg = typeof(int);
            var name = "OnlinerBaseControlView`1";
            var buildedComponentName =  $"{_templatesNamespace}.{name}";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetGenericComponent(buildedComponentName, typeArg);
            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseControlView`1", component.GetType().Name);
        }

        [Fact]
        public void Get_BaseGenericIntDisplayComponent_NotNullAreEqual()
        {
            //Arrange
            Type typeArg = typeof(int);
            var name = "OnlinerBaseDisplayView`1";
            var buildedComponentName = $"{_templatesNamespace}.{name}";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetGenericComponent(buildedComponentName, typeArg);
            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseDisplayView`1", component.GetType().Name);
        }
        //-----------------------------------------------------------------//
        //BaseComponents Display
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BoolDisplayView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerBool";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_display}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_DateTimeDisplayView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerDateTime";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_display}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_DateDisplayView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerDate";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_display}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_TimeOfDayDisplayView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerTimeOfDay";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_display}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        //-----------------------------------------------------------------//
        //BaseComponents Control
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BoolControlView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerBool";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_control}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_DateTimeControlView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerDateTime";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_control}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_DateControlView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerDateTime";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_control}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_TimeOfDayControlView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerTimeOfDay";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_control}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        //-----------------------------------------------------------------//
        //BaseComponents ShadowDisplay
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BoolShadowDisplayView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerBool";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_shadowDisplay}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_DateTimeShadowDisplayView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerDateTime";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_shadowDisplay}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_DateShadowDisplayView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerDate";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_shadowDisplay}View";
            //Act
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_TimeOfDayShadowDisplayView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerTimeOfDay";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_shadowDisplay}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        //-----------------------------------------------------------------//
        //BaseComponents ShadowControl
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BoolShadowControlView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerBool";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_shadowControl}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_DateTimeShadowControlView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerDateTime";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_shadowControl}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_DateShadowControlView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerDate";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_shadowControl}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_TimeOfDayShadowControlView_NotNullAreEqual()
        {
            //Arrange
            var name = "OnlinerTimeOfDay";
            var buildedComponentName = $"{_templatesNamespace}.{name}{_shadowControl}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal(buildedComponentName, expected);

        }
        [Fact]
        public void Get_BoolControlComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testBool.GetType();

            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.testBool, _control);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBoolControlView", component.GetType().Name);

        }

        [Fact]
        public void Get_BoolDisplayComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testBool.GetType();


            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.testBool, _display);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBoolDisplayView", component.GetType().Name);

        }
        [Fact]
        public void Get_DateTimeDisplayComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.TestDateTime.GetType();


            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.TestDateTime, _display);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerDateTimeDisplayView", component.GetType().Name);

        }
        [Fact]
        public void Get_ComponentWithinPipeline_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.TestDateTime.GetType();

            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.TestDateTime,"Base-Display");

            //Assert
            Assert.NotNull(component);
            var expected = $"{_templatesNamespace}.{component.GetType().Name}";
            Assert.Equal("OnlinerDateTimeDisplayView", component.GetType().Name);

        }

        [Fact]
        public void Get_ComponentWithinPipelineRandomString_Unsuccessfull()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.TestDateTime.GetType();
            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.TestDateTime, "Bases-Displays");
            //Assert
            Assert.Null(component);


        }
        [Fact]
        public void Get_ComponentWithinPipelineEmptyString_Unsuccessfull()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.TestDateTime.GetType();
            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.TestDateTime, "");
            //Assert
            Assert.Null(component);

        }
        //-----------------------------------------------------------------//
        // Test for getting generics
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_GenericBaseControlComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testInteger.GetType();

            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.testInteger, _control);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseControlView`1", component.GetType().Name);

        }
        [Fact]
        public void Get_GenericBaseDisplayComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testInteger.GetType();

            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.testInteger, _display);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseDisplayView`1", component.GetType().Name);

        }
        [Fact]
        public void Get_GenericBaseControlWithinPipeline_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testInteger.GetType();

            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _fixture.Connector.testingProgram.testPrimitive.testInteger, "Base-Manual-Service-Control");

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseControlView`1", component.GetType().Name);
        }

        [Fact]
        public void Get_GenericEnumControlComponentWithBuilderPipeline_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testEnum.GetType();
            //Act
            var component = _fixture.RenderableContent.ViewEnumLocatorBuilder(testedObjType, "ahoj-cau-control");
            //Assert
            Assert.NotNull(component);
            Assert.Equal("EnumeratorContainerControlView`1", component.GetType().Name);

        }

        [Fact]
        public void Get_GenericEnumDisplayComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testEnum.GetType();
            //Act
            var component = _fixture.RenderableContent.ViewEnumLocatorBuilder(testedObjType, _display);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("EnumeratorContainerDisplayView`1", component.GetType().Name);

        }

        [Fact]
        public void Get_GenericEnumRandomString_Unsuccessfull()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testEnum.GetType();
            //Act
            var component = _fixture.RenderableContent.ViewEnumLocatorBuilder(testedObjType, "fadfa=-faf");
            //Assert
            Assert.Null(component);

        }
        //-----------------------------------------------------------------//

    }
}