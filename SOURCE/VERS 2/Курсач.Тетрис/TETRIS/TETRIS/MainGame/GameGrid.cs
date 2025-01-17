﻿namespace TETRIS
{
    public class GameGrid
    {
        // Двумерный массив для хранения игрового поля
        // Этот массив представляет собой сетку, в которой каждая ячейка может содержать целое число.
        // Это может быть использовано для хранения различных идентификаторов или состояний ячеек, таких как цвет блока в игре Тетрис.
        private readonly int[,] grid;

        // Свойства для доступа к количеству строк и столбцов
        // Эти свойства позволяют получить количество строк и столбцов в игровом поле.
        public int Rows { get; }
        public int Columns { get; }

        // Индексатор для доступа к элементам игрового поля
        // Этот индексатор позволяет получать и устанавливать значения внутри игрового поля с помощью синтаксиса массива.
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        // Конструктор для создания нового экземпляра игрового поля с заданным количеством строк и столбцов
        // Конструктор инициализирует новый двумерный массив с заданными размерами и присваивает значения свойствам Rows и Columns.
        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        // Метод для проверки, находится ли заданная ячейка внутри игрового поля
        // Этот метод проверяет, находится ли заданная ячейка в пределах игрового поля.
        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        // Метод для проверки, является ли заданная ячейка пустой
        // Этот метод проверяет, является ли заданная ячейка пустой, то есть содержит ли она значение 0.
        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }

        // Метод для проверки, заполнена ли заданная строка
        public bool IsRowFull(int r)
        {
            // Проходим по каждому столбцу в указанной строке
            for (int c = 0; c < Columns; c++)
            {
                // Если в каком-либо столбце в строке есть пустое место (значение 0),
                // значит, строка не заполнена и возвращаем false
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            // Если мы прошли все столбцы и не нашли пустых мест, значит, строка заполнена
            return true;
        }

        // Метод для проверки, является ли заданная строка пустой
        public bool IsRowEmpty(int r)
        {
            // Проходим по каждому столбцу в указанной строке
            for (int c = 0; c < Columns; c++)
            {
                // Если в каком-либо столбце в строке есть непустое место (значение не равно 0),
                // значит, строка не пуста и возвращаем false
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            // Если мы прошли все столбцы и не нашли непустых мест, значит, строка пуста
            return true;
        }

        // Метод для очистки заданной строки
        private void ClearRow(int r)
        {
            // Проходим по каждому столбцу в указанной строке и устанавливаем значение 0
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        // Метод для сдвига строк вниз на заданное количество позиций
        private void MoveRowDown(int r, int numRows)
        {
            // Проходим по каждому столбцу в указанной строке
            for (int c = 0; c < Columns; c++)
            {
                // Сдвигаем каждый элемент вниз на numRows позиций
                grid[r + numRows, c] = grid[r, c];
                // Очищаем исходную позицию
                grid[r, c] = 0;
            }
        }

        // Метод для очистки полных строк и сдвига оставшихся строк вниз
        public int ClearFullRows()
        {
            int cleared = 0;

            // Проходим по строкам снизу вверх
            for (int r = Rows - 1; r >= 0; r--)
            {
                // Если строка заполнена,   очищаем ее и увеличиваем счетчик
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                // Если счетчик больше 0, сдвигаем оставшиеся строки вниз
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            // Возвращаем количество очищенных строк
            return cleared;
        }
    }
}