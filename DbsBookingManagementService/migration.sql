CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'booking') THEN
        CREATE SCHEMA booking;
    END IF;
END $EF$;

CREATE TABLE booking."Bookings" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "EnvironmentId" uuid NOT NULL,
    "StartDateTime" timestamp with time zone NOT NULL,
    "EndDateTime" timestamp with time zone NOT NULL,
    "Notes" text NOT NULL,
    "CreatedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "UpdatedDate" timestamp with time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_Bookings" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241219123048_InitialMigration', '8.0.11');

COMMIT;

