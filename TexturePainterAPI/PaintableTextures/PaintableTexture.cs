using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TexturePainterAPI.PaintableTextures;
public abstract class PaintableTexture
{
    public abstract Texture CurrentTexture { get; }
    public abstract Texture CreateCopy();
}
