using System;
namespace Mathematics
{

	//Исключение при вычислении логаримфма неправильного числа
	class LogException:Exception
	{
		public LogException(double val):base("Невозможно вычислить логарифм от "+val)
		{
		}
    }

	//Исключение при неверном числе переменных
	class WrongNumberOfVariablesException:Exception
	{
		public WrongNumberOfVariablesException():base("Неверное число переменных")
		{
		}
	}
}
