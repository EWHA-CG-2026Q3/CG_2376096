using UnityEngine;

// 무대 위에 깔리는 연기(헤이즈): 크고 옅은 파티클이 천천히 떠오르며 퍼짐
[ExecuteAlways]
public class LH_Haze : MonoBehaviour
{
    public Material hazeMaterial;
    public Vector3 area = new Vector3(8f, 0.2f, 2f);   // 연기가 나오는 상자 크기
    public Color tint = new Color(0.75f, 0.7f, 0.85f, 1f);

    void OnEnable()
    {
        var ps = GetComponent<ParticleSystem>();
        if (ps == null) ps = gameObject.AddComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var main = ps.main;
        main.duration = 10f;
        main.loop = true;
        main.prewarm = true;
        main.startLifetime = new ParticleSystem.MinMaxCurve(10f, 14f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.02f, 0.08f);
        main.startSize = new ParticleSystem.MinMaxCurve(2f, 3.5f);
        main.startRotation = new ParticleSystem.MinMaxCurve(0f, Mathf.PI * 2f);
        main.startColor = tint;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = 150;

        var em = ps.emission;
        em.rateOverTime = 5f;

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = area;

        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.World;
        vel.x = new ParticleSystem.MinMaxCurve(-0.05f, 0.05f);
        vel.y = new ParticleSystem.MinMaxCurve(0.05f, 0.12f);
        vel.z = new ParticleSystem.MinMaxCurve(-0.15f, -0.05f);   // 객석 쪽으로 천천히 밀려옴

        var noise = ps.noise;
        noise.enabled = true;
        noise.strength = 0.15f;
        noise.frequency = 0.3f;
        noise.scrollSpeed = 0.1f;
        noise.damping = true;

        var col = ps.colorOverLifetime;
        col.enabled = true;
        var g = new Gradient();
        g.SetKeys(
            new[] { new GradientColorKey(Color.white, 0f), new GradientColorKey(Color.white, 1f) },
            new[] { new GradientAlphaKey(0f, 0f), new GradientAlphaKey(0.18f, 0.3f), new GradientAlphaKey(0.12f, 0.7f), new GradientAlphaKey(0f, 1f) });
        col.color = g;

        var size = ps.sizeOverLifetime;
        size.enabled = true;
        size.size = new ParticleSystem.MinMaxCurve(1f, AnimationCurve.Linear(0f, 0.6f, 1f, 1.5f));

        var r = ps.GetComponent<ParticleSystemRenderer>();
        r.renderMode = ParticleSystemRenderMode.Billboard;
        r.maxParticleSize = 3f;
        r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        if (hazeMaterial != null) r.sharedMaterial = hazeMaterial;

        ps.Play();
    }
}
