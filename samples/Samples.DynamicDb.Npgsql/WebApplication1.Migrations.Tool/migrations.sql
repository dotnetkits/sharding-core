﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "AspNetRoles" (
    "Id" text NOT NULL,
    "Name" character varying(256) NULL,
    "NormalizedName" character varying(256) NULL,
    "ConcurrencyStamp" text NULL,
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetUsers" (
    "Id" text NOT NULL,
    "UserName" character varying(256) NULL,
    "NormalizedUserName" character varying(256) NULL,
    "Email" character varying(256) NULL,
    "NormalizedEmail" character varying(256) NULL,
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text NULL,
    "SecurityStamp" text NULL,
    "ConcurrencyStamp" text NULL,
    "PhoneNumber" text NULL,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone NULL,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
);

CREATE TABLE "GuidShardingTables_001" (
    "Id" uuid NOT NULL,
    "Name" text NULL,
    CONSTRAINT "PK_GuidShardingTables_001" PRIMARY KEY ("Id")
);

CREATE TABLE "GuidShardingTables_005" (
    "Id" uuid NOT NULL,
    "Name" text NULL,
    CONSTRAINT "PK_GuidShardingTables_005" PRIMARY KEY ("Id")
);

CREATE TABLE "GuidShardingTables_003" (
    "Id" uuid NOT NULL,
    "Name" text NULL,
    CONSTRAINT "PK_GuidShardingTables_003" PRIMARY KEY ("Id")
);

CREATE TABLE "GuidShardingTables_004" (
    "Id" uuid NOT NULL,
    "Name" text NULL,
    CONSTRAINT "PK_GuidShardingTables_004" PRIMARY KEY ("Id")
);

CREATE TABLE "GuidShardingTables_002" (
    "Id" uuid NOT NULL,
    "Name" text NULL,
    CONSTRAINT "PK_GuidShardingTables_002" PRIMARY KEY ("Id")
);

CREATE TABLE "GuidShardingTables_000" (
    "Id" uuid NOT NULL,
    "Name" text NULL,
    CONSTRAINT "PK_GuidShardingTables_000" PRIMARY KEY ("Id")
);

CREATE TABLE "Orders" (
    "Id" uuid NOT NULL,
    "Name" text NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY ("Id")
);

CREATE TABLE "Students_001" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    CONSTRAINT "PK_Students_001" PRIMARY KEY ("Id")
);

CREATE TABLE "Students_002" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    CONSTRAINT "PK_Students_002" PRIMARY KEY ("Id")
);

CREATE TABLE "Students_005" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    CONSTRAINT "PK_Students_005" PRIMARY KEY ("Id")
);

CREATE TABLE "Students_004" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    CONSTRAINT "PK_Students_004" PRIMARY KEY ("Id")
);

CREATE TABLE "Students_000" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    CONSTRAINT "PK_Students_000" PRIMARY KEY ("Id")
);

CREATE TABLE "Students_003" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    CONSTRAINT "PK_Students_003" PRIMARY KEY ("Id")
);

CREATE TABLE "TestModels_202202" (
    "Id" uuid NOT NULL,
    "Content" text NULL,
    "Description" text NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "TestNewField" text NULL,
    CONSTRAINT "PK_TestModels_202202" PRIMARY KEY ("Id")
);

CREATE TABLE "TestModels_202203" (
    "Id" uuid NOT NULL,
    "Content" text NULL,
    "Description" text NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "TestNewField" text NULL,
    CONSTRAINT "PK_TestModels_202203" PRIMARY KEY ("Id")
);

CREATE TABLE "TestModels_202205" (
    "Id" uuid NOT NULL,
    "Content" text NULL,
    "Description" text NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "TestNewField" text NULL,
    CONSTRAINT "PK_TestModels_202205" PRIMARY KEY ("Id")
);

CREATE TABLE "TestModels_202204" (
    "Id" uuid NOT NULL,
    "Content" text NULL,
    "Description" text NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "TestNewField" text NULL,
    CONSTRAINT "PK_TestModels_202204" PRIMARY KEY ("Id")
);

CREATE TABLE "TestModels_202201" (
    "Id" uuid NOT NULL,
    "Content" text NULL,
    "Description" text NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "TestNewField" text NULL,
    CONSTRAINT "PK_TestModels_202201" PRIMARY KEY ("Id")
);

CREATE TABLE "TestModels_202206" (
    "Id" uuid NOT NULL,
    "Content" text NULL,
    "Description" text NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "TestNewField" text NULL,
    CONSTRAINT "PK_TestModels_202206" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetRoleClaims" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "RoleId" text NOT NULL,
    "ClaimType" text NULL,
    "ClaimValue" text NULL,
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserClaims" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "UserId" text NOT NULL,
    "ClaimType" text NULL,
    "ClaimValue" text NULL,
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" text NOT NULL,
    "ProviderKey" text NOT NULL,
    "ProviderDisplayName" text NULL,
    "UserId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserRoles" (
    "UserId" text NOT NULL,
    "RoleId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserTokens" (
    "UserId" text NOT NULL,
    "LoginProvider" text NOT NULL,
    "Name" text NOT NULL,
    "Value" text NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220615110332_InitialCreate', '6.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE "TestModels_202202" ADD "AfterShardingDb" text NULL;

ALTER TABLE "TestModels_202203" ADD "AfterShardingDb" text NULL;

ALTER TABLE "TestModels_202205" ADD "AfterShardingDb" text NULL;

ALTER TABLE "TestModels_202204" ADD "AfterShardingDb" text NULL;

ALTER TABLE "TestModels_202201" ADD "AfterShardingDb" text NULL;

ALTER TABLE "TestModels_202206" ADD "AfterShardingDb" text NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220615152140_TestModel_Add_AfterShardingDb', '6.0.5');

COMMIT;

