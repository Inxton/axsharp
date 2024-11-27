// AXSharp.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Text;
using AXSharp.Connector;

namespace AXSharp.Presentation.Blazor.Services
{
    public class ViewModelCacheService
    {
        public ViewModelCacheService()
        {
            ViewModelCache = new Dictionary<string, RenderableViewModelBase>();
        }
    

        private IDictionary<string, RenderableViewModelBase> ViewModelCache { get; set; }


        public RenderableViewModelBase GetViewModelFromCache(string id)
        {
            RenderableViewModelBase viewModel;
            if (ViewModelCache.TryGetValue(id, out viewModel))
            {
                return viewModel;
            }
            return null;
        }

        public void AddViewModelToCache(string id, RenderableViewModelBase viewModel)
        {
            ViewModelCache.Add(id, viewModel);
        }
        public void ClearViewModelCache()
        {
            ViewModelCache.Clear();
        }

        private int _pageRenderingCounter { get; set; }


        public void ResetCounter()
        {
            if(_pageRenderingCounter != 0)
                _pageRenderingCounter = 0;
        }

        public string CreateCacheId(string uri, string symbol, string presentation)
        {
           return $"{uri}_{symbol}_{presentation.ToLower()}_{_pageRenderingCounter++}";
        }
  
    

    }
    

}
