﻿using Imya.Models;
using Imya.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Imya.Validation
{
    internal class CyclicDependencyValidator : IModValidator
    {
        public void Validate(IEnumerable<Mod> changed, IReadOnlyCollection<Mod> all)
        {
            foreach (Mod x in all)
                x.Attributes.RemoveAttributesByType(AttributeType.CyclicDependency);
            foreach (Mod x in changed)
            {
                var cyclics = CyclicDependencies(x, all);
                if (cyclics.Count() > 0)
                {
                    x.Attributes.Add(CyclicDependencyAttributeFactory.Get(cyclics));   
                }
            }
        }

        private IEnumerable<Mod> CyclicDependencies(Mod x, IReadOnlyCollection<Mod> others)
        {
            if (!x.IsActive)
                return Enumerable.Empty<Mod>();

            return others.Where(y =>
                y.IsActive && (y.Modinfo?.LoadAfterIds?.Contains(x.ModID) ?? false)
                && (x.Modinfo?.LoadAfterIds?.Contains(y.ModID) ?? false));
        }
    }
}
