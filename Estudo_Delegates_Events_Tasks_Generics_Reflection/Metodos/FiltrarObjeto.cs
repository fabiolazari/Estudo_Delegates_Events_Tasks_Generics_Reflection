using System;
using System.Collections.Generic;
using System.Linq;

namespace Estudo_Delegates_events.Metodos
{
	public class FiltrarObjeto<T>
	{
		public static List<T> Executa(List<T> lObjeto, int IdadeMinima)
		{
			Func<int, bool> Idade = f => f >= IdadeMinima;

			Func<List<T>, Func<int, bool>, List<T>> ListaClientes = (lista, filter) =>
			{
				return lista.Where(l => filter(Convert.ToInt32(l.GetType()
																.GetProperty("Idade")
																.GetValue(l))))
							.Select(c => c).ToList();

			};

			List<T> minhaLista = ListaClientes(lObjeto, Idade);

			return minhaLista;
		}
	}
}
