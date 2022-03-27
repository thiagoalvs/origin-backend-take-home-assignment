using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OriginTechDemo.Shared.Helpers
{
    public class EnumHelper
    {
        public static List<string> GetNames<T>(T enumType, bool toLower = true, bool ignoreNone = true) where T : Type
        {
            var names = Enum.GetNames(enumType).ToList();

            if (ignoreNone)
                names.Remove("None");

            if (toLower)
                names = names.Select(name => name.ToLower()).ToList();

            return names;
        }

        public static string StringfyNames<T>(T enumType, bool toLower = true, bool ignoreNone = true) where T : Type
        {
            var names = Enum.GetNames(enumType).ToList();

            if (ignoreNone)
                names.Remove("None");

            if (toLower)
                names = names.Select(name => name.ToLower()).ToList();

            return JsonSerializer.Serialize(names).Replace("\"", "");

        }
    }
}
