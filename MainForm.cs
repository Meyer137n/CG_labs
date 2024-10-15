using System;
using System.Drawing;
using System.Reflection.Metadata;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace CG_LAB1
{
    public partial class MainForm : Form
    {
        private float[,] T;
        private float[,] proection;
        private int cenX;
        private int cenY;
        private Graphics _graphics;

        public MainForm() 
            => InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cenX = Size.Width / 2 - 700;
            cenY = Size.Height / 2 - 250;
            SetDefaultPosition();
            //кабинетное проецирование относительно центра правосторонней системы координат
            float[,] p =
            {
                { 1, 0, 0, 0},
                { 0, -1, 0, 0},
                { -(float)(Math.Cos(Math.PI/4))/2, (float)(Math.Cos(Math.PI/4))/2, 0, 0},
                { cenX, cenY, 0, 1}
            };
            proection = p;
            DrawT();
        }

        //умножение матриц
        private float[,] Mult(float[,] X, float[,] Y)
        {
            float[,] result = new float[X.GetLength(0), Y.GetLength(1)];
            for (int i = 0; i < X.GetLength(0); i++)
                for (int j = 0; j < Y.GetLength(1); j++)
                    for (int k = 0; k < Y.GetLength(0); k++)
                        result[i, j] += X[i, k] * Y[k, j];
            return result;
        }

        //отрисовка осей
        private void DrawAxis()
        {
            _graphics = CreateGraphics();
            _graphics.Clear(Color.White);
            float[,] Axis =
            {
                { 0, 0, 0, 1},
                { 500, 0, 0, 1},
                { 0, 400, 0, 1},
                { 0, 0, 500, 1},
                { 490, 5, 0, 1},
                { 490, -5, 0, 1},
                { 5, 390, 0, 1},
                { -5, 390, 0, 1},
                { 12, 0, 495, 1},
                { -10, 0, 480, 1}
            };
            Axis = Mult(Axis, proection);
            #region X
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[1, 0], Axis[1, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[1, 0], Axis[1, 1], Axis[4, 0], Axis[4, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[1, 0], Axis[1, 1], Axis[5, 0], Axis[5, 1]);
            #endregion
            #region Y
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[2, 0], Axis[2, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[2, 0], Axis[2, 1], Axis[6, 0], Axis[6, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[2, 0], Axis[2, 1], Axis[7, 0], Axis[7, 1]);
            #endregion
            #region Z
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[3, 0], Axis[3, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[3, 0], Axis[3, 1], Axis[8, 0], Axis[8, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[3, 0], Axis[3, 1], Axis[9, 0], Axis[9, 1]);
            #endregion
        }

        private void SetDefaultPosition()
        {
            // Координаты для буквы "T"
            float[,] DefT =
            {
        // Перекладина сверху
        { -30, 50, 0, 1 },  // A - левая верхняя точка перекладины
        { 30, 50, 0, 1 },   // B - правая верхняя точка перекладины
        { 30, 40, 0, 1 },   // C - правая нижняя точка перекладины
        { -30, 40, 0, 1 },  // D - левая нижняя точка перекладины

        // Ножка
        { -10, 40, 0, 1 },  // E - верхняя левая точка ножки
        { 10, 40, 0, 1 },   // F - верхняя правая точка ножки
        { 10, 0, 0, 1 },    // G - нижняя правая точка ножки
        { -10, 0, 0, 1 },   // H - нижняя левая точка ножки

        // Вершины для 3D (с высотой)
        { -30, 50, 10, 1 },  // A' - левая верхняя точка перекладины с высотой
        { 30, 50, 10, 1 },   // B' - правая верхняя точка перекладины с высотой
        { 30, 40, 10, 1 },   // C' - правая нижняя точка перекладины с высотой
        { -30, 40, 10, 1 },  // D' - левая нижняя точка перекладины с высотой
        { -10, 40, 10, 1 },  // E' - верхняя левая точка ножки с высотой
        { 10, 40, 10, 1 },   // F' - верхняя правая точка ножки с высотой
        { 10, 0, 10, 1 },    // G' - нижняя правая точка ножки с высотой
        { -10, 0, 10, 1 }    // H' - нижняя левая точка ножки с высотой
    };

            T = DefT;
        }


        // Отрисовка проекции буквы "T"
        private void DrawT()
        {
            _graphics = CreateGraphics();
            DrawAxis();
            float[,] matrixDraw = Mult(T, proection);

            // Перекладина "T"
            _graphics.DrawLine(Pens.Red, matrixDraw[0, 0], matrixDraw[0, 1], matrixDraw[1, 0], matrixDraw[1, 1]); // A -> B
            _graphics.DrawLine(Pens.Red, matrixDraw[1, 0], matrixDraw[1, 1], matrixDraw[2, 0], matrixDraw[2, 1]); // B -> C
            _graphics.DrawLine(Pens.Red, matrixDraw[2, 0], matrixDraw[2, 1], matrixDraw[3, 0], matrixDraw[3, 1]); // C -> D
            _graphics.DrawLine(Pens.Red, matrixDraw[3, 0], matrixDraw[3, 1], matrixDraw[0, 0], matrixDraw[0, 1]); // D -> A

            // Ножка "T"
            _graphics.DrawLine(Pens.Red, matrixDraw[4, 0], matrixDraw[4, 1], matrixDraw[5, 0], matrixDraw[5, 1]); // E -> F
            _graphics.DrawLine(Pens.Red, matrixDraw[5, 0], matrixDraw[5, 1], matrixDraw[6, 0], matrixDraw[6, 1]); // F -> G
            _graphics.DrawLine(Pens.Red, matrixDraw[6, 0], matrixDraw[6, 1], matrixDraw[7, 0], matrixDraw[7, 1]); // G -> H
            _graphics.DrawLine(Pens.Red, matrixDraw[7, 0], matrixDraw[7, 1], matrixDraw[4, 0], matrixDraw[4, 1]); // H -> E

            // Соединяем передние точки с задними (для 3D)
            for (int i = 0; i < 8; i++)
            {
                _graphics.DrawLine(Pens.Red, matrixDraw[i, 0], matrixDraw[i, 1], matrixDraw[i + 8, 0], matrixDraw[i + 8, 1]);
            }

            // Перекладина сзади
            _graphics.DrawLine(Pens.Red, matrixDraw[8, 0], matrixDraw[8, 1], matrixDraw[9, 0], matrixDraw[9, 1]);  // A' -> B'
            _graphics.DrawLine(Pens.Red, matrixDraw[9, 0], matrixDraw[9, 1], matrixDraw[10, 0], matrixDraw[10, 1]); // B' -> C'
            _graphics.DrawLine(Pens.Red, matrixDraw[10, 0], matrixDraw[10, 1], matrixDraw[11, 0], matrixDraw[11, 1]); // C' -> D'
            _graphics.DrawLine(Pens.Red, matrixDraw[11, 0], matrixDraw[11, 1], matrixDraw[8, 0], matrixDraw[8, 1]);  // D' -> A'

            // Ножка сзади
            _graphics.DrawLine(Pens.Red, matrixDraw[12, 0], matrixDraw[12, 1], matrixDraw[13, 0], matrixDraw[13, 1]); // E' -> F'
            _graphics.DrawLine(Pens.Red, matrixDraw[13, 0], matrixDraw[13, 1], matrixDraw[14, 0], matrixDraw[14, 1]); // F' -> G'
            _graphics.DrawLine(Pens.Red, matrixDraw[14, 0], matrixDraw[14, 1], matrixDraw[15, 0], matrixDraw[15, 1]); // G' -> H'
            _graphics.DrawLine(Pens.Red, matrixDraw[15, 0], matrixDraw[15, 1], matrixDraw[12, 0], matrixDraw[12, 1]); // H' -> E'
        }


        //поместить буквы начального размера в центр системы координат
        private void buttonDeffaultPosition_Click(object sender, EventArgs e)
        {
            SetDefaultPosition();
            DrawT();
        }

        //движение вдоль OX в положительном направлении
        private void MovePlusX_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { toMove, 0, 0, 1}
            };
            T = Mult(T, Move);
            DrawT();
        }

        //движение вдоль OX в отрицательном направлении
        private void MoveMinusX_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { -toMove, 0, 0, 1}
            };
            T = Mult(T, Move);
            DrawT();
        }

        //движение вдоль OY в положительном направлении
        private void MovePlusY_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, toMove, 0, 1}
            };
            T = Mult(T, Move);
            DrawT();
        }

        //движение вдоль OY в отрицательном направлении
        private void MoveMinusY_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, -toMove, 0, 1}
            };
            T = Mult(T, Move);
            DrawT();
        }

        //движение вдоль OZ в положительном направлении
        private void MovePlusZ_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, toMove, 1}
            };
            T = Mult(T, Move);
            DrawT();
        }

        //движение вдоль OZ в отрицательном направлении
        private void MoveMinusZ_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, -toMove, 1}
            };
            T = Mult(T, Move);
            DrawT();
        }

        // Общее вращение вокруг OX
        private void RotateAroundGlobalAxisX(float angle)
        {
            float[,] RotateXGlobal =
            {
        { 1, 0, 0, 0 },
        { 0, (float)Math.Cos(angle), -(float)Math.Sin(angle), 0 },
        { 0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0 },
        { 0, 0, 0, 1 }
    };

            T = Mult(T, RotateXGlobal);
        }

        //вращение вокруг OX вправо
        private void RotateRightX_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI /180);
            RotateAroundGlobalAxisX(angle);
            DrawT();

        }

        //вращение вокруг OX влево
        private void RotateLeftX_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            RotateAroundGlobalAxisX(-angle);
            DrawT();
        }

        // Общее вращение вокруг OY
        private void RotateAroundGlobalAxisY(float angle)
        {
            float[,] RotateYGlobal =
            {
                { (float)Math.Cos(angle), 0, (float)Math.Sin(angle), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0 },
                { 0, 0, 0, 1 }
            };

            T = Mult(T, RotateYGlobal);
        }

        //вращение вокруг OY вправо
        private void RotateRightY_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            RotateAroundGlobalAxisY(angle);
            DrawT();
        }

        //вращение вокруг OY влево
        private void RotateLeftY_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            RotateAroundGlobalAxisY(-angle);
            DrawT();
        }


        // Общее вращение вокруг OZ
        private void RotateAroundGlobalAxisZ(float angle)
        {
            float[,] RotateZGlobal =
            {
        { (float)Math.Cos(angle), -(float)Math.Sin(angle), 0, 0 },
        { (float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 0, 0, 1 }
    };

            T = Mult(T, RotateZGlobal);
        }

        //вращение вокруг OZ вправо
        private void RotateRightZ_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            RotateAroundGlobalAxisZ(angle);
            DrawT();
        }

        //вращение вокруг OZ влево
        private void RotateLeftZ_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            RotateAroundGlobalAxisZ(-angle);
            DrawT();
        }

        //растяжение
        private void Stretch_Click(object sender, EventArgs e)
        {
            float[,] Stretch =
            {
                { 2, 0, 0, 0},
                { 0, 2, 0, 0},
                { 0, 0, 2, 0},
                { 0, 0, 0, 1}
            };
            T = Mult(T, Stretch);
            DrawT();
        }

        //сжатие
        private void Clench_Click(object sender, EventArgs e)
        {
            float[,] Clench =
            {
                { (float)(0.5), 0, 0, 0},
                { 0, (float)(0.5), 0, 0},
                { 0, 0, (float)(0.5), 0},
                { 0, 0, 0, 1}
            };
            T = Mult(T, Clench);
            DrawT();
        }


        // Вращение вокруг своей оси X
        private void SpinX()
        {
            int toRotate = 5;
            float angle = (float)(toRotate * Math.PI / 180);

            // Вычисляем центр фигуры
            float centerX = (T[0, 0] + T[1, 0]) / 2;
            float centerY = (T[4, 1] + T[7, 1]) / 2;
            float centerZ = (T[0, 2] + T[8, 2]) / 2;

            // Матрица вращения вокруг оси X без смещения в начало координат
            float[,] Rotate =
            {
        { 1, 0, 0, 0 },
        { 0, (float)Math.Cos(angle), -(float)Math.Sin(angle), 0 },
        { 0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0 },
        { 0, 0, 0, 1 }
    };

            // Применяем вращение к каждой вершине
            for (int i = 0; i < T.GetLength(0); i++)
            {
                float[] vertex = { T[i, 0] - centerX, T[i, 1] - centerY, T[i, 2] - centerZ, 1 };
                float[] rotatedVertex = new float[4];

                for (int row = 0; row < 4; row++)
                {
                    rotatedVertex[row] = 0;
                    for (int col = 0; col < 4; col++)
                    {
                        rotatedVertex[row] += Rotate[row, col] * vertex[col];
                    }
                }

                // Обновляем координаты
                T[i, 0] = rotatedVertex[0] + centerX;
                T[i, 1] = rotatedVertex[1] + centerY;
                T[i, 2] = rotatedVertex[2] + centerZ;
            }

            DrawT();
        }


        // Вращение вокруг своей оси Y
        private void SpinY()
        {
            int toRotate = 5;
            float angle = (float)(toRotate * Math.PI / 180);

            // Вычисляем центр фигуры
            float centerX = (T[0, 0] + T[1, 0]) / 2;
            float centerY = (T[4, 1] + T[7, 1]) / 2;
            float centerZ = (T[0, 2] + T[8, 2]) / 2;

            // Матрица вращения вокруг оси Y
            float[,] Rotate =
            {
        { (float)Math.Cos(angle), 0, (float)Math.Sin(angle), 0 },
        { 0, 1, 0, 0 },
        { -(float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0 },
        { 0, 0, 0, 1 }
    };

            // Применяем вращение к каждой вершине
            for (int i = 0; i < T.GetLength(0); i++)
            {
                float[] vertex = { T[i, 0] - centerX, T[i, 1] - centerY, T[i, 2] - centerZ, 1 };
                float[] rotatedVertex = new float[4];

                for (int row = 0; row < 4; row++)
                {
                    rotatedVertex[row] = 0;
                    for (int col = 0; col < 4; col++)
                    {
                        rotatedVertex[row] += Rotate[row, col] * vertex[col];
                    }
                }

                // Обновляем координаты
                T[i, 0] = rotatedVertex[0] + centerX;
                T[i, 1] = rotatedVertex[1] + centerY;
                T[i, 2] = rotatedVertex[2] + centerZ;
            }

            DrawT();
        }


        // Вращение вокруг своей оси Z
        private void SpinZ()
        {
            int toRotate = 5;
            float angle = (float)(toRotate * Math.PI / 180);

            // Вычисляем центр фигуры
            float centerX = (T[0, 0] + T[1, 0]) / 2;
            float centerY = (T[4, 1] + T[7, 1]) / 2;
            float centerZ = (T[0, 2] + T[8, 2]) / 2;

            // Матрица вращения вокруг оси Z
            float[,] Rotate =
            {
        { (float)Math.Cos(angle), -(float)Math.Sin(angle), 0, 0 },
        { (float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 0, 0, 1 }
    };

            // Применяем вращение к каждой вершине
            for (int i = 0; i < T.GetLength(0); i++)
            {
                float[] vertex = { T[i, 0] - centerX, T[i, 1] - centerY, T[i, 2] - centerZ, 1 };
                float[] rotatedVertex = new float[4];

                for (int row = 0; row < 4; row++)
                {
                    rotatedVertex[row] = 0;
                    for (int col = 0; col < 4; col++)
                    {
                        rotatedVertex[row] += Rotate[row, col] * vertex[col];
                    }
                }

                // Обновляем координаты
                T[i, 0] = rotatedVertex[0] + centerX;
                T[i, 1] = rotatedVertex[1] + centerY;
                T[i, 2] = rotatedVertex[2] + centerZ;
            }

            DrawT();
        }

        // Анимация по оси X
        private void RotateAndAnimateX_Click(object sender, EventArgs e)
        {
            int steps = 360; // Количество шагов для полного оборота вокруг оси X
            int currentStep = 0;
            float globalAngleStep = (float)(Math.PI / 180); // Угол в радианах для вращения вокруг оси X (1 градус)

            Timer timer = new Timer();
            timer.Interval = 10; // Ускорим анимацию
            timer.Tick += new EventHandler((o, ev) =>
            {
                if (currentStep < steps)
                {
                    currentStep++;

                    // Вращение фигуры вокруг оси Y системы координат
                    RotateAroundGlobalAxisX(-globalAngleStep);

                    // Вращение фигуры вокруг её геометрического центра относительно оси X
                    SpinX();
                }
                else
                {
                    Timer t = o as Timer;
                    t.Stop(); // Останавливаем таймер после завершения анимации
                }
            });

            timer.Start();
        }

        // Анимация по оси Y
        private void RotateAndAnimateY_Click(object sender, EventArgs e)
        {
            int steps = 360; // Количество шагов для полного оборота вокруг глобальной оси
            int currentStep = 0;
            float globalAngleStep = (float)(Math.PI / 180);  // Увеличиваем скорость вращения вокруг центра (4 градуса)

            Timer timer = new Timer();
            timer.Interval = 10; // Ускорим анимацию, уменьшив интервал таймера до 10 мс
            timer.Tick += new EventHandler((o, ev) =>
            {
                if (currentStep < steps)
                {
                    currentStep++;

                    // Вращение фигуры вокруг оси Y системы координат
                    RotateAroundGlobalAxisY(-globalAngleStep);

                    // Ускоренное вращение фигуры вокруг её геометрического центра
                    SpinY();
                }
                else
                {
                    Timer t = o as Timer;
                    t.Stop(); // Останавливаем таймер после завершения анимации
                }
            });

            timer.Start();
        }

        // Анимация по оси Z
        private void RotateAndAnimateZ_Click(object sender, EventArgs e)
        {
            int steps = 360; // Количество шагов для полного оборота вокруг оси Z
            int currentStep = 0;
            float globalAngleStep = (float)(Math.PI / 180); // Угол в радианах для вращения вокруг оси Z (1 градус)

            Timer timer = new Timer();
            timer.Interval = 10; // Ускорим анимацию
            timer.Tick += new EventHandler((o, ev) =>
            {
                if (currentStep < steps)
                {
                    currentStep++;

                    // Вращение фигуры вокруг оси Z системы координат
                    RotateAroundGlobalAxisZ(-globalAngleStep);

                    // Ускоренное вращение фигуры вокруг её геометрического центра относительно оси Z
                    SpinZ();
                }
                else
                {
                    Timer t = o as Timer;
                    t.Stop(); // Останавливаем таймер после завершения анимации
                }
            });

            timer.Start();
        }
    }
}
