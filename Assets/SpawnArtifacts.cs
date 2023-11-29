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

    public float artifactHeight;
    public float pedestalHeight;
    public int NumberOfArtifactsInGame;

    private List<GameObject> Artifacts = new List<GameObject>();
    private List<GameObject> ArtifactSpawns = new List<GameObject>();
    private List<GameObject> ArtifactsInUse = new List<GameObject>();

    private int artifactIndex;
    private int spawnIndex;
    private GameObject artifact;
    private GameObject pedestal;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in ArtifactParent.transform)
            Artifacts.Add(child.gameObject);

        foreach (Transform child in ArtifactSpawnsParent.transform)
            ArtifactSpawns.Add(child.gameObject);

        SPAWNFORPRESENTATION(); // REMOVE BEFORE FRIDAY

        /*
        for (int i = 0; i < NumberOfArtifactsInGame; i++)
        {
            artifactIndex = Random.Range(1, Artifacts.Count); // CHANGE TO 0 BEFORE FRIDAY
            spawnIndex = Random.Range(1, ArtifactSpawns.Count); // CHANGE TO 0 AND DELETE PRESENTATION SPAWN BEFORE FRIDAY

            pedestal = Instantiate(Pedestal, ArtifactSpawns[spawnIndex].transform.position + (Vector3.up * pedestalHeight), Pedestal.transform.rotation, PedestalsInGameParent.transform);
            pedestal.SetActive(true);

            artifact = Instantiate(Artifacts[artifactIndex], ArtifactSpawns[spawnIndex].transform.position + (Vector3.up * artifactHeight), Artifacts[artifactIndex].transform.rotation, ArtifactsInGameParent.transform);
            artifact.SetActive(true);

            Artifacts.RemoveAt(artifactIndex);
            ArtifactSpawns.RemoveAt(spawnIndex);
        }
        */
    }

    // REMOVE BEFORE FRIDAY
    private void SPAWNFORPRESENTATION()
    {
        pedestal = Instantiate(Pedestal, ArtifactSpawns[0].transform.position + (Vector3.up * pedestalHeight), Pedestal.transform.rotation, PedestalsInGameParent.transform);
        pedestal.SetActive(true);
        artifact = Instantiate(Artifacts[0], ArtifactSpawns[0].transform.position + (Vector3.up * artifactHeight), Artifacts[0].transform.rotation, ArtifactsInGameParent.transform);
        artifact.SetActive(true);
        pedestal = Instantiate(Pedestal, ArtifactSpawns[1].transform.position + (Vector3.up * pedestalHeight), Pedestal.transform.rotation, PedestalsInGameParent.transform);
        pedestal.SetActive(true);
        artifact = Instantiate(Artifacts[1], ArtifactSpawns[1].transform.position + (Vector3.up * artifactHeight), Artifacts[1].transform.rotation, ArtifactsInGameParent.transform);
        artifact.SetActive(true);
    }    
}
