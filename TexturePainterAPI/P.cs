using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TexturePainterAPI;
internal static class P
{
    public static readonly int MaskTex = Shader.PropertyToID("_MaskTex");
    public static readonly int MaskTexA = Shader.PropertyToID("_MaskTexA");
    public static readonly int MaskTexB = Shader.PropertyToID("_MaskTexB");
    public static readonly int MaskTexC = Shader.PropertyToID("_MaskTexC");
    public static readonly int MaskColA = Shader.PropertyToID("_MaskColA");
    public static readonly int MaskColB = Shader.PropertyToID("_MaskColB");
    public static readonly int MaskColC = Shader.PropertyToID("_MaskColC");
}
