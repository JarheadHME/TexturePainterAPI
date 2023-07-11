using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TexturePainterAPI.PaintableTextures;
public abstract class PaintableTexture : IDisposable
{
    public abstract Texture CurrentTexture { get; }
    public abstract Texture CreateCopy();
    public abstract void Dispose();
}
