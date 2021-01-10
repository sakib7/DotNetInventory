Select * from UserTbl;
Select * from Organization;
select * from Category;
select * from Purchase;
select * from Product;


SELECT SUM(remStock)
FROM Purchase WHERE productID=2

SELECT * FROM Product
JOIN OrgProduct ON Product.orgProductID = OrgProduct.id
JOIN Category ON Category.id = OrgProduct.categoryID
JOIN Branch ON Branch.id = Product.branchID
WHERE Product.branchID=5

SELECT * FROM Product 
JOIN OrgProduct ON Product.orgProductID=OrgProduct.id 
JOIN Branch ON Branch.id=Product.branchID 
WHERE OrgProduct.organizationID=8 ORDER BY Branch.id


SELECT Purchase.id,OrgProduct.name,Purchase.status,Purchase.quantity,
Purchase.remStock,Purchase.date,Purchase.purchasePrice ,
Vendor.id,Vendor.name,
Branch.id,Branch.name
FROM Organization
JOIN Branch ON Branch.organizationID=Organization.id
JOIN Product ON Product.branchID = Branch.id
JOIN OrgProduct ON OrgProduct.id = Product.orgProductID
JOIN Purchase ON Purchase.productID = Product.id
LEFT JOIN Vendor ON Vendor.id = Purchase.vendorID
WHERE Organization.id = 8 AND Purchase.id=4
ORDER BY Purchase.date desc

SELECT Purchase.id,OrgProduct.name,Purchase.status,Purchase.quantity,Purchase.remStock,Purchase.date 
FROM Purchase
JOIN Product ON Product.id = Purchase.productID
JOIN OrgProduct ON OrgProduct.id = Product.orgProductID
JOIN Branch ON Product.branchID = Branch.id
WHERE Branch.id = 5

SELECT * FROM 
OrgProduct JOIN
Organization ON OrgProduct.organizationID=Organization.id JOIN
Category ON OrgProduct.categoryID = Category.id 
WHERE OrgProduct.organizationID = 8

SELECT * FROM Branch JOIN Organization ON Branch.organizationID = Organization.id WHERE Branch.organizationID = 8
SELECT * FROM Product JOIN OrgProduct ON Product.orgProductID=OrgProduct.id JOIN Branch ON Branch.id=Product.branchID WHERE OrgProduct.organizationID=8

SELECT * FROM Product 
JOIN OrgProduct ON Product.orgProductID=OrgProduct.id 
JOIN Category ON Category.id=OrgProduct.categoryID
JOIN Branch ON Branch.id=Product.branchID 
WHERE Product.branchID=5

SELECT * FROM UserTbl  
JOIN Organization ON Organization.id=UserTbl.organizationID 
WHERE user_name='user' AND pass='1'

SELECT * FROM UserTbl 
JOIN Organization ON Organization.id=UserTbl.organizationID 
JOIN Branch ON Branch.id=UserTbl.branchID 
WHERE user_name='employee1' AND pass='123'

SELECT * FROM UserTbl JOIN Branch ON UserTbl.branchID=Branch.id 
WHERE branchID IS NOT NULL AND UserTbl.organizationID=8

delete from UserTbl;
delete from Organization;
delete from Category;
delete from Purchase;

SELECT SCOPE_IDENTITY();