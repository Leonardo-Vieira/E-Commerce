CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Brands" (
    "Id" uuid NOT NULL,
    "Code" text NULL,
    "Name" text NULL,
    "Description" text NULL,
    CONSTRAINT "PK_Brands" PRIMARY KEY ("Id")
);

CREATE TABLE "Persons" (
    "Id" uuid NOT NULL,
    "Name" text NULL,
    "IdentificationNumber" text NULL,
    "PostalCode" text NULL,
    "Place" text NULL,
    "Address" text NULL,
    CONSTRAINT "PK_Persons" PRIMARY KEY ("Id")
);

CREATE TABLE "StoredEvents" (
    "Id" uuid NOT NULL,
    "MessageType" text NULL,
    "AggregateId" uuid NOT NULL,
    "Timestamp" timestamp without time zone NOT NULL,
    "Data" text NULL,
    CONSTRAINT "PK_StoredEvents" PRIMARY KEY ("Id")
);

CREATE TABLE "Products" (
    "Id" uuid NOT NULL,
    "Code" text NULL,
    "Name" text NULL,
    "Status" boolean NOT NULL,
    "Description" text NULL,
    "Price" numeric NOT NULL,
    "Stock" integer NOT NULL,
    "BrandId" uuid NOT NULL,
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Products_Brands_BrandId" FOREIGN KEY ("BrandId") REFERENCES "Brands" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Clients" (
    "Id" uuid NOT NULL,
    "PersonId" uuid NULL,
    "Username" text NULL,
    "Email" text NULL,
    "Password" text NULL,
    "PasswordHash" bytea NULL,
    "PasswordSalt" bytea NULL,
    CONSTRAINT "PK_Clients" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Clients_Persons_PersonId" FOREIGN KEY ("PersonId") REFERENCES "Persons" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "OrderItems" (
    "Id" uuid NOT NULL,
    "Quantity" integer NOT NULL,
    "ProductId" uuid NOT NULL,
    CONSTRAINT "PK_OrderItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_OrderItems_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Orders" (
    "Id" uuid NOT NULL,
    "ClientId" uuid NOT NULL,
    "OrderItemId" uuid NULL,
    "DateOrder" timestamp without time zone NOT NULL,
    "State" boolean NOT NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Orders_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Orders_OrderItems_OrderItemId" FOREIGN KEY ("OrderItemId") REFERENCES "OrderItems" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Clients_PersonId" ON "Clients" ("PersonId");

CREATE INDEX "IX_OrderItems_ProductId" ON "OrderItems" ("ProductId");

CREATE INDEX "IX_Orders_ClientId" ON "Orders" ("ClientId");

CREATE INDEX "IX_Orders_OrderItemId" ON "Orders" ("OrderItemId");

CREATE INDEX "IX_Products_BrandId" ON "Products" ("BrandId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190131174556_InitalPostegreMigrations', '2.2.1-servicing-10028');

