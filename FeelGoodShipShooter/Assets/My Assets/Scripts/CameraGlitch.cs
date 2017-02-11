using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(VignetteAndChromaticAberration))]
public class CameraGlitch : MonoBehaviour {

    [System.Serializable]
    public class GlitchSetting
    {
        public float intensity = 0.036f;                    // intensity == 0 disables pre pass (optimization)
        public float chromaticAberration = 0.2f;
        public float axialAberration = 0.5f;
        public float blur = 0.0f;                           // blur == 0 disables blur pass (optimization)
        public float blurSpread = 0.75f;
        public float luminanceDependency = 0.25f;
        public float blurDistance = 2.5f;

        public GlitchSetting(VignetteAndChromaticAberration toCopy)
        {
            Copy(toCopy);
        }

        private void Copy(VignetteAndChromaticAberration toCopy)
        {
            intensity = toCopy.intensity;
            chromaticAberration = toCopy.chromaticAberration;
            axialAberration = toCopy.axialAberration;
            blur = toCopy.blur;
            blurSpread = toCopy.blurSpread;
            luminanceDependency = toCopy.luminanceDependency;
            blurDistance = toCopy.blurDistance;
        }

        public void Overwrite(VignetteAndChromaticAberration target)
        {
            target.intensity = intensity;
            target.chromaticAberration = chromaticAberration;
            target.axialAberration = axialAberration;
            target.blur = blur;
            target.blurSpread = blurSpread;
            target.luminanceDependency = luminanceDependency;
            target.blurDistance = blurDistance;
        }

        public static void Lerp(ref GlitchSetting target, GlitchSetting start, GlitchSetting end, float amount)
        {
            target.chromaticAberration = Mathf.Lerp(start.chromaticAberration, end.chromaticAberration, amount);
            target.axialAberration = Mathf.Lerp(start.axialAberration, end.axialAberration, amount);
            target.blur = Mathf.Lerp(start.blur, end.blur, amount);
            target.blurSpread = Mathf.Lerp(start.blurSpread, end.blurSpread, amount);
            target.luminanceDependency = Mathf.Lerp(start.luminanceDependency, end.luminanceDependency, amount);
            target.blurDistance = Mathf.Lerp(start.blurDistance, end.blurDistance, amount);
        }
    }


    VignetteAndChromaticAberration hardware;

    private GlitchSetting nonGlitch;
    private GlitchSetting currentGlitch;
    public GlitchSetting maxGlitch;

    private float startTime = 0;
    private float endTime = 1;

    void Awake()
    {
        hardware = GetComponent<VignetteAndChromaticAberration>();
        nonGlitch = new GlitchSetting(hardware);
        currentGlitch = new GlitchSetting(hardware);
    }

    void Update()
    {
        float totalTime = endTime - startTime;
        float currentTime = Time.time - startTime;
        GlitchSetting.Lerp(ref currentGlitch, maxGlitch, nonGlitch, currentTime / totalTime);
        currentGlitch.Overwrite(hardware);
    }

    //sets a hard glitch to happen, and fades it out
    public void Glitch(float outTime)
    {
        startTime = Time.time;
        endTime = Time.time + outTime;
    }
}