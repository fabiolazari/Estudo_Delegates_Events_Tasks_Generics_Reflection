using System;
using System.Linq;
using System.Collections.Generic;
using Estudo_Delegates_events.Eventos;
using Estudo_Delegates_events.Entidades;
using Estudo_Delegates_events.Metodos;
using Estudo_Delegates_events.Servicos;
using System.Resources;

namespace Estudo_Delegates_Events_Tasks_Generics_Reflection
{
	class Program
	{
		static void Main(string[] args)
		{
			//1ª Parte
			//Exemplos.Executa();


			//2ª Parte
			//Entrando com dados iniciais
			Console.Write("Entre com a idade mínima: ");
			int IdadeMinima = Convert.ToInt32(Console.ReadLine());

			Console.Write("Entre com a idade máximo: ");
			int IdadeMaxima = Convert.ToInt32(Console.ReadLine());

			Func<int, bool> Idade = f => f >= IdadeMaxima;

			//Instanciando e criado evento
			Evento1 evento1 = new Evento1();
			evento1.LimiteIdade += ExecutaEvento;

			//----------------------------------------------------------------------------------------------------------------------------------
			// Instanciando Objetos
			Cliente[] arrayClientes = new Cliente[]
			{
				new Cliente() { Id = 1, Nome = "Fulano da Silva", Email = "fulano@gmail.com", Idade = 25 },
				new Cliente() { Id = 2, Nome = "Ciclano de Oliveira", Email = "ciclano@hotmail.com", Idade = 15 },
				new Cliente() { Id = 3, Nome = "Locha de Souza", Email = "locha@gmail.com", Idade = 13 },
				new Cliente() { Id = 4, Nome = "Dunha Costa", Email = "dunha@gmail.com", Idade = 12 },
				new Cliente() { Id = 5, Nome = "Nhola Bueno", Email = "nhola@hotmail.com", Idade = 31 },
				new Cliente() { Id = 6, Nome = "Beltrano Dias", Email = "beltrano@gmail.com", Idade = 44 }
			};

			Fornecedor[] arrayFornecedores = new Fornecedor[]
			{
				new Fornecedor() { Id = 1, Nome = "Genésio Galvão", Email = "genesio@hotmail.com", Idade = 55 },
				new Fornecedor() { Id = 2, Nome = "Joaquim dos Santos", Email = "jsantos@gmail.com", Idade = 17 },
				new Fornecedor() { Id = 3, Nome = "José de Oliveira Junior", Email = "josejunior@hotmail.com", Idade = 16 },
				new Fornecedor() { Id = 4, Nome = "Maria Esteves Costa", Email = "mesteves@gmail.com", Idade = 19 },
				new Fornecedor() { Id = 5, Nome = "Manuel Bento Silva", Email = "manebento@hotmail.com", Idade = 25 },
				new Fornecedor() { Id = 6, Nome = "Francisco Cesar Beltrão", Email = "chicocesar@gmail.com", Idade = 22 }
			};

			//-------------------------------------------------------------------------------------
			List<Cliente> listaClientes = InstanciarObjeto<Cliente>.Executa(arrayClientes).Result;
			List<Fornecedor> listaFornecedores = InstanciarObjeto<Fornecedor>.Executa(arrayFornecedores).Result;
			Imprime.Mensagem("Em progresso...");

			//----------------------------------------------------------------------------------------------------------------------------------

			Imprime.Separador();
			Imprime.Mensagem("Listando Clientes");
			Imprime.Separador();
			Imprime.Espaco();

			ImprimeObjeto<Cliente> impClientes = Imprime.Entidade<Cliente>;
			impClientes(listaClientes);
			Imprime.Espaco();

			var listaClienteIdadeMaiores = listaClientes.Where(c => Idade(c.Idade)).Select(c => c).ToList();
			evento1.OnLimite<Cliente>(listaClienteIdadeMaiores);

			//----------------------------------------------------------------------------------------------------------------------------------
			Imprime.Separador();
			Imprime.Mensagem("Listando Clientes Filtrados!");
			Imprime.Separador();
			Imprime.Espaco();

			List<Cliente> listaFitradaClientes = FiltrarObjeto<Cliente>.Executa(listaClientes, IdadeMinima);
			impClientes(listaFitradaClientes);

			//----------------------------------------------------------------------------------------------------------------------------------
			Imprime.Espaco();
			Imprime.Separador();
			Imprime.Mensagem("Listando Fornecedores");
			Imprime.Separador();
			Imprime.Espaco();

			ImprimeObjeto<Fornecedor> impFornecedores = Imprime.Entidade<Fornecedor>;
			impFornecedores(listaFornecedores);
			Imprime.Espaco();

			var listaFornecedoresIdadeMaiores = listaFornecedores.Where(f => Idade(f.Idade)).Select(f => f).ToList();
			evento1.OnLimite<Fornecedor>(listaFornecedoresIdadeMaiores);

			//----------------------------------------------------------------------------------------------------------------------------------
			Imprime.Separador();
			Imprime.Mensagem("Listando Fornecedores Filtrados!");
			Imprime.Separador();
			Imprime.Espaco();

			List<Fornecedor> listaFitradaFornecedores = FiltrarObjeto<Fornecedor>.Executa(listaFornecedores, IdadeMinima);
			impFornecedores(listaFitradaFornecedores);
			
			/*
			//3ª Parte
			// Define a estrutura da classe generica
			Dictionary<string, Type> dicionario = new Dictionary<string, Type>();
			dicionario.Add("Id", typeof(int));
			dicionario.Add("Nome", typeof(string));
			dicionario.Add("Email", typeof(string));
			dicionario.Add("Idade", typeof(int));

			// Instancia uma classe dinamicamente
			GenericaService genericaservice = new GenericaService();
			genericaservice.Fields = dicionario;
			genericaservice.CreateNewObject();
			Generica generica = genericaservice.Generica;

			List<Generica> lGenerica = new List<Generica>();
			//lGenerica.Add(new Generica() { generica.GetType().GetProperty("Id").SetValue(generica, 1),
			//								});

			//Adiciona Itens a classe e na lista
			generica.GetType().GetProperty("Id").SetValue(generica, 1);
			generica.GetType().GetProperty("Nome").SetValue(generica, "Fabio Lazari");
			generica.GetType().GetProperty("Email").SetValue(generica, "fabio@gmail.com");
			generica.GetType().GetProperty("Idade").SetValue(generica, 46);
			lGenerica.Add(generica);

			//Adiciona o segundo item
			generica.GetType().GetProperty("Id").SetValue(generica, 2);
			generica.GetType().GetProperty("Nome").SetValue(generica, "Ciclano de Oliveira");
			generica.GetType().GetProperty("Email").SetValue(generica, "ciclano@hotmail.com");
			generica.GetType().GetProperty("Idade").SetValue(generica, 15);
			lGenerica.Add(generica);
			
			//Imprimindo a lista
			lGenerica.ForEach(l => Console.WriteLine(l.GetType().GetProperty("Id").GetValue(l).ToString() + " - " +
								   l.GetType().GetProperty("Nome").GetValue(l).ToString() + " - " +
								   l.GetType().GetProperty("Email").GetValue(l).ToString() + " - " +
								   l.GetType().GetProperty("Idade").GetValue(l).ToString()
							));

				//new Generica() { Id = 3, Nome = "Locha de Souza", Email = "locha@gmail.com", Idade = 13 },
				//new Generica() { Id = 4, Nome = "Dunha Costa", Email = "dunha@gmail.com", Idade = 12 },
				//new Generica() { Id = 5, Nome = "Nhola Bueno", Email = "nhola@hotmail.com", Idade = 31 },
				//new Generica() { Id = 6, Nome = "Beltrano Dias", Email = "beltrano@gmail.com", Idade = 44 }
			*/

			Console.ReadLine();
		}

		static void ExecutaEvento(object source, Evento1Args e)
		{
			Console.WriteLine($"{source} tem mais de 40 anos, executado evento as {e.HoraLimiteIdade.TimeOfDay}!");
		}
	}
}
