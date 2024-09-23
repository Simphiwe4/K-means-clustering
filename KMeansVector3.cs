using UnityEngine;
using System.IO;

public class KMeansVector3 : MonoBehaviour {

   
    public bool showClustered = true;
    public int sampleSize = 777;
    public int spaceSize = 10;
    public int clusterCount = 5;
    public int iterations = 100;

    private KMeansResults results;
    private Vector3[] data;
    string input = File.ReadAllText(@"C:\College_Data.csv");
    string[] buffer;
    string[] top10Perc;
    string[] top25Perc;
    string[] personal;
    string[] Private;
    int count = 0;

    private void Start()
    {
        top10Perc = new string[sampleSize];
        top25Perc = new string[sampleSize];
        personal = new string[sampleSize];
        Private = new string[sampleSize];
        //buffer= new string[sampleSize];
        count= 0;
        foreach (var row in input.Split('\n'))
        {
           
                if (count < sampleSize)
                {
                    buffer = row.Split(';');
                    //Debug.Log(buffer[0]);
                    Private[count] = buffer[1];
                    top10Perc[count] = buffer[7];
                    top25Perc[count] = buffer[8];
                    personal[count] = buffer[17];

                    count++;

                }
                else
                {
                    break;
                }

            
        }
        data = new Vector3[sampleSize];

        for (int i = 1; i < data.Length; i++)
        {
            
            
                data[i] = new Vector3(float.Parse(top10Perc[i]), float.Parse(personal[i]), float.Parse(top25Perc[12])) * spaceSize;
            
            results = KMeans.Cluster(data, clusterCount, iterations, 0);
        }
    }
    void OnDrawGizmos()
    {
        if (results == null)
            return;

        // Calculate the number of clusters.
        int numClusters = results.clusters.Length;

        // Calculate the width of each cell and the number of cells per row.
        float cellWidth = 2.0f / (numClusters * 10);
        int cellsPerRow = 50;

        for (int i = 0; i < numClusters; i++)
        {
            for (int j = 0; j < results.clusters[i].Length; j++)
            {
                // Calculate the position based on cluster and index.
                int row = j / cellsPerRow;
                int col = j % cellsPerRow;
                Vector3 position = new Vector3(i * 10 + col * cellWidth, row, 0);

                // Adjust the color based on the cluster.
                Color color = i == 0 ? Color.red : Color.blue;

                Gizmos.color = color;
               // Gizmos.DrawSphere(position, 0.1f);

                // Draw cluster number as text at the center of the cell.
                GizmosUtils.DrawText(GUI.skin, Private[j+1].ToString(), position, Color.white, 10);
            }
        }
    }
}    

