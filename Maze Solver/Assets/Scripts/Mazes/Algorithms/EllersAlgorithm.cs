using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EllersAlgorithm : IMazeCarver
{
    protected Grid _grid;

    public EllersAlgorithm(Grid grid)
    {
        _grid = grid;
    }

    public void CarveMaze()
    {
        RowState rowState = new RowState(_grid.Cols);

        foreach (var row in _grid.EachRow())
        {
            foreach (var cell in row)
            {
                if (cell.West == null)
                {
                    int firtsSet = rowState.SetFor(cell);
                    continue;
                }

                int current_set = rowState.SetFor(cell);
                int prev_set = rowState.SetFor(cell.West);

                bool shouldLink = current_set != prev_set && (cell.South == null || Random.Range(0, 2) == 0);
                if (shouldLink)
                {
                    cell.Link(cell.West);
                    rowState.Merge(prev_set, current_set);
                }
            }

            if (row[0].South != null)
            {
                RowState nextRowState = rowState.Next();
                foreach (var kvp in rowState.EachSet())
                {
                    var shuffledList = kvp.Value
                        .OrderBy(t => Random.Range(0, 100))
                        .Select((cell, index) =>
                        {
                            return new
                            {
                                Cell = cell,
                                Index = index
                            };
                        })
                        .ToList();

                    foreach (var item in shuffledList)
                    {
                        if (item.Index == 0 || Random.Range(0, 3) == 0)
                        {
                            item.Cell.Link(item.Cell.South);
                            nextRowState.Record(rowState.SetFor(item.Cell), item.Cell.South);
                        }
                    }
                }

                rowState = nextRowState;
            }
        }
    }

    class RowState
    {
        private int _gridWidth;
        private List<int> _setForCell;
        private Dictionary<int, List<Cell>> _cellsInSet;
        private int _nextSet;
        public RowState( int gridWidth, int startingSet = 0)
        {
            _gridWidth = gridWidth;
            _cellsInSet = new Dictionary<int, List<Cell>>();
            _nextSet = startingSet;
            _setForCell = new List<int>(_gridWidth);
            for (int i = 0; i < _gridWidth; i++)
            {
                _setForCell.Add(-1);
            }
        }

        public void Record(int setNumber, Cell cell)
        {
            _setForCell[cell.Column] = setNumber;

            if (!_cellsInSet.ContainsKey(setNumber))
            {
                _cellsInSet[setNumber] = new List<Cell>();
            }
            _cellsInSet[setNumber].Add(cell);
        }

        public int SetFor(Cell cell)
        {
            if (_setForCell[cell.Column] == -1)
            {
                Record(_nextSet, cell);
                _nextSet++;
            }

            return _setForCell[cell.Column];
        }

        public void Merge(int winner, int loser)
        {
            foreach (var cell in _cellsInSet[loser])
            {
                _setForCell[cell.Column] = winner;
                _cellsInSet[winner].Add(cell);
            }
            _cellsInSet.Remove(loser);
        }

        public RowState Next()
        {
            return new RowState(_gridWidth, _nextSet);
        }

        public IEnumerable<KeyValuePair<int, List<Cell>>> EachSet()
        {
            foreach (var item in _cellsInSet)
            {
                yield return item;
            }
        }
    }
}
