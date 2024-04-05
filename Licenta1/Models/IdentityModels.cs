using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace Licenta1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Nume { get; set; }
        public string Prenume { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
         
        //public DbSet<Procedura> Proceduras { get; set; }
        //public DbSet<Serviciu> Servicius { get; set; }
        //public DbSet<ServiciuProcedura> ServiciuProceduras { get; set; }
        //public DbSet<Testimonial> Testimonials { get; set; }
        //public DbSet<Programare> Programares { get; set; }

        //public System.Data.Entity.DbSet<Licenta1.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
    //public class Procedura
    //{
    //    public int Id { get; set; }
    //    public string Denumire { get; set; }
    //    public int Pret { get; set; }
    //    public string Descriere { get; set; }
    //    public int Durata { get; set; }


    //    public virtual ICollection<ServiciuProcedura> Serviciu_Procedura { get; set; }
    //}
    //public class Serviciu
    //{

    //    public int ServiciuID { get; set; }
    //    public string Denumire { get; set; }
    //    public string Descriere { get; set; }
    //    //public List<Procedura> Proceduras { get; set; }

    //    public virtual ICollection<ServiciuProcedura> Serviciu_Procedura { get; set; }
    //}
    //public class ServiciuProcedura
    //{
    //    public int ServiciuProceduraID { get; set; }
    //    //public int Serviciu_Id { get; set; }
    //    //public int Procedura_Id { get; set; }

    //    public virtual Procedura Procedura { get; set; }
    //    public virtual ICollection<Programare> Programares { get; set; }
    //    public virtual Serviciu Serviciu { get; set; }
    //}
    //public partial class Testimonial
    //{
    //    //public int TestID { get; set; }
    //    public int Id { get; set; }
    //    public Nullable<System.DateTime> Data { get; set; }

    //    public string Descriere { get; set; }
    //    public bool Validat { get; set; }

    //    public virtual ApplicationUser ApplicationUser { get; set; }
    //}
    //public class Programare
    //{
    //    public int ProgramareID { get; set; }
    //    public System.DateTime Data { get; set; }
    //    public string PacientAnonim { get; set; }

    //    public virtual ServiciuProcedura ServiciuProcedura { get; set; }
    //    public virtual ApplicationUser ApplicationUser { get; set; }
    //    public virtual ApplicationUser ApplicationUser1 { get; set; }

    //}
}