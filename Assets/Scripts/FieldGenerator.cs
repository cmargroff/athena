using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
  public List<GameObject> fieldPrefabs;
  // Start is called before the first frame update
  void Awake()
  {
    int fieldSize = 5;
    float tileSize = 1;
    
    tileSize = fieldPrefabs[0].GetComponent<MeshRenderer>().bounds.size.x;

    var fieldLength = fieldSize * tileSize;
    
    for (int x = 0; x < fieldSize; x++)
    {
      for (int y = 0; y < fieldSize; y++)
      {
        Instantiate(
            fieldPrefabs[Random.Range(0, fieldPrefabs.Count)],
            new Vector3(
                x * tileSize - (fieldLength / 2),
                0,
                y * tileSize - (fieldLength / 2)
            ),
            Quaternion.identity
        );
      }
    }
  }
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
