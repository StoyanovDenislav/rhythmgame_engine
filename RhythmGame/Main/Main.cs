
using RhythmGame.HitObjectInterface;
using RhythmGame.TextureBindings;
using System.Reflection;


namespace RhythmGame.Main
{

    public class ClassCollector
    {
        public List<Type> GetDerivedClasses<T>() where T : IHitObject
        {
            List<Type> derivedClasses = new List<Type>();

            Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
            Type baseType = typeof(T); // Get the base class type

            // Get all the types in the assembly that are derived from the base class
            var derivedTypes = assembly.GetTypes()
                .Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract);

            derivedClasses.AddRange(derivedTypes);

            return derivedClasses;
        }
    }

    
}

