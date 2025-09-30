using System;
using System.Numerics; 

public sealed class TLine2D<T> where T : struct, INumber<T>
{
    private static readonly T EPS = T.CreateChecked(1e-9);

public T A { get; set; }
    public T B { get; set; }
    public T C { get; set; }

    public TLine2D() { A = B = C = T.Zero; }
    public TLine2D(T a, T b, T c) { A = a; B = b; C = c; }

    public void Input()
    {
        Console.Write($"Введіть коефіцієнти прямої ({typeof(T).Name}) (a b c): ");
        string[] parts = Console.ReadLine().Split();
        A = T.Parse(parts[0], null);
        B = T.Parse(parts[1], null);
        C = T.Parse(parts[2], null);
    }

    public void Print()
    {
        Console.WriteLine($"{A}x + {B}y + {C} = 0");
    }

    public bool Belongs(T x, T y)
    {
        T val = A * x + B * y + C;
        return T.Abs(val) < EPS;
    }

    public int IntersectWith(TLine2D<T> other, out T x, out T y)
    {
        T det = A * other.B - other.A * B;

        if (T.Abs(det) > EPS)
        {
            x = (B * other.C - other.B * C) / det;
            y = (other.A * C - A * other.C) / det;
            return 0; 
        }
        else
        {
            x = y = T.Zero;
            if (T.Abs(A * other.C - other.A * C) < EPS &&
                T.Abs(B * other.C - other.B * C) < EPS)
                return 2; 
            else
                return 1; 
        }
    }

}

class TestLine
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

    TLine2D<double> line1 = new TLine2D<double>(1, 1, -2);
        Console.Write("Перша пряма: ");
        line1.Print();

        Console.WriteLine("\n=== Введення другої прямої ===");
        TLine2D<double> line2 = new TLine2D<double>();
        line2.Input();
        Console.Write("Друга пряма: "); line2.Print();

        int status = line1.IntersectWith(line2, out double x, out double y);
        Console.WriteLine();
        if (status == 0)
            Console.WriteLine($"Прямі перетинаються в точці ({x:F2}, {y:F2})");
        else if (status == 1)
            Console.WriteLine("Прямі паралельні.");
        else
            Console.WriteLine("Прямі збігаються.");

        var sum = new TLine2D<double>(line1.A + line2.A, line1.B + line2.B, line1.C + line2.C);
        var diff = new TLine2D<double>(line1.A - line2.A, line1.B - line2.B, line1.C - line2.C);

        Console.WriteLine("\nСума прямих: "); sum.Print();
        Console.WriteLine("Різниця прямих: "); diff.Print();

        Console.WriteLine("\n=== Перевірка належності точки до прямих ===");
        Console.Write("Введіть координати точки (x y): ");
        string[] coords = Console.ReadLine().Split();
        double px = double.Parse(coords[0]);
        double py = double.Parse(coords[1]);

        if (line1.Belongs(px, py))
            Console.WriteLine($"Точка ({px}, {py}) належить першій прямій.");
        else
            Console.WriteLine($"Точка ({px}, {py}) не належить першій прямій.");

        if (line2.Belongs(px, py))
            Console.WriteLine($"Точка ({px}, {py}) належить другій прямій.");
        else
            Console.WriteLine($"Точка ({px}, {py}) не належить другій прямій.");

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }

}


