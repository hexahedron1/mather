using System.Diagnostics.Tracing;
using System.Text;
using MathNet.Numerics;
using static System.Console;
// не ржать над кодом
// не ругаться матом на код
// наказание: смертная казнь
InputEncoding = OutputEncoding = Encoding.Unicode;
WriteLine(); { { { { { { { { { { { WriteLine("hello wordle"); } } } } } } } } } } }
while (true) {
	Write("> ");
	string input = ReadLine() ?? string.Empty;
	string[] arguments = input.Split(' ');
	if (input == "info") {
		NumberPrompt().ToString();
	} else if (arguments.Length > 1 && arguments[0] == "sqrt") {
		if (uint.TryParse(arguments[1], out uint digits)) {
			//MathNet.Numerics.BigRa
			SquareRoot(NumberPrompt().Number, digits, arguments.Contains("-print"));
		}
	}
}
void SquareRoot(decimal x, uint digits, bool print = false) {
	if (x < 0) {
		WriteLine("ВНИМАНИЕ ⚠️⚠️🍄🍄🍄🍄🍄⚠️⚠️⚠️ ВОСCТАНИЕ ГРИБОВ 🍄🍄⚠️⚠️⚠️⚠️⚠️⚠️🍄🍄 ⚠️⚠️⚠️⚠️ ВСЕМ ПРИГОТОВИТЬСЯ 🏃‍🏃‍⚠️⚠️⚠️⚠️🍄🍄⚠️⚠️⚠️⚠️⚠️🍄🍄🍄 К ОТПЛЫТИЮ 🚣‍🚣‍⚠️⚠️⚠️⚠️🚣‍🚣‍🍄🍄🍄🍄🍄🚣‍🚣‍");
		return;
	}
	// TODO: Найти библу с BigFloat у которой есть рабочий метод ToString
	/*BigRational lower = x > 0 ? 0 : x;
	WriteLine($"Lower: {lower}");
	BigRational upper = x > 0 ? x : 0;
	WriteLine($"Upper: {upper}");
	BigRational mult = 1;
	BigRational prev = lower;
	for (int i = 0; i < digits; i++) {
		if (print) {
			WriteLine($"Digit {i}");
			WriteLine($"{lower}<========>{upper}");
		}
		for (BigRational j = lower; j <= upper; j += mult) {
			bool bigger = BigRational.Pow(j, 2) > x;
			if (print)
				WriteLine($"> {j}: {bigger}");
			if (bigger) {
				lower = prev;
				upper = j;
				char digit = BigRational.;
				if (print)
					WriteLine($"Digit output: {digit}");
				else
					Write(digit);
				if (mult == 1) {
					if (print)
						WriteLine("DOT.");
					else
						Write('.');
				}
				mult /= 10;
				break;
			}
			prev = j;
		}
	}
	WriteLine(print ? $"Final number: {lower}" : ""); 
	*/
}
// ввод числа (ПОДДЕРЖИВАЕТ ПЕРИОДИЧЕСКИЕ ДРОБИ!!!)
NumberInfo NumberPrompt() {
	Write("<∙ ");
	int x = CursorLeft;
	int y = CursorTop;
	ConsoleKeyInfo key;
	bool dec = false;
	bool period = false;
	bool periodClosed = false;
	string output = "";
	do {
		SetCursorPosition(x, y);
		Write("".PadRight(WindowWidth));
		SetCursorPosition(x, y);
		Write(output);
		key = ReadKey(true);
		if (key.Key == ConsoleKey.Backspace && output.Length > 0)
			output = output[..(output.Length - 1)];
		else if (!periodClosed) {
			if (key.KeyChar == '-' && output.Length == 0)
				output += '-';
			else if (char.IsDigit(key.KeyChar))
				output += key.KeyChar;
			else if (key.KeyChar == '.' && !dec)
				output += '.';
			else if (key.KeyChar == '(' && !period && dec)
				output += '(';
			else if (key.KeyChar == ')' && period)
				output += ')';

		}
		dec = output.Contains('.');
		period = output.Contains('(');
		periodClosed = output.Contains(')');
	} while (key.Key != ConsoleKey.Enter);
	WriteLine();
	return new NumberInfo(output);
}
// рисует десятичную дробь в консоли
void Frac(string num, string den) {
	int x = CursorLeft;
	int y = CursorTop;
	int width = Math.Max(num.Length, den.Length);
	Write(new string('-', width));
	SetCursorPosition(x + ((width - num.Length) / 2), y - 1);
	Write(num);
	SetCursorPosition(x + ((width - den.Length) / 2), y + 1);
	Write(den);
}
// срака
class NumberInfo {
	public NumberInfo(string str) {
		decimal output = 0;
		int sign = str.StartsWith('-') ? -1 : 1;
		if (sign == -1)
			str = str[1..];
		string whole = "";
		string frac = "";
		string period = "";
		string[] split = str.Split('.');
		whole = split[0];
		if (split.Length > 1) {
			frac = split[1];
			split = frac.Split('(');
			frac = split[0];
			if (split.Length > 1) {
				period = split[1];
				period = period[..(period.Length - 1)];
			}
		}
		output += int.Parse(whole);
		if (period.Length > 0) {
			for (int i = 0; i < 28 / period.Length; i++) {
				frac += period;
			}
		}
		for (int i = 0; i < frac.Length; i++) {
			if (!char.IsDigit(frac[i]))
				continue;
			output += (decimal)(int.Parse(frac[i].ToString()) / Math.Pow(10, i + 1));
		}
		Number = output * sign;
	}
	public decimal Number { get; }
	public int Sign { get; }
	public ulong Whole { get; }
	public ulong Fraction { get; }
	public ulong Period { get; }
	public override string ToString() {
		return Number.ToString();
	}
	public void Print() {
		WriteLine(this);
		WriteLine($"Sign: {(Sign > 0 ? "+" : "-")}");
		WriteLine($"Whole: {Whole}");
		WriteLine($"Fractional: {Fraction}");
		WriteLine($"Period: {Period}");
	}
}