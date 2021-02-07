using System;

namespace Estudo_Delegates_events.Eventos
{
	public class Evento1Args : EventArgs
	{
		public DateTime HoraLimiteIdade { get; set; }

		public Evento1Args(DateTime hora)
		{
			this.HoraLimiteIdade = hora;
		}
	}
}
