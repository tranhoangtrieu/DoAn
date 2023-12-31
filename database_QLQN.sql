PGDMP     ;    .                {            QuanLyQuanNuoc    15.3    15.3 *    :           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ;           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            <           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            =           1262    16407    QuanLyQuanNuoc    DATABASE     �   CREATE DATABASE "QuanLyQuanNuoc" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
     DROP DATABASE "QuanLyQuanNuoc";
                postgres    false                        3079    24576    unaccent 	   EXTENSION     <   CREATE EXTENSION IF NOT EXISTS unaccent WITH SCHEMA public;
    DROP EXTENSION unaccent;
                   false            >           0    0    EXTENSION unaccent    COMMENT     P   COMMENT ON EXTENSION unaccent IS 'text search dictionary that removes accents';
                        false    2            �            1255    16511    pr_gettablelist()    FUNCTION     �   CREATE FUNCTION public.pr_gettablelist() RETURNS TABLE(id integer, name character varying, status character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT id, name, status FROM "TableFood";
END;
$$;
 (   DROP FUNCTION public.pr_gettablelist();
       public          postgres    false            �            1255    16877    usp_getlistbilldate(date, date)    FUNCTION     �  CREATE FUNCTION public.usp_getlistbilldate(checkin date, checkout date) RETURNS TABLE(name character varying, totalprice double precision, datecheckin date, datecheckout date, discount integer)
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
 G   DROP FUNCTION public.usp_getlistbilldate(checkin date, checkout date);
       public          postgres    false            �            1255    16878    usp_insertbill(integer) 	   PROCEDURE     �   CREATE PROCEDURE public.usp_insertbill(IN in_idtable integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO public."Bill" ("datecheckin", "datecheckout", "idTable", "status", "discount")
    VALUES (CURRENT_DATE, NULL, in_idTable, 0, 0);
END;
$$;
 =   DROP PROCEDURE public.usp_insertbill(IN in_idtable integer);
       public          postgres    false            �            1255    24588 -   usp_insertbillinfo(integer, integer, integer) 	   PROCEDURE     6  CREATE PROCEDURE public.usp_insertbillinfo(IN idbill integer, IN idfood integer, IN itemcount integer)
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
 f   DROP PROCEDURE public.usp_insertbillinfo(IN idbill integer, IN idfood integer, IN itemcount integer);
       public          postgres    false            �            1255    16518    utg_updatebill()    FUNCTION       CREATE FUNCTION public.utg_updatebill() RETURNS trigger
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
 '   DROP FUNCTION public.utg_updatebill();
       public          postgres    false            �            1255    16516    utg_updatebillinfo()    FUNCTION     �  CREATE FUNCTION public.utg_updatebillinfo() RETURNS trigger
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
 +   DROP FUNCTION public.utg_updatebillinfo();
       public          postgres    false            �            1259    16451    Account    TABLE     �   CREATE TABLE public."Account" (
    "DisplayName" character varying(50) NOT NULL,
    "UserName" character varying(50) NOT NULL,
    "PassWord" character varying(50) NOT NULL,
    "Type" integer DEFAULT 0 NOT NULL
);
    DROP TABLE public."Account";
       public         heap    postgres    false            �            1259    16825    Bill    TABLE       CREATE TABLE public."Bill" (
    id integer NOT NULL,
    datecheckin date DEFAULT CURRENT_DATE NOT NULL,
    datecheckout date,
    "idTable" integer NOT NULL,
    discount integer,
    totalprice double precision,
    status integer DEFAULT 0 NOT NULL
);
    DROP TABLE public."Bill";
       public         heap    postgres    false            �            1259    16847    BillInfo    TABLE     �   CREATE TABLE public."BillInfo" (
    id integer NOT NULL,
    "idBill" integer NOT NULL,
    "idFood" integer NOT NULL,
    count integer DEFAULT 0 NOT NULL
);
    DROP TABLE public."BillInfo";
       public         heap    postgres    false            �            1259    16466    Food    TABLE     �   CREATE TABLE public."Food" (
    id integer DEFAULT 0 NOT NULL,
    name character varying(50) DEFAULT 'Chưa đặt tên'::character varying NOT NULL,
    "idCategory" integer NOT NULL,
    price double precision DEFAULT 0 NOT NULL
);
    DROP TABLE public."Food";
       public         heap    postgres    false            �            1259    16458    FoodCategory    TABLE     �   CREATE TABLE public."FoodCategory" (
    id integer DEFAULT 0 NOT NULL,
    name character varying(50) DEFAULT 'Chưa đặt tên'::character varying NOT NULL
);
 "   DROP TABLE public."FoodCategory";
       public         heap    postgres    false            �            1259    16443 	   TableFood    TABLE     �   CREATE TABLE public."TableFood" (
    id integer DEFAULT 0 NOT NULL,
    name character varying(50) DEFAULT 'Chưa đặt tên'::character varying NOT NULL,
    status character varying(50) DEFAULT 'Trống'::character varying NOT NULL
);
    DROP TABLE public."TableFood";
       public         heap    postgres    false            �            1259    16824    bill_id_seq    SEQUENCE     �   CREATE SEQUENCE public.bill_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.bill_id_seq;
       public          postgres    false    223            ?           0    0    bill_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.bill_id_seq OWNED BY public."Bill".id;
          public          postgres    false    222            �            1259    16846    billinfo_id_seq    SEQUENCE     �   CREATE SEQUENCE public.billinfo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.billinfo_id_seq;
       public          postgres    false    225            @           0    0    billinfo_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.billinfo_id_seq OWNED BY public."BillInfo".id;
          public          postgres    false    224            �            1259    16465    food_id_seq    SEQUENCE     �   CREATE SEQUENCE public.food_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.food_id_seq;
       public          postgres    false    221            A           0    0    food_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.food_id_seq OWNED BY public."Food".id;
          public          postgres    false    220            �            1259    16457    foodcategory_id_seq    SEQUENCE     �   CREATE SEQUENCE public.foodcategory_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.foodcategory_id_seq;
       public          postgres    false    219            B           0    0    foodcategory_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.foodcategory_id_seq OWNED BY public."FoodCategory".id;
          public          postgres    false    218            �            1259    16442    tablefood_id_seq    SEQUENCE     �   CREATE SEQUENCE public.tablefood_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.tablefood_id_seq;
       public          postgres    false    216            C           0    0    tablefood_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.tablefood_id_seq OWNED BY public."TableFood".id;
          public          postgres    false    215            �           2604    16828    Bill id    DEFAULT     d   ALTER TABLE ONLY public."Bill" ALTER COLUMN id SET DEFAULT nextval('public.bill_id_seq'::regclass);
 8   ALTER TABLE public."Bill" ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    222    223    223            �           2604    16850    BillInfo id    DEFAULT     l   ALTER TABLE ONLY public."BillInfo" ALTER COLUMN id SET DEFAULT nextval('public.billinfo_id_seq'::regclass);
 <   ALTER TABLE public."BillInfo" ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    225    224    225            �           2606    16456    Account account_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT account_pkey PRIMARY KEY ("UserName");
 @   ALTER TABLE ONLY public."Account" DROP CONSTRAINT account_pkey;
       public            postgres    false    217            �           2606    16832    Bill bill_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public."Bill"
    ADD CONSTRAINT bill_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public."Bill" DROP CONSTRAINT bill_pkey;
       public            postgres    false    223            �           2606    16853    BillInfo billinfo_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."BillInfo"
    ADD CONSTRAINT billinfo_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public."BillInfo" DROP CONSTRAINT billinfo_pkey;
       public            postgres    false    225            �           2606    16473    Food food_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public."Food"
    ADD CONSTRAINT food_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public."Food" DROP CONSTRAINT food_pkey;
       public            postgres    false    221            �           2606    16464    FoodCategory foodcategory_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public."FoodCategory"
    ADD CONSTRAINT foodcategory_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public."FoodCategory" DROP CONSTRAINT foodcategory_pkey;
       public            postgres    false    219            �           2606    16450    TableFood tablefood_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."TableFood"
    ADD CONSTRAINT tablefood_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public."TableFood" DROP CONSTRAINT tablefood_pkey;
       public            postgres    false    216            �           2620    16865    Bill utg_updatebill    TRIGGER     s   CREATE TRIGGER utg_updatebill AFTER UPDATE ON public."Bill" FOR EACH ROW EXECUTE FUNCTION public.utg_updatebill();
 .   DROP TRIGGER utg_updatebill ON public."Bill";
       public          postgres    false    223    244            �           2620    16864    BillInfo utg_updatebillinfo    TRIGGER     �   CREATE TRIGGER utg_updatebillinfo AFTER INSERT OR UPDATE ON public."BillInfo" FOR EACH ROW EXECUTE FUNCTION public.utg_updatebillinfo();
 6   DROP TRIGGER utg_updatebillinfo ON public."BillInfo";
       public          postgres    false    225    245            �           2606    16833    Bill bill_idtable_fkey    FK CONSTRAINT        ALTER TABLE ONLY public."Bill"
    ADD CONSTRAINT bill_idtable_fkey FOREIGN KEY ("idTable") REFERENCES public."TableFood"(id);
 B   ALTER TABLE ONLY public."Bill" DROP CONSTRAINT bill_idtable_fkey;
       public          postgres    false    216    223    3225            �           2606    16854    BillInfo billinfo_idbill_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."BillInfo"
    ADD CONSTRAINT billinfo_idbill_fkey FOREIGN KEY ("idBill") REFERENCES public."Bill"(id);
 I   ALTER TABLE ONLY public."BillInfo" DROP CONSTRAINT billinfo_idbill_fkey;
       public          postgres    false    225    3233    223            �           2606    16859    BillInfo billinfo_idfood_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."BillInfo"
    ADD CONSTRAINT billinfo_idfood_fkey FOREIGN KEY ("idFood") REFERENCES public."Food"(id);
 I   ALTER TABLE ONLY public."BillInfo" DROP CONSTRAINT billinfo_idfood_fkey;
       public          postgres    false    221    225    3231            �           2606    16474    Food food_idcategory_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Food"
    ADD CONSTRAINT food_idcategory_fkey FOREIGN KEY ("idCategory") REFERENCES public."FoodCategory"(id);
 E   ALTER TABLE ONLY public."Food" DROP CONSTRAINT food_idcategory_fkey;
       public          postgres    false    3229    219    221           