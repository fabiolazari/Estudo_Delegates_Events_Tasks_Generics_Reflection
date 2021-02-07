using System;
using System.Collections.Generic;

namespace Estudo_Delegates_events.Metodos
{
	public delegate void ImprimeObjeto<T>(List<T> objetoT);

	public class Imprime
	{
		public static void Mensagem(string mensagem)
		{
			Console.WriteLine(mensagem);
		}

		public static void Separador()
		{
			Console.WriteLine(new string('-', 80));
		}

		public static void Espaco()
		{
			Console.WriteLine();
		}

		public static void Entidade<T>(List<T> entidade)
		{
			Action<List<T>> impEntidade = lc => lc.ForEach(c => Console.WriteLine(c));

			impEntidade(entidade);
		}
	}
}
