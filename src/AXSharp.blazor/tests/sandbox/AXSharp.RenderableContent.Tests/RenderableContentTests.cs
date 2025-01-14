// AXSharp.RenderableContent.Tests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using Xunit;
using Bunit;
using AXSharp.Presentation.Blazor.Controls.RenderableContent;
using Microsoft.Extensions.DependencyInjection;
using AXSharp.Presentation.Blazor.Services;
using System.IO;

namespace AXSharp.RenderableContent.Tests
{
    public class RenderableContentTests : TestContext, IClassFixture<RenderableContentTestsFixture>
    {

        private RenderableContentTestsFixture _fixture;
        private string _projectDirectory;
     
        public RenderableContentTests(RenderableContentTestsFixture fixture)
        {
            _fixture = fixture;
           Services.AddSingleton<ComponentService>();
           Services.AddSingleton<AttributesHandler>();
           Services.AddScoped<ViewModelCacheService>();
           _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            JSInterop.SetupModule("/_content/AXSharp.Presentation.Blazor.Controls/js/addToolTipsAndPopOvers.js");
            JSInterop.SetupModule("/_content/AXSharp.Presentation.Blazor.Controls/js/bootstrap.bundle.min.js");
        }

        private void Compare(string fileName, object twin, string presentation)
        {
            // Arrange
            var path = Path.Combine(_projectDirectory, "HtmlFiles", fileName);
            var html = File.ReadAllText(path);
            // Act
            var cut = RenderComponent<RenderableContentControl>(param => param
                .Add(p => p.Context, twin)
                .Add(p => p.Presentation, presentation));

            // File.WriteAllText(path, cut.Markup);

            // Assert
            cut.MarkupMatches(html);
        }

        [Fact]
        public void Render_prgWeatherStations_Tabs_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "prgWeatherStations.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.prgWeatherStations)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("prgWeatherStations.html", _fixture.Connector.prgWeatherStations, "Display");
        }

