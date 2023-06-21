using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpawnManager : MonoBehaviour
{
    private int num = 11;
    public Transform[] rowsTransform;
    public GameObject prefab;

    private void Start()
    {
        GenerateSeats();
    }

    private void GenerateSeats()
    {
        int rows = Mathf.FloorToInt(11 / 4);
        int lastRowCount = 11 % 3;

        for (int i = 0; i <= rows; i++)
        {
            int count = i == rows ? lastRowCount : 4;
            float offset = 0;
            for (int j = 0; j < count; j++)
            {
                Vector3 pos = rowsTransform[i].position + new Vector3(0, 0, offset);
                Instantiate(prefab, pos, Quaternion.identity, rowsTransform[i]);
                offset -= 6f;
            }
        }
    }
}
