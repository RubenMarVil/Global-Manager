-- Create COUNTRY table
CREATE TABLE "COUNTRY" (
	"Name"	TEXT,
	"PowerDistance"	INTEGER NOT NULL,
	"Individualism"	INTEGER NOT NULL,
	"Masculinity"	INTEGER NOT NULL,
	"UncertantyAvoidance"	INTEGER NOT NULL,
	"LongTermOrientation"	INTEGER NOT NULL,
	"Indulgence"	INTEGER NOT NULL,
	"TimeZone"	REAL NOT NULL,
	"Salary"	REAL NOT NULL,
	PRIMARY KEY("Name")
);

-- Create LANGUAGE table
CREATE TABLE "LANGUAGE" (
	"Name"	TEXT,
	PRIMARY KEY("Name")
);

-- Create SITE table
CREATE TABLE "SITE" (
	"CodSite"	INTEGER,
	"Country"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	"TeamSize"	INTEGER NOT NULL,
	"LevelCommonLanguage"	TEXT NOT NULL,
	FOREIGN KEY("Country") REFERENCES "COUNTRY"("Name"),
	PRIMARY KEY("CodSite" AUTOINCREMENT)
);

-- Create COMMUNICATION table
CREATE TABLE "COMMUNICATION" (
	"Name"	TEXT,
	"Type"	TEXT NOT NULL,
	PRIMARY KEY("Name")
);

-- Create PLAYER table
CREATE TABLE "PLAYER" (
	"Username"	TEXT,
	"Age"	INTEGER,
	"UserLevel"	TEXT NOT NULL,
	"Score"	INTEGER,
	"NumProjects"	INTEGER,
	PRIMARY KEY("Username")
);

-- Create GAME table
CREATE TABLE "GAME" (
	"CodGame"	INTEGER,
	"Player"	TEXT NOT NULL,
	"Language"	TEXT NOT NULL,
	"CustomerCountry"	TEXT NOT NULL,
	"NumSites"	INTEGER NOT NULL,
	"ProjectDifficulty"	TEXT NOT NULL,
	FOREIGN KEY("CustomerCountry") REFERENCES "COUNTRY"("Name"),
	FOREIGN KEY("Player") REFERENCES "PLAYER"("Username"),
	FOREIGN KEY("Language") REFERENCES "LANGUAGE"("Name"),
	PRIMARY KEY("CodGame" AUTOINCREMENT)
);

-- Create relationship between SITE and GAME
CREATE TABLE "SiteGame" (
	"Game"	INTEGER,
	"Site"	INTEGER,
	"MainSite"	INTEGER,
	FOREIGN KEY("Game") REFERENCES "GAME"("CodGame"),
	FOREIGN KEY("Site") REFERENCES "SITE"("CodSite"),
	PRIMARY KEY("Game","Site")
);

-- Create relationship between COUNTRY and LANGUAGE
CREATE TABLE "Speak" (
	"Country"	TEXT,
	"Language"	TEXT,
    "Official"	INTEGER NOT NULL,
	PRIMARY KEY("Country","Language"),
	FOREIGN KEY("Country") REFERENCES "COUNTRY"("Name"),
	FOREIGN KEY("Language") REFERENCES "LANGUAGE"("Name")
);

-- Create relationship between GAME, SITE, SITE and COMMUNICATION
CREATE TABLE "Communicate" (
	"Game"	INTEGER,
	"Site1"	INTEGER,
	"Site2"	INTEGER,
	"Communication"	TEXT,
	PRIMARY KEY("Game","Site1","Site2","Communication"),
	FOREIGN KEY("Communication") REFERENCES "COMMUNICATION"("Name"),
	FOREIGN KEY("Game") REFERENCES "GAME"("CodGame"),
	FOREIGN KEY("Site1") REFERENCES "SITE"("CodSite"),
	FOREIGN KEY("Site2") REFERENCES "SITE"("CodSite")
);