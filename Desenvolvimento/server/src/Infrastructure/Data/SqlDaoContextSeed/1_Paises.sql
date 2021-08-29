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
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (12, 'Colômbia','CO');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (13, 'Bolívia','BO');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (14, 'Argentina','AR');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (15, 'Rússia','RU');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (16, 'Paraguai','PA');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (17, 'China','CH');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (19, 'Costa Rica','CR');
			INSERT INTO "SCA"."Pais" ("Id", "Nome", "Sigla") VALUES (20, 'Nigéria','NG');
        END IF;
    END
$$;
