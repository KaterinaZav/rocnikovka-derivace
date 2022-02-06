using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace derivace
{

    public partial class Derivace : Form
    {

        public string res = ""; 

        public Derivace()
        {
            InitializeComponent();
        }
        

        private void guide_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Níže naleznete pravidla pro zadávání vstupní funkce:" +
                "\n\n - Složené funkce vždy zadávejte se závorkami okolo argumentu: sin(2x), NE sin2x, kromě případů, kdy je argument pouze x, tam bude funkce správně interpretována, např. lnx" +
                "\n - Součiny a podíly vždy zádávejte se závorkami okolo obou členů: (2x)*(e^x), NE 2x*e^x, opět kromě případů pouze s x" +
                "\n - Odmocniny vyšších řádů zadávejte jako racionální exponenty, jakékoli číslo před odmocninou bude interpretováno jako koeficient: x^1/3, NE 3√x");
        }

        private void InsertString(string n)
        {
            var position = TextBox.SelectionStart + n.Length;  
            TextBox.Text = TextBox.Text.Insert(TextBox.SelectionStart, n);
            TextBox.SelectionStart = position;
            TextBox.Focus();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            TextBox.Text = String.Empty;
            TextBox.Focus();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var position = TextBox.SelectionStart - 1;

            if (TextBox.SelectionStart > 0)
            {
                TextBox.Text = TextBox.Text.Substring(0,TextBox.SelectionStart - 1) + TextBox.Text.Substring(TextBox.SelectionStart);
            }

            TextBox.SelectionStart = position;
            TextBox.Focus();
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            res = "";
            TextBox.Text = TextBox.Text.Replace(" ", "");
            TextBox.Text = TextBox.Text.Replace("sinx", "sin(x)");
            TextBox.Text = TextBox.Text.Replace("cosx", "cos(x)");
            TextBox.Text = TextBox.Text.Replace("tgx", "tg(x)");
            TextBox.Text = TextBox.Text.Replace("lnx", "ln(x)");
            TextBox.Text = TextBox.Text.Replace("1/", "(1)/");
            TextBox.Text = TextBox.Text.Replace("^(1)/", "^1/");
            TextBox.Text = TextBox.Text.Replace("^-(1)/", "^-1/");
            TextBox.Text = TextBox.Text.Replace("x/", "(x)/");
            TextBox.Text = TextBox.Text.Replace("/x", "/(x)");
            TextBox.Text = TextBox.Text.Replace("x*", "(x)*");
            TextBox.Text = TextBox.Text.Replace("*x", "*(x)");

            res = TakeDerivative(TextBox.Text);

            Result.Text = "f'(x): y = " + Tidy(res);
        }

        #region OperatorMethods

        private void Multiply_Click(object sender, EventArgs e)
        {
            InsertString("*");

        }

        private void Divide_Click(object sender, EventArgs e)
        {
            InsertString("/");

        }

        private void Add_Click(object sender, EventArgs e)
        {
            InsertString("+");

        }

        private void Subtract_Click(object sender, EventArgs e)
        {
            InsertString("-");

        }

        private void Period_Click(object sender, EventArgs e)
        {

            InsertString(",");
        }

        private void LeftBracket_Click(object sender, EventArgs e)
        {
            InsertString("(");

        }

        private void RightBracket_Click(object sender, EventArgs e)
        {
            InsertString(")");

        }

        private void Left_Click(object sender, EventArgs e)
        {
            TextBox.SelectionStart = TextBox.SelectionStart - 1;
            TextBox.Focus();
        }

        private void Right_Click(object sender, EventArgs e)
        {
            TextBox.SelectionStart = TextBox.SelectionStart + 1;
            TextBox.Focus();
        }

        #endregion

        #region NumberMethods

        private void Zero_Click(object sender, EventArgs e)
        {
            InsertString("0");
        }

        private void One_Click(object sender, EventArgs e)
        {
            InsertString("1");
        }

        private void Two_Click(object sender, EventArgs e)
        {
            InsertString("2");
        }

        private void Three_Click(object sender, EventArgs e)
        {
            InsertString("3");
        }

        private void Four_Click(object sender, EventArgs e)
        {
            InsertString("4");
        }

        private void Five_Click(object sender, EventArgs e)
        {
            InsertString("5");
        }

        private void Six_Click(object sender, EventArgs e)
        {
            InsertString("6");
        }

        private void Seven_Click(object sender, EventArgs e)
        {
            InsertString("7");
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            InsertString("8");
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            InsertString("9");
        }
        #endregion

        #region FunctionMethods

        private void VariableX_Click(object sender, EventArgs e)
        {
            InsertString("x");
        }

        private void Pi_Click(object sender, EventArgs e)
        {
            InsertString("π");
        }

        private void Euler_Click(object sender, EventArgs e)
        {
            InsertString("e");
        }

        private void Power_Click(object sender, EventArgs e)
        {
            InsertString("^");
        }

        private void SquareRoot_Click(object sender, EventArgs e)
        {
            InsertString("√");
        }

        private void Sin_Click(object sender, EventArgs e)
        {
            InsertString("sin()");
            TextBox.SelectionStart = TextBox.SelectionStart - 1;
        }

        private void Cos_Click(object sender, EventArgs e)
        {
            InsertString("cos()");
            TextBox.SelectionStart = TextBox.SelectionStart - 1;
        }

        private void Tg_Click(object sender, EventArgs e)
        {
            InsertString("tg()");
            TextBox.SelectionStart = TextBox.SelectionStart - 1;
        }

        private void NaturalLog_Click(object sender, EventArgs e)
        {
            InsertString("ln()");
            TextBox.SelectionStart = TextBox.SelectionStart - 1;
        }

        private void Log_Click(object sender, EventArgs e)
        {
            InsertString("log");
        }

        #endregion

        public string TakeDerTerm(string a)
        {
            string der = "";

            if (a.Contains("√"))
            {
                if (a.IndexOf('√') == 0)
                {
                    der = "1/(2√x)";
                }
                else
                {
                    der = a.Substring(0, a.IndexOf('√')) + "/(2√x)";
                }
            }
            else if (a.Contains("^"))
            {
                var j = a.IndexOf("^");
                var i = a.IndexOf("x");
                if (a.Substring(j + 1).Contains("x"))
                {
                    der = "ln" + a.Substring(0, j) + " * " + a;
                }
                else if (a.Substring(j + 1).Contains('/'))
                {
                    int o = Convert.ToInt32(a.Substring(j + 1, a.IndexOf('/') - j - 1));
                    int r = Convert.ToInt32(a.Substring(a.IndexOf('/') + 1));
                    if (i == 0)
                    {
                        der = a.Substring(j + 1) + "x^" + Convert.ToString(o - r) + "/" + Convert.ToString(r);
                    }
                    else
                    {
                        int p = Convert.ToInt32(a.Substring(0, i));
                        der = Convert.ToString(p * o) + "/" + Convert.ToString(r) + "x^" + Convert.ToString(o - r) + "/" + Convert.ToString(r);
                    }
}
                else
                {
                    var n = Convert.ToDouble(a.Substring(i + 2));
                    string k = a.Substring(i + 2);
                    if (i > 0)
                    {
                        if (a.Substring(0, i).Contains("ln"))
                        {
                            int h = a.IndexOf("ln");
                            if (h == 0)
                            {
                                k = Convert.ToString(n) + a.Substring(0, i);
                            }
                            else
                            {
                                double l = Convert.ToDouble(a.Substring(0, h));
                                k = Convert.ToString(l * n) + a.Substring(h, i - h);
                            }
                        }
                        else if(a.Substring(0, i).Contains("π"))
                        {
                            int h = a.IndexOf("π");
                            if (h == 0)
                            {
                                k = Convert.ToString(n) + a.Substring(0, i);
                            }
                            else
                            {
                                double l = Convert.ToDouble(a.Substring(0, a.IndexOf("π"))) ;
                                k = Convert.ToString(l * n) + "π";
                            }
                        }
                        else if (a.Substring(0, i).Contains("e"))
                        {
                            int h = a.IndexOf("e");
                            if (h == 0)
                            {
                                k = Convert.ToString(n) + a.Substring(0, i);
                            }
                            else
                            {
                                double l = Convert.ToDouble(a.Substring(0, a.IndexOf("e")));
                                k = Convert.ToString(l * n) + "e";
                            }
                        }
                        else
                        {
                            k = Convert.ToString(Convert.ToDouble(a.Substring(0, i)) * n);
                        }
                    }

                    der = k + "x^" + Convert.ToString(n - 1);
                }
            }else if (a.Contains("x"))
            {
                if (a.IndexOf("x") > 0)
                {
                     der = a.Substring(0, a.IndexOf("x"));
                }
                else
                {
                    der = "1";
                }
            }

            return der;
        }

        public string TakeDerivative(string source)
        {
            string output = "";
            char[] arr = source.ToCharArray();
            int count = 0;
            int prevI = -1;
            int prevI2 = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                string sign = "+";
                if (arr[i] == '(')
                {
                    count++;
                }
                else if (arr[i] == ')')
                {
                    if (count == 1)
                    {
                        string sub = source;
                        string sub2 = source.Substring(i + 1);
                        if (prevI == -1)
                        {
                            sub = source.Substring(0, 2);
                        }
                        else
                        {
                            sub = source.Substring(prevI, 2);
                        }
                        string term = "";
                        string termD = "";
                        if (sub.Contains('-'))
                        {
                            sign = "-";
                        }

                        char o = ' ';
                        if (i + 1 < source.Length)
                        {
                            o = arr[i + 1];
                        }
                        if (o == '/')
                        {
                            sign = "";
                        }
                        else if (arr[prevI + 1] == '/')
                        {

                            if (prevI2 == -1)
                            {
                                sub = source.Substring(0, 2);
                                if (sub.Contains('-'))
                                {
                                    sign = "-";
                                }
                                term = source.Substring(0, i + 1);
                            }
                            else
                            {
                                sub = source.Substring(prevI2, 2);
                                if (sub.Contains('-'))
                                {
                                    sign = "-";
                                }
                                term = source.Substring(prevI2 + 2, i - prevI2 - 1);
                            }
                            termD = TakeDerFraction(term);
                        }
                        else if (o == '*')
                        {
                            sign = "";
                        }
                        else if (arr[prevI + 1] == '*')
                        {

                            if (prevI2 == -1)
                            {
                                sub = source.Substring(0, 2);
                                if (sub.Contains('-'))
                                {
                                    sign = "-";
                                }
                                term = source.Substring(0, i + 1);
                            }
                            else
                            {
                                sub = source.Substring(prevI2, 2);
                                if (sub.Contains('-'))
                                {
                                    sign = "-";
                                }
                                term = source.Substring(prevI2 + 2, i - prevI2 - 1);
                            }
                            termD = TakeDerProduct(term);
                        }
                        else if (prevI == -1)
                        {
                            int k = sub2.IndexOf('+');
                            int l = sub2.IndexOf('-');
                            if ((k == -1) && (l == -1))
                            {
                                i = i + sub2.Length;
                            }
                            else if (k == -1)
                            {
                                i = i + l;
                            }
                            else if (l == -1)
                            {
                                i = i + k;
                            }
                            else
                            {
                                i = i + Math.Min(k, l);
                            }

                            term = source.Substring(0, i + 1);
                            termD = TakeDerComposite(term);
                        }
                        else
                        {
                            int k = sub2.IndexOf('+');
                            int l = sub2.IndexOf('-');
                            if ((k == -1) && (l == -1))
                            {
                                i = i + sub2.Length;
                            }
                            else if (k == -1)
                            {
                                i = i + l;
                            }
                            else if (l == -1)
                            {
                                i = i + k;
                            }
                            else
                            {
                                i = i + Math.Min(k, l);
                            }
                            term = source.Substring(prevI + 2, i - prevI - 1);
                            termD = TakeDerComposite(term);
                        }
                        
                        output = output + sign + termD;
                        prevI2 = prevI;
                        prevI = i;
                        count = 0;
                    }
                    else if (count > 1)
                    {
                        count--;
                    }
                    
                }
                else if ((arr[i] == 'x') && (count == 0))
                {
                    int j = 0;
                    string term = "";
                    if (prevI == -1)
                    {
                        string sub = source;
                        if (source.Substring(0,1) == "-")
                        {
                            sign = "-";
                            sub = source.Substring(1);
                            j = 1;
                        }
                        
                        int k = sub.IndexOf('+');
                        int l = sub.IndexOf('-');
                        int m = sub.IndexOf('^');

                        if(l == m + 1)
                        {
                            string sub2 = sub.Substring(l + 1);
                            int p = sub2.IndexOf('+');
                            int q = sub2.IndexOf('-');
                            if (p > q)
                            {
                                if (q == -1)
                                {
                                    l = l + p + 1;
                                }
                                else
                                {
                                    l = l + q + 1;
                                }
                            }
                            else if (p == q)
                            {
                                l = k;
                            }
                            else
                            {
                                if (p == -1)
                                {
                                    l = l + q + 1;
                                }
                                else
                                {
                                    l = l + p + 1;
                                }
                            }
                        }

                        if(k > l)
                        {
                            if (l == -1)
                            {
                                term = sub.Substring(0, k);
                                j = j + k - 1;
                            }
                            else
                            {
                                term = sub.Substring(0, l);
                                j = j + l - 1;
                            }
                        }
                        else if(k == l)
                        {
                            term = sub;
                        }
                        else
                        {
                            if (k == -1)
                            {
                                term = sub.Substring(0, l);
                                j = j + l - 1;
                            }
                            else
                            {
                                term = sub.Substring(0, k);
                                j = j + k - 1;

                            }
                        }

                        output = output + sign + TakeDerTerm(term);
                        prevI2 = prevI;
                        prevI = j;
                    }
                    else
                    {
                        string sub1 = source.Substring(0, i);
                        string sub2 = source.Substring(i);
                        j = sub1.LastIndexOf('+');
                        int h = source.Length - (sub2.Length - sub2.IndexOf('+'));
                        int m = source.Length - (sub2.Length - sub2.IndexOf('^'));
                        if (sub1.LastIndexOf('-') > j)
                        {
                            sign = "-";
                            j = sub1.LastIndexOf('-');
                        }


                        if ((sub2.IndexOf('+') == -1) || (sub2.IndexOf('+') > sub2.IndexOf('-')))
                        {
                            if (sub2.IndexOf('-') == -1)
                            {
                                h = source.Length - (sub2.Length - sub2.IndexOf('+'));
                            }
                            else
                            {
                                h = source.Length - (sub2.Length - sub2.IndexOf('-'));
                            }
                        }

                        if (h == m + 1)
                        {
                            string s = sub2.Substring(sub2.IndexOf('-') + 1);
                            int p = s.IndexOf('+');
                            int q = s.IndexOf('-');
                            if (p > q)
                            {
                                if (q == -1)
                                {
                                    h = h + p + 1;
                                }
                                else
                                {
                                    h = h + q + 1;
                                }
                            }
                            else if (p == q)
                            {
                                term = source.Substring(j + 1);
                                h = source.Length;
                            }
                            else
                            {
                                if (p == -1)
                                {
                                    h = h + q + 1;
                                }
                                else
                                {
                                    h = h + p + 1;
                                }
                            }
                        }

                        if ((sub2.IndexOf('+') == -1) && (sub2.IndexOf('-') == -1))
                        {
                            term = source.Substring(j + 1);
                            h = source.Length - 1;
                        }
                        else
                        {
                            term = source.Substring(j + 1, h - j - 1);
                        }

                        output = output + sign + TakeDerTerm(term);
                        prevI2 = prevI;
                        prevI = h - 1;
                    }

                }
            }

            return output;
        }
        

        public string TakeDerComposite(string source)
        {
            string output = "";
            string f = source.Substring(0, source.IndexOf('('));
            char[] sub = source.Substring(source.IndexOf(f) + f.Length + 1).ToCharArray();
            int end = 0;
            for (int i = 0; i < sub.Length; i++)
            {
                int count = 0;
                if (sub[i] == '(')
                {
                    count++;
                }
                else if (sub[i] == ')')
                {
                    if (count == 0)
                    {
                        end = i;
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }

            string s = new string(sub);
            string inner = s.Substring(0, end);
            string outer = s.Substring(end + 1);

            if (outer.Contains("^"))
            {
                var j = source.IndexOf('(');
                string outerD = TakeDerTerm(source.Substring(0, j) + "x" + outer);
                output = Tidy(outerD.Replace("x", "(" + inner + ")")) + "(" + Tidy(TakeDerivative(inner)) + ")";
            }

            switch (f)
            {
                case string a when a.Contains("^"):
                    var j = source.IndexOf("^");
                    output = "(" + Tidy(TakeDerivative(inner)) + ")" + "*ln" + f.Substring(0, j) + "*" + source;
                    break;
                case string b when b.Contains("√"):
                    j = source.IndexOf("√");
                    string innerD = TakeDerivative(inner);
                    if (innerD == "+1")
                    {
                        output = "1/(2√(" + inner + "))";
                    }
                    else
                    {
                        output = source.Substring(0, j) + "(" + Tidy(innerD) + ")/(2√(" + inner + "))";
                    }
                    break;
                case string c when c.Contains("ln"):
                    j = source.IndexOf("ln");
                    output = source.Substring(0, j) + "(" + Tidy(TakeDerivative(inner)) + ")/(" + inner + ")";
                    break;
                case string d when d.Contains("log"):
                    j = source.IndexOf("log");
                    int k = source.IndexOf('g');
                    output = source.Substring(0, j) + "(" + Tidy(TakeDerivative(inner)) + ")/(" + inner + ")ln" + source.Substring(k + 1, source.IndexOf('(') - k - 1);
                    break;
                case string e when e.Contains("sin"):
                    j = source.IndexOf("sin");
                    output = source.Substring(0, j) + "(" + Tidy(TakeDerivative(inner)) + ")cos(" + inner + ")";
                    break;
                case string g when g.Contains("cos"):
                    j = source.IndexOf("cos");
                    output = "-" + source.Substring(0, j) + "(" + Tidy(TakeDerivative(inner)) + ")sin(" + inner + ")";
                    break;
                case string h when h.Contains("tg"):
                    j = source.IndexOf("tg");
                    output = source.Substring(0, j) + "(" + Tidy(TakeDerivative(inner)) + ")/cos\u00B2(" + inner + ")";
                    break;
            }
            
            return output;
        }

        public string TakeDerFraction(string source)
        {
            if (source.Substring(0,1) != "(")
            {
                source = source.Substring(1);
            }
            int i = source.IndexOf('/');
            string f = source.Substring(1, i - 2);
            string g = source.Substring(i + 2, source.Length - i - 3);
            string output = "((" + Tidy(TakeDerivative(f)) + ")*(" + g + ")-(" + f + ")*(" + Tidy(TakeDerivative(g)) + "))/(" + g + ")\u00B2";
            return output;
        }

        public string TakeDerProduct(string source)
        {
            int i = source.IndexOf('*');
            string f = source.Substring(1, i - 2);
            string g = source.Substring(i + 2, source.Length - i - 3);
            string output = "(" + Tidy(TakeDerivative(f)) + ")*(" + g + ")+(" + Tidy(TakeDerivative(g)) + ")*(" + f + ")" ;
            return output;
        }

        public string Tidy(string s)
        {
            s = s.Replace(" ", "");
            s = s.Replace("+-", "-");
            s = s.Replace("--", "+");
            s = s.Replace("(+1)", "1");
            s = s.Replace("(+", "(");
            s = s.Replace("+0", "");
            s = s.Replace("-1x", "-x");
            s = s.Replace("+1x", "+x");
            s = s.Replace("(1x", "(x");
            if (s.IndexOf("^1") == s.Length - 2)
            {
                s = s.Replace("^1", "");
            }
            s = s.Replace("^1+", "+");
            s = s.Replace("^1-", "-");
            if (s.Contains("^"))
            {
                string[] subs = s.Split('^');
                for(int j = 1; j < subs.Length; j++)
                {
                    char[] substr = subs[j].ToCharArray();
                    for (int i = 0; i < substr.Length; i++)
                    { 
                        switch (substr[i])
                        {
                            case '1':
                                substr[i] = '\u00B9';
                                break;
                            case '2':
                                substr[i] = '\u00B2';
                                break;
                            case '3':
                                substr[i] = '\u00B3';
                                break;
                            case '4':
                                substr[i] = '\u2074';
                                break;
                            case '5':
                                substr[i] = '\u2075';
                                break;
                            case '6':
                                substr[i] = '\u2076';
                                break;
                            case '7':
                                substr[i] = '\u2077';
                                break;
                            case '8':
                                substr[i] = '\u2078';
                                break;
                            case '9':
                                substr[i] = '\u2079';
                                break;
                            case ',':
                                substr[i] = '\u22C5';
                                break;
                            case '/':
                                substr[i] = '\u141F';
                                break;
                            case 'x':
                                substr[i] = '\u02E3';
                                break;
                            case char a when ((a == '-') && (i == 0)):
                                substr[i] = '\u207B';
                                break;
                            default:
                                break;

                        }
                       
                        subs[j] = new string(substr);
                    }

                    s = "";
                    foreach(var sub in subs)
                    {
                        s = s + sub;
                    }
                }

            }
            s = s.Replace("1l", "l");
            s = s.Replace("1s", "s");
            s = s.Replace("1c", "c");
            s = s.Replace("1t", "t");
            if (s.Contains("+(1)") || s.Contains("-(1)") || s.Contains("(1)/"))
            {
                s = s.Replace("(1)", "1");
            }
            else
            {
                s = s.Replace("(1)", "");
            }

            if (s.Substring(0, 1) == "+")
            {
                s = s.Substring(1);
            }

            s = s.Replace("*1", "");
            s = s.Replace("1*", "");
            s = s.Replace("(*(", "((");
            s = s.Replace("lne*", "");
            s = s.Replace("lne", "");
            s = s.Replace("(x)", "x");
            s = s.Replace("+", " + ");
            s = s.Replace("-", " - ");

            return s;
        }

    }



 }

