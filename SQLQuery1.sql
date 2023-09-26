Create Database Yummy;
-----------------------------------------
CREATE TABLE Registeration(
ID int primary Key Identity(1,1),
Name NVARCHAR(200),
Email NVARCHAR(200),
Password NVARCHAR(200),
Role NVARCHAR(200) DEFAULT 'User',
AgreeAllStatement bit
)
-----------------------------------------
Create Proc SP_Registeration
(
@Name NVARCHAR(200),
@Email  NVARCHAR(200),
@Password NVARCHAR(200),
@AgreeAllStatement bit
)
AS
Begin
INSERT INTO Registeration(Name,Email,Password,AgreeAllStatement)values(@Name,@Email,@Password,@AgreeAllStatement)
End
------------------------------------------
CREATE PROCEDURE SP_CheckEmailExist
    @Email NVARCHAR(200),
    @EmailExists BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Registeration WHERE Email = @Email)
        SET @EmailExists = 1;
    ELSE
        SET @EmailExists = 0;
END
-----------------------------------------------------------
Create Proc SP_CheckLogin(
@Email NVARCHAR(200)
)
AS
Begin
SELECT Email,Password,Role From Registeration 
Where Email=@Email
End
-----------------------------------------------------------
CREATE TABLE Product(
Id_Product int Primary Key Identity(1,1),
Name NVARCHAR(Max),
Price  NVARCHAR(300),
Description NVARCHAR(MAX),
Image varBinary(Max),
Category_name NVARCHar(Max)
)
-----------------------------------------------------------
Create Proc SP_Product(
@Name NVARCHAR(Max),
@Price  NVARCHAR(300),
@Description NVARCHAR(MAX),
@Image varBinary(Max),
@Category_name NVARCHar(Max)
)
AS
Begin
INSERT INTO Product(Name,Price,Description,Image,Category_name) VALUES 
(@Name,@Price,@Description,@Image,@Category_name)
End
--------------------------------------------------------------------
CREATE PROC SP_GetAllProduct
AS
Begin
SElECT * FROM Product
End
---------------------------------------------------------------------
Create Proc SP_UpdateProduct
(
@product_Id int,
@Name NVARCHAR(Max),
@Price  NVARCHAR(300),
@Description NVARCHAR(MAX),
@Image varBinary(Max),
@Category_name NVARCHar(Max)
)
AS
BEGIN
    UPDATE Product
    SET 
        Name = @Name,
        Price = @Price,
        Description = @Description,
        Image = @Image,
        Category_Name = @Category_Name
    WHERE ID_product = @product_Id;
END
------------------------------------------------------------------------
Create Proc SP_DeleteProduct
(
@ID int
)
AS
Begin
Delete From Product where ID_product=@ID
End
--------------------------------------------------------------------------
Create Table Chefs
(
Chef_Id int Primary Key Identity(1,1),
Chef_Name NVARCHAR(200),
Chef_Job NVARCHAR(Max),
Image Varbinary(Max),
Chef_Description NVARCHAR(Max)
)
--------------------------------------------------------------------------
Create Proc SP_AddChef
(
@Chef_Name NVARCHAR(200),
@Chef_Job NVARCHAR(Max),
@Image varBinary(Max),
@Chef_Description NVARCHAR(Max)
)
As
Begin
Insert into Chefs(Chef_Name,Chef_Job,Image,Chef_Description) Values (@Chef_Name,@Chef_Job,@Image,@Chef_Description)
End
--------------------------------------------------------------------------------
CREATE PROC SP_GetProductWithCatagory
(
@Category_name NVARCHAR(Max)
)
AS
Begin
SElECT * FROM Product 
Where Category_name=@Category_name
End
-------------------------------------------------------------------------------
CREATE PROC SP_GetProductById
(
@ID int
)
AS
Begin
Select Name,Price,Description,Image From Product
Where Id_Product=@ID
End
---------------------------------------------------------------------------------
Create proc FilterByPrice
(
@Price NVARCHAR(300),
@Category_name NVARCHAR(Max)
)
As
Begin
Select Name,Price,Description,Image from Product
Where Price=@Price 
AND Category_name=@Category_name
End
------------------------------------------------------------------------------