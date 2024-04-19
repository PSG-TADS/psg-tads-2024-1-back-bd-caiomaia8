using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Trabalho_01
{
    internal class Program
    {
        // Classe Veiculo
        public class Veiculo
        {
            [Key]
            public int VeiculoID { get; set; }
            // Primária

            public string? Modelo { get; set; }
            public string? Marca { get; set; }
            public int Ano { get; set; }
            public string? Placa { get; set; }
            public string? Status { get; set; }

            // Relação com a classe reserva
            public ICollection<Reserva>? Reservas { get; set; }
        }

        // Classe Cliente
        public class Cliente
        {
            [Key]
            public int ClienteID { get; set; }
            // Primária

            public string? Nome { get; set; }
            public string? Telefone { get; set; }
            public string? Endereco { get; set; }

            public string? Email { get; set; }

            // Relação com a classe reserva
            public ICollection<Reserva>? Reservas { get; set; }
        }

        // Classe Reserva
        public class Reserva
        {
            [Key]
            public int ReservaID { get; set; }
            //Primária

            public int VeiculoID { get; set; } // Estrangeira veiculo
            public int ClienteID { get; set; } // Estrangeira cliente
            public DateTime Inicio { get; set; }
            public DateTime Fim { get; set; }

            // Definida a chave estrangeira para veículo
            [ForeignKey("VeiculoID")]
            public Veiculo? Veiculo { get; set; }

            // Definida a chave estrangeira para cliente
            [ForeignKey("ClienteID")]
            public Cliente? Cliente { get; set; }
        }

        public class ApplicationDbContext : DbContext //classe responsável pelo contexto de conexão

        {
            public DbSet<Veiculo> Veiculos { get; set; }
            public DbSet<Cliente> Clientes { get; set; }

            public DbSet<Reserva> Reservas { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // string de conexão
            {
                _ = optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TRABALHO01;Trusted_Connection=True;TrustServerCertificate=true");
            }

        }

        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext())
            {
                var cliente = new Cliente()
                {
                    Nome = "Teste 1",
                    Endereco = "Rua 0",
                    Telefone = "3799999-9999",
                    Email = "ch.maia230397@gmail.com"
                    
                };
                context.Clientes.Add(cliente);
                context.SaveChanges();
                var veiculo = new Veiculo()
                {
                    Marca = "Fiat",
                    Modelo = "Palio",
                    Ano = 1997,
                    Placa = "CHM2303",
                    Status = "Muito conservado"
                };
                context.Veiculos.Add(veiculo);
                context.SaveChanges();
                var reserva = new Reserva()
                {
                    VeiculoID = veiculo.VeiculoID,
                    ClienteID = cliente.ClienteID,
                    Inicio = DateTime.Now,
                    Fim = DateTime.Now.AddDays(7)
                };
                context.Reservas.Add(reserva);
                context.SaveChanges();
            }
        }
    }
}