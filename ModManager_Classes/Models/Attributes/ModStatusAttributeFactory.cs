﻿using Imya.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imya.Models.Attributes
{
    // TODO obsolete may be a different kind of state more similar to other compatibility issues.
    //      Obsolete may also be detected on startup, not only after zip installation.
    public enum ModStatus
    {
        Default,
        New,
        Updated,
        Obsolete
    }

    public class ModStatusAttributeFactory
    {
        private static Dictionary<ModStatus, IAttribute> static_attribs = new()
        {
            {
                ModStatus.Default,
                new ModStatusAttribute()
                {
                    AttributeType = AttributeType.ModStatus,
                    Description = new SimpleText("Mod Status Attribute"),
                    Status = ModStatus.Default
                }
            },
            {
                ModStatus.New,
                new ModStatusAttribute()
                {
                    AttributeType = AttributeType.ModStatus,
                    Description = TextManager.Instance.GetText("ATTRIBUTE_STATUS_NEW"),
                    Status = ModStatus.New
                }
            },
            {
                ModStatus.Updated,
                new ModStatusAttribute()
                {
                    AttributeType = AttributeType.ModStatus,
                    Description = TextManager.Instance.GetText("ATTRIBUTE_STATUS_UPDATE"),
                    Status = ModStatus.Updated
                }
            },
            {
                ModStatus.Obsolete,
                new ModStatusAttribute()
                {
                    AttributeType = AttributeType.ModStatus,
                    Description = TextManager.Instance.GetText("ATTRIBUTE_STATUS_OBSOLETE"),
                    Status = ModStatus.Obsolete
                }
            }
        };

        public static IAttribute Get(ModStatus status)
        {
            return static_attribs[status];
        }
    }
}
