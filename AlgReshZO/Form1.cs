using org.mariuszgromada.math.mxparser;

namespace AlgReshZO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            exp = textBox1.Text;
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.ScrollToCaret();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (CheckFormula(textBox1.Text))
            {
                textBox2.Text = String.Join("; ", chars.Select(x => x.Value));
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                textBox2.Enabled = true;
                textBox1.Enabled = false;
                label2.Text = labelText + String.Join("; ", chars.Select(x => x.Key));
                label4.Text = "";
            }
            else
            {
                label4.Text = "Проверьте правильность введенной формулы";
            }
            button2.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var vals = textBox2.Text.Replace('.', ',').Replace(" ", "").Split(';', StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != chars.Count)
                label3.Text = "Проверьте правильность введенных параметров";
            else
                label3.Text = "";
            try
            {
                var parsed = vals.Select(x => double.Parse(x)).ToList();
                var cnt = 0;
                foreach (var item in chars)
                {
                    chars[item.Key] = parsed[cnt];
                    cnt++;
                }
            }
            catch (Exception)
            {
                label3.Text = "Проверьте правильность введенных параметров";
                return;
            }
            richTextBox1.Text = "";
            new Thread(() => {
                Calculate();

            }).Start();
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Text = "";
            richTextBox1.Text = "";
            button3.Enabled = false;
            button2.Enabled= false;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public static string exp = "3*x^2+y^2+7*z^2+x*y-2*y*z+8*x-6*y-6*z+1";
        public static Dictionary<char, double> chars;

        public bool CheckFormula(string expression)
        {
            char[] oddChars = { '+', '-', '*', '/', '^', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            var splittedStrings = expression.Split(oddChars, StringSplitOptions.RemoveEmptyEntries);
            if (splittedStrings.Any(x => x.Length != 1))
                return false;
            var splittedChars = splittedStrings.Distinct().Select(x => x[0]).OrderBy(x => x).ToArray();
            Random rnd = new Random();
            var values = splittedChars.ToDictionary(x => x, y => (double)rnd.Next(1, 100));
            var funcResult = CalculateFunction(values);
            if (Double.IsNaN((double)funcResult))
                return false;
            exp = expression;
            chars = values;
            return true;
        }

        public void PrintLog(string value)
        {
            richTextBox1.Invoke((MethodInvoker)delegate { 
                richTextBox1.Text = richTextBox1.Text + value + "\n";
            });
        }

        public void Calculate()
        {
            License.iConfirmCommercialUse("Garik");
            var eps = 0.001;
            var k = 0;

            var previousSk = new Dictionary<char, double>();
            var currentSk = new Dictionary<char, double>();

            var previousX = new Dictionary<char, double>();
            var currentX = chars.ToDictionary(x => x.Key, y => y.Value);

            var previousGradient = new Dictionary<char, double>();
            var currentGradient = currentX.ToDictionary(x => x.Key, y => 0d);

            PrintLog($"Функция: {exp}");
            PrintVector($"точка X{k}", currentX);
            PrintLog($"eps = {eps}");
            PrintLog($"");

            while (true)
            {
                PrintLog($"Шаг {k + 1}");
                PrintLog($"k = {k}");

                PrintLog($"Значение функции в точке Х{k}: {CalculateFunction(currentX)}");

                //Считаем градиент
                foreach (var item in currentX)
                {
                    var newExp = exp;

                    foreach (var item2 in currentX)
                    {
                        if (item.Key != item2.Key)
                        {
                            var val = item2.Value.ToString().Replace(',', '.');
                            newExp = newExp.Replace(item2.Key.ToString(), val);
                        }

                    }
                    Expression e = new Expression($"der({newExp}, {item.Key}, {item.Value.ToString().Replace(',', '.')})");

                    double result = e.calculate();

                    currentGradient[item.Key] = result;
                }
                PrintVector("Градиент", currentGradient);

                //Проверяем что ∇fxk≤ε
                var xz = Math.Round(VectorsProduct(currentGradient, currentGradient), 4);
                if (xz <= eps)
                {
                    PrintLog($"||∇f(x)||={xz} <= {eps}");
                    break;
                }
                else
                {
                    PrintLog($"||∇f(x)||={xz} > {eps}");
                }

                //Считаем Sk

                if (k == 0)
                {
                    currentSk = currentGradient.ToDictionary(x => x.Key, y => -y.Value);
                }
                else
                {
                    var b = Math.Round(VectorsProduct(currentGradient, currentGradient) / VectorsProduct(previousGradient, previousGradient), 4);
                    PrintLog($"b = {b}");
                    previousSk = currentSk;
                    currentSk = currentGradient.ToDictionary(x => x.Key, y => -y.Value + b * previousSk[y.Key]);
                }

                PrintVector($"S{k}", currentSk);

                //Считаем альфу

                var alpha = GetAlpha(currentX, currentSk);
                PrintLog($"Alpha = {alpha}");

                //Считаем следующий Х
                k++;
                previousGradient = currentGradient.ToDictionary(x => x.Key, y => y.Value);
                previousX = currentX;
                currentX = GetNextX(currentX, currentSk, alpha);
                PrintVector($"точка X{k}", currentX);

                //Проверка
                var rv = VectorsDifference(currentX, previousX);
                var pv = VectorsProduct(rv, rv);

                if (pv <= eps)
                {
                    PrintLog($"||x{k}-x{k-1}||={pv} <= {eps}");
                    break;
                }
                else
                {
                    PrintLog($"||x{k}-x{k - 1}||={pv} > {eps}");
                }
                PrintLog("");
            }
            PrintLog("Рассчет закончен");
            button3.Invoke((MethodInvoker)delegate {
                button3.Enabled = true;
            });
        }

        double VectorsProduct(Dictionary<char, double> vector1, Dictionary<char, double> vector2)
        {
            return Math.Round(vector1.Sum(x => x.Value * vector2[x.Key]), 4);
        }

        Dictionary<char, double> VectorsDifference(Dictionary<char, double> vector1, Dictionary<char, double> vector2)
        {
            return vector1.ToDictionary(x => x.Key, y => y.Value - vector2[y.Key]);
        }

        double CalculateFunction(Dictionary<char, double> values)
        {
            var newExp = exp;
            foreach (var item in values)
            {
                newExp = newExp.Replace(item.Key.ToString(), item.Value.ToString().Replace(',', '.'));
            }
            var newEExp = new Expression(newExp);
            var newEres = newEExp.calculate();
            return newEres;
        }

        void PrintVector(string description, Dictionary<char, double> values)
        {
            var resStr = $"{description}: ({String.Join(", ", values.Select(x => Math.Round(x.Value, 4)))})";
            PrintLog(resStr);
        }

        Dictionary<char, double> GetNextX(Dictionary<char, double> values, Dictionary<char, double> antiGrad, double alpha)
        {
            return values.ToDictionary(x => x.Key, y => y.Value + alpha * antiGrad[y.Key]);
        }

        double GetAlpha(Dictionary<char, double> values, Dictionary<char, double> antiGrad)
        {
            int total = 0;
            double eps = 0.001, delta = 0.001;
            double x1, x2, y1, y2;
            double a = -10000, b = 10000;
            while ((b - a) > 2 * eps)
            {
                x1 = (a + b - delta) / 2;
                x2 = (a + b + delta) / 2;
                total++;
                var temp = GetNextX(values, antiGrad, x1);
                y1 = CalculateFunction(temp);
                temp = GetNextX(values, antiGrad, x2);
                y2 = CalculateFunction(temp);

                if (y1 > y2)
                    a = x1;
                else
                    b = x2;
                Console.WriteLine($"total: {total}");
            }
            Console.WriteLine($"total: {total}");
            return Math.Round((a + b) / 2, 4);
        }
    }
}
