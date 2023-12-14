using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.ViewModels;

namespace WebApplication1.Data
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
        public string MyRegisteredUserEmail { get; set; }
        public virtual MyRegisteredUser MyRegisteredUser { get; set; }
        public Client()
        {
            ClientAccounts = new HashSet<ClientAccount>();
        }

    }
    public class BankAccount
    {
        [Key]
        public int AccountNum { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }

        public string MyRegisteredUserEmail { get; set; }
        public virtual MyRegisteredUser MyRegisteredUser { get; set; }
        public BankAccount()
        {
        ClientAccounts = new HashSet<ClientAccount>();
        }
    }
    public class ClientAccount
    {
        [Key]
        public int ClientId { get; set; }
        public int AccountNum { get; set; }
        public virtual Client Client { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
    public class MyRegisteredUser
    {
        [Key]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Account Type")]
        [Required]
        public string AccountType { get; set; }

        [Display(Name = "Balance")]
        [Required]
        public decimal Balance { get; set; }
        public virtual Client Client { get; set; }

        public virtual BankAccount BankAccount { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<ClientAccount> ClientAccounts { get; set; }
        public DbSet<MyRegisteredUser> MyRegisteredUsers { get; set; }
        public DbSet<WebApplication1.ViewModels.RoleVM> RoleVM { get; set; } = default!;
        public DbSet<WebApplication1.ViewModels.UserVM> UserVM { get; set; } = default!;
        public DbSet<WebApplication1.ViewModels.UserRoleVM> UserRoleVM { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<ClientAccount>()
                    .HasKey(ca => new
                    {
                        ca.ClientId,
                        ca.AccountNum 
                    });

          
            modelBuilder.Entity<ClientAccount>()
                .HasOne(c => c.Client)
                .WithMany(c => c.ClientAccounts)
                .HasForeignKey(fk => fk.ClientId) 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClientAccount>()
            .HasOne(c => c.BankAccount)
            .WithMany(c => c.ClientAccounts)
            .HasForeignKey(fk => fk.AccountNum)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.MyRegisteredUser)
                .WithOne(mru => mru.Client)
                .HasForeignKey<Client>(c => c.MyRegisteredUserEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccount>()
                .HasOne(ba => ba.MyRegisteredUser)
                .WithOne(mru => mru.BankAccount)
                .HasForeignKey<BankAccount>(ba => ba.MyRegisteredUserEmail)
                .OnDelete(DeleteBehavior.Cascade);

      
        }
    }
}

