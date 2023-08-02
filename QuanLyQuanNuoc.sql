CREATE TABLE TableFood(
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL DEFAULT 'Chưa đặt tên',
    status VARCHAR(50) NOT NULL DEFAULT 'Trống'
);

CREATE TABLE Account(
    DisplayName VARCHAR(50) NOT NULL,
    UserName VARCHAR(50) PRIMARY KEY,
    PassWord VARCHAR(50) NOT NULL,
    Type INT NOT NULL DEFAULT 0
);

CREATE TABLE FoodCategory(
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL DEFAULT 'Chưa đặt tên'
);

CREATE TABLE Food(
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL DEFAULT 'Chưa đặt tên',
    idCategory INT NOT NULL,
    price FLOAT NOT NULL DEFAULT 0,
    FOREIGN KEY (idCategory) REFERENCES FoodCategory(id)
);

CREATE TABLE Bill(
    id SERIAL PRIMARY KEY,
    DateCheckIn DATE NOT NULL DEFAULT CURRENT_DATE,
    DateCheckOut DATE,
    idTable INT NOT NULL,
    discount INT,
    TotalPrice FLOAT,
    status INT NOT NULL DEFAULT 0,
    FOREIGN KEY (idTable) REFERENCES TableFood(id)
);

CREATE TABLE BillInfo(
    id SERIAL PRIMARY KEY,
    idBill INT NOT NULL,
    idFood INT NOT NULL,
    count INT NOT NULL DEFAULT 0,
    FOREIGN KEY (idBill) REFERENCES Bill(id),
    FOREIGN KEY (idFood) REFERENCES Food(id)
);


INSERT INTO TableFood (name)
SELECT 'Bàn ' || generate_series(0, 10)::text;

INSERT INTO "Account" ("UserName", "DisplayName", "PassWord", "Type")
VALUES ('admin', 'admin1', '1', 1);

INSERT INTO "Account" ("UserName", "DisplayName", "PassWord", "Type")
VALUES ('nv1', 'hoang', '1', 0);

select * from public."FoodCategory"


CREATE OR REPLACE FUNCTION PR_GETTABLELIST()
RETURNS TABLE (
    id INT,
    name VARCHAR(50),
    status VARCHAR(50)
)
AS $$
BEGIN
    RETURN QUERY SELECT id, name, status FROM "TableFood";
END;
$$ LANGUAGE plpgsql;


INSERT INTO "Food" ("name" ,"idCategory", "price") values ('Trà đá',1,5000)
INSERT INTO "FoodCategory" ("name") VALUES ('Nước');
INSERT INTO "FoodCategory" ("name") VALUES ('Đồ ăn');

INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('Trà đào', 1, 15000);

-- Thêm bản ghi 'Coffee' vào bảng "Food"
INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('Coffee', 1, 10000);

-- Thêm bản ghi 'Trà sữa' vào bảng "Food"
INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('Trà sữa', 1, 20000);

-- Thêm bản ghi 'Cafe đen đá' vào bảng "Food"
INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('Cafe đen đá', 1, 10000);

-- Thêm bản ghi 'Bạc xỉu' vào bảng "Food"
INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('Bạc xỉu', 1, 15000);

-- Thêm bản ghi '7Up' vào bảng "Food"
INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('7Up', 1, 10000);

-- Thêm bản ghi 'Bánh ngọt' vào bảng "Food"
INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('Bánh ngọt', 2, 12000);

-- Thêm bản ghi 'Bánh tráng' vào bảng "Food"
INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('Bánh tráng', 2, 15000);

-- Thêm bản ghi 'Bánh bông lan' vào bảng "Food"
INSERT INTO "Food" ("name", "idCategory", "price") VALUES ('Bánh bông lan', 2, 20000);

select * from public."BillInfo"
select * from public."Bill"
SELECT * FROM public."TableFood"
SELECT "UserName","PassWord" FROM "Account" WHERE "UserName" = 'admin' AND "PassWord" = '1'

