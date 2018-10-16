using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sam.Api.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Alchemint.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Sam.Api
{
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var currentAssembly = typeof(GenericTypeControllerFeatureProvider).Assembly;
            var assem = EntityFactory.GetAssembly(EntityFactory.GetAssemblyFilePath());


            var candidates = assem.GetExportedTypes(); // .Any(); // .Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any());

            var entityClasses = (from y in candidates
                     where y.GetConstructor(Type.EmptyTypes) != null
                     select y
              ).ToArray<Type>();

            

            foreach (var candidate in entityClasses)
            {
                feature.Controllers.Add(typeof(BaseController<>).MakeGenericType(candidate).GetTypeInfo());
            }
        }
    }
}
