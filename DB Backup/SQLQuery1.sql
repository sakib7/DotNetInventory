Select * from UserTbl;
Select * from Organization;
select * from Category;
select * from Product;
select * from Sale;
select * from SalePurchase;
select * from Purchase;
delete from Sale;
delete from Purchase;

select id, remStock from Purchase where productID=3
select * from Sale join SalePurchase on Sale.id=SalePurchase.saleID join Purchase on Purchase.id=SalePurchase.purchaseID;

SELECT Sale.id, Organization.name,Branch.name,OrgProduct.name,SalePurchase.quantity,Sale.date,
Sale.retailPrice,Sale.retailPrice*SalePurchase.quantity as revenue,
Purchase.purchasePrice,Purchase.purchasePrice*SalePurchase.quantity as cost, 
sale.retailPrice*SalePurchase.quantity-Purchase.purchasePrice*SalePurchase.quantity as profit,
(Sale.retailPrice*SalePurchase.quantity-Purchase.purchasePrice*SalePurchase.quantity)/(Sale.retailPrice*SalePurchase.quantity)*100 as margin
FROM Organization
JOIN Branch ON Branch.organizationID=Organization.id
JOIN Product ON Product.branchID=Branch.id
JOIN OrgProduct ON OrgProduct.id=Product.orgProductID
JOIN Sale ON Sale.productID=Product.id
JOIN SalePurchase ON SalePurchase.saleID=Sale.id
JOIN Purchase ON Purchase.id=SalePurchase.purchaseID;

SELECT SUM(SalePurchase.quantity) AS quantity,
SUM(Sale.retailPrice*SalePurchase.quantity) AS revenue,
SUM(Purchase.purchasePrice*SalePurchase.quantity) AS cost, 
SUM(sale.retailPrice*SalePurchase.quantity-Purchase.purchasePrice*SalePurchase.quantity) AS profit
FROM Organization
JOIN Branch ON Branch.organizationID=Organization.id
JOIN Product ON Product.branchID=Branch.id
JOIN OrgProduct ON OrgProduct.id=Product.orgProductID
JOIN Sale ON Sale.productID=Product.id
JOIN SalePurchase ON SalePurchase.saleID=Sale.id
JOIN Purchase ON Purchase.id=SalePurchase.purchaseID
WHERE Product.id=10;

SELECT Sale.id,MAX(OrgProduct.name), MIN(Sale.retailPrice)
FROM Organization
JOIN Branch ON Branch.organizationID=Organization.id
JOIN Product ON Product.branchID=Branch.id
JOIN OrgProduct ON OrgProduct.id=Product.orgProductID
JOIN Sale ON Sale.productID=Product.id
JOIN SalePurchase ON SalePurchase.saleID=Sale.id
JOIN Purchase ON Purchase.id=SalePurchase.purchaseID
GROUP BY Sale.id;

select Sale.id,Sale.customer,Sale.quantity,Sale.retailPrice,Sale.date,OrgProduct.name from Sale 
                                join SalePurchase on Sale.id=SalePurchase.saleID 
                                join Purchase on Purchase.id=SalePurchase.purchaseID 
                                join Product on Product.id=Sale.productID 
								join OrgProduct on OrgProduct.id=Product.orgProductID
                                where Product.branchID=11;

SELECT SUM(remStock)
FROM Purchase WHERE productID=3

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

SELECT Product.id,OrgProduct.name FROM Product 
JOIN OrgProduct ON Product.orgProductID=OrgProduct.id 
JOIN Category ON Category.id=OrgProduct.categoryID
JOIN Branch ON Branch.id=Product.branchID 
WHERE Branch.id=11

SELECT Branch.id,Branch.name FROM Branch JOIN Organization ON Branch.organizationID = Organization.id WHERE Organization.id=13;

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

select id, remStock from Purchase where productID=3 AND status='received'

SELECT SCOPE_IDENTITY();