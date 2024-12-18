using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    See https://www.ronja-tutorials.com/post/048-material-property-blocks/

    The script is set up this way to allow altering the material properties
    on a per instance basis instead of it applying to everything using the material.
*/

public class SpriteEffectsPropertySetter : MonoBehaviour
{
    [SerializeField] private Color TintColor = Color.white;

    [Range(0f, 1f)]
    [SerializeField] private float TintAmount;

    [Range(0f, 6.28318531f)]
    [SerializeField] private float HueShift;

    [Range(0f, 5f)]
    [SerializeField] private float Saturation = 1;

    [Range(-1f, 1f)]
    [SerializeField] private float Brightness;

    [Range(0f, 1f)]
    [SerializeField] private float Opacity = 1f;

    private Renderer renderer;

    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;

    // OnValidate is called in the editor after the component is edited
    void OnValidate() {
        renderer = GetComponent<Renderer>();
        UpdateShaderProperties();
    }

    void UpdateShaderProperties()
    {
        GetRendererPropertyBlock();

        propertyBlock.SetColor("_TintColor", TintColor);
        propertyBlock.SetFloat("_TintAmount", TintAmount);
        propertyBlock.SetFloat("_HueShift", HueShift);
        propertyBlock.SetFloat("_Saturation", Saturation);
        propertyBlock.SetFloat("_Brightness", Brightness);
        propertyBlock.SetFloat("_Opacity", Opacity);

        renderer.SetPropertyBlock(propertyBlock);
    }

    void Awake() {
        renderer = GetComponent<Renderer>();
        if(!renderer.material.name.Contains("SpriteEffects")) {
            Debug.LogError("This object has a SpriteEffectsPropertySetter, but the material was not set to SpriteEffects, so it won't work", this);
        }
        UpdateShaderProperties();
    }

    public void SetTintColor(Color newTintColor) {
        TintColor = newTintColor;
        
        GetRendererPropertyBlock();
        propertyBlock.SetColor("_TintColor", TintColor);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetTintAmount(float newTintAmount) {
        TintAmount = newTintAmount;

        GetRendererPropertyBlock();
        propertyBlock.SetFloat("_TintAmount", TintAmount);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetHueShift(float newHueShift) {
        HueShift = newHueShift;

        GetRendererPropertyBlock();
        propertyBlock.SetFloat("_HueShift", HueShift);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetSaturation(float newSaturation) {
        Saturation = newSaturation;

        GetRendererPropertyBlock();
        propertyBlock.SetFloat("_Saturation", Saturation);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetBrightness(float newBrightness) {
        Brightness = newBrightness;

        GetRendererPropertyBlock();
        propertyBlock.SetFloat("_Brightness", Brightness);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetOpacity(float newOpacity) {
        Opacity = newOpacity;

        GetRendererPropertyBlock();
        propertyBlock.SetFloat("_Opacity", Opacity);
        renderer.SetPropertyBlock(propertyBlock);
    }

    private void GetRendererPropertyBlock() {
        if (propertyBlock == null) {
            propertyBlock = new MaterialPropertyBlock();
        }
        if (renderer.HasPropertyBlock()) {
            renderer.GetPropertyBlock(propertyBlock);
        }
    }
}
