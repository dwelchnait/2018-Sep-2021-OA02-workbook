﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Models
{
    public record NamedColor(string RgbCode, string HexCode, string Name, int ColorType, bool Available)
    {

    }
}
