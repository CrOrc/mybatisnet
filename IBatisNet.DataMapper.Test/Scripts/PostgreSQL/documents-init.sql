DROP TABLE Documents;

CREATE TABLE Documents
(
  Document_Id int4 NOT NULL,
  Document_Title varchar(32),
  Document_Type varchar(32),
  Document_PageNumber int4,
  Document_City varchar(32),
  CONSTRAINT PK_Documents PRIMARY KEY (Document_Id)
) 
WITHOUT OIDS;
ALTER TABLE Documents OWNER TO "IBatisNet";

INSERT INTO Documents VALUES (1, 'The World of Null-A', 'Book', 55, null);
INSERT INTO Documents VALUES (2, 'Le Progres de Lyon', 'Newspaper', null , 'Lyon');
INSERT INTO Documents VALUES (3, 'Lord of the Rings', 'Book', 3587, null);
INSERT INTO Documents VALUES (4, 'Le Canard enchaine', 'Tabloid', null , 'Paris');
INSERT INTO Documents VALUES (5, 'Le Monde', 'Broadsheet', null , 'Paris');
INSERT INTO Documents VALUES (6, 'Foundation', 'Monograph', 557, null);
