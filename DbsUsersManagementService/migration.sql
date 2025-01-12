CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'identity') THEN
        CREATE SCHEMA identity;
    END IF;
END $EF$;

CREATE TABLE identity."Roles" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "DisplayName" text NOT NULL,
    "CreatedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "UpdatedDate" timestamp with time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
);

CREATE TABLE identity."Teams" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "CreatedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "UpdatedDate" timestamp with time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_Teams" PRIMARY KEY ("Id")
);

CREATE TABLE identity."UserRoles" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    "CreatedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "UpdatedDate" timestamp with time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_UserRoles" PRIMARY KEY ("Id")
);

CREATE TABLE identity."Users" (
    "Id" uuid NOT NULL,
    "UserName" text NOT NULL,
    "Password" text,
    "Email" text NOT NULL,
    "TeamId" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    "CreatedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "UpdatedDate" timestamp with time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

INSERT INTO identity."Roles" ("Id", "CreatedBy", "CreatedDate", "DisplayName", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate")
VALUES ('1fbd0afe-4e8c-4b9a-8631-735107d30cb2', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850535Z', 'Power User', FALSE, 'POWER_USER', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850535Z');
INSERT INTO identity."Roles" ("Id", "CreatedBy", "CreatedDate", "DisplayName", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate")
VALUES ('37d00899-66a7-4ed2-b8da-6ee0f8395201', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850535Z', 'General User', FALSE, 'GENERAL_USER', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850535Z');
INSERT INTO identity."Roles" ("Id", "CreatedBy", "CreatedDate", "DisplayName", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate")
VALUES ('e4b257ef-3638-4156-ad81-c98692b06229', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850534Z', 'Admin', FALSE, 'ADMIN', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850534Z');

INSERT INTO identity."Teams" ("Id", "CreatedBy", "CreatedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate")
VALUES ('54466f17-02af-48e7-8ed3-5a4a8bfacf6f', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850532Z', FALSE, 'Mocha', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850532Z');
INSERT INTO identity."Teams" ("Id", "CreatedBy", "CreatedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate")
VALUES ('f808ddcd-b5e5-4d80-b732-1ca523e48434', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850532Z', FALSE, 'Latte', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', TIMESTAMPTZ '2024-12-11T08:36:11.850532Z');

INSERT INTO identity."Users" ("Id", "CreatedBy", "CreatedDate", "Email", "IsActive", "IsDeleted", "Password", "RoleId", "TeamId", "UpdatedBy", "UpdatedDate", "UserName")
VALUES ('d3b8e8b1-4b8b-4b8b-4b8b-4b8b4b8b4b8b', 'd3b8e8b1-4b8b-4b8b-4b8b-4b8b4b8b4b8b', TIMESTAMPTZ '2024-12-11T08:36:11.850524Z', 'testuser@example.com', TRUE, FALSE, NULL, 'e4b257ef-3638-4156-ad81-c98692b06229', 'f808ddcd-b5e5-4d80-b732-1ca523e48434', 'd3b8e8b1-4b8b-4b8b-4b8b-4b8b4b8b4b8b', TIMESTAMPTZ '2024-12-11T08:36:11.850524Z', 'testuser');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241211083613_FirstMigration', '8.0.11');

COMMIT;

