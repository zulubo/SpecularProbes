using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Light))]
public class SpecularProbeLight : MonoBehaviour {
#if UNITY_EDITOR
    [Tooltip("The intensity of the Light will be multiplied by this. Recommended to raise for small lights, and lower for large lights")]
    public float intensityMultiplier = 1;
    [Tooltip("Radius of the light sphere geometry")]
    public float radius = 0.25f;


    const float intensityConstant = 5;

    private LightRenderer instance;
    private Light light;

    private void OnDrawGizmosSelected()
    {
        Start();
        Gizmos.color = light.color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private class LightRenderer
    {
        public GameObject gameObject;
        public MeshFilter meshFilter;
        public MeshRenderer renderer;
        public Transform transform { get { return gameObject.transform; } }

        public LightRenderer(GameObject g)
        {
            gameObject = g;
            meshFilter = g.AddComponent<MeshFilter>();
            renderer = g.AddComponent<MeshRenderer>();
        }

        public void Destroy()
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        if(light==null)
            light = GetComponent<Light>();
    }

    public void Draw()
    {
        Start();

        if (instance != null)
        {
            Hide();
        }
        instance = new LightRenderer(new GameObject("lightRenderer"));
        instance.transform.localScale = Vector3.one * radius;
        instance.transform.parent = transform;
        instance.transform.localPosition = Vector3.zero;
        instance.meshFilter.sharedMesh = ((GameObject)Resources.Load("SpecSphere", typeof(GameObject))).GetComponent<MeshFilter>().sharedMesh;

#if UNITY_2018_1_OR_NEWER
        if (GraphicsSettings.currentRenderPipeline != null)
        {
            // Scriptable Render Pipeline Support
            instance.renderer.sharedMaterial = new Material(GraphicsSettings.currentRenderPipeline.defaultParticleMaterial.shader);
            instance.renderer.sharedMaterial.SetColor("_BaseColor", light.color * CalcBrightness());
        }
        else
#endif
        {
            // Built in Render Pipeline Support
            instance.renderer.sharedMaterial = new Material(Shader.Find("Particles/Standard Unlit"))
            {
                color = light.color * CalcBrightness()
            };
        }

        instance.gameObject.isStatic = true;
    }

    public void Hide()
    {
        if (instance != null)
        {
            if (instance.gameObject != null)
            {
                DestroyImmediate(instance.renderer.sharedMaterial);
                instance.Destroy();
            }
        }
        instance = null;
    }

    float CalcBrightness()
    {
        return light.intensity * intensityMultiplier * intensityConstant;
    }
#endif
    }
