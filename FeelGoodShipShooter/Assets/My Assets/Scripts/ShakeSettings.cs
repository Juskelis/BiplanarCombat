using EZCameraShake;

[System.Serializable]
public struct ShakeSettings {
    public float magnitude;
    public float roughness;
    public float fadeInTime;
    public float fadeOutTime;

    public void Shake()
    {
        foreach(CameraShaker shaker in CameraShaker.FindObjectsOfType<CameraShaker>())
        {
            shaker.ShakeOnce(
                magnitude,
                roughness,
                fadeInTime,
                fadeOutTime);
        }
    }
}
