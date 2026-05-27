using UnityEngine;

public class Board : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;

    // 棋盤資料
    public static Transform[,] grid =
        new Transform[width, height];

    // 是否在邊界內
    public static bool InsideBorder(Vector2 pos)
    {
        return (
            (int)pos.x >= 0 &&
            (int)pos.x < width &&
            (int)pos.y >= 0
        );
    }

    // 判斷一列是否已滿
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
                return false;
        }

        return true;
    }

    // 刪除一列
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }

    // 方塊下降
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];

                grid[x, y] = null;

                grid[x, y - 1].position += Vector3.down;
            }
        }
    }

    // 上方列下降
    public static void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < height; i++)
        {
            DecreaseRow(i);
        }
    }

    // 刪除所有滿行
    public static void DeleteFullRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);

                DecreaseRowsAbove(y + 1);

                y--;
            }
        }
    }
}
