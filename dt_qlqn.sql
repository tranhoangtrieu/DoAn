--
-- PostgreSQL database dump
--

-- Dumped from database version 15.3
-- Dumped by pg_dump version 15.3

-- Started on 2023-08-02 17:47:25

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2 (class 3079 OID 24576)
-- Name: unaccent; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS unaccent WITH SCHEMA public;


--
-- TOC entry 3389 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION unaccent; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION unaccent IS 'text search dictionary that removes accents';


--
-- TOC entry 226 (class 1255 OID 16511)
-- Name: pr_gettablelist(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.pr_gettablelist() RETURNS TABLE(id integer, name character varying, status character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT id, name, status FROM "TableFood";
END;
$$;


ALTER FUNCTION public.pr_gettablelist() OWNER TO postgres;

--
-- TOC entry 227 (class 1255 OID 16877)
-- Name: usp_getlistbilldate(date, date); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.usp_getlistbilldate(checkin date, checkout date) RETURNS TABLE(name character varying, totalprice double precision, datecheckin date, datecheckout date, discount integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT t.name, b.totalprice, b.DateCheckIn, b.DateCheckOut, b.discount
FROM public."Bill" b 
INNER JOIN public."TableFood" t ON t.id = b."idTable"
WHERE b.datecheckin >= checkIn AND b.datecheckout <= checkOut AND b.status = 1;
 END;
$$;


ALTER FUNCTION public.usp_getlistbilldate(checkin date, checkout date) OWNER TO postgres;

--
-- TOC entry 228 (class 1255 OID 16878)
-- Name: usp_insertbill(integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.usp_insertbill(IN in_idtable integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO public."Bill" ("datecheckin", "datecheckout", "idTable", "status", "discount")
    VALUES (CURRENT_DATE, NULL, in_idTable, 0, 0);
END;
$$;


ALTER PROCEDURE public.usp_insertbill(IN in_idtable integer) OWNER TO postgres;

--
-- TOC entry 246 (class 1255 OID 24588)
-- Name: usp_insertbillinfo(integer, integer, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.usp_insertbillinfo(IN idbill integer, IN idfood integer, IN itemcount integer)
    LANGUAGE plpgsql
    AS $$
DECLARE
    isExitsBillInfo INT;
    foodCount INT := 1;
    newCount INT;
BEGIN
    SELECT b.id,b.count  INTO isExitsBillInfo, foodCount
    FROM "BillInfo" AS b
    WHERE b."idBill" = idBill AND b."idFood" = idFood;

    IF (isExitsBillInfo > 0) THEN
        newCount := foodCount + itemcount;
        IF (newCount > 0) THEN
            UPDATE "BillInfo"
            SET "count" = foodCount + itemcount
            WHERE "idFood" = idFood;
        ELSE
            DELETE FROM "BillInfo" WHERE "idBill" = idBill AND "idFood" = idFood;
        END IF;
    ELSE
        INSERT INTO "BillInfo" ("idBill", "idFood", "count")
        VALUES (idBill, idFood, itemcount);
    END IF;
END;
$$;


ALTER PROCEDURE public.usp_insertbillinfo(IN idbill integer, IN idfood integer, IN itemcount integer) OWNER TO postgres;

--
-- TOC entry 244 (class 1255 OID 16518)
-- Name: utg_updatebill(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.utg_updatebill() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
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
$$;


ALTER FUNCTION public.utg_updatebill() OWNER TO postgres;

--
-- TOC entry 245 (class 1255 OID 16516)
-- Name: utg_updatebillinfo(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.utg_updatebillinfo() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
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
$$;


ALTER FUNCTION public.utg_updatebillinfo() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 16451)
-- Name: Account; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Account" (
    "DisplayName" character varying(50) NOT NULL,
    "UserName" character varying(50) NOT NULL,
    "PassWord" character varying(50) NOT NULL,
    "Type" integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."Account" OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16825)
-- Name: Bill; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Bill" (
    id integer NOT NULL,
    datecheckin date DEFAULT CURRENT_DATE NOT NULL,
    datecheckout date,
    "idTable" integer NOT NULL,
    discount integer,
    totalprice double precision,
    status integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."Bill" OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 16847)
-- Name: BillInfo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."BillInfo" (
    id integer NOT NULL,
    "idBill" integer NOT NULL,
    "idFood" integer NOT NULL,
    count integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."BillInfo" OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16466)
-- Name: Food; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Food" (
    id integer NOT NULL,
    name character varying(50) DEFAULT 'Chưa đặt tên'::character varying NOT NULL,
    "idCategory" integer NOT NULL,
    price double precision DEFAULT 0 NOT NULL
);


ALTER TABLE public."Food" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16458)
-- Name: FoodCategory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."FoodCategory" (
    id integer NOT NULL,
    name character varying(50) DEFAULT 'Chưa đặt tên'::character varying NOT NULL
);


ALTER TABLE public."FoodCategory" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16443)
-- Name: TableFood; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TableFood" (
    id integer NOT NULL,
    name character varying(50) DEFAULT 'Chưa đặt tên'::character varying NOT NULL,
    status character varying(50) DEFAULT 'Trống'::character varying NOT NULL
);


ALTER TABLE public."TableFood" OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 16824)
-- Name: bill_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bill_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.bill_id_seq OWNER TO postgres;

