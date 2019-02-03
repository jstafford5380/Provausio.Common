using System.Collections.Generic;
using System.Reflection;

namespace Provausio.Common
{
    public interface IAppDomain
    {
        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <returns></returns>
        IList<Assembly> GetAssemblies();
    }
}