        [Fact]
        public void Render_stTestPrimitive_Wrap_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles","stTestPrimitive.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testPrimitive)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestPrimitive.html", _fixture.Connector.testingProgram.testPrimitive, "Display");
        }

        [Fact]
        public void Render_stTestComplex_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestComplex.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testComplex)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestComplex.html", _fixture.Connector.testingProgram.testComplex, "Display");
        }

        [Fact]
        public void Render_stTestEnum_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestEnum.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testEnum)
            //.Add(p => p.Presentation, "Control"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestEnum.html", _fixture.Connector.testingProgram.testEnum, "Control");
        }

        [Fact]
        public void Render_stTestRenderIgnore_Display_Date_Ignored()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestRenderIgnoreDisplay.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testRenderIgnore)
            //.Add(p => p.Presentation, "Control"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestRenderIgnoreDisplay.html", _fixture.Connector.testingProgram.testRenderIgnore, "Control");
        }

        [Fact]
        public void Render_stTestRenderIgnore_Control_Bool_Ignored()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestRenderIgnoreControl.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testRenderIgnore)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestRenderIgnoreControl.html", _fixture.Connector.testingProgram.testRenderIgnore, "Display");
        }

        [Fact]
        public void Render_stTestRenderIgnore_ShadowDisplayControl_DateAndBool_Ignored()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestRenderIgnoreShadowDisplayControl.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testRenderIgnore)
            //.Add(p => p.Presentation, "ShadowDisplay-ShadowControl"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestRenderIgnoreShadowDisplayControl.html", _fixture.Connector.testingProgram.testRenderIgnore, "ShadowDisplay-ShadowControl");
        }


        [Fact]
        public void Render_stTestEmpty_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestEmpty.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testEmpty)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestEmpty.html", _fixture.Connector.testingProgram.testEmpty, "Display");
        }

        [Fact]
        public void Render_stTestLayoutOverwrite_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutOverWrite.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayoutOverwrite)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutOverWrite.html", _fixture.Connector.testingProgram.testLayoutOverwrite, "Display");
        }

        [Fact]
        public void Render_stTestMixed_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestMixed.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testMixed)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestMixed.html", _fixture.Connector.testingProgram.testMixed, "Display");
        }

        [Fact]
        public void Render_stTestMultipleLayouts_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestMultipleLayouts.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testMultipleLayouts)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestMultipleLayouts.html", _fixture.Connector.testingProgram.testMultipleLayouts, "Display");
        }

        [Fact]
        public void Render_stTestSimple_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestSimple.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testSimple)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestSimple.html", _fixture.Connector.testingProgram.testSimple, "Display");
        }

        [Fact]
        public void Render_stTestWithoutLayouts_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestWithoutLayouts.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testWithoutLayouts)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestWithoutLayouts.html", _fixture.Connector.testingProgram.testWithoutLayouts, "Display");
        }

        [Fact]
        public void Render_stTestSimpleNested_Success()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestSimpleNested.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testSimpleNested)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestSimpleNested.html", _fixture.Connector.testingProgram.testSimpleNested, "Display");
        }

        [Fact]
        public void Render_stTestMultipleNested_TabsAndComples_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestMultipleNested.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testMultipleNested)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestMultipleNested.html", _fixture.Connector.testingProgram.testMultipleNested, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_Stack_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsStack.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_stack)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsStack.html", _fixture.Connector.testingProgram.testLayouts.test_stack, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_Wrap_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsWrap.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_wrap)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsWrap.html", _fixture.Connector.testingProgram.testLayouts.test_wrap, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_Tabs_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsTabs.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_tabs)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsTabs.html", _fixture.Connector.testingProgram.testLayouts.test_tabs, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_Uniform_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsUniform.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_uniform)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsUniform.html", _fixture.Connector.testingProgram.testLayouts.test_uniform, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_GroupBox_Stack_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsGroupBoxStack.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_groupbox_stack)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsGroupBoxStack.html", _fixture.Connector.testingProgram.testLayouts.test_groupbox_stack, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_Border_Stack_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsBorderStack.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_border_stack)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsBorderStack.html", _fixture.Connector.testingProgram.testLayouts.test_border_stack, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_GroupBox_Wrap_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsGroupBoxWrap.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_groupbox_wrap)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsGroupBoxWrap.html", _fixture.Connector.testingProgram.testLayouts.test_groupbox_wrap, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_Border_Wrap_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsBorderWrap.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_border_wrap)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsBorderWrap.html", _fixture.Connector.testingProgram.testLayouts.test_border_wrap, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_GroupBox_Tabs_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsGroupBoxTabs.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_groupbox_tabs)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsGroupBoxTabs.html", _fixture.Connector.testingProgram.testLayouts.test_groupbox_tabs, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_Border_Tabs_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsBorderTabs.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_border_tabs)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsBorderTabs.html", _fixture.Connector.testingProgram.testLayouts.test_border_tabs, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_GroupBox_UniformGrid_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsGroupBoxUniformGrid.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_groupbox_uniformGrid)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsGroupBoxUniformGrid.html", _fixture.Connector.testingProgram.testLayouts.test_groupbox_uniformGrid, "Display");
        }

        [Fact]
        public void Render_stTestLayouts_Border_UniformGrid_Successfull()
        {
            //// Arrange
            //var path = Path.Combine(_projectDirectory, "HtmlFiles", "stTestLayoutsBorderUniformGrid.html");
            //var html = File.ReadAllText(path);
            //// Act
            //var cut = RenderComponent<RenderableContentControl>(param => param
            //.Add(p => p.Context, _fixture.Connector.testingProgram.testLayouts.test_border_uniformGrid)
            //.Add(p => p.Presentation, "Display"));
            //// Assert
            //cut.MarkupMatches(html);

            Compare("stTestLayoutsBorderUniformGrid.html", _fixture.Connector.testingProgram.testLayouts.test_border_uniformGrid, "Display");
        }



        //[Fact]
        //public void GetGroupElements_TestEmpty_NoLayouts_OneGroupSuccesfull()
        //{
        //    //Arrange
        //    var testPrimitive = _fixture.Connector.testingProgram.testEmpty;
        //    var expectedGroupCount = 0;


        //    //Act
        //    var parentLayout = _fixture.RenderableContent.TryLoadLayoutType(testPrimitive);
        //    var groupedElements = _fixture.RenderableContent.GroupElementsByLayouts(testPrimitive, parentLayout);

        //    //Assert
        //    Assert.NotNull(groupedElements);
        //    Assert.Equal(expectedGroupCount, groupedElements.Groups.Count());
        //    //Assert.Equal(expectedIxElementsInGroupCount, groupedElements.Count);
        //}


        //[Fact]
        //public void Render_testingProgram_testRenderIgnoreAttributes_Display_KidsCountEqual()
        //{
        //    //Arrange
        //    var __builder = new RenderTreeBuilder();
        //    var testRenderIgnore = _fixture.Connector.testingProgram.testRenderIgnore;
        //    _fixture.RenderableContent.Presentation = Presentation.Blazor.Enums.PresentationType.Display;
        //    var ignoredPropCount = 2;
        //    var expectedCount = testRenderIgnore.GetKids().Count() - ignoredPropCount;

        //    //Act
        //    var renderer = _fixture.RenderableContent.RenderComponent(testRenderIgnore);
        //    renderer.Invoke(__builder);
        //    var frames = __builder.GetFrames();
        //    var ixAttributeCount = frames.Array.AsEnumerable().Where(x => x.AttributeName == AttributeNameIx).Count();

        //    //Assert
        //    Assert.Equal(ixAttributeCount, expectedCount);

        //}

        [Fact]
        public void Render_stTestRenderOverridedSimpleStruct_Success()
        {
            Compare("stTestRenderOverridedSimpleStruct.html", _fixture.Connector.testingProgram.testRenderOverridedSimpleStruct, "Display");
        }

        [Fact]
        public void Render_stTestRenderInnerOverridedStruct_Success()
        {
            Compare("stTestRenderInnerOverridedStruct.html", _fixture.Connector.testingProgram.testRenderInnerOverridedStruct, "Display");
        }

        [Fact]
        public void Render_stTestRenderOverridedTag_Success()
        {
            Compare("stTestRenderOverridedTag.html", _fixture.Connector.testingProgram.testRenderOverridedTag, "Display");
        }
    }
}
