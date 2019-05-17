namespace BankAccountManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessAccounts",
                c => new
                    {
                        PinNumber = c.Int(nullable: false),
                        AccountHolder = c.String(nullable: false, maxLength: 128),
                        BankName = c.Int(nullable: false),
                        BusinessName = c.String(),
                        Balance = c.Single(nullable: false),
                        AccountOpenDate = c.DateTime(nullable: false),
                        BusinessAccount_PinNumber = c.Int(),
                        BusinessAccount_AccountHolder = c.String(maxLength: 128),
                        BusinessAccount_BankName = c.Int(),
                    })
                .PrimaryKey(t => new { t.PinNumber, t.AccountHolder, t.BankName })
                .ForeignKey("dbo.BusinessAccounts", t => new { t.BusinessAccount_PinNumber, t.BusinessAccount_AccountHolder, t.BusinessAccount_BankName })
                .Index(t => new { t.BusinessAccount_PinNumber, t.BusinessAccount_AccountHolder, t.BusinessAccount_BankName });
            
            CreateTable(
                "dbo.CheckingAccounts",
                c => new
                    {
                        PinNumber = c.Int(nullable: false),
                        AccountHolder = c.String(nullable: false, maxLength: 128),
                        BankName = c.Int(nullable: false),
                        MonthlyTransferAmount = c.Single(nullable: false),
                        Balance = c.Single(nullable: false),
                        AccountOpenDate = c.DateTime(nullable: false),
                        CheckingAccount_PinNumber = c.Int(),
                        CheckingAccount_AccountHolder = c.String(maxLength: 128),
                        CheckingAccount_BankName = c.Int(),
                    })
                .PrimaryKey(t => new { t.PinNumber, t.AccountHolder, t.BankName })
                .ForeignKey("dbo.CheckingAccounts", t => new { t.CheckingAccount_PinNumber, t.CheckingAccount_AccountHolder, t.CheckingAccount_BankName })
                .Index(t => new { t.CheckingAccount_PinNumber, t.CheckingAccount_AccountHolder, t.CheckingAccount_BankName });
            
            CreateTable(
                "dbo.SavingsAccounts",
                c => new
                    {
                        PinNumber = c.Int(nullable: false),
                        AccountHolder = c.String(nullable: false, maxLength: 128),
                        BankName = c.Int(nullable: false),
                        WithdrawalsThisMonth = c.Int(nullable: false),
                        Balance = c.Single(nullable: false),
                        AccountOpenDate = c.DateTime(nullable: false),
                        SavingsAccount_PinNumber = c.Int(),
                        SavingsAccount_AccountHolder = c.String(maxLength: 128),
                        SavingsAccount_BankName = c.Int(),
                    })
                .PrimaryKey(t => new { t.PinNumber, t.AccountHolder, t.BankName })
                .ForeignKey("dbo.SavingsAccounts", t => new { t.SavingsAccount_PinNumber, t.SavingsAccount_AccountHolder, t.SavingsAccount_BankName })
                .Index(t => new { t.SavingsAccount_PinNumber, t.SavingsAccount_AccountHolder, t.SavingsAccount_BankName });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" }, "dbo.SavingsAccounts");
            DropForeignKey("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" }, "dbo.CheckingAccounts");
            DropForeignKey("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" }, "dbo.BusinessAccounts");
            DropIndex("dbo.SavingsAccounts", new[] { "SavingsAccount_PinNumber", "SavingsAccount_AccountHolder", "SavingsAccount_BankName" });
            DropIndex("dbo.CheckingAccounts", new[] { "CheckingAccount_PinNumber", "CheckingAccount_AccountHolder", "CheckingAccount_BankName" });
            DropIndex("dbo.BusinessAccounts", new[] { "BusinessAccount_PinNumber", "BusinessAccount_AccountHolder", "BusinessAccount_BankName" });
            DropTable("dbo.SavingsAccounts");
            DropTable("dbo.CheckingAccounts");
            DropTable("dbo.BusinessAccounts");
        }
    }
}
