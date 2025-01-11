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

    [SerializeField] private SerializableBoolean[] serializedGrid;

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

    public void LoadGridFromSerialized()
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

    public bool GetValue(int x, int y) //왜인지 모르겠는데 2차원 배열이 왼쪽으로 90도 돌아가 있음.
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return false;

        return Grid[x, y];
    }

    public void RotateGrid()
    {
        // 1. 임시 배열 생성 (행과 열 크기 바꿈)
        bool[,] tempGrid = new bool[Height, Width];

        // 2. 회전 로직: 90도 시계 방향으로 회전
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                tempGrid[j, Width - 1 - i] = runtimeGrid[i, j];
            }
        }

        // 3. Width와 Height 교체
        (Width, Height) = (Height, Width);

        // 4. runtimeGrid 업데이트
        runtimeGrid = tempGrid;

        // 5. serializedGrid 업데이트
        serializedGrid = new SerializableBoolean[Width * Height];
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                int index = j * Width + i;
                serializedGrid[index] = new SerializableBoolean(runtimeGrid[i, j]);
            }
        }
    }





}