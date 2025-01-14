// Build
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Build.FilteredSolution;

public class FilteredSolution
{
    public string path { get; set; }
    public List<string> projects { get; set; }

    public static SolutionRoot Parse(string pathToFilteredSolution)
    {
        var json = File.ReadAllText(pathToFilteredSolution);
        SolutionRoot solution = JsonConvert.DeserializeObject<SolutionRoot>(json);
        return solution;
    }
}