using System;
using System.Data.Entity;

namespace CRUD_Pessoa.Models
{
    public class PessoaFisicaModel
    {
        public int ID { get; set; }
        public string NomeCompleto { get; set; }

        public DateTime DataNascimento { get; set; }
        public string ValorRenda { get; set; }
        public string CPF { get; set; }
    }

    public class PessoaDBContext : DbContext
    {
        public DbSet<PessoaFisicaModel> Pessoas { get; set; }
    }
}