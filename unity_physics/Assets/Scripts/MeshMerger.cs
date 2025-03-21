using UnityEngine;

public class MeshMerger : MonoBehaviour
{
    [SerializeField] MeshFilter[] meshFilters;
    
    private void Start()
    {
        MergeMeshed();
    }

    private void MergeMeshed()
    {
        if (meshFilters != null)
        {
            Material[] materials = new Material[meshFilters.Length];
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                materials[i] = meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial;

                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }

            Mesh newMesh = new Mesh();
            newMesh.CombineMeshes(combine);

            GameObject mergedObject = new GameObject("MergedMesh");
            mergedObject.transform.parent = this.transform;
            mergedObject.transform.position = transform.position;

            MeshFilter mergedFilter = mergedObject.AddComponent<MeshFilter>();

            MeshRenderer mergedRenderer = mergedObject.AddComponent<MeshRenderer>();
            mergedRenderer.sharedMaterials = materials;
            mergedFilter.mesh = newMesh;

            MeshCollider collider = mergedObject.AddComponent<MeshCollider>();
            collider.convex = true;
        }
    }
}
