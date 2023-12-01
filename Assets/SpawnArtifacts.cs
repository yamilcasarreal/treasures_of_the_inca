using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArtifacts : MonoBehaviour
{
    public GameObject ArtifactParent;
    public GameObject ArtifactSpawnsParent;
    public GameObject ArtifactsInGameParent;
    public GameObject PedestalsInGameParent;
    public GameObject Pedestal;

    public float pedestalHeight;
    public GameObject player;

    private List<GameObject> Artifacts = new List<GameObject>();
    private List<GameObject> ArtifactSpawns = new List<GameObject>();
    private List<GameObject> ArtifactsInUse = new List<GameObject>();

    private int numArtifacts;
    private float artifactHeight;
    private int artifactIndex;
    private int spawnIndex;
    private GameObject artifact;
    private GameObject pedestal;

    // Start is called before the first frame update
    void Start()
    {
        numArtifacts = player.GetComponent<ArtifactInteractions>().numberOfArtifacts;

        foreach (Transform child in ArtifactParent.transform)
            Artifacts.Add(child.gameObject);

        foreach (Transform child in ArtifactSpawnsParent.transform)
            ArtifactSpawns.Add(child.gameObject);

        for (int i = 0; i < numArtifacts; i++)
        {
            artifactIndex = Random.Range(0, Artifacts.Count);
            spawnIndex = Random.Range(0, ArtifactSpawns.Count);

            pedestal = Instantiate(Pedestal, ArtifactSpawns[spawnIndex].transform.position + (Vector3.up * pedestalHeight), Pedestal.transform.rotation, PedestalsInGameParent.transform);
            pedestal.SetActive(true);

            artifactHeight = Artifacts[artifactIndex].GetComponent<RotateArtifact>().artifactHeight;

            artifact = Instantiate(Artifacts[artifactIndex], ArtifactSpawns[spawnIndex].transform.position + (Vector3.up * artifactHeight), Artifacts[artifactIndex].transform.rotation, ArtifactsInGameParent.transform);
            artifact.SetActive(true);

            Artifacts.RemoveAt(artifactIndex);
            ArtifactSpawns.RemoveAt(spawnIndex);
        }
    }
}
