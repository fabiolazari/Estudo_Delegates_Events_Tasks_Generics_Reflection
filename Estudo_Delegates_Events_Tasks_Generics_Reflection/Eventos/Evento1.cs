using System;
using System.Collections.Generic;


namespace Estudo_Delegates_events.Eventos
{
	public class Evento1
	{
		public event EventHandler<Evento1Args> LimiteIdade;

		public void OnLimite<T>(List<T> objeto)
		{
			foreach (var c in objeto)
			{
				string NomeObjeto = (c.GetType().GetProperty("Id").GetValue(c) +
								  " - " +
								  c.GetType().GetProperty("Nome").GetValue(c));

				LimiteIdade?.Invoke(NomeObjeto, new Evento1Args(DateTime.Now));
				//Thread.Sleep(1000);
			}
		}
	}
}
