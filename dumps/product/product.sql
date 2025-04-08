CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190214123459_Init') THEN
    CREATE TABLE "Brands" (
        "Id" uuid NOT NULL,
        "Code" text NULL,
        "Name" text NULL,
        "Description" text NULL,
        CONSTRAINT "PK_Brands" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190214123459_Init') THEN
    CREATE TABLE "Products" (
        "Id" uuid NOT NULL,
        "Code" text NULL,
        "Name" text NULL,
        "Status" boolean NOT NULL,
        "Description" text NULL,
        "Price" decimal precision NOT NULL,
        "Stock" integer NOT NULL,
        "ProviderId" uuid NULL,
        "BrandId" uuid NULL,
        "ProductTypeId" uuid NULL,
        CONSTRAINT "PK_Products" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190214123459_Init') THEN
    CREATE TABLE "ProductTypes" (
        "Id" uuid NOT NULL,
        "Code" text NULL,
        "Name" text NULL,
        "Description" text NULL,
        CONSTRAINT "PK_ProductTypes" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190214123459_Init') THEN
    CREATE TABLE "Provider" (
        "Id" uuid NOT NULL,
        "Code" text NULL,
        "Name" text NULL,
        "Description" text NULL,
        "Phone" text NULL,
        "PostalCode" text NULL,
        "Place" text NULL,
        "IdentificationNumber" text NULL,
        CONSTRAINT "PK_Provider" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190214123459_Init') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190214123459_Init', '2.2.0-rtm-35687');
    END IF;
END $$;
