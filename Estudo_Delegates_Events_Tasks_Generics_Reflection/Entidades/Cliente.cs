
namespace Estudo_Delegates_events.Entidades
{
	public class Cliente : ClassePai
	{
		#region variaveis

		private string _nome;
		private string _email;
		private int _idade;

		#endregion

		#region propriedades

		public string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

		public int Idade
		{
			get { return _idade; }
			set { _idade = value; }
		}

		#endregion

		#region Construtores

		public Cliente() : base()
		{
		}

		public Cliente(int id, string nome, string email, int idade) : base ()
		{
			Id = id;
			Nome = nome;
			Email = email;
			Idade = idade;
		}

		#endregion

		#region métodos

		public override string ToString()
		{
			return "Cliente Id: " +
				   Id +
				   " Nome: " +
				   Nome +
				   " Email: " +
				   Email +
				   " Idade: " +
				   Idade;
		}

		#endregion
	}
}
