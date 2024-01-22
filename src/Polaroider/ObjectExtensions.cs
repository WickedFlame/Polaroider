using System;

namespace Polaroider
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Map a object to another object. Can be used to make simpler snapshots
        /// </summary>
        /// <typeparam name="Tout"></typeparam>
        /// <typeparam name="Tin"></typeparam>
        /// <param name="obj"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static Tout MapTo<Tout, Tin>(this Tin obj, Func<Tin, Tout> factory)
        {
            return factory(obj);
        }
    }
}
