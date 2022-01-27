using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RP
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class CombineMesh : MonoBehaviour
    {
        // Copy meshes from children into the parent's Mesh.
        // CombineInstance stores the list of meshes.  These are combined
        // and assigned to the attached Mesh.

        public void Start()
        {
            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);

                i++;
            }
            var meshFilter = transform.GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.CombineMeshes(combine);
            GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
            transform.gameObject.SetActive(true);

            transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;
        }
    }
}

