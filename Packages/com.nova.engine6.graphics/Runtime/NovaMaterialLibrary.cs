using UnityEngine;

namespace Nova.Engine.Graphics
{
    public static class NovaMaterialLibrary
    {
        public static Material CreatePbrMaterial(
            string materialName, Color baseColor, float metallic = 0f, float smoothness = 0.5f)
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null)
                shader = Shader.Find("Standard");

            if (shader == null)
            {
                Debug.LogError("[NOVA Graphics] No compatible Lit/Standard shader found.");
                return null;
            }

            var material = new Material(shader)
            {
                name = string.IsNullOrWhiteSpace(materialName) ? "NOVA PBR Material" : materialName
            };

            if (material.HasProperty("_BaseColor"))
                material.SetColor("_BaseColor", baseColor);
            else if (material.HasProperty("_Color"))
                material.SetColor("_Color", baseColor);

            if (material.HasProperty("_Metallic"))
                material.SetFloat("_Metallic", Mathf.Clamp01(metallic));

            if (material.HasProperty("_Smoothness"))
                material.SetFloat("_Smoothness", Mathf.Clamp01(smoothness));

            return material;
        }
    }
}
