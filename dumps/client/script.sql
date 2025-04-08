CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
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
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE TABLE "Orders" (
        "Id" uuid NOT NULL,
        "ClientId" uuid NULL,
        "DateOrder" timestamp without time zone NOT NULL,
        "State" boolean NOT NULL,
        CONSTRAINT "PK_Orders" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE TABLE "Persons" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "IdentificationNumber" text NOT NULL,
        "PostalCode" text NOT NULL,
        "Place" text NOT NULL,
        "Address" text NOT NULL,
        CONSTRAINT "PK_Persons" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
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
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE TABLE "StoredEvents" (
        "Id" uuid NOT NULL,
        "MessageType" text NULL,
        "AggregateId" uuid NOT NULL,
        "Timestamp" timestamp without time zone NOT NULL,
        "Data" text NULL,
        CONSTRAINT "PK_StoredEvents" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE TABLE "Clients" (
        "Id" uuid NOT NULL,
        "PersonId" uuid NOT NULL,
        "PasswordHash" bytea NULL,
        "PasswordSalt" bytea NULL,
        "Username" character varying(255) NULL,
        "Email" text NULL,
        CONSTRAINT "PK_Clients" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Clients_Persons_PersonId" FOREIGN KEY ("PersonId") REFERENCES "Persons" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE TABLE "Products" (
        "Id" uuid NOT NULL,
        "BrandId" uuid NULL,
        "ProductTypeId" uuid NULL,
        "Name" character varying(255) NULL,
        "Description" character varying(255) NULL,
        "State" boolean NOT NULL,
        "Price" numeric NOT NULL,
        CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Products_Brands_BrandId" FOREIGN KEY ("BrandId") REFERENCES "Brands" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_Products_ProductTypes_ProductTypeId" FOREIGN KEY ("ProductTypeId") REFERENCES "ProductTypes" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE TABLE "OrderItems" (
        "Id" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "OrderId" uuid NOT NULL,
        "Quantity" integer NOT NULL,
        CONSTRAINT "PK_OrderItems" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_OrderItems_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_OrderItems_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE INDEX "IX_Clients_PersonId" ON "Clients" ("PersonId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE INDEX "IX_OrderItems_OrderId" ON "OrderItems" ("OrderId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE INDEX "IX_OrderItems_ProductId" ON "OrderItems" ("ProductId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE INDEX "IX_Products_BrandId" ON "Products" ("BrandId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    CREATE INDEX "IX_Products_ProductTypeId" ON "Products" ("ProductTypeId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190204105548_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190204105548_InitialCreate', '2.2.1-servicing-10028');
    END IF;
END $$;
