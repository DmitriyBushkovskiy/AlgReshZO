//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using org.mariuszgromada.math.mxparser;

//namespace AlgReshZO
//{
//    static internal class Calculator
//    {

//        //const string exp = "x^2+4*y^2-x*y+x";
//        public static string exp = "3*x^2+y^2+7*z^2+x*y-2*y*z+8*x-6*y-6*z+1";
//        public static Dictionary<char, decimal> chars;


//        //public Calculator(string exp)
//        //{
//        //    this.exp = exp;
//        //}

//        public static bool CheckFormula(string expr)
//        { 
//            char[] oddChars = { '+', '-', '*', '/', '^' , '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};
//            var r = expr.Split(oddChars, StringSplitOptions.RemoveEmptyEntries);
//            if (r.Any(x => x.Length != 1))
//               return false;
//            var ch = r.Distinct().Select(x => x[0]).OrderBy(x => x).ToArray();
//            Random rnd = new Random();
//            var temp = ch.ToDictionary(x => x, y => (decimal) rnd.Next(1, 100));
//            var funcRes = calculateFunc(temp);
//            if (Double.IsNaN((double)funcRes))
//                return false;
//            var rr = 1;
//            exp = expr;
//            chars = temp;
//            return true;
//        }

       

//        public static void Calculate()
//        {
//            License.iConfirmCommercialUse("Garik");
//            var eps = 0.001m;
//            var k = 0;

//            var previousSk = new Dictionary<char, decimal>();
//            var currentSk = new Dictionary<char, decimal>();

//            var previousX = new Dictionary<char, decimal>();
//            //var currentX = new Dictionary<char, decimal>()
//            //{
//            //    {'x', 6 },
//            //    {'y', 5 }
//            //};

//            var currentX = new Dictionary<char, decimal>()
//            {
//                {'x', 3 },
//                {'y', 33 },
//                { 'z', 12}
//            };

//            var previousGradient = new Dictionary<char, decimal>();
//            var currentGradient = currentX.ToDictionary(x => x.Key, y => 0m);

//            PrintVector($"X{k}", currentX);
//            //var antiGrad = new Dictionary<char, decimal>();

//            while (true)
//            {
//                Console.WriteLine($"Шаг {k + 1}");
//                Console.WriteLine($"k = {k}");

//                Console.WriteLine($"Значение функции в точке Х{k}: {calculateFunc(currentX)}");

//                //Считаем градиент
//                foreach (var item in currentX)
//                {
//                    var newExp = exp;

//                    foreach (var item2 in currentX)
//                    {
//                        if (item.Key != item2.Key)
//                        {
//                            //var val = "0.23235";
//                            //var val2 = $"({item2.Value.ToString().Replace(',', '.')})";
//                            var val = item2.Value.ToString().Replace(',', '.');
//                            newExp = newExp.Replace(item2.Key.ToString(), val /*$"({item2.Value.ToString().Replace(',', '.')})"*/);
//                            //Console.WriteLine($"newExp {item2.Key}: {newExp}");
//                        }

//                    }
//                    //Console.WriteLine($"For {item.Key} = {item.Value}");
//                    Expression e = new Expression($"der({newExp}, {item.Key}, {item.Value.ToString().Replace(',', '.')})");

//                    decimal result = (decimal)Math.Round(e.calculate(), 3);

//                    currentGradient[item.Key] = result;
//                    //Console.WriteLine("Res: " + result);
//                }
//                PrintVector("Градиент", currentGradient);

//                //Проверяем что ∇fxk≤ε
//                var xz = ProizvVectorov(currentGradient, currentGradient);
//                if (xz <= eps)
//                {
//                    Console.WriteLine($"{xz} <= {eps}");
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine($"{xz} > {eps}");
//                }

//                //Считаем Sk

//                if (k == 0)
//                {
//                    currentSk = currentGradient.ToDictionary(x => x.Key, y => -y.Value);
//                }
//                else
//                {
//                    var b = ProizvVectorov(currentGradient, currentGradient) / ProizvVectorov(previousGradient, previousGradient);
//                    Console.WriteLine($"b = {b}");
//                    previousSk = currentSk;
//                    currentSk = currentGradient.ToDictionary(x => x.Key, y => -y.Value + b * previousSk[y.Key]);
//                }

//                PrintVector($"S{k}", currentSk);

//                //Считаем альфу

//                var alpha = getAlpha(currentX, currentSk);
//                Console.WriteLine($"Alpha = {alpha}");

//                //Считаем следующий Х
//                k++;
//                previousGradient = currentGradient.ToDictionary(x => x.Key, y => y.Value);
//                previousX = currentX;
//                currentX = getNextX(currentX, currentSk, alpha);
//                PrintVector($"X{k}", currentX);

//                //Проверка
//                var rv = RaznostVectorov(currentX, previousX);
//                var pv = ProizvVectorov(rv, rv);

//                if (pv <= eps)
//                {
//                    Console.WriteLine($"{pv} <= {eps}");
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine($"{pv} > {eps}");
//                }
//                var rrrr = 1;
//                Console.WriteLine();
//            }
//            Console.WriteLine("Рассчет закончен");
//        }

//        static decimal ProizvVectorov(Dictionary<char, decimal> vector1, Dictionary<char, decimal> vector2)
//        {
//            return vector1.Sum(x => x.Value * vector2[x.Key]);
//        }

//        static Dictionary<char, decimal> RaznostVectorov(Dictionary<char, decimal> vector1, Dictionary<char, decimal> vector2)
//        {
//            return vector1.ToDictionary(x => x.Key, y => y.Value - vector2[y.Key]);
//        }

//        static decimal calculateFunc(Dictionary<char, decimal> values)
//        {
//            var newExp = exp;
//            foreach (var item in values)
//            {
//                newExp = newExp.Replace(item.Key.ToString(), item.Value.ToString().Replace(',', '.'));
//            }
//            var newEExp = new Expression(newExp);
//            var newEres = (decimal)Math.Round(newEExp.calculate(), 3);
//            return newEres;
//        }

//        static void PrintVector(string description, Dictionary<char, decimal> values)
//        {
//            Console.Write($"{description}: ");
//            foreach (var item in values)
//            {
//                Console.Write($"{item.Key}: {item.Value}, ");
//            }
//            Console.WriteLine();
//        }

//        static Dictionary<char, decimal> getNextX(Dictionary<char, decimal> values, Dictionary<char, decimal> antiGrad, decimal alpha)
//        {
//            return values.ToDictionary(x => x.Key, y => Math.Round(y.Value + alpha * antiGrad[y.Key], 2));
//        }

//        static decimal getAlpha(Dictionary<char, decimal> values, Dictionary<char, decimal> antiGrad)
//        {
//            int total = 0;
//            decimal eps = 0.001m, delta = 0.001m;
//            decimal x1, x2, y1, y2;
//            decimal a = -10000, b = 10000;
//            while ((b - a) > 2 * eps)
//            {
//                x1 = (a + b - delta) / 2;
//                x2 = (a + b + delta) / 2;
//                total++;
//                var temp = getNextX(values, antiGrad, x1);
//                y1 = calculateFunc(temp);
//                temp = getNextX(values, antiGrad, x2);
//                y2 = calculateFunc(temp);

//                if (y1 > y2)
//                    a = x1;
//                else
//                    b = x2;
//                Console.WriteLine($"total: {total}");
//            }
//            Console.WriteLine($"total: {total}");
//            return Math.Round((a + b) / 2, 2);


//        }
//    }

//}
