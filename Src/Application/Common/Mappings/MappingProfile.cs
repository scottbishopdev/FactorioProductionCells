using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace FactorioProductionCells.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Pass the currently executing assembly to our function.
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            // This is going through the provided assembly and finding all classes that implement IMapFrom
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();
            
            foreach(var type in types)
            {
                var instance = Activator.CreateInstance(type);

                // This is looking for a method called "Mapping" on the classes that implement IMapFrom<>. If they don't implement it, it grabs a generic one from IMapFrom<>.
                // TODO: What on earth is the backtick in there for?
                var methodInfo = type.GetMethod("Mapping") ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
