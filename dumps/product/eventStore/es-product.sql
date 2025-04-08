CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190214123602_Init') THEN
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
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190214123602_Init') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190214123602_Init', '2.2.0-rtm-35687');
    END IF;
END $$;
