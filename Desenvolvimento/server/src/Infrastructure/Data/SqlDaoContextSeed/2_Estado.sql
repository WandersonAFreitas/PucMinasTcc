DO $$                  
    BEGIN 
        IF NOT EXISTS
            (SELECT 1
               FROM "SCA"."Estado")
        THEN
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (1,7,'ACRE','AC');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (2,7,'ALAGOAS','AL');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (3,7,'AMAPÁ','AP');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (4,7,'AMAZONAS','AM');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (5,7,'BAHIA','BA');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (6,7,'CEARÁ','CE');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (7,7,'DISTRITO FEDERAL','DF');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (8,7,'ESPÍRITO SANTO','ES');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (9,7,'GOIÁS','GO');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (10,7,'MARANHÃO','MA');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (11,7,'MATO GROSSO','MT');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (12,7,'MATO GROSSO DO SUL','MS');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (13,7,'MINAS GERAIS','MG');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (14,7,'PARÁ','PA');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (15,7,'PARAÍBA','PB');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (16,7,'PARANÁ','PR');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (17,7,'PERNAMBUCO','PE');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (18,7,'PIAUÍ','PI');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (19,7,'RIO DE JANEIRO','RJ');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (20,7,'RIO GRANDE DO NORTE','RN');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (21,7,'RIO GRANDE DO SUL','RS');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (22,7,'RONDÔNIA','RO');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (23,7,'RORAIMA','RR');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (24,7,'SANTA CATARINA','SC');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (25,7,'SÃO PAULO','SP');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (26,7,'SERGIPE','SE');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (27,7,'TOCANTINS','TO');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (28,13,'COCHABAMBA','EX');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (29,13,'POTOSI','EX');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (30,13,'LA PAZ','EX');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (31,13,'ORURO','EX');
			INSERT INTO "SCA"."Estado" ("Id", "PaisId", "Nome", "Sigla") VALUES (32,20,'ABIA STATE','EX');
        END IF;
    END
$$;