--
-- TOC entry 3390 (class 0 OID 0)
-- Dependencies: 222
-- Name: bill_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bill_id_seq OWNED BY public."Bill".id;


--
-- TOC entry 224 (class 1259 OID 16846)
-- Name: billinfo_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.billinfo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.billinfo_id_seq OWNER TO postgres;

--
-- TOC entry 3391 (class 0 OID 0)
-- Dependencies: 224
-- Name: billinfo_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.billinfo_id_seq OWNED BY public."BillInfo".id;


--
-- TOC entry 220 (class 1259 OID 16465)
-- Name: food_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.food_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.food_id_seq OWNER TO postgres;

--
-- TOC entry 3392 (class 0 OID 0)
-- Dependencies: 220
-- Name: food_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.food_id_seq OWNED BY public."Food".id;


--
-- TOC entry 218 (class 1259 OID 16457)
-- Name: foodcategory_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.foodcategory_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.foodcategory_id_seq OWNER TO postgres;

--
-- TOC entry 3393 (class 0 OID 0)
-- Dependencies: 218
-- Name: foodcategory_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.foodcategory_id_seq OWNED BY public."FoodCategory".id;


--
-- TOC entry 215 (class 1259 OID 16442)
-- Name: tablefood_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tablefood_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tablefood_id_seq OWNER TO postgres;

--
-- TOC entry 3394 (class 0 OID 0)
-- Dependencies: 215
-- Name: tablefood_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tablefood_id_seq OWNED BY public."TableFood".id;


--
-- TOC entry 3219 (class 2604 OID 16828)
-- Name: Bill id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Bill" ALTER COLUMN id SET DEFAULT nextval('public.bill_id_seq'::regclass);


--
-- TOC entry 3222 (class 2604 OID 16850)
-- Name: BillInfo id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."BillInfo" ALTER COLUMN id SET DEFAULT nextval('public.billinfo_id_seq'::regclass);


--
-- TOC entry 3216 (class 2604 OID 16469)
-- Name: Food id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Food" ALTER COLUMN id SET DEFAULT nextval('public.food_id_seq'::regclass);


--
-- TOC entry 3214 (class 2604 OID 16461)
-- Name: FoodCategory id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."FoodCategory" ALTER COLUMN id SET DEFAULT nextval('public.foodcategory_id_seq'::regclass);


--
-- TOC entry 3210 (class 2604 OID 16446)
-- Name: TableFood id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TableFood" ALTER COLUMN id SET DEFAULT nextval('public.tablefood_id_seq'::regclass);


--
-- TOC entry 3227 (class 2606 OID 16456)
-- Name: Account account_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT account_pkey PRIMARY KEY ("UserName");


--
-- TOC entry 3233 (class 2606 OID 16832)
-- Name: Bill bill_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Bill"
    ADD CONSTRAINT bill_pkey PRIMARY KEY (id);


--
-- TOC entry 3235 (class 2606 OID 16853)
-- Name: BillInfo billinfo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."BillInfo"
    ADD CONSTRAINT billinfo_pkey PRIMARY KEY (id);


--
-- TOC entry 3231 (class 2606 OID 16473)
-- Name: Food food_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Food"
    ADD CONSTRAINT food_pkey PRIMARY KEY (id);


--
-- TOC entry 3229 (class 2606 OID 16464)
-- Name: FoodCategory foodcategory_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."FoodCategory"
    ADD CONSTRAINT foodcategory_pkey PRIMARY KEY (id);


--
-- TOC entry 3225 (class 2606 OID 16450)
-- Name: TableFood tablefood_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TableFood"
    ADD CONSTRAINT tablefood_pkey PRIMARY KEY (id);


--
-- TOC entry 3240 (class 2620 OID 16865)
-- Name: Bill utg_updatebill; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER utg_updatebill AFTER UPDATE ON public."Bill" FOR EACH ROW EXECUTE FUNCTION public.utg_updatebill();


--
-- TOC entry 3241 (class 2620 OID 16864)
-- Name: BillInfo utg_updatebillinfo; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER utg_updatebillinfo AFTER INSERT OR UPDATE ON public."BillInfo" FOR EACH ROW EXECUTE FUNCTION public.utg_updatebillinfo();


--
-- TOC entry 3237 (class 2606 OID 16833)
-- Name: Bill bill_idtable_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Bill"
    ADD CONSTRAINT bill_idtable_fkey FOREIGN KEY ("idTable") REFERENCES public."TableFood"(id);


--
-- TOC entry 3238 (class 2606 OID 16854)
-- Name: BillInfo billinfo_idbill_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."BillInfo"
    ADD CONSTRAINT billinfo_idbill_fkey FOREIGN KEY ("idBill") REFERENCES public."Bill"(id);


--
-- TOC entry 3239 (class 2606 OID 16859)
-- Name: BillInfo billinfo_idfood_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."BillInfo"
    ADD CONSTRAINT billinfo_idfood_fkey FOREIGN KEY ("idFood") REFERENCES public."Food"(id);


--
-- TOC entry 3236 (class 2606 OID 16474)
-- Name: Food food_idcategory_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Food"
    ADD CONSTRAINT food_idcategory_fkey FOREIGN KEY ("idCategory") REFERENCES public."FoodCategory"(id);


-- Completed on 2023-08-02 17:47:25

--
-- PostgreSQL database dump complete
--

