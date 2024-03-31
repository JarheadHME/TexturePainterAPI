using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TexturePainterAPI.PaintableTextures;
public class PaintableChannelMaskedTexture : PaintableTexture
{
    private Texture2D _MainTexture;
    private Texture2D _MaskTexture;
    private Color _TintA, _TintB, _TintC;

    private RenderTexture _ResultTexture;

    private bool _IsReady = false;

    public override Texture CurrentTexture => _ResultTexture;

    public PaintableChannelMaskedTexture(Texture2D mainTexture)
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

    public void SetMaskTexture(Texture2D mask)
    {
        _MaskTexture = mask;
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

        var mat = Assets.Tint3MaskCompMat;
        mat.SetTexture(P.MaskTex, _MaskTexture);
        mat.SetColor(P.MaskColA, _TintA);
        mat.SetColor(P.MaskColB, _TintB);
        mat.SetColor(P.MaskColC, _TintC);
        Graphics.Blit(_MainTexture, _ResultTexture, mat);
    }

    public override Texture CreateCopy()
    {
        var newTexture = new Texture2D(_MainTexture.width, _MainTexture.height, TextureFormat.ARGB32, true, false);
        Graphics.CopyTexture(_ResultTexture, newTexture);
        newTexture.Apply();
        newTexture.hideFlags = HideFlags.HideAndDontSave;
        return newTexture;
    }

    public override void Destroy()
    {
        if (_ResultTexture != null)
            UnityEngine.Object.Destroy(_ResultTexture);
    }
}

