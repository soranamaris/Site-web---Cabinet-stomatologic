namespace Licenta1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proceduras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Denumire = c.String(),
                        Pret = c.Int(nullable: false),
                        Descriere = c.String(),
                        Durata = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiciuProceduras",
                c => new
                    {
                        ServiciuProceduraID = c.Int(nullable: false, identity: true),
                        Procedura_Id = c.Int(),
                        Serviciu_ServiciuID = c.Int(),
                    })
                .PrimaryKey(t => t.ServiciuProceduraID)
                .ForeignKey("dbo.Proceduras", t => t.Procedura_Id)
                .ForeignKey("dbo.Servicius", t => t.Serviciu_ServiciuID)
                .Index(t => t.Procedura_Id)
                .Index(t => t.Serviciu_ServiciuID);
            
            CreateTable(
                "dbo.Programares",
                c => new
                    {
                        ProgramareID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        PacientAnonim = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser1_Id = c.String(maxLength: 128),
                        ServiciuProcedura_ServiciuProceduraID = c.Int(),
                    })
                .PrimaryKey(t => t.ProgramareID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser1_Id)
                .ForeignKey("dbo.ServiciuProceduras", t => t.ServiciuProcedura_ServiciuProceduraID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser1_Id)
                .Index(t => t.ServiciuProcedura_ServiciuProceduraID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nume = c.String(),
                        Prenume = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Servicius",
                c => new
                    {
                        ServiciuID = c.Int(nullable: false, identity: true),
                        Denumire = c.String(),
                        Descriere = c.String(),
                    })
                .PrimaryKey(t => t.ServiciuID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(),
                        Descriere = c.String(),
                        Validat = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Testimonials", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ServiciuProceduras", "Serviciu_ServiciuID", "dbo.Servicius");
            DropForeignKey("dbo.Programares", "ServiciuProcedura_ServiciuProceduraID", "dbo.ServiciuProceduras");
            DropForeignKey("dbo.Programares", "ApplicationUser1_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Programares", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiciuProceduras", "Procedura_Id", "dbo.Proceduras");
            DropIndex("dbo.Testimonials", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Programares", new[] { "ServiciuProcedura_ServiciuProceduraID" });
            DropIndex("dbo.Programares", new[] { "ApplicationUser1_Id" });
            DropIndex("dbo.Programares", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ServiciuProceduras", new[] { "Serviciu_ServiciuID" });
            DropIndex("dbo.ServiciuProceduras", new[] { "Procedura_Id" });
            DropTable("dbo.Testimonials");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Servicius");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Programares");
            DropTable("dbo.ServiciuProceduras");
            DropTable("dbo.Proceduras");
        }
    }
}