SELECT "UserName","PassWord" FROM "Account" WHERE "UserName" = 'admin' AND "PassWord" = '12345' 
--------------------------------------
UPDATE "Bill" SET "status" = 1,"DateCheckOut" = CURRENT_DATE,"discount" = 20,"TotalPrice" = 10000  WHERE "id" = 1;

UPDATE "TableFood" SET "status" = DEFAULT 
-------------------------
update 
select* from "Food" order by "id"
SELECT * FROM "Food" where "name" = '$a$'
SELECT * FROM "Food" WHERE unaccent(lower("name")) ILIKE '%tra%' ORDER BY "id"
delete from "Food" where id=12
UPDATE "Food" SET "name"='Bánh bò',"idCategory"=2,"price"=15000 WHERE "id"=11 
CREATE EXTENSION unaccent;
CREATE OR REPLACE PROCEDURE public.USP_InsertBillInfo(
    in_idBill INT,
    in_idFood INT,
    in_count INT
)
LANGUAGE plpgsql
AS $$
DECLARE
    isExitsBillInfo INT;
    foodCount INT := 1;
    newCount INT;
BEGIN
    SELECT id, count INTO isExitsBillInfo, foodCount
    FROM public."BillInfo" AS b
    WHERE idBill = in_idBill AND idFood = in_idFood;

    IF (isExitsBillInfo IS NOT NULL) THEN
        newCount := foodCount + in_count;
        IF (newCount > 0) THEN
            UPDATE public."BillInfo" SET "count" = newCount WHERE idBill = in_idBill AND idFood = in_idFood;
        ELSE
            DELETE FROM public."BillInfo" WHERE idBill = in_idBill AND idFood = in_idFood;
        END IF;
    ELSE
        INSERT INTO public."BillInfo" (idBill, idFood, "count")
        VALUES (in_idBill, in_idFood, in_count);
    END IF;
END;
$$;

