using UnityEngine;

// 등불 위로 튀어 오르는 불티 파티클 (Additive, HDR 색)
// ParticleSystem이 없으면 만들고, 값을 코드로 설정한다.
[ExecuteAlways]
public class S15_Abs_Sparks : MonoBehaviour
{
    public Color sparkColor = new Color(6f, 2.5f, 0.5f, 1f); // HDR(1 초과) 색 → Bloom 대상

    ParticleSystem ps;
    Texture2D dot;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps == null) ps = gameObject.AddComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var main = ps.main;
        main.duration = 5f;
        main.loop = true;
        main.prewarm = true;
        main.startLifetime = new ParticleSystem.MinMaxCurve(1f, 2f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.6f, 1.4f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.015f, 0.035f);
        main.startColor = Color.white;
        main.gravityModifier = -0.05f;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = 200;

        var em = ps.emission;
        em.rateOverTime = 18f;

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 28f;
        shape.radius = 0.03f;
        shape.rotation = new Vector3(-90f, 0f, 0f); // 위쪽으로

        var noise = ps.noise;
        noise.enabled = true;
        noise.strength = 0.3f;
        noise.frequency = 0.8f;
        noise.scrollSpeed = 0.5f;

        var col = ps.colorOverLifetime;
        col.enabled = true;
        var g = new Gradient();
        g.SetKeys(
            new[] { new GradientColorKey(Color.white, 0f), new GradientColorKey(new Color(1f, 0.5f, 0.2f), 1f) },
            new[] { new GradientAlphaKey(0f, 0f), new GradientAlphaKey(1f, 0.1f), new GradientAlphaKey(0f, 1f) });
        col.color = g;

        var size = ps.sizeOverLifetime;
        size.enabled = true;
        size.size = new ParticleSystem.MinMaxCurve(1f, AnimationCurve.Linear(0f, 1f, 1f, 0.2f));

        var r = ps.GetComponent<ParticleSystemRenderer>();
        r.renderMode = ParticleSystemRenderMode.Billboard;
        r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        r.receiveShadows = false;
        if (r.sharedMaterial == null || r.sharedMaterial.name == "Default-ParticleSystem")
            r.sharedMaterial = MakeMaterial();

        ps.Play();
    }

    Material MakeMaterial()
    {
        Shader sh = Shader.Find("Universal Render Pipeline/Particles/Unlit");
        if (sh == null) sh = Shader.Find("Sprites/Default");
        var m = new Material(sh) { name = "S15_Abs_Spark_Runtime", hideFlags = HideFlags.HideAndDontSave };

        // 가운데가 밝고 가장자리가 투명한 동그란 점
        dot = new Texture2D(32, 32, TextureFormat.RGBA32, false) { hideFlags = HideFlags.HideAndDontSave };
        for (int y = 0; y < 32; y++)
            for (int x = 0; x < 32; x++)
            {
                float d = Vector2.Distance(new Vector2(x, y), new Vector2(15.5f, 15.5f)) / 16f;
                float a = Mathf.Clamp01(1f - d);
                a *= a;
                dot.SetPixel(x, y, new Color(1f, 1f, 1f, a));
            }
        dot.Apply();

        m.SetTexture("_BaseMap", dot);
        m.SetColor("_BaseColor", sparkColor);
        m.SetFloat("_Surface", 1f);
        m.SetFloat("_Blend", 2f);          // Additive
        m.SetFloat("_SrcBlend", 1f);
        m.SetFloat("_DstBlend", 1f);
        m.SetFloat("_ZWrite", 0f);
        m.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        m.SetOverrideTag("RenderType", "Transparent");
        m.renderQueue = 3000;
        return m;
    }

    void OnDisable()
    {
        if (dot != null) DestroyImmediate(dot);
    }
}
