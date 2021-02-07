using System;
using System.Collections.Generic;
using System.Linq;
using Estudo_Delegates_events.Entidades;

namespace Estudo_Delegates_events
{
	public class Exemplos
	{
		public delegate void Imprime(string message);
		public delegate void ImprimeClientes(List<Cliente> clientes);

		public static void Executa()
		{
			Imprime imp = ImprimeMessage;
			//imp += ImprimeMessage;

			List<Cliente> clientes = new List<Cliente>();

			clientes.Add(new Cliente() { Id = 1, Nome = "Fulano da Silva", Email = "fulano@gmail.com", Idade = 25 });
			clientes.Add(new Cliente() { Id = 2, Nome = "Ciclano de Oliveira", Email = "ciclano@hotmail.com", Idade = 15 });
			clientes.Add(new Cliente() { Id = 3, Nome = "Locha de Souza", Email = "locha@gmail.com", Idade = 18 });
			clientes.Add(new Cliente() { Id = 4, Nome = "Dunha Costa", Email = "dunha@gmail.com", Idade = 12 });
			clientes.Add(new Cliente() { Id = 5, Nome = "Nhola Bueno", Email = "nhola@hotmail.com", Idade = 31 });
			clientes.Add(new Cliente() { Id = 6, Nome = "Beltrano Dias", Email = "beltrano@gmail.com", Idade = 40 });

			imp("Lista de Clientes Total:");

			/*	foreach (var c in clientes)
				{
					Console.WriteLine(c);
				}*/

			//clientes.ForEach(c => Console.WriteLine(c));

			ImprimeClientes impClientes = ImprimeCliente;
			impClientes(clientes);

			Console.WriteLine();
			Console.WriteLine(new string('-', 40));
			Console.WriteLine();

			// Invocando metodos da instancia do delegate impClientes


			foreach (var del in impClientes.GetInvocationList())
			{
				Console.WriteLine(del.Method);
			}

			Console.WriteLine();
			Console.WriteLine(new string('-', 40));
			Console.WriteLine();

			// Delegade Func para filtrar os maiores de 18 anos
			Func<int, bool> maiorIdade = f => f >= 18;

			// Um novo delegate Func usando como entrada a lista de clientes e o delegate func criado acima como filtro
			Func<List<Cliente>, Func<int, bool>, List<Cliente>> ListaClientes = (list, filter) =>
			{
				//List<Cliente> result = new List<Cliente>();

				/*
				foreach (Cliente a in list)
				{
					if (filter(a.Idade))
					{
						result.Add(new Cliente()
						{
							Id = a.Id,
							Nome = a.Nome,
							Email = a.Email,
							Idade = a.Idade
						});
					}
				}
				*/
				/*
				list.ForEach(a => 
				{
					if (filter(a.Idade))
					{
						result.Add(new Cliente()
						{
							Id = a.Id,
							Nome = a.Nome,
							Email = a.Email,
							Idade = a.Idade
						});
					}
				});
				*/
				//	return result;
				/*
				return list.Where(l => filter(l.Idade))
								 .Select(c => new Cliente()
								 {
									 Id = c.Id,
									 Nome = c.Nome,
									 Email = c.Email,
									 Idade = c.Idade
								 }).ToList();
				*/

				return list.Where(l => filter(l.Idade))
						 .Select(c => c).ToList();

			};

			// Executando a chamado do delegate passando a lista de clientes e o filtro(outro func)
			List<Cliente> minhaLista = ListaClientes(clientes, maiorIdade);

			// Imprimindo na tela a lista de clientes filtrada usando o metodo ImprimeCliente que usa o delegate action
			impClientes(minhaLista);

			Console.ReadLine();
		}

		public static void ImprimeMessage(string message)
		{
			Console.WriteLine(message);
		}

		public static void ImprimeCliente(List<Cliente> clientes)
		{
			//Delegate Action para imprimir o resultado
			Action<List<Cliente>> impClientes = lc => lc.ForEach(c => Console.WriteLine(c));

			impClientes(clientes);
		}
	}
}
