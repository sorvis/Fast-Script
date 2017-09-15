using System;
using System.Collections.Generic;
using System.Linq;

namespace Fast_Script.PresenterFolder
{
    public static class ReferenceListExtensions
    {
        public static string GetReferences(this List<ReferenceItem> items)
        {
            var references = String.Join(Environment.NewLine, items.Select(x => x.ToString()));
            return references;
        }
    }
}
