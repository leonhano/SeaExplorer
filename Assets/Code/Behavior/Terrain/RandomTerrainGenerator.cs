using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomTerrainGenerator : MonoBehaviour
{

    //The higher the numbers, the more hills/mountains there are
    public float HM = 6;//Random.Range(4, 8);

    //The lower the numbers in the number range, the higher the hills/mountains will be...
    public float divRange = 2.3f;//Random.Range(30, 150);

    void Start()
    {
        //OnWizardCreate();
        
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Create Terrain"))
            CreateTerrainTextureWithHeight(gameObject.GetComponent<Terrain>(), HM);
    }
// 
//     [UnityEditor.MenuItem("Terrain/Generate Random Terrain")]
//     public static void CreateWizard(UnityEditor.MenuCommand command)
//     {
//         UnityEditor.ScriptableWizard.DisplayWizard("Generate Random Terrain", typeof(RandomTerrainGenerator));
//     }


    public void OnWizardCreate()
    {
        GameObject G = gameObject;//Selection.activeGameObject;
        if (G.GetComponent<Terrain>())
        {
            GenerateTerrain(G.GetComponent<Terrain>(), HM);
        }
    }

    //Our Generate Terrain function
    public void GenerateTerrain(Terrain t, float tileSize)
    {
        //Heights For Our Hills/Mountains
        float[,] hts = new float[t.terrainData.heightmapWidth, t.terrainData.heightmapHeight];
        for (int i = 0; i < t.terrainData.heightmapWidth; i++)
        {
            for (int k = 0; k < t.terrainData.heightmapHeight; k++)
            {
                hts[i, k] = Mathf.PerlinNoise(((float)i / (float)t.terrainData.heightmapWidth) * tileSize, ((float)k / (float)t.terrainData.heightmapHeight) * tileSize) / divRange;                
            }
        }
        Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
        t.terrainData.SetHeights(0, 0, hts);

        t.detailObjectDensity = 0.2f;
    }

    static void UpdateTerrainTexture(TerrainData terrainData, int textureNumberFrom, int textureNumberTo)
    {
        //get current paint mask
        float[, ,] alphas = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
        // make sure every grid on the terrain is modified
        for (int i = 0; i < terrainData.alphamapWidth; i++) 
        { 
            for (int j = 0; j < terrainData.alphamapHeight; j++) 
            { 
                //for each point of mask do: 
                //paint all from old texture to new texture (saving already painted in new texture) 
                alphas[i, j, textureNumberTo] = Mathf.Max(alphas[i, j, textureNumberFrom], alphas[i, j, textureNumberTo]); 
                //set old texture mask to zero 
                alphas[i, j, textureNumberFrom] = 0f; 
            } 
        } 
        //apply the new alpha 
        terrainData.SetAlphamaps(0, 0, alphas); 
    }

    void CreateTerrainTextureWithHeight(Terrain t, float tileSize)
    {
        TerrainData terrainData = t.terrainData;
        //Heights For Our Hills/Mountains
        float[,] hts = new float[terrainData.heightmapWidth, terrainData.heightmapHeight];

        //get current paint mask
        float[, ,] alphas = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
        int textureNumberFrom = 0;
        int textureNumberTo = 0;

        for (int i = 0; i < terrainData.heightmapWidth; i++)
        {
            for (int k = 0; k < terrainData.heightmapHeight; k++)
            {
                float height = Mathf.PerlinNoise(((float)i / (float)terrainData.heightmapWidth) * tileSize, ((float)k / (float)t.terrainData.heightmapHeight) * tileSize) / divRange;

                hts[i, k] = height;

                if ((k < terrainData.alphamapHeight) && (i < terrainData.alphamapWidth))
                {
                    if (height > 0.25)
                    {
                        if (height > 0.4)
                            textureNumberTo = 3;
                        else if (height > 0.3)
                            textureNumberTo = 2;
                        else
                            textureNumberTo = 1;

                        //for each point of mask do: 
                        //paint all from old texture to new texture (saving already painted in new texture) 
                        alphas[i, k, textureNumberTo] = Mathf.Max(alphas[i, k, textureNumberFrom], alphas[i, k, textureNumberTo]);
                        //set old texture mask to zero 
                        alphas[i, k, textureNumberFrom] = 0f;
                    }
                }
            }
        }
        Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
        terrainData.SetHeights(0, 0, hts);

        //detailObjectDensity = 0.2f;

        //apply the new alpha 
        terrainData.SetAlphamaps(0, 0, alphas);
    }
}