// AXSharp.CompilerTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using Xunit;
using AXSharp.Compiler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AXSharp.CompilerTests
{
    public class CompanionInfoTests
    {
        [Fact]
        public void CompanionInfo_serializes_UiId_and_UiVersion_when_set()
        {
            var info = new CompanionInfo { Id = "My.Twin", Version = "1.0.0", UiId = "My.Twin.UI", UiVersion = "1.0.0" };
            var json = JObject.Parse(JsonConvert.SerializeObject(info));

            Assert.Equal("My.Twin.UI", json["UiId"]?.Value<string>());
            Assert.Equal("1.0.0", json["UiVersion"]?.Value<string>());
        }

        [Fact]
        public void CompanionInfo_omits_UiId_and_UiVersion_from_json_when_null()
        {
            var info = new CompanionInfo { Id = "My.Twin", Version = "1.0.0" };
            var json = JObject.Parse(JsonConvert.SerializeObject(info));

            Assert.False(json.ContainsKey("UiId"));
            Assert.False(json.ContainsKey("UiVersion"));
        }

        [Fact]
        public void CompanionInfo_round_trips_through_file_with_ui_fields()
        {
            var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
            try
            {
                var original = new CompanionInfo { Id = "My.Twin", Version = "2.3.4", UiId = "My.Twin.UI", UiVersion = "2.3.4" };
                CompanionInfo.ToFile(original, tempFile);
                var restored = CompanionInfo.TryFromFile(tempFile);

                Assert.NotNull(restored);
                Assert.Equal(original.Id, restored!.Id);
                Assert.Equal(original.Version, restored.Version);
                Assert.Equal(original.UiId, restored.UiId);
                Assert.Equal(original.UiVersion, restored.UiVersion);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void CompanionInfo_round_trips_through_file_without_ui_fields()
        {
            var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
            try
            {
                var original = new CompanionInfo { Id = "My.Twin", Version = "1.0.0" };
                CompanionInfo.ToFile(original, tempFile);
                var restored = CompanionInfo.TryFromFile(tempFile);

                Assert.NotNull(restored);
                Assert.Equal(original.Id, restored!.Id);
                Assert.Equal(original.Version, restored.Version);
                Assert.Null(restored.UiId);
                Assert.Null(restored.UiVersion);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void CompanionInfo_TryFromFile_returns_null_when_file_missing()
        {
            var result = CompanionInfo.TryFromFile(Path.Combine(Path.GetTempPath(), "nonexistent_companion.json"));
            Assert.Null(result);
        }

        [Fact]
        public void CompanionInfo_deserializes_legacy_json_without_ui_fields()
        {
            var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
            try
            {
                // Simulate a legacy axsharp.companion.json that has no UiId/UiVersion
                File.WriteAllText(tempFile, "{\"Id\":\"Legacy.Twin\",\"Version\":\"0.9.0\"}");
                var result = CompanionInfo.TryFromFile(tempFile);

                Assert.NotNull(result);
                Assert.Equal("Legacy.Twin", result!.Id);
                Assert.Equal("0.9.0", result.Version);
                Assert.Null(result.UiId);
                Assert.Null(result.UiVersion);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void CompanionInfo_with_UiId_derived_from_csproj_PackageId_element()
        {
            // Simulate a .csproj that declares a PackageId
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            var csprojPath = Path.Combine(tempDir, "MyApp.UI.csproj");
            File.WriteAllText(csprojPath, "<Project><PropertyGroup><PackageId>Acme.App.UI</PackageId></PropertyGroup></Project>");

            var companionFile = Path.Combine(tempDir, CompanionInfo.COMPANIONS_FILE_NAME);
            try
            {
                // Build a companion as GenerateCompanionData would when UiHostProject csproj exists
                var info = new CompanionInfo { Id = "Acme.Twin", Version = "1.2.3", UiId = "Acme.App.UI", UiVersion = "1.2.3" };
                CompanionInfo.ToFile(info, companionFile);
                var restored = CompanionInfo.TryFromFile(companionFile);

                Assert.Equal("Acme.App.UI", restored!.UiId);
                Assert.Equal("1.2.3", restored.UiVersion);
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Fact]
        public void CompanionInfo_with_UiId_uses_apax_name_plus_UI_suffix_when_csproj_missing()
        {
            // When the UiHostProject csproj does not exist, UiId = apaxName + ".UI"
            const string apaxName = "my-lib";
            var expectedUiId = apaxName + ".UI";

            var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
            try
            {
                var info = new CompanionInfo { Id = "my-lib", Version = "0.1.0", UiId = expectedUiId, UiVersion = "0.1.0" };
                CompanionInfo.ToFile(info, tempFile);
                var restored = CompanionInfo.TryFromFile(tempFile);

                Assert.Equal(expectedUiId, restored!.UiId);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}
