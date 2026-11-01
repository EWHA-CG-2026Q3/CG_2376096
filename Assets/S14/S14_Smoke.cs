using UnityEngine;

// 화로에서 피어오르는 연기 (실습 5): 파티클 시스템 설정을 코드로 적용
[ExecuteAlways]
public class S14_Smoke : MonoBehaviour
{
    public Material smokeMaterial;   // Mat_S14_Smoke

    void OnEnable()
    {
        var ps = GetComponent<ParticleSystem>();
        if (ps == null) ps = gameObject.AddComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var main = ps.main;
        main.duration = 5f;
        main.loop = true;
        main.prewarm = true;
        main.startLifetime = new ParticleSystem.MinMaxCurve(3f, 5f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.1f, 0.2f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.25f, 0.5f);
        main.startRotation = new ParticleSystem.MinMaxCurve(0f, Mathf.PI * 2f);
        main.startColor = new ParticleSystem.MinMaxGradient(Color.white, new Color(0.6f, 0.6f, 0.6f));
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = 200;

        var em = ps.emission;
        em.rateOverTime = 10f;

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 10f;
        shape.radius = 0.05f;
        shape.radiusThickness = 1f;

        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.Local;
        vel.x = new ParticleSystem.MinMaxCurve(0f);
        vel.y = new ParticleSystem.MinMaxCurve(0f);
        vel.z = new ParticleSystem.MinMaxCurve(0.15f);

        var noise = ps.noise;
        noise.enabled = true;
        noise.strength = 0.1f;
        noise.frequency = 0.5f;
        noise.scrollSpeed = 0.2f;
        noise.damping = true;
        noise.quality = ParticleSystemNoiseQuality.Medium;

        var col = ps.colorOverLifetime;
        col.enabled = true;
        var g = new Gradient();
        g.SetKeys(
            new[] {
                new GradientColorKey(new Color(0.8f, 0.5f, 0.3f), 0f),
                new GradientColorKey(new Color(0.55f, 0.55f, 0.55f), 0.4f),
                new GradientColorKey(new Color(0.45f, 0.45f, 0.45f), 1f) },
            new[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(0.7f, 0.2f),
                new GradientAlphaKey(0.4f, 0.7f),
                new GradientAlphaKey(0f, 1f) });
        col.color = g;

        var size = ps.sizeOverLifetime;
        size.enabled = true;
        size.size = new ParticleSystem.MinMaxCurve(1f, AnimationCurve.Linear(0f, 0.4f, 1f, 1.6f));

        var r = ps.GetComponent<ParticleSystemRenderer>();
        r.renderMode = ParticleSystemRenderMode.Billboard;
        r.maxParticleSize = 1f;
        r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        if (smokeMaterial != null) r.sharedMaterial = smokeMaterial;

        ps.Play();
    }
}
