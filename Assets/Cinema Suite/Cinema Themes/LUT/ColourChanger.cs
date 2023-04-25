using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColourChanger : MonoBehaviour
{
    [SerializeField] private RenderPipelineAsset _renderPipelineAsset;
    [SerializeField] private Texture2D[] _luts;

    private int _index;

    private T GetRendererFeature<T>() where T : ScriptableRendererFeature
    {
        var pipeline = ((UniversalRenderPipelineAsset)GraphicsSettings.renderPipelineAsset);

        IEnumerable<ScriptableRendererFeature> _rendererFeatures = pipeline.scriptableRenderer.GetType()
               .GetProperty("rendererFeatures", BindingFlags.NonPublic | BindingFlags.Instance)
               ?.GetValue(pipeline.scriptableRenderer, null) as IEnumerable<ScriptableRendererFeature>;

        var rendererFeature = _rendererFeatures.FirstOrDefault<ScriptableRendererFeature>(r => r is T) as T;
        return rendererFeature;
    }


    IEnumerator Start()
    {
        MyBlitFeature blit = GetRendererFeature<MyBlitFeature>();

        while (true)
        {
            yield return new WaitForSeconds(1f);
            blit.settings.MaterialToBlit.SetTexture("_LUT", _luts[_index]);
            _index++;
            _index %= _luts.Length;
        }
    }
}
