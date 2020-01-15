using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(ReflectionProbe))]
public class SpecularProbeRenderer : MonoBehaviour {

#if UNITY_EDITOR
    [Tooltip("Will only draw specular highlights for lights in this radius")]
    public float radius = 100;

    private ReflectionProbe probe;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 1, 0.25f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
        if (Application.isPlaying) Destroy(this);
    }

#if UNITY_2019_2_OR_NEWER
    // Unity added bakeCompleted action in 2019.2
    private void OnEnable()
    {
        UnityEditor.Lightmapping.bakeCompleted += OnBakeCompleted;
    }

    private void OnDisable()
    {
        UnityEditor.Lightmapping.bakeCompleted -= OnBakeCompleted;
    }

    void OnBakeCompleted()
    {
        Debug.Log("Baking Specular Highlights");
        Render();
    }
#endif

    [ContextMenu("Render")]
    public void Render()
    {
        probe = GetComponent<ReflectionProbe>();

        SpecularProbeLight[] allLights = FindObjectsOfType<SpecularProbeLight>();
        List<SpecularProbeLight> closeLights = new List<SpecularProbeLight>();

        for (int i = 0; i < allLights.Length; i++)
        {
            //find lights within radius of probe
            if ((allLights[i].transform.position - transform.position).sqrMagnitude < radius * radius)
            {
                closeLights.Add(allLights[i]);
                // create specular highlight sphere
                allLights[i].Draw();
            }
        }
        //render probe
        string path = UnityEditor.AssetDatabase.GetAssetPath(probe.bakedTexture);
        UnityEditor.Lightmapping.BakeReflectionProbe(probe, path);

        // remove all created lights, cleaning up scene
        for (int i = 0; i < closeLights.Count; i++)
        {
            closeLights[i].Hide();
        }
    }

    [ContextMenu("Render All")]
    public void RenderAll()
    {
        foreach(SpecularProbeRenderer r in FindObjectsOfType<SpecularProbeRenderer>())
        {
            r.Render();
        }
    }

#endif
}
