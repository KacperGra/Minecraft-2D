using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DevKacper.Mechanic
{
    public class GenericGrid<T>
    {
        public class OnGridObjectChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

        private readonly int width;
        private readonly int height;
        private readonly float cellSize;
        private Vector3 originPosition;

        private readonly T[,] gridArray;
        private readonly TextMesh[,] debugTextArray;

        public GenericGrid(int width, int height, float cellSize, Vector3 originPosition, Func<GenericGrid<T>, int, int, T> createGridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new T[width, height];

            for (int x = 0; x < gridArray.GetLength(0); ++x)
            {
                for (int y = 0; y < gridArray.GetLength(1); ++y)
                {
                    gridArray[x, y] = createGridObject(this, x, y);
                }
            }

            bool drawGrid = false;
            if (drawGrid)
            {
                debugTextArray = new TextMesh[width, height];

                for (int x = 0; x < gridArray.GetLength(0); ++x)
                {
                    for (int y = 0; y < gridArray.GetLength(1); ++y)
                    {
                        debugTextArray[x, y] = Utility.UtilityClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

                OnGridObjectChanged += GenericGrid_OnGridObjectChanged;
            }
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public float GetCellSize()
        {
            return cellSize;
        }

        public Vector3 GetOrigin()
        {
            return originPosition;
        }

        private void GenericGrid_OnGridObjectChanged(object sender, OnGridObjectChangedEventArgs e)
        {
            debugTextArray[e.x, e.y].text = gridArray[e.x, e.y]?.ToString();
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPosition;
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }

        public void SetObject(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                debugTextArray[x, y].text = value.ToString();
                OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
            }
        }

        public void TriggerGridObjectChanged(int x, int y)
        {
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }

        public void SetObject(Vector3 worldPosition, T value)
        {
            GetXY(worldPosition, out int x, out int y);
            SetObject(x, y, value);
        }

        public T GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            return default;
        }

        public T GetValue(Vector3 worldPosition)
        {
            GetXY(worldPosition, out int x, out int y);
            return GetValue(x, y);
        }
    }
}

