using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Connector
{
    /// <summary>
    /// Provides a series of extension methods to work with POCO objects.
    /// </summary>
    public static class IPlainExtensions
    {
        /// <summary>
        /// Copies data from the POCO object into respective shadows of the twin object.
        /// </summary>
        /// <typeparam name="T">Source POCO type</typeparam>
        /// <param name="plain">Source POCO instance</param>
        /// <param name="twin">Target twin instance.</param>
        public static void ToShadow<T>(this T plain, ITwinObject twin) where T : IPlain
        {
            twin.PlainToShadow(plain);
        }

        /// <summary>
        /// Copies data from twin shadow object into a POCO object.
        /// </summary>
        /// <typeparam name="T">POCO object type</typeparam>
        /// <param name="plain">Target POCO object.</param>
        /// <param name="twin">Source twin object.</param>
        /// <returns>New instance of POCO object populated with data from the shadows of the twin object.</returns>
        public static async Task<T> FromShadow<T>(this T plain, ITwinObject twin) where T : IPlain
        {
            return await twin.ShadowToPlain<T>();
        }

        /// <summary>
        /// Copies data from plain object to respective twin object online data.
        /// </summary>
        /// <typeparam name="T">POCO object type</typeparam>
        /// <param name="plain">Source POCO object</param>
        /// <param name="twin">Target twin object.</param>
        /// <returns></returns>
        public static async Task ToOnline<T>(this T plain, ITwinObject twin) where T : IPlain
        {
            await twin.PlainToOnline<T>(plain);
        }

        /// <summary>
        /// Copies data from online of a twin object into a new instance of respective POCO object
        /// </summary>
        /// <typeparam name="T">POCO object type</typeparam>
        /// <param name="plain">Target POCO object</param>
        /// <param name="twin">Source online twin object.</param>
        /// <returns>New instance of a POCO object populated by the online data.</returns>
        public static async Task<T> FromOnline<T>(this T plain, ITwinObject twin) where T : IPlain
        {
            return await twin.OnlineToPlain<T>();
        }
    }
}