CREATE OR REPLACE PROCEDURE public.USP_InsertBill(
    in_idTable INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO public."Bill" ("datecheckin", "datecheckout", "idTable", "status", "discount")
    VALUES (CURRENT_DATE, NULL, in_idTable, 0, 0);
END;
$$;

------------------------------------------------

CREATE OR REPLACE PROCEDURE USP_InsertBillInfo(
    IN idBill INT,
    IN idFood INT,
    IN count INT
)
LANGUAGE plpgsql
AS $$
DECLARE
    isExitsBillInfo INT;
    foodCount INT := 1;
    newCount INT;
BEGIN
    SELECT b.id, b."count" INTO isExitsBillInfo, foodCount
    FROM "BillInfo" AS b
    WHERE b."idBill" = idBill AND b."idFood" = idFood;

    IF (isExitsBillInfo > 0) THEN
        newCount := foodCount + count;
        IF (newCount > 0) THEN
            UPDATE "BillInfo"
            SET "count" = foodCount + count
            WHERE "idFood" = idFood;
        ELSE
            DELETE FROM "BillInfo" WHERE "idBill" = idBill AND "idFood" = idFood;
        END IF;
    ELSE
        INSERT INTO "BillInfo" ("idBill", "idFood", "count")
        VALUES (idBill, idFood, count);
    END IF;
END;
$$;




	CALL public.USP_InsertBill(3);
select * from public."Bill"
select * from public."BillInfo"
select * from public."TableFood"
select * from "Food" where "idCategory" = 2
call public.usp_insertbill(in_idtable)



SELECT f.name, bi.count, f.price, f.price*bi.count AS "totalPrice"
FROM public."BillInfo" AS bi
INNER JOIN public."Bill" AS b ON bi."idBill" = b."id"
INNER JOIN public."Food" AS f ON bi."idFood" = f."id"
WHERE b."status" = 0 AND b."idTable" = 3;


call public.usp_insertbill(9)
call public.usp_insertbillinfo(1, 7, 3)  public.usp_insertbill(in_idtable)
	CREATE OR REPLACE FUNCTION public.UTG_UpdateBillInfo()
RETURNS TRIGGER AS $$
DECLARE
    idBill INT;
    idTable INT;
    count INT;
BEGIN
    IF (TG_OP = 'INSERT') THEN
        idBill := NEW."idBill";
    ELSIF (TG_OP = 'UPDATE') THEN
        idBill := NEW."idBill";
    END IF;

    SELECT "idTable" INTO idTable FROM public."Bill" WHERE "id" = idBill AND "status" = 0;

    SELECT COUNT(*) INTO count FROM public."BillInfo" WHERE "idBill" = idBill;

    IF (count > 0) THEN
        UPDATE public."TableFood" SET "status" = N'Có người' WHERE "id" = idTable;
    ELSE
        UPDATE public."TableFood" SET "status" = N'Trống' WHERE "id" = idTable;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER UTG_UpdateBillInfo
AFTER INSERT OR UPDATE ON public."BillInfo"
FOR EACH ROW
EXECUTE FUNCTION public.UTG_UpdateBillInfo();

  
  
  
  ALTER TABLE "Bill"
ADD COLUMN DateCheckIn timestamp;

UPDATE "Bill" SET status = 1, DateCheckOut = now(),   discount = 20,  TotalPrice = 10000  WHERE id = 3
  UPDATE "Bill" SET
    status = 1,
    DateCheckOut = now()::timestamp with time zone,
    discount = 20,
    TotalPrice = 10000
WHERE id = 3;
  DateCheckOut = TIMESTAMP WITH TIME ZONE
  UPDATE "Bill" SET
    status = 1,
    DateCheckOut = now(),
    discount = 20,
    TotalPrice = 10000
WHERE id = 3;
  CREATE OR REPLACE FUNCTION public.UTG_UpdateBill()
RETURNS TRIGGER AS $$
DECLARE
    idBill INT;
    idTable INT;
    count INT;
BEGIN
    IF (TG_OP = 'UPDATE') THEN
        idBill := NEW."id";
    END IF;

    SELECT "idTable" INTO idTable FROM public."Bill" WHERE "id" = idBill;

    SELECT COUNT(*) INTO count FROM public."Bill" WHERE "idTable" = idTable AND "status" = 0;

    IF (count = 0) THEN
        UPDATE public."TableFood" SET "status" = N'Trống' WHERE "id" = idTable;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER UTG_UpdateBill
AFTER UPDATE ON public."Bill"
FOR EACH ROW
EXECUTE FUNCTION public.UTG_UpdateBill();
select * from public."TableFood"
select * from "Bill" where status =0 and "idTable" = 2

select MAX(id) from "Bill"


select * from "BillInfo" where "idBill


CREATE OR REPLACE FUNCTION public.USP_GetListBillDate(
    checkIn date,
    checkOut date
)
RETURNS TABLE (name character varying, totalprice double precision, DateCheckIn date, DateCheckOut date, discount integer)
AS $$
BEGIN
    RETURN QUERY
    SELECT t.name, b.totalprice, b.datecheckin, b.datecheckout, b.discount
FROM public."Bill" b 
INNER JOIN public."TableFood" t ON t.id = b."idTable"
WHERE b.datecheckin >= checkIn AND b.datecheckout <= checkOut AND b.status = 1;
 END;
$$ LANGUAGE plpgsql;

SELECT * FROM "usp_get_list_bill_date"("22-07-2023", "2023-07-23")
public.usp_getlistbilldate(checkin, checkout)

select * from public.usp_getlistbilldate("22-07-2023", "2023-07-23")

SELECT * FROM public.USP_GetListBillDate('2023-07-22', '2023-07-23');
select * from "Account"
	Procedures
	
		select * from "Account" where "UserName" = 'admin'
	
	select * from public."TableFood" order by "id" 
	
	


