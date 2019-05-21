/*Bank List based on enum defined in the BankAccountManager Namespace*/
Create Table BankAccountManager.dbo.BankCodes
(
BankCode int Primary Key,
BankName varchar(50)
)

Insert Into BankAccountManager.dbo.BankCodes
Values
(1, 'Chase'),
(2, 'Capitol One'),
(3, 'Bank of America'),
(4, 'HSBC'),
(5, 'TD Bank'),
(6, 'Citi Bank'),
(7, 'Morgan Stanley'),
(8, 'Goldman Sachs')

