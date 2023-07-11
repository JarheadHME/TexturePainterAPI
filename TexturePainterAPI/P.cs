using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TexturePainterAPI;
internal static class P
{
    public static readonly int BaseTexture = Shader.PropertyToID("_BaseTexture");
    public static readonly int MaskA = Shader.PropertyToID("_MaskA");
    public static readonly int MaskB = Shader.PropertyToID("_MaskB");
    public static readonly int MaskC = Shader.PropertyToID("_MaskC");
    public static readonly int TintA = Shader.PropertyToID("_TintA");
    public static readonly int TintB = Shader.PropertyToID("_TintB");
    public static readonly int TintC = Shader.PropertyToID("_TintC");
    public static readonly int Result = Shader.PropertyToID("_Result");
}
