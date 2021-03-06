﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
