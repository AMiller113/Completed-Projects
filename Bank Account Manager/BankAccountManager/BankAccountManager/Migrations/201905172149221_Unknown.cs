namespace BankAccountManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unknown : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" }, "dbo.BusinessAccounts");
            DropForeignKey("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" }, "dbo.CheckingAccounts");
            DropForeignKey("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" }, "dbo.SavingsAccounts");
            DropIndex("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" });
            DropIndex("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" });
            DropIndex("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" });
            DropPrimaryKey("dbo.BusinessAccounts");
            DropPrimaryKey("dbo.CheckingAccounts");
            DropPrimaryKey("dbo.SavingsAccounts");
            AlterColumn("dbo.BusinessAccounts", "BankName", c => c.Short(nullable: false));
            AlterColumn("dbo.BusinessAccounts", "BusinessAccount_BankName", c => c.Short());
            AlterColumn("dbo.CheckingAccounts", "BankName", c => c.Short(nullable: false));
            AlterColumn("dbo.CheckingAccounts", "CheckingAccount_BankName", c => c.Short());
            AlterColumn("dbo.SavingsAccounts", "BankName", c => c.Short(nullable: false));
            AlterColumn("dbo.SavingsAccounts", "SavingsAccount_BankName", c => c.Short());
            AddPrimaryKey("dbo.BusinessAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            AddPrimaryKey("dbo.CheckingAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            AddPrimaryKey("dbo.SavingsAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            CreateIndex("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" });
            CreateIndex("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" });
            CreateIndex("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" });
            AddForeignKey("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" }, "dbo.BusinessAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            AddForeignKey("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" }, "dbo.CheckingAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            AddForeignKey("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" }, "dbo.SavingsAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" }, "dbo.SavingsAccounts");
            DropForeignKey("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" }, "dbo.CheckingAccounts");
            DropForeignKey("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" }, "dbo.BusinessAccounts");
            DropIndex("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" });
            DropIndex("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" });
            DropIndex("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" });
            DropPrimaryKey("dbo.SavingsAccounts");
            DropPrimaryKey("dbo.CheckingAccounts");
            DropPrimaryKey("dbo.BusinessAccounts");
            AlterColumn("dbo.SavingsAccounts", "SavingsAccount_BankName", c => c.Int());
            AlterColumn("dbo.SavingsAccounts", "BankName", c => c.Int(nullable: false));
            AlterColumn("dbo.CheckingAccounts", "CheckingAccount_BankName", c => c.Int());
            AlterColumn("dbo.CheckingAccounts", "BankName", c => c.Int(nullable: false));
            AlterColumn("dbo.BusinessAccounts", "BusinessAccount_BankName", c => c.Int());
            AlterColumn("dbo.BusinessAccounts", "BankName", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SavingsAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            AddPrimaryKey("dbo.CheckingAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            AddPrimaryKey("dbo.BusinessAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            CreateIndex("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" });
            CreateIndex("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" });
            CreateIndex("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" });
            AddForeignKey("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" }, "dbo.SavingsAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            AddForeignKey("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" }, "dbo.CheckingAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
            AddForeignKey("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" }, "dbo.BusinessAccounts", new[] { "PinNumber", "AccountHolder", "BankName" });
        }
    }
}
