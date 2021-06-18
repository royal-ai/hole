using UnityEngine;

public class FireLightScript : MonoBehaviour
{

    //This script randomly changes the intensity of light object to simulate fire

    [Header ("Light")]
	public float minIntensity = 2f; 
	public float maxIntensity = 3f;

	public Light fireLight;

	float random;

	void Update()
	{
		random = Random.Range(0.0f, 150.0f);
		float noise = Mathf.PerlinNoise(random, Time.time);
		fireLight.GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
	}
}