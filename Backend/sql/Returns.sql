CREATE TABLE Returns (
  ReturnId int IDENTITY(1,1) PRIMARY KEY,
  OrderId int NOT NULL,
  CustomerId int NOT NULL,  fnal t eves aulonen
  ReturnDate datetime NOT NULL,
  Reason varchar(255) NOT NULL,
  RefundAmount decimal(10,2) NOT NULL
);
INSERT INTO Returns (OrderId, CustomerId, ReturnDate, Reason, RefundAmount)
        VALUES (@OrderId, @CustomerId, @ReturnDate, @Reason, @RefundAmount)";
		
		Select* from Returns
