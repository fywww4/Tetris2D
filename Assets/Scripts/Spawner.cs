using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;

    public void SpawnNext()
    {
        int index =
            Random.Range(0, tetrominoes.Length);

        Instantiate(
            tetrominoes[index],
            transform.position,
            Quaternion.identity
        );
    }

    void Start()
    {
        SpawnNext();
    }
}
