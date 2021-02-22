using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public class BibliotecaDbContext : IdentityDbContext<Leitor>
    {
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Classificacao> Classificacoes { get; set; }
        public DbSet<Nucleo> Nucleos { get; set; }
        public DbSet<Requisicao> Requisicoes { get; set; }

        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Nucleo>()
                .HasMany(nucleo => nucleo.Obras)
                .WithMany(obra => obra.Nucleos)
                .UsingEntity<ObrasNucleo>(
                    on => on.HasOne<Obra>().WithMany(),
                    on => on.HasOne<Nucleo>().WithMany());
        }
    }    
}
