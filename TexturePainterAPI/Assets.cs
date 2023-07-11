using GTFO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TexturePainterAPI;
internal static class Assets
{
    public static ComputeShader Tint3MaskCS;
    public static int Tint3MaskKernel;

    public static void Init()
    {
        Tint3MaskCS = AssetAPI.GetLoadedAsset<ComputeShader>("Assets/Modding/TexturePainter/3MaskTint.compute");
        Tint3MaskKernel = Tint3MaskCS.FindKernel("CSMain");
    }
}
