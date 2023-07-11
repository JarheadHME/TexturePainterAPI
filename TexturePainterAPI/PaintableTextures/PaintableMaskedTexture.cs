using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace TexturePainterAPI.PaintableTextures;
public class PaintableMaskedTexture : PaintableTexture
{
    private static RenderTexture _TempRT;

    private Texture2D _MainTexture;
    private Texture2D _MaskA, _MaskB, _MaskC;
    private Color _TintA, _TintB, _TintC;

    private RenderTexture _ResultTexture;

    private bool _IsReady = false;

    public override Texture CurrentTexture => _ResultTexture;

    public PaintableMaskedTexture(Texture2D mainTexture)
    {
        SetMainTexture(mainTexture);
        SetMaskTexture(Texture2D.whiteTexture);
        SetTintColor(Color.clear);

        _IsReady = true;
        UpdateTexture();
    }

    public void SetMainTexture(Texture2D mainTexture)
    {
        _MainTexture = mainTexture;
        if (_ResultTexture != null)
        {
            UnityEngine.Object.Destroy(_ResultTexture);
        }

        _ResultTexture = new RenderTexture(_MainTexture.width, _MainTexture.height, 1, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB)
        {
            name = "PaintableMaskedTexture",
            enableRandomWrite = true,
            hideFlags = HideFlags.HideAndDontSave
        };
        _ResultTexture.Create();
    }

    public void SetMaskTexture(Texture2D mask1)
    {
        SetMaskTexture(mask1, Texture2D.whiteTexture, Texture2D.whiteTexture);
    }

    public void SetMaskTexture(Texture2D mask1, Texture2D mask2)
    {
        SetMaskTexture(mask1, mask2, Texture2D.whiteTexture);
    }

    public void SetMaskTexture(Texture2D mask1, Texture2D mask2, Texture2D mask3)
    {
        _MaskA = mask1;
        _MaskB = mask2;
        _MaskC = mask3;
        UpdateTexture();
    }

    public void SetTintColor(Color color1)
    {
        SetTintColor(color1, Color.clear, Color.clear);
    }

    public void SetTintColor(Color color1, Color color2)
    {
        SetTintColor(color1, color2, Color.clear);
    }

    public void SetTintColor(Color color1, Color color2, Color color3)
    {
        _TintA = color1;
        _TintB = color2;
        _TintC = color3;
        UpdateTexture();
    }

    private void UpdateTexture()
    {
        if (!_IsReady)
            return;

        var width = _MainTexture.width;
        var height = _MainTexture.height;

        var cs = Assets.Tint3MaskCS;
        var kernel = Assets.Tint3MaskKernel;

        cs.SetTexture(kernel, P.BaseTexture, _MainTexture);
        cs.SetTexture(kernel, P.MaskA, _MaskA);
        cs.SetTexture(kernel, P.MaskB, _MaskB);
        cs.SetTexture(kernel, P.MaskC, _MaskC);
        cs.SetTexture(kernel, P.Result, _ResultTexture);
        cs.SetVector(P.TintA, _TintA);
        cs.SetVector(P.TintB, _TintB);
        cs.SetVector(P.TintC, _TintC);
        cs.Dispatch(kernel, width, height, 1);
    }

    public override Texture CreateCopy()
    {
        var newTexture = new Texture2D(_MainTexture.width, _MainTexture.height, TextureFormat.ARGB32, true, false);
        Graphics.CopyTexture(_ResultTexture, newTexture);
        newTexture.Apply();
        return newTexture;
    }

    public override void Destroy()
    {
        if (_ResultTexture != null)
            UnityEngine.Object.Destroy(_ResultTexture);
    }
}
