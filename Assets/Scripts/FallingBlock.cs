using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private float previousTime;

    public float fallTime = 1f;

    void Update()
    {
        // 左移
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;

            if (!ValidMove())
                transform.position += Vector3.right;
        }

        // 右移
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;

            if (!ValidMove())
                transform.position += Vector3.left;
        }

        // 旋轉
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            if (!ValidMove())
                transform.Rotate(0, 0, 90);
        }

        // ↓ 加速下降（Soft Drop）
        float currentFallTime =
            Input.GetKey(KeyCode.DownArrow)
            ? fallTime * 0.1f
            : fallTime;

        // 自動下降
        if (Time.time - previousTime > currentFallTime)
        {
            transform.position += Vector3.down;

            if (!ValidMove())
            {
                transform.position += Vector3.up;

                AddToGrid();

                Board.DeleteFullRows();

                FindObjectOfType<Spawner>().SpawnNext();

                enabled = false;
            }

            previousTime = Time.time;
        }
    }

    bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = Round(child.position);

            // 左右底部邊界
            if ((int)pos.x < 0 ||
                (int)pos.x >= Board.width ||
                (int)pos.y < 0)
            {
                return false;
            }

            // 上方超出時不檢查
            if ((int)pos.y >= Board.height)
                continue;

            Transform t =
                Board.grid[(int)pos.x, (int)pos.y];

            if (t != null &&
                t.parent != transform)
            {
                return false;
            }
        }

        return true;
    }

    void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = Round(child.position);

            int x = (int)pos.x;
            int y = (int)pos.y;

            // 防止超出範圍
            if (x >= 0 &&
                x < Board.width &&
                y >= 0 &&
                y < Board.height)
            {
                Board.grid[x, y] = child;
            }
        }
    }

    Vector2 Round(Vector2 v)
    {
        return new Vector2(
            Mathf.Round(v.x),
            Mathf.Round(v.y)
        );
    }
}
