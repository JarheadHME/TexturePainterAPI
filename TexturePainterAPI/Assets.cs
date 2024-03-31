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
    public static Material Tint3MaskMat;
    public static Material Tint3MaskCompMat;

    public static void Init()
    {
        Tint3MaskMat = AssetAPI.GetLoadedAsset<Material>("Assets/Modding/TexturePainter/3MaskTintMat.mat");
        Tint3MaskCompMat = AssetAPI.GetLoadedAsset<Material>("Assets/Modding/TexturePainter/3MaskTintCompressedMat.mat");
    }
}
