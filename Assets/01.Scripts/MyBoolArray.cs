using UnityEngine;

[System.Serializable]
public class SerializableBoolean
{
    public bool value;

    public SerializableBoolean(bool value)
    {
        this.value = value;
    }
}

public class MyBoolArray : MonoBehaviour
{
    public int rows = 3;
    public int columns = 3;

    // 직렬화된 1D 배열
    [SerializeField] private SerializableBoolean[] serializedGrid;

    // 런타임 중에 사용할 2D 배열
    private bool[,] runtimeGrid;

    public bool[,] Grid
    {
        get
        {
            // 배열 크기가 일치하지 않으면 직렬화된 값에서 로드
            if (runtimeGrid == null || runtimeGrid.GetLength(0) != rows || runtimeGrid.GetLength(1) != columns)
            {
                LoadGridFromSerialized();
            }
            return runtimeGrid;
        }
    }

    private void OnValidate()
    {
        if (rows < 1) rows = 1;
        if (columns < 1) columns = 1;

        // 직렬화된 배열 크기 확인 후 필요시 새로 할당
        if (serializedGrid == null || serializedGrid.Length != rows * columns)
        {
            serializedGrid = new SerializableBoolean[rows * columns];
            for (int i = 0; i < serializedGrid.Length; i++)
            {
                serializedGrid[i] = new SerializableBoolean(false);
            }
        }

        // 직렬화된 값으로 2D 배열 로드
        LoadGridFromSerialized();
    }

    private void Awake()
    {
        LoadGridFromSerialized();
    }

    private void LoadGridFromSerialized()
    {
        // 런타임 2D 배열로 변환
        runtimeGrid = new bool[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int index = i * columns + j;
                if (index < serializedGrid.Length)
                {
                    runtimeGrid[i, j] = serializedGrid[index]?.value ?? false;
                }
            }
        }
    }

    public void SetValue(int row, int col, bool value)
    {
        if (row < 0 || row >= rows || col < 0 || col >= columns)
            return;

        // 값이 변경되었을 때만 업데이트
        if (runtimeGrid[row, col] != value)
        {
            runtimeGrid[row, col] = value;
            int index = row * columns + col;

            // 직렬화된 배열 업데이트
            if (serializedGrid != null && index < serializedGrid.Length)
            {
                if (serializedGrid[index] == null)
                    serializedGrid[index] = new SerializableBoolean(value);
                else
                    serializedGrid[index].value = value;
            }
        }
    }

    public bool GetValue(int row, int col)
    {
        if (row < 0 || row >= rows || col < 0 || col >= columns)
            return false;

        return Grid[row, col];
    }
}
