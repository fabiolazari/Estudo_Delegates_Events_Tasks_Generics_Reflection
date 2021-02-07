using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estudo_Delegates_events.Metodos
{
	public class InstanciarObjeto<T>
	{
		public async static Task<List<T>> Executa(T[] arrayGenerico)
		{
			return await Task<List<T>>.Factory.StartNew(() =>
			{
				return arrayGenerico.Select(c => c).ToList();
			});
		}
	}
}
