using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace сg_lab1
{
    public partial class MainForm : Form
    {
        private float[,] Z;
        private float[,] proection;
        private int cenX;
        private int cenY;
        private Graphics _graphics;

        public MainForm() 
            => InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cenX = Size.Width / 2;
            cenY = Size.Height / 2;
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
            DrawZ();
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

            Z = DefT;
        }


        // Отрисовка проекции буквы "T"
        private void DrawZ()
        {
            _graphics = CreateGraphics();
            DrawAxis();
            float[,] matrixDraw = Mult(Z, proection);

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
            DrawZ();
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
            Z = Mult(Z, Move);
            DrawZ();
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
            Z = Mult(Z, Move);
            DrawZ();
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
            Z = Mult(Z, Move);
            DrawZ();
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
            Z = Mult(Z, Move);
            DrawZ();
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
            Z = Mult(Z, Move);
            DrawZ();
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
            Z = Mult(Z, Move);
            DrawZ();
        }

        //вращение вокруг OX вправо
        private void RotateRightX_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI /180);
            float[,] Rotate =
            {
                { 1, 0, 0, 0},
                { 0, (float)(Math.Cos(angle)), (float)(Math.Sin(angle)), 0},
                { 0, -(float)(Math.Sin(angle)), (float)(Math.Cos(angle)), 0},
                { 0, 0, 0, 1}
            };
            Z = Mult(Z, Rotate);
            DrawZ();
        }

        //вращение вокруг OX влево
        private void RotateLeftX_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { 1, 0, 0, 0},
                { 0, (float)Math.Cos(angle), -((float)(Math.Sin(angle))), 0},
                { 0, ((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            Z = Mult(Z, Rotate);
            DrawZ();
        }

        //вращение вокруг OY вправо
        private void RotateRightY_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), 0, ((float)(Math.Sin(angle))), 0},
                { 0, 1, 0, 0},
                { -((float)(Math.Sin(angle))), 0, ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            Z = Mult(Z, Rotate);
            DrawZ();
        }

        //вращение вокруг OY влево
        private void RotateLeftY_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), 0, -((float)(Math.Sin(angle))), 0},
                { 0, 1, 0, 0},
                { ((float)(Math.Sin(angle))), 0, ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            Z = Mult(Z, Rotate);
            DrawZ();
        }

        //вращение вокруг OZ вправо
        private void RotateRightZ_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), -((float)(Math.Sin(angle))), 0, 0},
                { ((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };
            Z = Mult(Z, Rotate);
            DrawZ();
        }

        //вращение вокруг OZ влево
        private void RotateLeftZ_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), ((float)(Math.Sin(angle))), 0, 0},
                { -((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };
            Z = Mult(Z, Rotate);
            DrawZ();
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
            Z = Mult(Z, Stretch);
            DrawZ();
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
            Z = Mult(Z, Clench);
            DrawZ();
        }

        private float[] GetGeometricCenter()
        {
            float centerX = 0, centerY = 0, centerZ = 0;
            int numPoints = Z.GetLength(0);

            for (int i = 0; i < numPoints; i++)
            {
                centerX += Z[i, 0];
                centerY += Z[i, 1];
                centerZ += Z[i, 2];
            }

            centerX /= numPoints;
            centerY /= numPoints;
            centerZ /= numPoints;

            return new float[] { centerX, centerY, centerZ };
        }

        private void MoveToOrigin(float[] center)
        {
            float[,] MoveToOrigin =
            {
        { 1, 0, 0, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 },
        { -center[0], -center[1], -center[2], 1 }
    };

            Z = Mult(Z, MoveToOrigin);
        }

        private void Rotate(float angle, char axis)
        {
            float[,] RotateMatrix;
            switch (axis)
            {
                case 'X':
                    RotateMatrix = new float[,]
                    {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(angle), (float)Math.Sin(angle), 0 },
                { 0, -(float)Math.Sin(angle), (float)Math.Cos(angle), 0 },
                { 0, 0, 0, 1 }
                    };
                    break;
                case 'Y':
                    RotateMatrix = new float[,]
                    {
                { (float)Math.Cos(angle), 0, -(float)Math.Sin(angle), 0 },
                { 0, 1, 0, 0 },
                { (float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0 },
                { 0, 0, 0, 1 }
                    };
                    break;
                case 'Z':
                    RotateMatrix = new float[,]
                    {
                { (float)Math.Cos(angle), (float)Math.Sin(angle), 0, 0 },
                { -(float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
                    };
                    break;
                default:
                    throw new ArgumentException("Неправильная ось вращения");
            }

            Z = Mult(Z, RotateMatrix);
        }

        private void MoveBackToCenter(float[] center)
        {
            float[,] MoveBack =
            {
        { 1, 0, 0, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 },
        { center[0], center[1], center[2], 1 }
    };

            Z = Mult(Z, MoveBack);
        }

        private void RotateAroundGeometricCenter(float angle, char axis)
        {
            // Шаг 1: Вычисляем центр фигуры
            float[] center = GetGeometricCenter();

            // Шаг 2: Перемещаем фигуру в начало координат
            MoveToOrigin(center);

            // Шаг 3: Выполняем вращение
            Rotate(angle, axis);

            // Шаг 4: Возвращаем фигуру на исходное место
            MoveBackToCenter(center);

            // Перерисовываем фигуру
            DrawZ();
        }

        private void RotateAndAnimateX(object sender, EventArgs e)
        {
            int steps = 360; // Количество шагов для полного оборота вокруг оси X
            int currentStep = 0;
            float globalAngleStep = (float)(Math.PI / 180); // Угол в радианах для вращения вокруг оси X (1 градус)
            float centerAngleStep = (float)(Math.PI / 90);  // Ускоренное вращение вокруг центра (4 градуса)

            Timer timer = new Timer();
            timer.Interval = 10; // Ускорим анимацию
            timer.Tick += new EventHandler((o, ev) =>
            {
                if (currentStep < steps)
                {
                    currentStep++;

                    // Вращение фигуры вокруг оси X системы координат
                    RotateAroundGlobalAxisX(globalAngleStep);

                    // Ускоренное вращение фигуры вокруг её геометрического центра относительно оси X
                    RotateAroundGeometricCenter(centerAngleStep, 'X');

                    // Перерисовка фигуры
                    DrawZ();
                }
                else
                {
                    Timer t = o as Timer;
                    t.Stop(); // Останавливаем таймер после завершения анимации
                }
            });

            timer.Start();
        }

        private void RotateAndAnimateY(object sender, EventArgs e)
        {
            int steps = 360; // Количество шагов для полного оборота вокруг глобальной оси
            int currentStep = 0;
            float globalAngleStep = (float)(Math.PI / 180); // Угол в радианах для вращения вокруг оси Y (1 градус)
            float centerAngleStep = (float)(Math.PI / 90);  // Увеличиваем скорость вращения вокруг центра (4 градуса)

            Timer timer = new Timer();
            timer.Interval = 10; // Ускорим анимацию, уменьшив интервал таймера до 10 мс
            timer.Tick += new EventHandler((o, ev) =>
            {
                if (currentStep < steps)
                {
                    currentStep++;

                    // Вращение фигуры вокруг оси Y системы координат
                    RotateAroundGlobalAxisY(globalAngleStep);

                    // Ускоренное вращение фигуры вокруг её геометрического центра
                    RotateAroundGeometricCenter(centerAngleStep, 'Y');

                    // Перерисовка фигуры
                    DrawZ();
                }
                else
                {
                    Timer t = o as Timer;
                    t.Stop(); // Останавливаем таймер после завершения анимации
                }
            });

            timer.Start();
        }

        private void RotateAndAnimateZ(object sender, EventArgs e)
        {
            int steps = 360; // Количество шагов для полного оборота вокруг оси Z
            int currentStep = 0;
            float globalAngleStep = (float)(Math.PI / 180); // Угол в радианах для вращения вокруг оси Z (1 градус)
            float centerAngleStep = (float)(Math.PI / 90);  // Ускоренное вращение вокруг центра (4 градуса)

            Timer timer = new Timer();
            timer.Interval = 10; // Ускорим анимацию
            timer.Tick += new EventHandler((o, ev) =>
            {
                if (currentStep < steps)
                {
                    currentStep++;

                    // Вращение фигуры вокруг оси Z системы координат
                    RotateAroundGlobalAxisZ(globalAngleStep);

                    // Ускоренное вращение фигуры вокруг её геометрического центра относительно оси Z
                    RotateAroundGeometricCenter(centerAngleStep, 'Z');

                    // Перерисовка фигуры
                    DrawZ();
                }
                else
                {
                    Timer t = o as Timer;
                    t.Stop(); // Останавливаем таймер после завершения анимации
                }
            });

            timer.Start();
        }


        private void RotateAroundGlobalAxisX(float angle)
        {
            float[,] RotateXGlobal =
            {
        { 1, 0, 0, 0 },
        { 0, (float)Math.Cos(angle), -(float)Math.Sin(angle), 0 },
        { 0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0 },
        { 0, 0, 0, 1 }
    };

            Z = Mult(Z, RotateXGlobal);
        }

        private void RotateAroundGlobalAxisY(float angle)
        {
            float[,] RotateYGlobal =
            {
                { (float)Math.Cos(angle), 0, -(float)Math.Sin(angle), 0 },
                { 0, 1, 0, 0 },
                { (float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0 },
                { 0, 0, 0, 1 }
            };

            Z = Mult(Z, RotateYGlobal);
        }

        //анимация вращения вокруг OY
        private void RotateAroundGlobalAxisZ(float angle)
        {
            float[,] RotateZGlobal =
            {
        { (float)Math.Cos(angle), (float)Math.Sin(angle), 0, 0 },
        { -(float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 0, 0, 1 }
    };

            Z = Mult(Z, RotateZGlobal);
        }


        //анимация движения по спирали вдоль OY
        private void taskOY_Click(object sender, EventArgs e)
        {
            RotateAndAnimateY(sender, e);
        }

        //анимация движения по спирали вдоль OX
        private void taskOX_Click(object sender, EventArgs e)
        {
            RotateAndAnimateX(sender, e);
        }

        //анимация движения по спирали вдоль OZ
        private void taskOZ_Click(object sender, EventArgs e)
        {
            RotateAndAnimateZ(sender, e);
        }

    }

}
