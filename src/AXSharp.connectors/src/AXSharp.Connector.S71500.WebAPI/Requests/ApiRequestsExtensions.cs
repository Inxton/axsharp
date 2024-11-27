using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebApi;
using Newtonsoft.Json;

namespace AXSharp.Connector.S71500.WebAPI
{
    internal static class ApiRequestsExtensions
    {
        public static IEnumerable<IEnumerable<T>> SegmentReadRequest<T>(this IEnumerable<T> data, int maxPayloadSize) where T : IWebApiPrimitive
        {
            var partialRequests = data.ToArray(); 
            var segmentLengths = new int[partialRequests.Length];
            int currentLength = 0;
            int currentSegmentStart = 0;

            // Calculate the length of each segment and the total length of each partial request
            for (int i = 0; i < partialRequests.Length; i++)
            {
                int partialRequestLength = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(partialRequests[i].PlcReadRequestData)).Length;
                segmentLengths[i] = partialRequestLength;

                if (currentLength + partialRequestLength > maxPayloadSize)
                {
                    // Start a new segment if adding the current partial request exceeds the max payload size
                    yield return partialRequests.Skip(currentSegmentStart).Take(i - currentSegmentStart); 
                    currentSegmentStart = i;
                    currentLength = 0;
                }

                currentLength += partialRequestLength;
            }

            // Yield the final segment
            if (currentSegmentStart < partialRequests.Length)
            {
                yield return partialRequests.Skip(currentSegmentStart); 
            }
        }

        public static IEnumerable<IEnumerable<T>> SegmentWriteRequest<T>(this IEnumerable<T> data, int maxPayloadSize) where T : IWebApiPrimitive
        {
            var partialRequests = data.ToArray(); 
            var segmentLengths = new int[partialRequests.Length];
            int currentLength = 0;
            int currentSegmentStart = 0;

            // Calculate the length of each segment and the total length of each partial request
            for (int i = 0; i < partialRequests.Length; i++)
            {
                int partialRequestLength = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(partialRequests[i].PlcWriteRequestData)).Length;
                segmentLengths[i] = partialRequestLength;

                if (currentLength + partialRequestLength > maxPayloadSize)
                {
                    // Start a new segment if adding the current partial request exceeds the max payload size
                    yield return partialRequests.Skip(currentSegmentStart).Take(i - currentSegmentStart); 
                    currentSegmentStart = i;
                    currentLength = 0;
                }

                currentLength += partialRequestLength;
            }

            // Yield the final segment
            if (currentSegmentStart < partialRequests.Length)
            {
                yield return partialRequests.Skip(currentSegmentStart); 
            }
        }
    }

  
}
