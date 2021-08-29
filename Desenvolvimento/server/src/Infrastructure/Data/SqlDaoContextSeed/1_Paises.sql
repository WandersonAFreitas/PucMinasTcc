DO $$                  
    BEGIN 
        IF NOT EXISTS
            (SELECT 1
               FROM "SCA"."Pais")
        THEN
            INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (7, 'Brasil','BR');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (8, 'Cuba','CU');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (9, 'Peru','PE');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (10, 'Portugal','PO');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (11, 'Israel','IS');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (12, 'Col�mbia','CO');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (13, 'Bol�via','BO');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (14, 'Argentina','AR');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (15, 'R�ssia','RU');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (16, 'Paraguai','PA');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (17, 'China','CH');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (19, 'Costa Rica','CR');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (20, 'Nig�ria','NG');
        END IF;
    END
$$;
