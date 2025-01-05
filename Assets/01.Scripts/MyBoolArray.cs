using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SerializableBoolean
{
    public bool value;

    public SerializableBoolean(bool value)
    {
        this.value = value;
    }
}

[System.Serializable]
public class MyBoolArray
{
    public int Width = 3;
    public int Height = 3;

    // 직렬화된 1D 배열
    [SerializeField]
    private SerializableBoolean[] serializedGrid;

    // 런타임 중에 사용할 2D 배열
    private bool[,] runtimeGrid;

    public bool[,] Grid
    {
        get
        {
            if (runtimeGrid == null || runtimeGrid.GetLength(0) != Width || runtimeGrid.GetLength(1) != Height)
            {
                LoadGridFromSerialized();
            }
            return runtimeGrid;
        }
    }

    public MyBoolArray(int width, int height)
    {
        Width = width;
        Height = height;
        InitializeGrid();
    }
    private void InitializeGrid()
    {
        serializedGrid = new SerializableBoolean[Width * Height];
        for (int j = 0; j < Height; j++) // 행 먼저 순회
        {
            for (int i = 0; i < Width; i++) // 열 순회
            {
                int index = j * Width + i;
                serializedGrid[index] = new SerializableBoolean(false);
            }
        }

        LoadGridFromSerialized();
    }

    private void LoadGridFromSerialized()
    {
        runtimeGrid = new bool[Width, Height];

        for (int j = 0; j < Height; j++) // 행 순회
        {
            for (int i = 0; i < Width; i++) // 열 순회
            {
                int index = j * Width + i;
                if (index < serializedGrid.Length)
                {
                    runtimeGrid[i, j] = serializedGrid[index]?.value ?? false;
                }
            }
        }
    }

    public void SetValue(int row, int col, bool value)
    {
        if (row < 0 || row >= Width || col < 0 || col >= Height)
            return;

        if (runtimeGrid[row, col] != value)
        {
            runtimeGrid[row, col] = value;
            int index = col * Width + row; // 행을 먼저 계산

            if (serializedGrid != null && index < serializedGrid.Length)
            {
                if (serializedGrid[index] == null)
                    serializedGrid[index] = new SerializableBoolean(value);
                else
                    serializedGrid[index].value = value;
            }
        }
    }


    public bool GetValue(int x, int y)//왜인지 모르겠는데 2차원 배열이 왼쪽으로 90도 돌아가 있음.
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return false;

        return Grid[x, y];
    }
}